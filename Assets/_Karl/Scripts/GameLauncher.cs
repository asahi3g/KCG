using Engine3D;
using Enums;
using KMath;
using UnityEngine;

public class GameLauncher : BaseMonoBehaviour
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
                int inventoryID = agentEntity.agentInventory.InventoryID;     
            
                // Add some test items
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SMG);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Shotgun);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.PumpShotgun);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.FragGrenade);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.GasBomb);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SniperRifle);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.LongRifle);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.HealthPotion, 5);
            
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.PlacementTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveTileTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemyGunnerTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemySwordmanTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ConstructionTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.GeometryPlacementTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveMech);
                
                App.Instance.GetPlayer().SetAgentRenderer(agentEntity.agentAgent3DModel.Renderer);
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
