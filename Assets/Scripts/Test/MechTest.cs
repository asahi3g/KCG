using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Mech;
using System.Linq;
using System.Collections.Generic;

namespace Planet.Unity
{
    public class MechTest : MonoBehaviour
    {
        [SerializeField] Material Material;

        public Planet.PlanetState Planet;
        AgentEntity Player;

        static bool Init = false;

        private bool showMechInventory;

        private int selectedMechIndex;

        private int totalMechs;

        private Color correctHlColor = Color.green;

        private Color wrongHlColor = Color.red;

        public Utility.FrameMesh HighliterMesh;

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
            ref var tileMap = ref Planet.TileMap;
            Material material = Material;

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedMechIndex++;
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectedMechIndex--;
            }

            selectedMechIndex = Mathf.Clamp(selectedMechIndex, 0, totalMechs);

            if (Input.GetKeyDown(KeyCode.T))
            {
                GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, (int)Enums.ActionType.DropAction, Player.agentID.ID);
            }

            GameState.MechGUIDrawSystem.Draw(ref Planet, Player);

            int inventoryID = Player.agentInventory.InventoryID;
            InventoryEntity Inventory = Planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
            int selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

            ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext, inventoryID, selectedSlot);

            if (item != null)
            {
                ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

                if (itemProperty.IsTool())
                {
                    showMechInventory = itemProperty.ToolActionType == Enums.ActionType.ToolActionConstruction;

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, itemProperty.ToolActionType,
                            Player.agentID.ID, item.itemID.ID);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PlaceSmashableBox();
            }
            
            Planet.Update(Time.deltaTime, Material, transform);
            Planet.DrawHUD(Player);
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            if (Event.current.type != EventType.Repaint)
                return;

            GameState.InventoryDrawSystem.Draw(Planet.EntitasContext);
            GameState.InventoryMouseSelectionSystem.Draw(Planet.EntitasContext);

            if(showMechInventory)
                GameState.MechGUIDrawSystem.Draw(ref Planet, Player);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(16, 16);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

            GenerateMap();

            Player = Planet.AddPlayer(GameResources.CharacterSpriteId, 32, 48, new Vec2f(2.0f, 4.0f), 0, 100, 100, 100, 100, 100);
            int inventoryID = Player.agentInventory.InventoryID;

            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Pistol, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.PumpShotgun, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.WaterBottle, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.PlanterTool, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.HarvestTool, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.ConstructionTool, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.PulseWeapon, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.SniperRifle, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.SMG, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Shotgun, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.LongRifle, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.RPG, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.SMG, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.GrenadeLauncher, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Sword, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.RiotShield, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.StunBaton, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.AutoCannon, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Bow, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Ore, new Vec2f(6.0f, 3.0f));

            Planet.InitializeSystems(Material, transform);
            Planet.InitializeHUD(Player);

            GameState.MechGUIDrawSystem.Initialize(ref Planet);

            var SpawnEnemyTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.SpawnEnemySlimeTool);
            GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnEnemyTool, inventoryID);
            var RemoveMech = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.RemoveMech);
            GameState.InventoryManager.AddItem(Planet.EntitasContext, RemoveMech, inventoryID);

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();

            HighliterMesh = new Utility.FrameMesh("HighliterGameObject", Material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), 30);
        }

        void GenerateMap()
        {
            ref var tileMap = ref Planet.TileMap;

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    TileID frontTile;

                    if (i >= tileMap.MapSize.X / 2)
                    {
                        if (j % 2 == 0 && i == tileMap.MapSize.X / 2)
                        {
                            frontTile = TileID.Moon;
                        }
                        else
                        {
                            frontTile = TileID.Glass;
                        }
                    }
                    else
                    {
                        if (j % 3 == 0 && i == tileMap.MapSize.X / 2 + 1)
                        {
                            frontTile = TileID.Glass;
                        }
                        else
                        {
                            frontTile = TileID.Moon;
                        }
                    }

                    if (j is > 1 and < 6 || (j > 8 + i))
                    {
                        frontTile = TileID.Air;
                    }


                    tileMap.SetFrontTile(i, j, frontTile);
                }
            }
            //TileMap.BuildLayerTexture(MapLayerType.Front);
        }

        private void PlaceMech()
        {
            Debug.Log("PLACE MECH");

            var planet = FindObjectOfType<ItemTest>().Planet;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            planet.AddMech(new Vec2f(x + 2F, y), MechType.Storage);
        }
        
        private void PlaceSmashableBox()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            var mech = Planet.GetMechFromPosition(new Vec2f(x, y), MechType.SmashableBox);
            if (mech != null)
            {
                Debug.Log("Destroy Smashable Box");
                Planet.RemoveMech(mech.mechID.ID);
            }
            else
            {
                Debug.Log("PLACE Smashable Box");
                Planet.AddMech(new Vec2f(x, y), MechType.SmashableBox);
            }
        }
    } 
}
