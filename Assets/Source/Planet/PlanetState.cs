 using Agent;
using Enums.Tile;
using Mech;
using Vehicle;
using Projectile;
using FloatingText;
using Particle;
using Enums;
using Item;
using Inventory;
using KMath;
using UnityEngine;
using HUD;
using KGUI.Elements;

namespace Planet
{
    public struct PlanetState
    {
        public int Index;
        public TimeState TimeState;

        public PlanetTileMap.TileMap TileMap;
        public AgentList AgentList;
        public MechList MechList;
        public VehicleList VehicleList;
        public ProjectileList ProjectileList;
        public FloatingTextList FloatingTextList;
        public ParticleEmitterList ParticleEmitterList;
        public ParticleList ParticleList;
        public ItemParticleList ItemParticleList;
        public InventoryList InventoryList;
        public UIElementList UIElementList;
        public CameraFollow cameraFollow;

        public AgentEntity Player;

        public Contexts EntitasContext;

        public void Init(Vec2i mapSize)
        {
            TileMap = new PlanetTileMap.TileMap(mapSize);
            AgentList = new AgentList();
            MechList = new MechList();
            VehicleList = new VehicleList();
            ProjectileList = new ProjectileList();
            FloatingTextList = new FloatingTextList();
            ParticleEmitterList = new ParticleEmitterList();
            ParticleList = new ParticleList();
            ItemParticleList = new ItemParticleList();
            InventoryList = new InventoryList();
            UIElementList = new UIElementList();
            cameraFollow = new CameraFollow();

            EntitasContext = new Contexts();
        }

        public void InitializeSystems(Material material, Transform transform)
        {
            GameState.ActionInitializeSystem.Initialize(EntitasContext, material);
            GameState.PathFinding.Initialize();

            // Mesh builders
            GameState.TileMapRenderer.Initialize(material, transform, 7);
            GameState.AgentMeshBuilderSystem.Initialize(material, transform, 11);
            GameState.ItemMeshBuilderSystem.Initialize(material, transform, 12);
            GameState.ProjectileMeshBuilderSystem.Initialize(material, transform, 13);
            GameState.ParticleMeshBuilderSystem.Initialize(material, transform, 20);
            GameState.MechMeshBuilderSystem.Initialize(material, transform, 10);
            GameState.Renderer.Initialize(material);

        }

        public void InitializeTGen(Material material, Transform transform)
        {
            GameState.TGenRenderMapMesh.Initialize(material, transform, 8);
        }

        public void InitializeHUD(AgentEntity agentEntity)
        {
            // GUI/HUD
            HUDManager.Initialize(this, agentEntity);
        }

        // Note(Mahdi): Deprecated will be removed soon
        public AgentEntity AddPlayer(int spriteId, int width, int height, Vec2f position, int startingAnimation, 
            int health, int food, int water, int oxygen, int fuel)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            int inventoryID = AddInventory(GameState.InventoryCreationApi.GetDefaultPlayerInventoryModelID(), "Bag").inventoryID.ID;
            int equipmentInventoryID =
                AddInventory(GameState.InventoryCreationApi.GetDefaultRestrictionInventoryModelID()).inventoryID.ID;

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnPlayer(EntitasContext, spriteId, 
                width, height, position, startingAnimation, health, food, water, oxygen, fuel, 0.2f, inventoryID,
                equipmentInventoryID));

            Player = newEntity;

            return newEntity;
        }

        public AgentEntity AddPlayer(Vec2f position)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            int inventoryID = AddInventory(GameState.InventoryCreationApi.GetDefaultPlayerInventoryModelID()).inventoryID.ID;
            int equipmentInventoryID =
                AddInventory(GameState.InventoryCreationApi.GetDefaultRestrictionInventoryModelID()).inventoryID.ID;

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    Agent.AgentType.Player, inventoryID, equipmentInventoryID));

            Player = newEntity;

            return newEntity;
        }

        public AgentEntity AddAgent(Vec2f position)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position, Agent.AgentType.Agent));
            return newEntity;
        }

        public AgentEntity AddAgent(Vec2f position, Agent.AgentType type)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    type));
            return newEntity;
        }

        public AgentEntity AddCorpse(Vec2f position, int spriteId, Agent.AgentType agentType)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            int inventoryID = AddInventory(GameState.InventoryCreationApi.GetDefaultCorpseInventoryModelID(), "Corpse Bag").inventoryID.ID;
            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnCorpse(EntitasContext, position,
                    spriteId, agentType, inventoryID));
            return newEntity;
        }

        public MechEntity AddMech(Vec2f position, MechType mechType)
        {
            Utils.Assert(MechList.Length < PlanetEntityLimits.MechLimit);

            MechEntity newEntity = MechList.Add(GameState.MechSpawnerSystem.Spawn(EntitasContext, position, mechType));
            if (newEntity.hasMechInventory)
            {
                InventoryEntity inventory = EntitasContext.inventory.GetEntityWithInventoryID(newEntity.mechInventory.InventoryID);
                AddInventory(inventory);
            }

            return newEntity;
        }
        
        public void RemoveMech(int index)
        {
            MechEntity entity = MechList.Get(index);
            Utils.Assert(entity.isEnabled);
            MechList.Remove(index);
        }

        public InventoryEntity AddInventory(int inventoryModelID, string name = "")
        {
            InventoryEntity inventoryEntity = GameState.InventoryManager.CreateInventory(EntitasContext, inventoryModelID, name);
            AddInventory(inventoryEntity);
            return inventoryEntity;
        }

        public void AddInventory(InventoryEntity inventory)
        {
            Utils.Assert(InventoryList.Length < PlanetEntityLimits.InventoryLimits);
            InventoryList.Add(inventory);
        }

        public void RemoveInventory(int inventoryID)
        {
            // Spawn itemsInventory inside as item particles.
            InventoryEntity entity = InventoryList.Get(inventoryID);

            for (int i = 0; i < entity.inventoryEntity.Size; i++)
            {
                ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(EntitasContext, inventoryID, i);
                if (itemInventory == null)
                    continue;

                GameState.InventoryManager.RemoveItem(EntitasContext, inventoryID, i);
                itemInventory.Destroy();
            }

            Utils.Assert(entity.isEnabled);
            InventoryList.Remove(entity.inventoryEntity.Index);
        }

        /// <summary>
        /// Remove Items and Spawn itemsParticles.
        /// </summary>
        public void RemoveInventory(int inventoryID, Vec2f pos)
        {
            // Spawn itemsInventory inside as item particles.
            InventoryEntity entity = InventoryList.Get(inventoryID);

            for (int i = 0; i < entity.inventoryEntity.Size; i++)
            {
                ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(EntitasContext, inventoryID, i);
                if (itemInventory == null)
                    continue;

                GameState.InventoryManager.RemoveItem(EntitasContext, inventoryID, i);
                GameState.ItemSpawnSystem.SpawnItemParticle(EntitasContext, itemInventory, pos);
            }

            Utils.Assert(entity.isEnabled);
            InventoryList.Remove(entity.inventoryEntity.Index);
        }


        public MechEntity GetMechFromPosition(Vec2f position, MechType mechType)
        {
            foreach (var mech in MechList.List)
            {
                if(mech == null) break;
                if(mech.mechType.mechType != mechType) continue;
                
                var mechBox = new AABox2D(mech.mechPosition2D.Value, mech.mechSprite2D.Size);
                if (mechBox.OverlapPoint(position))
                {
                    return mech;
                }
            }

            return null;
        }

        public MechEntity GetMechFromPosition(Vec2f position)
        {
            foreach (var mech in MechList.List)
            {
                if (mech == null) break;

                var mechBox = new AABox2D(mech.mechPosition2D.Value, mech.mechSprite2D.Size);
                if (mechBox.OverlapPoint(position))
                {
                    return mech;
                }
            }

            return null;
        }

        public AgentEntity AddEnemy(Vec2f position)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position, Agent.AgentType.Enemy));
            return newEntity;
        }

        public UIElementEntity AddUIText(string text, float timeToLive, Vec2f position, Vec2f areaSize)
        {
            Utils.Assert(UIElementList.Size < PlanetEntityLimits.UIElementLimit);

            UIElementEntity newEntity = UIElementList.Add(GameState.ElementSpawnerSystem.SpawnText(EntitasContext.uIElement, text, timeToLive, position,
                    areaSize, -1, ElementType.Text));
            return newEntity;
        }

        public UIElementEntity AddUIText(string text, Vec2f position, Vec2f areaSize)
        {
            Utils.Assert(UIElementList.Size < PlanetEntityLimits.UIElementLimit);

            UIElementEntity newEntity = UIElementList.Add(GameState.ElementSpawnerSystem.SpawnText(EntitasContext.uIElement, text, position,
                    areaSize, -1, ElementType.Text));
            return newEntity;
        }

        public UIElementEntity AddUIImage(string Name, Transform Parent, Sprite Sprite,
            Vec2f position, Vec3f scale, UnityEngine.UI.Image.Type Type)
        {
            Utils.Assert(UIElementList.Size < PlanetEntityLimits.UIElementLimit);

            UIElementEntity newEntity = UIElementList.Add(GameState.ElementSpawnerSystem.SpawnImage(EntitasContext.uIElement, Name, Parent, Sprite,
                position, scale, Type, -1, ElementType.Image));
            return newEntity;
        }

        public UIElementEntity AddUIImage(string Name, Transform Parent, Sprite Sprite,
            Vec2f position, Vec3f scale, UnityEngine.UI.Image.Type Type, Color color)
        {
            Utils.Assert(UIElementList.Size < PlanetEntityLimits.UIElementLimit);

            UIElementEntity newEntity = UIElementList.Add(GameState.ElementSpawnerSystem.SpawnImage(EntitasContext.uIElement, Name, Parent, Sprite,
                position, scale, Type, color, -1, ElementType.Image));
            return newEntity;
        }

        public UIElementEntity AddUIImage(string Name, Transform Parent, string path,
            Vec2f position, Vec3f scale, int width, int height)
        {
            Utils.Assert(UIElementList.Size < PlanetEntityLimits.UIElementLimit);

            UIElementEntity newEntity = UIElementList.Add(GameState.ElementSpawnerSystem.SpawnImage(EntitasContext.uIElement, Name, Parent, path,
                position, scale, width, height, -1, ElementType.Image));
            return newEntity;
        }

        public UIElementEntity AddUIImage(string Name, Transform Parent, int tileSpriteID,
    Vec2f position, Vec3f scale, int width, int height)
        {
            Utils.Assert(UIElementList.Size < PlanetEntityLimits.UIElementLimit);

            UIElementEntity newEntity = UIElementList.Add(GameState.ElementSpawnerSystem.SpawnImage(EntitasContext.uIElement, Name, Parent, tileSpriteID,
                position, scale, width, height, -1, ElementType.Image));
            return newEntity;
        }

        public void KillAgent(int agentIndex)
        {
            AgentEntity entity = AgentList.Get(agentIndex);
            Utils.Assert(entity.isEnabled);

            entity.DieInPlace();
            AgentProperties properties = GameState.AgentCreationApi.Get((int)Agent.AgentType.Enemy);

            if (!entity.hasAgentInventory)
            {
                InventoryEntity inventoryEntity = AddInventory(GameState.InventoryCreationApi.GetDefaultCorpseInventoryModelID(), "Corpse Bag");
                entity.AddAgentInventory(inventoryEntity.inventoryID.ID, -1, false);
            }
            else if (entity.agentInventory.InventoryID == -1)
            {
                InventoryEntity inventoryEntity = AddInventory(GameState.InventoryCreationApi.GetDefaultCorpseInventoryModelID(), "Corpse Bag");
                entity.agentInventory.InventoryID = inventoryEntity.inventoryID.ID;
            }

            GameState.LootDropSystem.Add(properties.DropTableID, entity.agentPhysicsState.Position);
            GameState.LootDropSystem.Add(properties.InventoryDropTableID, entity.agentInventory.InventoryID);
        }
        

        /*public void RemoveAgent(int agentIndex)
=======
        public UIElementEntity AddUIImage(string Name, Transform Parent, int tileSpriteID,
    Vec2f position, Vec3f scale, int width, int height)
>>>>>>> 9c351b67fac5c622abd4a72b183f8535d1419ec9
        {
            Utils.Assert(UIElementList.Size < PlanetEntityLimits.UIElementLimit);

            UIElementEntity newEntity = UIElementList.Add(GameState.ElementSpawnerSystem.SpawnImage(EntitasContext.uIElement, Name, Parent, tileSpriteID,
                position, scale, width, height, -1, ElementType.Image));
            return newEntity;
        }


        public void RemoveAgent(int agentId)
        {
            AgentEntity entity = AgentList.Get(agentId);
            Utils.Assert(entity.isEnabled);

            var physicsState = entity.agentPhysicsState;
            Vec2f agentPosition = physicsState.Position;


            if (entity.agentID.Type == Agent.AgentType.Enemy)
            {
                AgentEntity corpse = AddCorpse(agentPosition, GameResources.DeadSlimeSpriteId, Agent.AgentType.Enemy);
                AgentProperties properties = GameState.AgentCreationApi.Get((int)Agent.AgentType.Enemy);

                int inventoryID = corpse.agentInventory.InventoryID;

                GameState.LootDropSystem.Add(properties.DropTableID, corpse.agentPhysicsState.Position);
                GameState.LootDropSystem.Add(properties.InventoryDropTableID, inventoryID);
            }

            AgentList.Remove(agentId);

        }*/

        public FloatingTextEntity AddFloatingText(string text, float timeToLive, Vec2f velocity, Vec2f position)
        {
            FloatingTextEntity newEntity = FloatingTextList.Add(GameState.FloatingTextSpawnerSystem.SpawnFloatingText
                (EntitasContext.floatingText, text, timeToLive, velocity, position));
            return newEntity;
        }

        public void RemoveFloatingText(int index)
        {
            FloatingTextEntity entity = FloatingTextList.Get(index);
            Utils.Assert(entity.isEnabled);
            GameObject.Destroy(entity.floatingTextSprite.GameObject);
            FloatingTextList.Remove(index);
        }

        public ParticleEntity AddParticleEmitter(Vec2f position, Particle.ParticleEmitterType type)
        {
            ParticleEntity newEntity = ParticleEmitterList.Add(GameState.ParticleEmitterSpawnerSystem.Spawn(
                EntitasContext.particle, type, position));
            return newEntity;
        }

        public void RemoveParticleEmitter(int index)
        {
            ParticleEntity entity = ParticleEmitterList.Get(index);
            Utils.Assert(entity.isEnabled);
            ParticleEmitterList.Remove(entity.particleEmitterID.Index);
        }


        public ParticleEntity AddParticle(Vec2f position, Vec2f velocity, Particle.ParticleType type)
        {
            Utils.Assert(ParticleList.Length < PlanetEntityLimits.ParticleLimit);

            ParticleEntity newEntity = ParticleList.Add(GameState.ParticleSpawnerSystem.Spawn(
                EntitasContext.particle, type, position, velocity));
            return newEntity;
        }

        public void AddDebris(Vec2f position, int spriteId, float spriteWidth, float spriteHeight)
        {
            Utils.Assert(ParticleList.Length + 5 < PlanetEntityLimits.ParticleLimit);

            GameState.ParticleSpawnerSystem.SpawnSpriteDebris(this, position, spriteId, spriteWidth, spriteHeight);
        }

        public void RemoveParticle(int index)
        {
            ParticleList.Remove(index);
        }

        public ProjectileEntity AddProjectile(Vec2f position, Vec2f direction, Enums.ProjectileType projectileType)
        {
            Utils.Assert(ProjectileList.Length < PlanetEntityLimits.ProjectileLimit);
            ProjectileEntity newEntity = ProjectileList.Add(GameState.ProjectileSpawnerSystem.Spawn(EntitasContext.projectile,
                         position, direction, projectileType));
            return newEntity;
        }

        public void RemoveProjectile(int index)
        {
            ProjectileEntity entity = ProjectileList.Get(index);
            Utils.Assert(entity.isEnabled);
            ProjectileList.Remove(entity.projectileID.Index);
        }

        public VehicleEntity AddVehicle(UnityEngine.Material material, Vector2 position)
        {
            Utils.Assert(VehicleList.Length < PlanetEntityLimits.VehicleLimit);

            VehicleEntity newEntity = VehicleList.Add(new VehicleEntity());
            return newEntity;
        }

        public void RemoveVehicle(int index)
        {
            VehicleList.Remove(index);
        }

        public ItemParticleEntity AddItemParticle(Vec2f position, ItemType itemType)
        {
            Utils.Assert(ItemParticleList.Length < PlanetEntityLimits.ItemParticlesLimit);

            ItemParticleEntity newEntity = ItemParticleList.Add(GameState.ItemSpawnSystem.SpawnItemParticle(EntitasContext, itemType, position));
            return newEntity;
        }

        public void RemoveItemParticle(int index)
        {
            ItemParticleList.Remove(index);

        }

        public void RemoveUIElement(int elementID)
        {
            UIElementList.Remove(elementID);
        }

        // updates the entities, must call the systems and so on ..
        public void Update(float deltaTime, Material material, Transform transform)
        {
            float targetFps = 30.0f;
            float frameTime = 1.0f / targetFps;

            /*TimeState.Deficit += deltaTime;

            while (TimeState.Deficit >= frameTime)
            {
                TimeState.Deficit -= frameTime;
                // do a server/client tick right here
                {
                    TimeState.TickTime++;


                }

            }*/

            // check if the sprite atlas teSetTilextures needs to be updated
            for(int type = 0; type < GameState.SpriteAtlasManager.Length; type++)
            {
                GameState.SpriteAtlasManager.UpdateAtlasTexture(type);
            }

            // check if the tile sprite atlas textures needs to be updated
            for(int type = 0; type < GameState.TileSpriteAtlasManager.Length; type++)
            {
                GameState.TileSpriteAtlasManager.UpdateAtlasTexture(type);
            }

            // calling all the systems we have

            GameState.InputProcessSystem.Update(ref this);
            GameState.AgentProcessPhysicalState.Update(ref this, frameTime);
            GameState.AgentMovementSystem.Update(EntitasContext.agent);
            GameState.ItemMovableSystem.Update(EntitasContext.itemParticle);
            GameState.AgentProcessCollisionSystem.Update(EntitasContext.agent, ref TileMap);
            GameState.AgentModel3DMovementSystem.Update(EntitasContext.agent);
            GameState.AgentModel3DAnimationSystem.Update(EntitasContext.agent);
            GameState.ItemProcessCollisionSystem.Update(EntitasContext.itemParticle, ref TileMap);
            GameState.EnemyAiSystem.Update(ref this, frameTime);
            GameState.LootDropSystem.Update(EntitasContext);
            GameState.FloatingTextUpdateSystem.Update(ref this, frameTime);
            GameState.AnimationUpdateSystem.Update(EntitasContext, frameTime);
            GameState.ItemPickUpSystem.Update(EntitasContext);
            GameState.ActionSchedulerSystem.Update(EntitasContext, frameTime, ref this);
            GameState.ActionCoolDownSystem.Update(EntitasContext, deltaTime);
            GameState.ParticleEmitterUpdateSystem.Update(ref this);
            GameState.ParticleUpdateSystem.Update(ref this, EntitasContext.particle);
            GameState.ParticleProcessCollisionSystem.Update(EntitasContext.particle, ref TileMap);
            GameState.ProjectileMovementSystem.Update(EntitasContext.projectile);
            GameState.ProjectileCollisionSystem.UpdateEx(ref this);
            cameraFollow.Update(ref this);

            TileMap.UpdateTileSprites();

            if (GameState.TGenGrid != null)
            {
                GameState.TGenGrid.Update();
                GameState.TGenRenderMapMesh.UpdateMesh(GameState.TGenGrid);
                GameState.TGenRenderMapMesh.Draw();
            }

            // Update Meshes.
            GameState.TileMapRenderer.UpdateBackLayerMesh(TileMap);
            GameState.TileMapRenderer.UpdateMidLayerMesh(TileMap);
            GameState.TileMapRenderer.UpdateFrontLayerMesh(TileMap);
            GameState.ItemMeshBuilderSystem.UpdateMesh(EntitasContext);
            GameState.AgentMeshBuilderSystem.UpdateMesh(EntitasContext.agent);
            GameState.ProjectileMeshBuilderSystem.UpdateMesh(EntitasContext.projectile);
            GameState.ParticleMeshBuilderSystem.UpdateMesh(EntitasContext.particle);
            GameState.MechMeshBuilderSystem.UpdateMesh(EntitasContext.mech);

            // Draw Frames.
            GameState.TileMapRenderer.DrawLayer(MapLayerType.Back);
            GameState.TileMapRenderer.DrawLayer(MapLayerType.Mid);
            GameState.TileMapRenderer.DrawLayer(MapLayerType.Front);
            GameState.Renderer.DrawFrame(ref GameState.ItemMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle));
            GameState.Renderer.DrawFrame(ref GameState.AgentMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Agent));
            GameState.Renderer.DrawFrame(ref GameState.ProjectileMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle));
            GameState.Renderer.DrawFrame(ref GameState.ParticleMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle));
            GameState.Renderer.DrawFrame(ref GameState.MechMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Mech));

            GameState.FloatingTextDrawSystem.Draw(EntitasContext.floatingText, transform, 10000);
        }

        public void DrawHUD(AgentEntity agentEntity)
        {
            if(HUDManager.ShowGUI != null)
            {
                if(HUDManager.ShowGUI)
                {
                    HUDManager.Update(agentEntity);
                    HUDManager.Draw();

                    GameState.ElementUpdateSystem.Update(ref this, Time.deltaTime);
                    GameState.ElementDrawSystem.Draw(EntitasContext.uIElement);
                }
            }
        }

        public void DrawHUD()
        {
            if (HUDManager.ShowGUI != null)
            {
                if (HUDManager.ShowGUI)
                {
                    GameState.ElementUpdateSystem.Update(ref this, Time.deltaTime);
                    GameState.ElementDrawSystem.Draw(EntitasContext.uIElement);
                }
            }
        }
    }
}
