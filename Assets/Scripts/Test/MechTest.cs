//import UnityEngine

using Enums.Tile;
using KMath;
using Enums;
using System.Linq;
using System.Collections.Generic;

namespace Planet.Unity
{
    public class MechTest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        public Planet.PlanetState Planet;
        AgentEntity Player;

        static bool Init = false;

        private bool showMechInventory;

        private int selectedMechIndex;

        private int totalMechs;

        private UnityEngine.Color correctHlColor = UnityEngine.Color.green;

        private UnityEngine.Color wrongHlColor = UnityEngine.Color.red;

        public Utility.FrameMesh HighliterMesh;
        
        public InventoryEntity MaterialBag;
        public int InventoryID;
        Inventory.InventoryManager InventoryManager;

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
            UnityEngine.Material material = Material;

            if(UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
            {
                selectedMechIndex++;
            } else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
            {
                selectedMechIndex--;
            }

            selectedMechIndex = UnityEngine.Mathf.Clamp(selectedMechIndex, 0, totalMechs);

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.T))
            {
                GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, Enums.NodeType.DropAction, Player.agentID.ID);
            }

            GameState.MechGUIDrawSystem.Draw(ref Planet, Player);

            Planet.Update(UnityEngine.Time.deltaTime, Material, transform);
            
            MaterialBag.hasInventoryDraw = Planet.EntitasContext.inventory.GetEntityWithInventoryID(InventoryID).hasInventoryDraw;
        }

        private void OnGUI()
        {
            if (!Init) return;

            Planet.DrawHUD(Player);

            if (showMechInventory)
            {
                DrawCurrentMechHighlighter();
            }
        }

        private void DrawCurrentMechHighlighter()
        {
            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            //var viewportPos = Camera.main.WorldToViewportPoint(new Vector3(x, y));

            if (x >= 0 && x < Planet.TileMap.MapSize.X &&
            y >= 0 && y < Planet.TileMap.MapSize.Y)
            {
                var mech = GameState.MechCreationApi.Get((MechType)selectedMechIndex);
                var xRange = UnityEngine.Mathf.CeilToInt(mech.SpriteSize.X);
                var yRange = UnityEngine.Mathf.CeilToInt(mech.SpriteSize.Y);

                var allTilesAir = true;

                var w = mech.SpriteSize.X;
                var h = mech.SpriteSize.Y;

                for (int i = 0; i < xRange; i++)
                {
                    for (int j = 0; j < yRange; j++)
                    {
                        if (Planet.TileMap.GetMidTileID(x + i, y + j) != TileID.Air)
                        {
                            allTilesAir = false;
                            DrawQuad(HighliterMesh.obj, x, y, w, h, wrongHlColor);
                            break;
                        }
                        if (Planet.TileMap.GetFrontTileID(x + i, y + j) != TileID.Air)
                        {
                            allTilesAir = false;
                            DrawQuad(HighliterMesh.obj, x, y, w, h, wrongHlColor);
                            break;
                        }
                    }

                    if (!allTilesAir) break;
                }

                if (allTilesAir)
                {
                    DrawQuad(HighliterMesh.obj, x, y, w, h, correctHlColor);
                }

            }
        }

        private void DrawQuad(UnityEngine.GameObject gameObject, float x, float y, float w, float h, UnityEngine.Color color)
        {
            var mr = gameObject.GetComponent<UnityEngine.MeshRenderer>();
            mr.sharedMaterial.color = color;

            var mf = gameObject.GetComponent<UnityEngine.MeshFilter>();
            var mesh = mf.sharedMesh;

            List<int> triangles = new List<int>();
            List<UnityEngine.Vector3> vertices = new List<UnityEngine.Vector3>();

            Vec2f topLeft = new Vec2f(x, y + h);
            Vec2f BottomLeft = new Vec2f(x, y);
            Vec2f BottomRight = new Vec2f(x + w, y);
            Vec2f TopRight = new Vec2f(x + w, y + h);

            var p0 = new UnityEngine.Vector3(BottomLeft.X, BottomLeft.Y, 0);
            var p1 = new UnityEngine.Vector3(TopRight.X, TopRight.Y, 0);
            var p2 = new UnityEngine.Vector3(topLeft.X, topLeft.Y, 0);
            var p3 = new UnityEngine.Vector3(BottomRight.X, BottomRight.Y, 0);

            vertices.Add(p0);
            vertices.Add(p1);
            vertices.Add(p2);
            vertices.Add(p3);

            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 1);

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles.ToArray(), 0);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            InventoryManager = new Inventory.InventoryManager();
            
            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(16, 16);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

            GenerateMap();

            Player = Planet.AddPlayer(GameState.AnimationManager.CharacterSpriteId, 32, 48, new Vec2f(2.0f, 4.0f), 0, 100, 100, 100, 100, 100);
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
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Sword, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.RiotShield, new Vec2f(2.0f, 4.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.StunBaton, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.AutoCannon, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Bow, new Vec2f(3.0f, 3.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Ore, new Vec2f(6.0f, 3.0f));

            Planet.InitializeSystems(Material, transform);
            Planet.InitializeHUD();
            
            MaterialBag = Planet.AddInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID(), "MaterialBag");
            
            InventoryID = Player.agentInventory.InventoryID;

            GameState.MechGUIDrawSystem.Initialize(ref Planet);

            var SpawnEnemyTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.SpawnEnemySlimeTool);
            GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnEnemyTool, inventoryID);
            var RemoveMech = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.RemoveMech);
            GameState.InventoryManager.AddItem(Planet.EntitasContext, RemoveMech, inventoryID);

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();

            HighliterMesh = new Utility.FrameMesh("HighliterGameObject", Material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), 30);
            
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.MajestyPalm, 1, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.SagoPalm, 1, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.WaterBottle, 1, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.DracaenaTrifasciata, 1, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Chest, 1, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Planter, 1, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Light, 1, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.SmashableBox, 1, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(InventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.SmashableEgg, 1, Planet.EntitasContext);
        }
        
        /*
            MajestyPalm,
            SagoPalm,
            WaterBottle,
            DracaenaTrifasciata,
            Chest,
            Planter,
            Light,
            SmashableBox,
            SmashableEgg,
         */

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
            UnityEngine.Debug.Log("PLACE MECH");

            var planet = FindObjectOfType<ItemTest>().Planet;

            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            planet.AddMech(new Vec2f(x + 2F, y), MechType.Storage);
        }
    } 
}
