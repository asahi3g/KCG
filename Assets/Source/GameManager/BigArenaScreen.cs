using KMath;

namespace GameScreen
{



    public class BigArenaScreen : GameScreen
    {

        
        UnityEngine.Material Material;
        UnityEngine.Transform Transform;


        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;



        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();

            UnityEngine.Vector3 p = UnityEngine.Input.mousePosition;
            p.z = 20;
            UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(p);

            GameState.Planet.Update(UnityEngine.Time.deltaTime);



            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F1))
            {
                Planet.PlanetManager.Save(GameState.Planet, "generated-maps/big-arena-map.kmap");
                UnityEngine.Debug.Log("saved!");
            }

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F2))
            {
                GameState.Planet.AddDebris(new Vec2f(120.0f, 15.0f), GameState.ItemCreationApi.ChestIconParticle, 1.5f, 1.0f);
                UnityEngine.Debug.Log("spawned ! ");
            }

            /*if (Input.GetKeyDown(KeyCode.F2))
            {
                var camera = Camera.main;
                Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

                planet.Destroy();
                planet = PlanetManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);


               planet.InitializeSystems(Material, transform);

               Player = planet.AddPlayer(new Vec2f(3.0f, 20));
               PlayerID = Player.agentID.ID;
               inventoryID = Player.agentInventory.InventoryID;

               AddItemsToPlayer();

                Debug.Log("loaded!");
            }*/
        }

        public override void OnGui()
        {
            GameState.Planet.DrawHUD(Player);
        }

        public override void Init(UnityEngine.Transform sceneTransform)
        {
            Transform = sceneTransform;
            Initialize(Transform);
        }

        public override void LoadResources()
        {
            base.LoadResources();
        }

        public override void UnloadResources()
        {
            base.UnloadResources();
        }


         public void Initialize(UnityEngine.Transform transform)
        {
            Transform = transform;
            Material = UnityEngine.Resources.Load("Materials/TextureMaterial") as UnityEngine.Material;
            Tiled.TiledMap tileMap = Tiled.TiledMap.FromJson("generated-maps/big-arena.tmj", "generated-maps/");

            int materialCount = System.Enum.GetNames(typeof(Enums.MaterialType)).Length;
            int geometryTilesCount = System.Enum.GetNames(typeof(Enums.TileGeometryAndRotation)).Length;

            PlanetTileMap.TileProperty[][] MaterialGeometryMap = new PlanetTileMap.TileProperty[materialCount][];
            for(int i = 0; i < materialCount; i++)
            {
                MaterialGeometryMap[i] = new PlanetTileMap.TileProperty[geometryTilesCount];
            }


           

            UnityEngine.Application.targetFrameRate = 60;

            GameResources.Initialize();

             for(int i = 0; i < GameState.TileCreationApi.TilePropertyArray.Length; i++)
            {
                ref PlanetTileMap.TileProperty property = ref GameState.TileCreationApi.TilePropertyArray[i];

                MaterialGeometryMap[(int)property.MaterialType][(int)property.BlockShapeType] = property;

            }

            // Generating the map
            int mapWidth = tileMap.width;
            int mapHeight = tileMap.height;

            mapWidth = ((mapWidth + 16 - 1) / 16) * 16; // multiple of 16
            mapHeight = ((mapHeight + 16 - 1) / 16) * 16; // multiple of 16
            
            Vec2i mapSize = new Vec2i(mapWidth, mapHeight);
            GameState.Planet.Init(mapSize);

            int PlayerFaction = 0;
            int EnemyFaction = 1;

            Player = GameState.Planet.AddAgentAsPlayer(new Vec2f(120.0f, 15.0f), PlayerFaction);
            PlayerID = Player.agentID.ID;

            PlayerID = Player.agentID.ID;
            inventoryID = Player.agentInventory.InventoryID;

            GameState.Planet.InitializeSystems(Material, transform);
            //GenerateMap();
            var camera = UnityEngine.Camera.main;
            UnityEngine.Vector3 lookAtPosition = camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, camera.nearClipPlane));

            for(int j = 0; j < tileMap.height; j++)
            {
                for(int i = 0; i < tileMap.width; i++)
                {
                    int tileIndex = tileMap.layers[0].data[i + ((tileMap.height - 1) - j) * tileMap.width] - 1;
                    if (tileIndex >= 0)
                    {
                        Tiled.TiledMaterialAndShape tileMaterialAndShape = tileMap.GetTile(tileIndex);

                        Enums.PlanetTileMap.TileID tileID = MaterialGeometryMap[(int)tileMaterialAndShape.Material][(int)tileMaterialAndShape.Shape].TileID;

                        GameState.Planet.TileMap.GetTile(i, j).FrontTileID = tileID;
                    }
                }
            }


            GameState.Planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            GameState.Planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            GameState.Planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);

            AddItemsToPlayer();

            PlanetTileMap.TileMapGeometry.BuildGeometry(GameState.Planet.TileMap);

            UpdateMode(Player);
        }

        public void AddItemsToPlayer()
        {
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.PlacementTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveTileTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemyGunnerTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemySwordmanTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ConstructionTool);
            //Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveMech);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ParticleEmitterPlacementTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.HealthPotion);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SMG);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.GeometryPlacementTool);
        }

        private void UpdateMode(AgentEntity agentEntity)
        {
            ref var planet = ref GameState.Planet;
            agentEntity.agentPhysicsState.Invulnerable = false;
            UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planet.CameraFollow.canFollow = false;

            UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planet.CameraFollow.canFollow = true;
        }
    }
}