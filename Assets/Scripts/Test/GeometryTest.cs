using UnityEngine;
using Enums.PlanetTileMap;
using KMath;
using System.Linq;
using System;
using Collisions;
using PlanetTileMap;
using Audio;
using Engine3D;

namespace Planet.Unity
{
    class GeometryTest : MonoBehaviour
    {
        public Material Material;

        AgentEntity Player;
        int PlayerID;

        [UnityEngine.SerializeField] private bool enableBackgroundPlacementTool;

        int CharacterSpriteId;
        int inventoryID;

        private int totalMechs;
        private int selectedMechIndex;

        Planet.PlanetState Planet;

        private TGen.DarkGreyBackground.BackgroundPlacementTool placementTool;

        public void Start()
        {
            Initialize();
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            
            PlanetLoader.Load(transform, "map3", Material, App.Instance.GetPlayer().GetCamera().GetMain(), OnPlanerLoaderSuccess, OnPlanetLoaderFailed);

            void OnPlanerLoaderSuccess(PlanetLoader.Result result)
            {
                App.Instance.GetPlayer().SetCurrentPlanet(result);
                Planet = result.GetPlanetState();
                
                Player = Planet.AddAgentAsPlayer(new Vec2f(30.0f, 20.0f), Agent.AgentFaction.Player);
                
                App.Instance.GetPlayer().SetAgentRenderer(Player.agentAgent3DModel.Renderer);

                PlayerID = Player.agentID.ID;
                inventoryID = Player.agentInventory.InventoryID;

                if (enableBackgroundPlacementTool)
                {
                    placementTool = new TGen.DarkGreyBackground.BackgroundPlacementTool();
                    placementTool.Initialize(new Vec2i(result.GetTileMap().width, result.GetTileMap().height), Material, transform);
                }
            }

            void OnPlanetLoaderFailed(IError error)
            {
                Debug.LogError(error.GetMessage());
            }

            AddItemsToPlayer();

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();
        }
        
        Collisions.Box2D otherBox = new Box2D {x = 7, y = 21, w = 1.0f, h = 1.0f};
        Collisions.Box2D orrectedBox = new Box2D {x = 0, y = 17, w = 1.0f, h = 1.0f};

        public void Update()
        {
            return;

            var mouse = ECSInput.InputProcessSystem.GetCursorWorldPosition(20);
            
            var playerPhysicsState = Player.agentPhysicsState;
            Vec2f playerPosition = playerPhysicsState.Position;
            var playerCollider = Player.physicsBox2DCollider;

            orrectedBox.w = playerCollider.Size.X;
            orrectedBox.h = playerCollider.Size.Y;

            
            Vec2f velocity = new Vec2f(mouse.X - orrectedBox.x, mouse.Y - orrectedBox.y);
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

                planet.TileMap.SetBackTile(22, 22, TileID.Planet1);

               planet.InitializeSystems(Material, transform);


               Player = planet.AddAgentAsPlayer(new Vec2f(3.0f, 20));
               PlayerID = Player.agentID.ID;
               inventoryID = Player.agentInventory.InventoryID;

                AddItemsToPlayer();

                Debug.Log("loaded!");
            }

            planet.Update(Time.deltaTime);

            if (enableBackgroundPlacementTool)
            {
                placementTool.UpdateToolGrid(planet.TileMap);
            }

        }
        Texture2D texture;

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
            CameraMove cameraMove = Camera.main.gameObject.GetComponent<CameraMove>();
            if (cameraMove == null) return;
            
            var planet = GameState.Planet;
            agentEntity.agentPhysicsState.Invulnerable = false;
            cameraMove.enabled = false;
            planet.CameraFollow.canFollow = false;

            cameraMove.enabled = false;
            planet.CameraFollow.canFollow = true;
        }

    }


}
