using UnityEngine;
using Enums.Tile;
using KMath;
using Utility;
using System.Linq;
using System.Collections.Generic;
using System;
using Mech;

namespace Planet.Unity
{
    class TiledTest : MonoBehaviour
    {
        [SerializeField] Material Material;

        public PlanetState Planet;


        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;

        bool showMechInventory = false;

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

            Tiled.TiledMap tileMap = Tiled.TiledMap.FromJson("generated-maps/map2.tmj", "generated-maps/");

            int materialCount = Enum.GetNames(typeof(PlanetTileMap.MaterialType)).Length;
            int geometryTilesCount = Enum.GetNames(typeof(Enums.GeometryTileShape)).Length;

            PlanetTileMap.TileProperty[][] MaterialGeometryMap = new PlanetTileMap.TileProperty[materialCount][];
            for(int i = 0; i < materialCount; i++)
            {
                MaterialGeometryMap[i] = new PlanetTileMap.TileProperty[geometryTilesCount];
            }


           

            Application.targetFrameRate = 60;

            GameResources.Initialize();

             for(int i = 0; i < GameState.TileCreationApi.TilePropertyArray.Length; i++)
            {
                ref PlanetTileMap.TileProperty property = ref GameState.TileCreationApi.TilePropertyArray[i];

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
            Vec2i mapSize = new Vec2i(mapWidth, mapHeight);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

            int PlayerFaction = 0;
            int EnemyFaction = 1;

            Player = Planet.AddPlayer(new Vec2f(22.0f, 8), PlayerFaction);
            PlayerID = Player.agentID.ID;

            //Planet.AddAgent(new Vec2f(16.0f, 20), Enums.AgentType.EnemyMarine, EnemyFaction);

            PlayerID = Player.agentID.ID;
            inventoryID = Player.agentInventory.InventoryID;

            Planet.InitializeSystems(Material, transform);
            Planet.InitializeHUD();
            GameState.MechGUIDrawSystem.Initialize(ref Planet);
            //GenerateMap();
            var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

            /*Planet.TileMap = TileMapManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
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

                        Planet.TileMap.GetTile(i, j).FrontTileID = tileID;
                    }
                }
            }

            /*for(int i = 0; i < Planet.TileMap.MapSize.X; i++)
            {
                Planet.TileMap.GetTile(i, 0).FrontTileID =  TileID.Bedrock;
                Planet.TileMap.GetTile(i, Planet.TileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            for(int j = 0; j < Planet.TileMap.MapSize.Y; j++)
            {
                Planet.TileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                Planet.TileMap.GetTile(Planet.TileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }*/

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

            if (Input.GetKeyDown(KeyCode.F1))
            {
                PlanetManager.Save(Planet, "generated-maps/movement-map.kmap");
                Debug.Log("saved!");
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                var camera = Camera.main;
                Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

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

                Debug.Log("loaded!");
            }


            CharacterDisplay.Update();
            Planet.Update(Time.deltaTime, Material, transform);

        }
        Texture2D texture;

        private void OnGUI()
        {
            /*if (!Init)
                return;

            Planet.DrawHUD(Player); 
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
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            //var viewportPos = Camera.main.WorldToViewportPoint(new Vector3(x, y));

            if (x >= 0 && x < Planet.TileMap.MapSize.X &&
            y >= 0 && y < Planet.TileMap.MapSize.Y)
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
          /*  Planet.DrawDebug();

            // Set the color of gizmos
            Gizmos.color = Color.green;
            
            // Draw a cube around the map
            if(Planet.TileMap != null)
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(Planet.TileMap.MapSize.X, Planet.TileMap.MapSize.Y, 0.0f));

            // Draw lines around player if out of bounds
            if (Player != null)
                if(Player.agentPhysicsState.Position.X -10.0f >= Planet.TileMap.MapSize.X)
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
            Admin.AdminAPI.DrawChunkVisualizer(Planet.TileMap);*/

var pos = Player.agentPhysicsState.Position + Player.physicsBox2DCollider.Offset + Player.physicsBox2DCollider.Size.X / 2.0f;

                    Gizmos.color = Color.red;
                Gizmos.DrawSphere(new Vector3(pos.X, pos.Y, 20.0f), Player.physicsBox2DCollider.Size.X * 0.5f);


                Gizmos.color = Color.red;
                Gizmos.DrawSphere(new Vector3(pos.X, pos.Y + 2.0f, 20.0f), Player.physicsBox2DCollider.Size.X * 0.5f);
          
            for (int i = 0; i < Planet.DebugLinesCount; i++)
            {
                Line2D line = Planet.DebugLines[i];
                Gizmos.color = Planet.DebugLinesColors[i];
                Gizmos.DrawLine(new Vector3(line.A.X, line.A.Y, 1.0f), new Vector3(line.B.X, line.B.Y, 1.0f));
            }
        }


        public void AddItemsToPlayer()
        {
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveTileTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemyGunnerTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemySwordmanTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ConstructionTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveMech, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.HealthPositon, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SMG, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol, Planet.EntitasContext);
            //Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Sword, Planet.EntitasContext);
           // Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.FragGrenade, Planet.EntitasContext);
        }

            private void UpdateMode(ref Planet.PlanetState planetState, AgentEntity agentEntity)
        {
            agentEntity.agentPhysicsState.Invulnerable = false;
            Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planetState.cameraFollow.canFollow = false;

            Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planetState.cameraFollow.canFollow = true;
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
