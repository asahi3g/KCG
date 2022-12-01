using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine3D;
using Enums;
using Item;
using KMath;
using UnityEngine;
using UnityEngine.UIElements;

public class GameLauncher : BaseMonoBehaviour
{
    [TextArea(3, 6)]
    [SerializeField] private string _planet;
    [SerializeField] private Material _tileMaterial;
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
        
        GameState.TileSpriteAtlasManager.UpdateAtlasTextures();
        GameState.SpriteAtlasManager.UpdateAtlasTextures();

        RunTests();
        GameState.DebugAllItemsByItemGroup();
    }

    protected override void Start()
    {
        base.Start();

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
}
