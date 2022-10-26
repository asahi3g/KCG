using UnityEngine;
using KMath;

namespace Planet.Unity
{
    public class TGenTest : MonoBehaviour
    {

        [SerializeField]
        private Material Material;



        private static bool Init = false;

        [SerializeField]
        private bool enableTileGrid = true;

        [SerializeField]
        private bool drawMapBorder = true;

        private AgentEntity player;
        private Inventory.InventoryManager inventoryManager;
        private int inventoryID;
        private InventoryEntity materialBag;

        public void Start()
        {
            if (!Init)
            {
                Initialize();
                Init = true;
            }
        }

        public void Update()
        {
            GameState.Planet.Update(Time.deltaTime, Material, transform);
        }

        private void OnGUI()
        {
            GameState.Planet.DrawHUD(player);
        }

        public void Initialize()
        {
            GameResources.Initialize();
           
            // Generating the map
            Vec2i mapSize = new Vec2i(32, 32);

            GameState.Planet.Init(mapSize);

            player = GameState.Planet.AddPlayer(GameState.AnimationManager.CharacterSpriteId, 32, 48, new Vec2f(2.0f, 4.0f), 0, 100, 100, 100, 100, 100);

            inventoryManager = new Inventory.InventoryManager();

            GameState.Planet.InitializeSystems(Material, transform);
            GameState.Planet.InitializeHUD();

            GameState.Planet.InitializeTGen(Material, transform);

            GameState.TGenGrid.InitStage1(mapSize);

            if (enableTileGrid)
                GameState.TGenRenderGridOverlay.Initialize(Material, transform, mapSize.X, mapSize.Y, 30);

            if (drawMapBorder)
                GameState.TGenRenderMapBorder.Initialize(Material, transform, mapSize.X - 1, mapSize.Y - 1, 31);

            materialBag = GameState.Planet.AddInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID(), "MaterialBag");

            inventoryID = player.agentInventory.InventoryID;

            // Note: ItemType not exists
            //Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GeometryPlacementTool, GameState.Planet.EntitasContext);
        }

    }
}
