using System;
using System.Collections;
using Engine3D;
using Enums;
using KMath;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameLauncher : BaseMonoBehaviour
{
    [TextArea(3, 6)]
    [SerializeField] private string _planet;
    [SerializeField] private Material _tileMaterial;
    [SerializeField] private SOSceneName _mainSceneName;
    [Space]
    [SerializeField] private TestItems _testItems;

    [System.Serializable]
    private class TestItems
    {
        public bool active;
        public int maximumQuantity;
        public ItemGroupType[] itemGroups;
    }
    


    protected override void Awake()
    {
        base.Awake();

        GameResources.Initialize();
        AssetManager assetManager = AssetManager.Singelton; // force initialization
        GameState.AudioSystem = new Audio.AudioSystem();
        GameState.AudioSystem.SetAudioSource(GetComponent<AudioSource>());
        
        GameState.TileSpriteAtlasManager.UpdateAtlasTextures();
        GameState.SpriteAtlasManager.UpdateAtlasTextures();
        GameState.IsInitialized = true;

        RunTests();
        GameState.DebugAllItemsByItemGroup();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameState.IsInitialized = false;
    }

    protected override void Start()
    {
        base.Start();

        LoadMainScene(OnMainSceneLoadSuccess, OnMainSceneLoadFailed);

        void OnMainSceneLoadSuccess()
        {
            // Create planet
            PlanetLoader.Load(transform, _planet, _tileMaterial, App.Instance.GetPlayer().GetCamera().GetMain(), OnPlanetCreationSuccess, OnPlanetCreationFailed);

            // Planet creation successful
            void OnPlanetCreationSuccess(PlanetLoader.Result result)
            {
                Debug.Log($"Planet creation successful fileName[{result.GetFileName()}] size[{result.GetMapSize()}]");
                App.Instance.GetPlayer().SetCurrentPlanet(result);
            
                AgentEntity agentEntity = result.GetPlanetState().AddAgent(new Vec2f(10f, 10f), AgentType.Player, 0);

                // Player agent creation successful
                if (agentEntity != null)
                {
                    // Add some test items
                    if (_testItems.active)
                    {
                        Admin.AdminAPI.AddItems(agentEntity, _testItems.itemGroups, _testItems.maximumQuantity);   
                    }

                    App.Instance.GetPlayer().SetAgentRenderer(agentEntity.Agent3DModel.Renderer);
                }

                // Player agent creation failed
                else
                {
                    Debug.LogWarning("Failed to create player agent");
                }
            }
        
            // Planet creation failed
            void OnPlanetCreationFailed(IError error)
            {
                Debug.LogError($"Planet creation failed, reason: {error.GetMessage()}");
            }
        }

        void OnMainSceneLoadFailed(IError error)
        {
            Debug.LogError(error.GetMessage());
        }
    }

    private void RunTests()
    {
        TestLoader.Result testResult = TestLoader.Run();
        switch (testResult.Status)
        {
            case TestLoader.StatusType.Success:
            {
                Debug.Log("Tests passed successfully");
                break;
            }
            case TestLoader.StatusType.Failed:
            {
                Debug.LogError($"Tests failed (Click on this message to see details)\n{testResult.Message}");
                break;
            }
        }
    }

    private void LoadMainScene(UnityAction onSuccess, UnityAction<IError> onFailed)
    {
        if (_mainSceneName == null)
        {
            onFailed?.Invoke(new ErrorData("Main scene name scriptable object is missing"));
            return;
        }

        string mainSceneName = _mainSceneName.GetValue();
        int countLoaded = UnityEngine.SceneManagement.SceneManager.sceneCount;
        bool mainSceneAlreadyLoaded = false;

        for (int i = 0; i < countLoaded; i++)
        {
            Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
            bool isLoaded = String.CompareOrdinal(mainSceneName, scene.name) == 0;

            if (isLoaded)
            {
                Debug.Log($"Scene '{mainSceneName}' already loaded");
                mainSceneAlreadyLoaded = true;
                break;
            }
        }

        if (mainSceneAlreadyLoaded)
        {
            onSuccess?.Invoke();
            return;
        }

        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(mainSceneName, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = true;
        asyncOperation.completed += delegate(AsyncOperation operation)
        {
            Debug.Log($"Scene '{mainSceneName}' loaded from {nameof(GameLauncher)}");
            onSuccess?.Invoke();
            return;
        };
    }
}
