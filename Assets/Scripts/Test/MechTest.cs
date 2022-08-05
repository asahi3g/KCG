using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Mech;
using System.Linq;

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
            Planet.Update(Time.deltaTime, Material, transform);
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            if (Event.current.type != EventType.Repaint)
                return;

            GameState.InventoryDrawSystem.Draw(Planet.EntitasContext);
            GameState.InventoryMouseSelectionSystem.Draw(Planet.EntitasContext);

            if (showMechInventory)
            {
                GameState.MechGUIDrawSystem.Draw(Planet.EntitasContext, transform, selectedMechIndex);

                DrawCurrentMechHighlighter();
            }
        }

        private void DrawCurrentMechHighlighter()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            var viewportPos = Camera.main.WorldToViewportPoint(new Vector3(x, y));

            if (x >= 0 && x < Planet.TileMap.MapSize.X &&
            y >= 0 && y < Planet.TileMap.MapSize.Y)
            {
                //TODO: SET TO Get(selectedMechIndex)
                var mech = GameState.MechCreationApi.Get(2);
                Debug.Log(mech.Name);
                var xRange = Mathf.CeilToInt(mech.SpriteSize.X);
                var yRange = Mathf.CeilToInt(mech.SpriteSize.Y);
                
                Debug.Log(mech.SpriteSize.X);
                Debug.Log(mech.SpriteSize.Y);

                var allTilesAir = true;

                var w = mech.SpriteSize.X / Screen.width;
                var h = mech.SpriteSize.Y / Screen.height;

                for (int i = 0; i < xRange; i++)
                {
                    for (int j = 0; j < yRange; j++)
                    {
                        if (Planet.TileMap.GetMidTileID(x + i, y + j) != TileID.Air)
                        {
                            allTilesAir = false;
                            GameState.Renderer.DrawQuadColorGui(viewportPos.x, viewportPos.y, w, h, wrongHlColor);
                            break;
                        }
                        if (Planet.TileMap.GetFrontTileID(x + i, y + j) != TileID.Air)
                        {
                            allTilesAir = false;
                            GameState.Renderer.DrawQuadColorGui(viewportPos.x, viewportPos.y, w, h, wrongHlColor);
                            break;
                        }
                    }
                }

                if (allTilesAir)
                {
                    GameState.Renderer.DrawQuadColorGui(viewportPos.x, viewportPos.y, w, h, correctHlColor);
                }

            }
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(16, 16);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);

            GenerateMap();

            Player = Planet.AddPlayer(GameResources.CharacterSpriteId, 32, 48, new Vec2f(2.0f, 4.0f), 0, 100, 100, 100, 100, 100);
            int inventoryID = Player.agentInventory.InventoryID;

            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Pistol, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.PumpShotgun, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.WaterBottle, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.MajestyPalm, new Vec2f(2.0f, 4.0f));
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

            var SpawnEnemyTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.SpawnEnemySlimeTool);
            GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnEnemyTool, inventoryID);

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();
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
    } 
}
