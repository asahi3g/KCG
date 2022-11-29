using Engine3D;
using Enums;
using KMath;
using UnityEngine;

public class Game : Singleton<Game>
{
    [SerializeField] private PlanetRenderer _planet;


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
        _planet.Initialize(App.Instance.GetPlayer().GetCamera().GetMain(), OnPlanetCreationSuccess, OnPlanetCreationFailed);

        // Planet creation successful
        void OnPlanetCreationSuccess(IPlanetCreationResult result)
        {
            Debug.Log($"Planet creation successful fileName[{result.GetFileName()}] size[{result.GetMapSize()}]");
            App.Instance.GetPlayer().SetCurrentPlanet(result);

            // Player agent creation successful
            if (_planet.CreateAgent(new Vec2f(10f, 10f), AgentType.Player, 0, out AgentRenderer agentRenderer))
            {
                App.Instance.GetPlayer().SetAgentRenderer(agentRenderer);
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
