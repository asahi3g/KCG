using UnityEngine;
using KMath;
using Item;

namespace Planet.Unity
{
    public class TGenTest : MonoBehaviour
    {

        [SerializeField]
        private Material Material;

        public Planet.PlanetState Planet;

        private static bool Init = false;

        [SerializeField]
        private bool enableTileGrid = true;

        [SerializeField]
        private bool drawMapBorder = true;

        [SerializeField]
        private bool foregroundToolEnabled = true;

        private AgentEntity Player;


        Inventory.InventoryManager inventoryManager;

        int InventoryID;
        InventoryEntity MaterialBag;

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
            Planet.Update(Time.deltaTime, Material, transform);
        }

        private void OnGUI()
        {
            Planet.DrawHUD(Player);
        }

        public void Initialize()
        {
            GameResources.Initialize();
           
            // Generating the map
            Vec2i mapSize = new Vec2i(32, 32);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

            Player = Planet.AddPlayer(GameState.AnimationManager.CharacterSpriteId, 32, 48, new Vec2f(2.0f, 4.0f), 0, 100, 100, 100, 100, 100);

            inventoryManager = new Inventory.InventoryManager();

            Planet.InitializeSystems(Material, transform);
            Planet.InitializeHUD(Player);

            Planet.InitializeTGen(Material, transform);

            GameState.TGenGrid.InitStage1(mapSize);

            if (enableTileGrid)
                GameState.TGenRenderGridOverlay.Initialize(Material, transform, mapSize.X, mapSize.Y, 30);

            if (drawMapBorder)
                GameState.TGenRenderMapBorder.Initialize(Material, transform, mapSize.X - 1, mapSize.Y - 1, 31);

            MaterialBag = Planet.AddInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID(), "MaterialBag");

            InventoryID = Player.agentInventory.InventoryID;

            // Note: ItemType not exists
            //Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GeometryPlacementTool, Planet.EntitasContext);
        }

    }
}
