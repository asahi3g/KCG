using UnityEngine;
using Enums.PlanetTileMap;
using KMath;
using System.Linq;
using System.Collections.Generic;
using System;
using Collisions;
using PlanetTileMap;

namespace Planet.Unity
{
    class TiledTest : MonoBehaviour
    {
        [SerializeField] Material Material;




        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;

        private int totalMechs;
        private int selectedMechIndex;
        public Utility.FrameMesh HighliterMesh;
        private Color wrongHlColor = Color.red;
        private Color correctHlColor = Color.green;


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

            Tiled.TiledMap tileMap = Tiled.TiledMap.FromJson("generated-maps/untitled.tmj", "generated-maps/");

            int materialCount = Enum.GetNames(typeof(MaterialType)).Length;
            int geometryTilesCount = Enum.GetNames(typeof(Enums.GeometryTileShape)).Length;

            TileProperty[][] MaterialGeometryMap = new TileProperty[materialCount][];
            for(int i = 0; i < materialCount; i++)
            {
                MaterialGeometryMap[i] = new TileProperty[geometryTilesCount];
            }


           

            Application.targetFrameRate = 60;

            GameResources.Initialize();

             for(int i = 0; i < GameState.TileCreationApi.TilePropertyArray.Length; i++)
            {
                ref TileProperty property = ref GameState.TileCreationApi.TilePropertyArray[i];

                MaterialGeometryMap[(int)property.MaterialType][(int)property.BlockShapeType] = property;

            //    Debug.Log("loaded : " + property.MaterialType + " " + property.BlockShapeType + " = " +  property.TileID);
            }

         //   Debug.Log("test : " + MaterialGeometryMap[(int)PlanetTileMap.MaterialType.Metal][(int)Enums.GeometryTileShape.SB_R0].TileID);
//
        //    Debug.Log(GameState.TileCreationApi.GetTileProperty(TileID.SB_R0_Metal).MaterialType + " " + GameState.TileCreationApi.GetTileProperty(TileID.SB_R0_Metal).BlockShapeType);

            // Generating the map
            int mapWidth = tileMap.width;
            int mapHeight = tileMap.height;

            mapWidth = ((mapWidth + 16 - 1) / 16) * 16; // multiple of 16
            mapHeight = ((mapHeight + 16 - 1) / 16) * 16; // multiple of 16

            Debug.Log(mapWidth + " " + mapHeight);
            
            ref var planet = ref GameState.Planet;
            Vec2i mapSize = new Vec2i(mapWidth, mapHeight);
            planet.Init(mapSize);

            int PlayerFaction = 0;

            Player = planet.AddPlayer(new Vec2f(3.0f, 4), PlayerFaction);
            PlayerID = Player.agentID.ID;

            //planet.AddAgent(new Vec2f(16.0f, 20), Enums.AgentType.EnemyMarine, EnemyFaction);

            PlayerID = Player.agentID.ID;
            inventoryID = Player.agentInventory.InventoryID;

            planet.InitializeSystems(Material, transform);
            planet.InitializeHUD();
            GameState.MechGUIDrawSystem.Initialize();
            //GenerateMap();
            var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

            /*planet.TileMap = TileMapManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
                Debug.Log("loaded!");*/

            for(int j = 0; j < tileMap.height; j++)
            {
                for(int i = 0; i < tileMap.width; i++)
                {
                    int tileIndex = tileMap.layers[0].data[i + ((tileMap.height - 1) - j) * tileMap.width] - 1;
                    if (tileIndex >= 0)
                    {
                        Tiled.TiledMaterialAndShape tileMaterialAndShape = tileMap.GetTile(tileIndex);

                        TileID tileID = MaterialGeometryMap[(int)tileMaterialAndShape.Material][(int)tileMaterialAndShape.Shape].TileID;

                        planet.TileMap.GetTile(i, j).FrontTileID = tileID;
                    }
                }
            }

            /*for(int i = 0; i < planet.TileMap.MapSize.X; i++)
            {
                planet.TileMap.GetTile(i, 0).FrontTileID =  TileID.Bedrock;
                planet.TileMap.GetTile(i, planet.TileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            for(int j = 0; j < planet.TileMap.MapSize.Y; j++)
            {
                planet.TileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                planet.TileMap.GetTile(planet.TileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }*/

            //GenerateMap();

            planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);

            AddItemsToPlayer();

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();

            HighliterMesh = new Utility.FrameMesh("HighliterGameObject", Material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), 30);



            CharacterDisplay = new KGui.CharacterDisplay();
            CharacterDisplay.setPlayer(Player);

            //UpdateMode(ref Planet, Player);
        }
        Collisions.Box2D otherBox = new Box2D {x = 7, y = 21, w = 1.0f, h = 1.0f};
        Collisions.Box2D orrectedBox = new Box2D {x = 0, y = 17, w = 1.0f, h = 1.0f};

        public void Update()
        {

            Vector3 p = Input.mousePosition;
            p.z = 20;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(p);
            
            var playerPhysicsState = Player.agentPhysicsState;
            Vec2f playerPosition = playerPhysicsState.Position;
            var playerCollider = Player.physicsBox2DCollider;

            orrectedBox.w = playerCollider.Size.X;
            orrectedBox.h = playerCollider.Size.Y;

            
            Vec2f velocity = new Vec2f(mouse.x - orrectedBox.x, mouse.y - orrectedBox.y);
            Collisions.Collisions.SweptBox2dCollision(ref orrectedBox, velocity, otherBox, false);


            ref var planet = ref GameState.Planet;
            ref var tileMap = ref planet.TileMap;
            Material material = Material;

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedMechIndex++;
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectedMechIndex--;
            }

            selectedMechIndex = Mathf.Clamp(selectedMechIndex, 0, totalMechs);

            if (Input.GetKeyDown(KeyCode.F1))
            {
                PlanetManager.Save(planet, "generated-maps/movement-map.kmap");
                Debug.Log("saved!");
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                var camera = Camera.main;
                Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

                planet.Destroy();
                planet = PlanetManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);


               planet.InitializeSystems(Material, transform);
               planet.InitializeHUD();
               GameState.MechGUIDrawSystem.Initialize();

               Player = planet.AddPlayer(new Vec2f(3.0f, 20));
               PlayerID = Player.agentID.ID;
               inventoryID = Player.agentInventory.InventoryID;

               AddItemsToPlayer();

                Debug.Log("loaded!");
            }


            CharacterDisplay.Update();
            planet.Update(Time.deltaTime, Material, transform);

        }
        Texture2D texture;

        private void OnGUI()
        {
            /*if (!Init)
                return;

            GameState.Planet.DrawHUD(Player); 
            GameState.MechGUIDrawSystem.Draw(ref Planet, Player);

            if (showMechInventory)
            {
                DrawCurrentMechHighlighter();
            }

            
            CharacterDisplay.Draw();*/

                 
        }

        private void DrawQuad(GameObject gameObject, float x, float y, float w, float h, Color color)
        {
            var mr = gameObject.GetComponent<MeshRenderer>();
            mr.sharedMaterial.color = color;

            var mf = gameObject.GetComponent<MeshFilter>();
            var mesh = mf.sharedMesh;

            List<int> triangles = new List<int>();
            List<Vector3> vertices = new List<Vector3>();

            Vec2f topLeft = new Vec2f(x, y + h);
            Vec2f BottomLeft = new Vec2f(x, y);
            Vec2f BottomRight = new Vec2f(x + w, y);
            Vec2f TopRight = new Vec2f(x + w, y + h);

            var p0 = new Vector3(BottomLeft.X, BottomLeft.Y, 0);
            var p1 = new Vector3(TopRight.X, TopRight.Y, 0);
            var p2 = new Vector3(topLeft.X, topLeft.Y, 0);
            var p3 = new Vector3(BottomRight.X, BottomRight.Y, 0);

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
            ref var planet = ref GameState.Planet;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            //var viewportPos = Camera.main.WorldToViewportPoint(new Vector3(x, y));

            if (x >= 0 && x < planet.TileMap.MapSize.X &&
            y >= 0 && y < planet.TileMap.MapSize.Y)
            {
                //TODO: SET TO Get(selectedMechIndex)
                var mech = GameState.MechCreationApi.Get((Enums.MechType)selectedMechIndex);
                var xRange = Mathf.CeilToInt(mech.SpriteSize.X);
                var yRange = Mathf.CeilToInt(mech.SpriteSize.Y);

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
            ref var planet = ref GameState.Planet;
            planet.DrawDebug();

            // Set the color of gizmos
            Gizmos.color = Color.green;
            
            // Draw a cube around the map
            if(planet.TileMap != null)
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(planet.TileMap.MapSize.X, planet.TileMap.MapSize.Y, 0.0f));

            // Draw lines around player if out of bounds
            if (Player != null)
                if(Player.agentPhysicsState.Position.X -10.0f >= planet.TileMap.MapSize.X)
                {
                    // Out of bounds
                
                    // X+
                    Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X + 10.0f, Player.agentPhysicsState.Position.Y));

                    // X-
                    Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X - 10.0f, Player.agentPhysicsState.Position.Y));

                    // Y+
                    Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y + 10.0f));

                    // Y-
                    Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y - 10.0f));
                }

            // Draw Chunk Visualizer
            ChunkVisualizer.Draw(0.5f, 0.0f);


            bool drawRayCast = false;


            if (drawRayCast)
            {
                Vector3 p = Input.mousePosition;
                p.z = 20;
                Vector3 mouse = Camera.main.ScreenToWorldPoint(p);

                var rayCastResult = Collisions.Collisions.RayCastAgainstTileMap(new Line2D(Player.agentPhysicsState.Position, new Vec2f(mouse.x, mouse.y)));
                
                Vec2f startPos = Player.agentPhysicsState.Position;
                Vec2f endPos = new Vec2f(mouse.x, mouse.y);
                Gizmos.DrawLine(new Vector3(startPos.X, startPos.Y, 20), new Vector3(endPos.X, endPos.Y, 20));

                if (rayCastResult.Intersect)
                {
                    Gizmos.DrawWireCube(new Vector3(rayCastResult.Point.X, rayCastResult.Point.Y, 20),
                    new Vector3(0.3f, 0.3f, 0.3f));
                }
            }

            
            bool testCircleCollision = false;
            bool testRectangleCollision = false;

            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 20.0f;

            if (testCircleCollision)
            {
                int[] agentIds = Collisions.Collisions.BroadphaseAgentCircleTest(new Vec2f(worldPosition.x, worldPosition.y), 0.5f);

                Gizmos.color = Color.yellow;
                if (agentIds != null && agentIds.Length > 0)
                {
                    Gizmos.color = Color.red;
                }    
                
                Gizmos.DrawSphere(worldPosition, 0.5f);
            }

            if (testRectangleCollision)
            {
                int[] agentIds = Collisions.Collisions.BroadphaseAgentBoxTest(new AABox2D(new Vec2f(worldPosition.x, worldPosition.y), new Vec2f(0.5f, 0.75f)));

                Gizmos.color = Color.yellow;
                if (agentIds != null && agentIds.Length > 0)
                {
                    Gizmos.color = Color.red;
                }    
                
                Gizmos.DrawWireCube(worldPosition + new Vector3(0.25f, 0.75f * 0.5f, 0), new Vector3(0.5f, 0.75f, 0.5f));
            }

            bool testRayAgainstCircle = false;

            if (testRayAgainstCircle)
            {
                Vector3 p = Input.mousePosition;
                p.z = 20;
                Vector3 mouse = Camera.main.ScreenToWorldPoint(p);

                var rayCastResult = Collisions.Collisions.RayCastAgainstCircle(new Line2D(Player.agentPhysicsState.Position, new Vec2f(mouse.x, mouse.y)),
                 new Vec2f(9, 19), 1.0f);

                Vec2f startPos = Player.agentPhysicsState.Position;
                Vec2f endPos = new Vec2f(mouse.x, mouse.y);
                Gizmos.DrawLine(new Vector3(startPos.X, startPos.Y, 20), new Vector3(endPos.X, endPos.Y, 20));

                Gizmos.color = Color.yellow;
                if (rayCastResult.Intersect)
                {
                    Gizmos.DrawWireCube(new Vector3(rayCastResult.Point.X, rayCastResult.Point.Y, 20),
                    new Vector3(0.3f, 0.3f, 0.3f));

                    Gizmos.color = Color.red;
                }

                Gizmos.DrawSphere(new Vector3(9, 19, 20.0f), 1.0f);
            }


            bool testSweptCollision = true;
            if (testSweptCollision)
            {
                //var playerCollider = Player.physicsBox2DCollider;
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(new Vector3(otherBox.x, otherBox.y, 1.0f), new Vector3(otherBox.w, otherBox.h, 0.5f));
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(new Vector3(orrectedBox.x, orrectedBox.y, 1.0f), new Vector3(orrectedBox.w, orrectedBox.h, 0.5f));
            }
        }


        public void AddItemsToPlayer()
        {
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.PlacementTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveTileTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemyGunnerTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemySwordmanTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ConstructionTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveMech);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.HealthPositon);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SMG);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol);
            //Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Sword);
           // Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.FragGrenade);
        }

            private void UpdateMode(AgentEntity agentEntity)
        {
            ref var planet = ref GameState.Planet;
            agentEntity.agentPhysicsState.Invulnerable = false;
            Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planet.CameraFollow.canFollow = false;

            Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planet.CameraFollow.canFollow = true;
        }


        
        public TileID GetTileId(string geometry)
        {
            TileID result = TileID.Air;
            if (!Enum.TryParse<TileID>(geometry, out result))
            {
                result = TileID.Air;
            }



            return result;
        }

    }


}
