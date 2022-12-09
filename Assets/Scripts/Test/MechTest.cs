//import UnityEngine

using Enums.PlanetTileMap;
using KMath;
using Enums;
using System.Linq;
using System.Collections.Generic;

namespace Planet.Unity
{
    public class MechTest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;
        
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
            var planet = GameState.Planet;
            ref var tileMap = ref planet.TileMap;
            UnityEngine.Material material = Material;

            if(UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
            {
                selectedMechIndex++;
            } else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
            {
                selectedMechIndex--;
            }

            selectedMechIndex = UnityEngine.Mathf.Clamp(selectedMechIndex, 0, totalMechs);

            planet.Update(UnityEngine.Time.deltaTime);
            
            MaterialBag.hasInventoryDraw = planet.EntitasContext.inventory.GetEntityWithInventoryID(InventoryID).hasInventoryDraw;

            Draw();
        }

        private void Draw()
        {
            if (!Init) return;

            GameState.Planet.DrawHUD(Player);

            if (showMechInventory)
            {
                DrawCurrentMechHighlighter();
            }
        }

        private void DrawCurrentMechHighlighter()
        {
            var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
            int x = (int)worldPosition.X;
            int y = (int)worldPosition.Y;

            //var viewportPos = Camera.main.WorldToViewportPoint(new Vector3(x, y));
            var planet = GameState.Planet;

            if (x >= 0 && x < planet.TileMap.MapSize.X &&
            y >= 0 && y < planet.TileMap.MapSize.Y)
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
                        if (planet.TileMap.GetMidTileID(x + i, y + j) != TileID.Air)
                        {
                            allTilesAir = false;
                            DrawQuad(HighliterMesh.obj, x, y, w, h, wrongHlColor);
                            break;
                        }
                        if (planet.TileMap.GetFrontTileID(x + i, y + j) != TileID.Air)
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
            var planet = GameState.Planet;
            Vec2i mapSize = new Vec2i(16, 16);
            planet.Init(mapSize);

            GenerateMap();

            Player = planet.AddAgentAsPlayer(new Vec2f(2.0f, 4.0f));
            int inventoryID = Player.agentInventory.InventoryID;

            planet.AddItemParticle(ItemType.Pistol, new Vec2f(2.0f, 4.0f));
            planet.AddItemParticle(ItemType.PumpShotgun, new Vec2f(2.0f, 4.0f));
            planet.AddItemParticle(ItemType.WaterBottle, new Vec2f(2.0f, 4.0f));
            //planet.AddItemParticle(Enums.ItemType.PlanterTool, new Vec2f(2.0f, 4.0f));
            planet.AddItemParticle(ItemType.HarvestTool, new Vec2f(2.0f, 4.0f));
            planet.AddItemParticle(ItemType.ConstructionTool, new Vec2f(2.0f, 4.0f));
            //planet.AddItemParticle(Enums.ItemType.PulseWeapon, new Vec2f(2.0f, 4.0f));
            //planet.AddItemParticle(Enums.ItemType.SniperRifle, new Vec2f(2.0f, 4.0f));
            //planet.AddItemParticle(Enums.ItemType.SMG, new Vec2f(2.0f, 4.0f));
            //planet.AddItemParticle(Enums.ItemType.Shotgun, new Vec2f(3.0f, 3.0f));
            //planet.AddItemParticle(Enums.ItemType.LongRifle, new Vec2f(3.0f, 3.0f));
            //planet.AddItemParticle(Enums.ItemType.RPG, new Vec2f(3.0f, 3.0f));
            //planet.AddItemParticle(Enums.ItemType.SMG, new Vec2f(3.0f, 3.0f));
            //planet.AddItemParticle(Enums.ItemType.GrenadeLauncher, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(ItemType.Sword, new Vec2f(2.0f, 4.0f));
            //planet.AddItemParticle(Enums.ItemType.RiotShield, new Vec2f(2.0f, 4.0f));
            //planet.AddItemParticle(Enums.ItemType.StunBaton, new Vec2f(3.0f, 3.0f));
            //planet.AddItemParticle(Enums.ItemType.AutoCannon, new Vec2f(3.0f, 3.0f));
            //planet.AddItemParticle(Enums.ItemType.Bow, new Vec2f(3.0f, 3.0f));
            //planet.AddItemParticle(Enums.ItemType.Ore, new Vec2f(6.0f, 3.0f));

            planet.InitializeSystems(Material, transform);
            
            MaterialBag = planet.AddInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID());
            
            InventoryID = Player.agentInventory.InventoryID;

            var SpawnEnemyTool = GameState.ItemSpawnSystem.SpawnInventoryItem(ItemType.SpawnEnemySlimeTool);
            GameState.InventoryManager.AddItem(SpawnEnemyTool, inventoryID);
            var RemoveMech = GameState.ItemSpawnSystem.SpawnInventoryItem(ItemType.RemoveMech);
            GameState.InventoryManager.AddItem(RemoveMech, inventoryID);

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();

            HighliterMesh = new Utility.FrameMesh("HighliterGameObject", Material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Generic), 30);
            
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.MajestyPalm, 1);
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.SagoPalm, 1);
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.WaterBottle, 1);
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.DracaenaTrifasciata, 1);
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.Chest, 1);
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.Planter, 1);
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.Light, 1);
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.SmashableBox, 1);
            Admin.AdminAPI.AddItem(InventoryManager, MaterialBag.inventoryID.ID, ItemType.SmashableEgg, 1);
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
            var planet = GameState.Planet;
            ref var tileMap = ref planet.TileMap;

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

            var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
            float x = worldPosition.X;
            float y = worldPosition.Y;

            GameState.Planet.AddMech(new Vec2f(x + 2F, y), MechType.Storage);
        }
    } 
}
