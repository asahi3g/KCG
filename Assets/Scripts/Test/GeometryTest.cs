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
    class GeometryTest : MonoBehaviour
    {
        Material Material;




        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;

        private int totalMechs;
        private int selectedMechIndex;

        Planet.PlanetState Planet;


        public void Start()
        {
            Initialize();
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            Material = Resources.Load("Materials/TextureMaterial") as Material;
            Tiled.TiledMap tileMap = Tiled.TiledMap.FromJson("generated-maps/map3.tmj", "generated-maps/");

            int materialCount = Enum.GetNames(typeof(Enums.MaterialType)).Length;
            int geometryTilesCount = Enum.GetNames(typeof(Enums.TileGeometryAndRotation)).Length;

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

            }

            // Generating the map
            int mapWidth = tileMap.width;
            int mapHeight = tileMap.height;

            mapWidth = ((mapWidth + 16 - 1) / 16) * 16; // multiple of 16
            mapHeight = ((mapHeight + 16 - 1) / 16) * 16; // multiple of 16

            Debug.Log(mapWidth + " " + mapHeight);
            
            Planet = GameState.Planet;
            Vec2i mapSize = new Vec2i(mapWidth, mapHeight);
            Planet.Init(mapSize);

            int PlayerFaction = 0;
            int EnemyFaction = 1;

            Player = Planet.AddAgentAsPlayer(new Vec2f(30.0f, 20.0f), PlayerFaction);
            PlayerID = Player.agentID.ID;

            GameState.Planet.AddAgent(new Vec2f(10.0f, 10f), Enums.AgentType.EnemyMarine, EnemyFaction);

            GameState.Planet.AddVehicle(Enums.VehicleType.DropShip, new Vec2f(16.0f, 20));
            GameState.Planet.AddPod(new Vec2f(16.0f, 20), Enums.PodType.Default);


            PlayerID = Player.agentID.ID;
            inventoryID = Player.agentInventory.InventoryID;

            Planet.InitializeSystems(Material, transform);
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

                        Planet.TileMap.GetTile(i, j).FrontTileID = tileID;
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

            Planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            Planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            Planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);

            AddItemsToPlayer();

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();



            PlanetTileMap.TileMapGeometry.BuildGeometry(Planet.TileMap);

            UpdateMode(Player);
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

               Player = planet.AddAgentAsPlayer(new Vec2f(3.0f, 20));
               PlayerID = Player.agentID.ID;
               inventoryID = Player.agentInventory.InventoryID;

               AddItemsToPlayer();

                Debug.Log("loaded!");
            }


            planet.Update(Time.deltaTime);

        }
        Texture2D texture;

        private void OnGUI()
        {
            GameState.Planet.DrawHUD(Player);
            /* CharacterDisplay.Draw();*/
        }


        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

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

            for (int i = 0; i < Planet.TileMap.GeometryArrayCount; i++)
            {
                Line2D line = Planet.TileMap.GeometryArray[i].Line;
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(new Vector3(line.A.X, line.A.Y, 1.0f), new Vector3(line.B.X, line.B.Y, 1.0f));
            }


            GameState.Planet.DrawDebugEx();
        }


        public void AddItemsToPlayer()
        {
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.PlacementTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveTileTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemyGunnerTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemySwordmanTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ConstructionTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveMech);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.HealthPotion);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SMG);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.GeometryPlacementTool);
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

    }


}
