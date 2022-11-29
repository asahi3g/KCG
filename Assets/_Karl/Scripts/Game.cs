using Engine3D;
using Enums;
using KMath;
using UnityEngine;

public class Game : Singleton<Game>
{
    [TextArea(3, 6)]
    [SerializeField] private string _planet;
    [SerializeField] private Material _tileMaterial;


    protected override void Awake()
    {
        base.Awake();

        GameResources.Initialize();
        AssetManager assetManager = AssetManager.Singelton; // force initialization
        
        GameState.TileSpriteAtlasManager.UpdateAtlasTextures();
        GameState.SpriteAtlasManager.UpdateAtlasTextures();
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

    
}
