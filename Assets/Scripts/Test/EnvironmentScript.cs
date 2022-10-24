//imports UnityEngine

using Enums.Tile;
using KMath;
using Item;
using Animancer;
using PlanetTileMap;
using System.Linq;
using System.Collections.Generic;

namespace Planet.Unity
{
    class EnvironmentScript : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        public PlanetState Planet;


        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;

        bool showMechInventory = false;

        private int totalMechs;
        private int selectedMechIndex;
        public Utility.FrameMesh HighliterMesh;
        private UnityEngine.Color wrongHlColor = UnityEngine.Color.red;
        private UnityEngine.Color correctHlColor = UnityEngine.Color.green;


        KGui.CharacterDisplay CharacterDisplay;

        static bool Init = false;

        public void Start()
        {
            if (!Init)
            {

                Initialize();
                Init = true;
            }
        }

                // create the sprite atlas for testing purposes
        public void Initialize()
        {

            UnityEngine.Application.targetFrameRate = 60;

            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(128, 96);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

            Player = Planet.AddPlayer(new Vec2f(3.0f, 20));
            PlayerID = Player.agentID.ID;

            PlayerID = Player.agentID.ID;
            inventoryID = Player.agentInventory.InventoryID;

            Planet.InitializeSystems(Material, transform);
            Planet.InitializeHUD();
            GameState.MechGUIDrawSystem.Initialize(ref Planet);
            //GenerateMap();
            var camera = UnityEngine.Camera.main;
            UnityEngine.Vector3 lookAtPosition = camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, camera.nearClipPlane));

            Planet.TileMap = TileMapManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
            UnityEngine.Debug.Log("loaded!");

            //GenerateMap();

            Planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            Planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            Planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);

            AddItemsToPlayer();

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();

            HighliterMesh = new Utility.FrameMesh("HighliterGameObject", Material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), 30);



            CharacterDisplay = new KGui.CharacterDisplay();
            CharacterDisplay.setPlayer(Player);

            UpdateMode(ref Planet, Player);
        }
        Collisions.Box2D otherBox = new Collisions.Box2D{x = 7, y = 21, w = 1.0f, h = 1.0f};
        Collisions.Box2D orrectedBox = new Collisions.Box2D{x = 0, y = 17, w = 1.0f, h = 1.0f};

        public void Update()
        {

            UnityEngine.Vector3 p = UnityEngine.Input.mousePosition;
            p.z = 20;
            UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(p);
            
            var playerPhysicsState = Player.agentPhysicsState;
            Vec2f playerPosition = playerPhysicsState.Position;
            var playerCollider = Player.physicsBox2DCollider;

            orrectedBox.w = playerCollider.Size.X;
            orrectedBox.h = playerCollider.Size.Y;

            
            Vec2f velocity = new Vec2f(mouse.x - orrectedBox.x, mouse.y - orrectedBox.y);
            Collisions.Collisions.SweptBox2dCollision(ref orrectedBox, velocity, otherBox, false);


            
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

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F1))
            {
                PlanetManager.Save(Planet, "generated-maps/movement-map.kmap");
                UnityEngine.Debug.Log("saved!");
            }

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F2))
            {
                var camera = UnityEngine.Camera.main;
                UnityEngine.Vector3 lookAtPosition = camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, camera.nearClipPlane));

                Planet.Destroy();
                Planet = PlanetManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
                Planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                Planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                Planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);


               Planet.InitializeSystems(Material, transform);
               Planet.InitializeHUD();
               GameState.MechGUIDrawSystem.Initialize(ref Planet);

               Player = Planet.AddPlayer(new Vec2f(3.0f, 20));
               PlayerID = Player.agentID.ID;

               inventoryID = Player.agentInventory.InventoryID;

               AddItemsToPlayer();

                UnityEngine.Debug.Log("loaded!");
            }


            CharacterDisplay.Update();
            Planet.Update(UnityEngine.Time.deltaTime, Material, transform);

        }
        UnityEngine.Texture2D texture;

        private void OnGUI()
        {
            if (!Init)
                return;

            Planet.DrawHUD(Player); 
            GameState.MechGUIDrawSystem.Draw(ref Planet, Player);

            if (showMechInventory)
            {
                DrawCurrentMechHighlighter();
            }

            
            CharacterDisplay.Draw();

                 
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

        private void DrawCurrentMechHighlighter()
        {
            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            //var viewportPos = Camera.main.WorldToViewportPoint(new Vector3(x, y));

            if (x >= 0 && x < Planet.TileMap.MapSize.X &&
            y >= 0 && y < Planet.TileMap.MapSize.Y)
            {
                //TODO: SET TO Get(selectedMechIndex)
                var mech = GameState.MechCreationApi.Get(selectedMechIndex);
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

                    if (!allTilesAir)
                        break;
                }

                if (allTilesAir)
                {
                    DrawQuad(HighliterMesh.obj, x, y, w, h, correctHlColor);
                }

            }
        }

    


        private void OnDrawGizmos()
        {
            Planet.DrawDebug();

            // Set the color of gizmos
            UnityEngine.Gizmos.color = UnityEngine.Color.green;
            
            // Draw a cube around the map
            if(Planet.TileMap != null)
                UnityEngine.Gizmos.DrawWireCube(UnityEngine.Vector3.zero, new UnityEngine.Vector3(Planet.TileMap.MapSize.X, Planet.TileMap.MapSize.Y, 0.0f));

            // Draw lines around player if out of bounds
            if (Player != null)
                if(Player.agentPhysicsState.Position.X -10.0f >= Planet.TileMap.MapSize.X)
                {
                    // Out of bounds

                    // X+
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X + 10.0f, Player.agentPhysicsState.Position.Y));

                    // X-
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X - 10.0f, Player.agentPhysicsState.Position.Y));

                    // Y+
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y + 10.0f));

                    // Y-
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y - 10.0f));
                }

            // Draw Chunk Visualizer
            ChunkVisualizer.Draw(Planet.TileMap, 0.5f, 0.0f);


            bool drawRayCast = false;


            if (drawRayCast)
            {
                UnityEngine.Vector3 p = UnityEngine.Input.mousePosition;
                p.z = 20;
                UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(p);

                var rayCastResult = Collisions.Collisions.RayCastAgainstTileMap(Planet.TileMap, new Line2D(Player.agentPhysicsState.Position, new Vec2f(mouse.x, mouse.y)));
                
                Vec2f startPos = Player.agentPhysicsState.Position;
                Vec2f endPos = new Vec2f(mouse.x, mouse.y);
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(startPos.X, startPos.Y, 20), new UnityEngine.Vector3(endPos.X, endPos.Y, 20));

                if (rayCastResult.Intersect)
                {
                    UnityEngine.Gizmos.DrawWireCube(new UnityEngine.Vector3(rayCastResult.Point.X, rayCastResult.Point.Y, 20),
                    new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
                }
            }

            
            bool testCircleCollision = false;
            bool testRectangleCollision = false;


            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            worldPosition.z = 20.0f;

            if (testCircleCollision)
            {
                int[] agentIds = Collisions.Collisions.BroadphaseAgentCircleTest(Planet, new Vec2f(worldPosition.x, worldPosition.y), 0.5f);

                UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
                if (agentIds != null && agentIds.Length > 0)
                {
                    UnityEngine.Gizmos.color = UnityEngine.Color.red;
                }

                UnityEngine.Gizmos.DrawSphere(worldPosition, 0.5f);
            }

            if (testRectangleCollision)
            {
                int[] agentIds = Collisions.Collisions.BroadphaseAgentBoxTest(Planet, 
                new KMath.AABox2D(new Vec2f(worldPosition.x, worldPosition.y), new Vec2f(0.5f, 0.75f)));

                UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
                if (agentIds != null && agentIds.Length > 0)
                {
                    UnityEngine.Gizmos.color = UnityEngine.Color.red;
                }

                UnityEngine.Gizmos.DrawWireCube(worldPosition + new UnityEngine.Vector3(0.25f, 0.75f * 0.5f, 0), new UnityEngine.Vector3(0.5f, 0.75f, 0.5f));
            }

            bool testRayAgainstCircle = false;

            if (testRayAgainstCircle)
            {
                UnityEngine.Vector3 p = UnityEngine.Input.mousePosition;
                p.z = 20;
                UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(p);

                var rayCastResult = Collisions.Collisions.RayCastAgainstCircle(new Line2D(Player.agentPhysicsState.Position, new Vec2f(mouse.x, mouse.y)),
                 new Vec2f(9, 19), 1.0f);

                Vec2f startPos = Player.agentPhysicsState.Position;
                Vec2f endPos = new Vec2f(mouse.x, mouse.y);
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(startPos.X, startPos.Y, 20), new UnityEngine.Vector3(endPos.X, endPos.Y, 20));

                UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
                if (rayCastResult.Intersect)
                {
                    UnityEngine.Gizmos.DrawWireCube(new UnityEngine.Vector3(rayCastResult.Point.X, rayCastResult.Point.Y, 20),
                    new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));

                    UnityEngine.Gizmos.color = UnityEngine.Color.red;
                }

                UnityEngine.Gizmos.DrawSphere(new UnityEngine.Vector3(9, 19, 20.0f), 1.0f);
            }


            bool testSweptCollision = true;
            if (testSweptCollision)
            {
                //var playerCollider = Player.physicsBox2DCollider;
                UnityEngine.Gizmos.color = UnityEngine.Color.red;
                UnityEngine.Gizmos.DrawWireCube(new UnityEngine.Vector3(otherBox.x, otherBox.y, 1.0f), new UnityEngine.Vector3(otherBox.w, otherBox.h, 0.5f));
                UnityEngine.Gizmos.color = UnityEngine.Color.green;
                UnityEngine.Gizmos.DrawWireCube(new UnityEngine.Vector3(orrectedBox.x, orrectedBox.y, 1.0f), new UnityEngine.Vector3(orrectedBox.w, orrectedBox.h, 0.5f));
            }
        }


        public void AddItemsToPlayer()
        {
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
                        Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.GeometryPlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveTileTool, Planet.EntitasContext);
           // Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemyGunnerTool, Planet.EntitasContext);
           // Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemySwordmanTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ConstructionTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveMech, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.HealthPositon, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SMG, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol, Planet.EntitasContext);
            //Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Sword, Planet.EntitasContext);
           // Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.FragGrenade, Planet.EntitasContext);
        }



        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong) System.DateTime.Now.Ticks);
            
            ref var tileMap = ref Planet.TileMap;

            
            for(int i = 0; i < tileMap.MapSize.X; i++)
            {
                tileMap.GetTile(i, 0).FrontTileID =  TileID.Bedrock;
                tileMap.GetTile(i, tileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            for(int j = 0; j < tileMap.MapSize.Y; j++)
            {
                tileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(tileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }
        }

            private void UpdateMode(ref Planet.PlanetState planetState, AgentEntity agentEntity)
        {
            agentEntity.agentPhysicsState.Invulnerable = false;
            UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planetState.cameraFollow.canFollow = false;

            UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planetState.cameraFollow.canFollow = true;
        }

    }

}
