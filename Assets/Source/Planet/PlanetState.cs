 using Agent;
using Enums.Tile;
using Mech;
using Vehicle;
using Projectile;
using FloatingText;
using Particle;
using Enums;
using Item;
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
        public UIElementList UIElementList;
        public CameraFollow cameraFollow;

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
            UIElementList = new UIElementList();
            cameraFollow = new CameraFollow();

            EntitasContext = new Contexts();
        }

        public void InitializeSystems(Material material, Transform transform, AgentEntity agentEntity)
        {
            GameState.ActionInitializeSystem.Initialize(EntitasContext, material);


            // Mesh builders
            GameState.TileMapRenderer.Initialize(material, transform, 7);
            GameState.ItemMeshBuilderSystem.Initialize(material, transform, 11);
            GameState.AgentMeshBuilderSystem.Initialize(material, transform, 12);
            GameState.ProjectileMeshBuilderSystem.Initialize(material, transform, 13);
            GameState.ParticleMeshBuilderSystem.Initialize(material, transform, 20);
            GameState.MechMeshBuilderSystem.Initialize(material, transform, 10);
            GameState.Renderer.Initialize(material);

            // GUI/HUD
            GameState.HUDManager.Initialize(this, agentEntity);
        }


        // Note(Mahdi): Deprecated will be removed soon
        public AgentEntity AddPlayer(int spriteId, int width, int height, Vec2f position, int startingAnimation, 
            int health, int food, int water, int oxygen, int fuel)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnPlayer(EntitasContext, spriteId, 
                width, height, position, -1, startingAnimation, health, food, water, oxygen, fuel, 0.2f));
            return newEntity;
        }

        public AgentEntity AddPlayer(Vec2f position)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    -1, Agent.AgentType.Player));
            return newEntity;
        }

        public AgentEntity AddAgent(Vec2f position)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    -1, Agent.AgentType.Agent));
            return newEntity;
        }

        public AgentEntity AddCorpse(Vec2f position, int spriteId, Agent.AgentType agentType)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnCorpse(EntitasContext, position,
                    -1, spriteId, agentType));
            return newEntity;
        }

        public MechEntity AddMech(Vec2f position, MechType mechType)
        {
            Utils.Assert(MechList.Length < PlanetEntityLimits.MechLimit);

            MechEntity newEntity = MechList.Add(GameState.MechSpawnerSystem.Spawn(EntitasContext, position, -1, mechType));
            return newEntity;
        }
        
        public void RemoveMech(int mechId)
        {
            MechEntity entity = MechList.Get(mechId);
            Utils.Assert(entity.isEnabled);
            MechList.Remove(mechId);
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


        public AgentEntity AddEnemy(Vec2f position)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    -1, Agent.AgentType.Enemy));
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

        public void RemoveAgent(int agentId)
        {
            AgentEntity entity = AgentList.Get(agentId);
            Utils.Assert(entity.isEnabled);

            var pos = entity.agentPosition2D;
            Vec2f agentPosition = pos.Value;

            AgentEntity corpse = AddCorpse(agentPosition, GameResources.DeadSlimeSpriteId, Agent.AgentType.Enemy);

            if (entity.hasAgentItemDrop)
            {
                var itemDrop = entity.agentItemDrop;
                int inventoryID = corpse.agentInventory.InventoryID;
                if (itemDrop.Drops != null)
                {
                    
                    for(int dropIndex = 0; dropIndex < itemDrop.Drops.Length; dropIndex++)
                    {
                        Enums.ItemType dropType = itemDrop.Drops[dropIndex];
                        int maxDropCount = itemDrop.MaxDropCount[dropIndex];
                        float dropRate = itemDrop.DropRate[dropIndex];

                        
                        
                        float randXOffset = KMath.Random.Mt19937.genrand_realf() * 0.5f;

                        
                        Utils.Assert(maxDropCount < 100 && maxDropCount > 0);
                        int currentDrop = 0;
                        while (currentDrop < maxDropCount)
                        {
                            float randomDrop = KMath.Random.Mt19937.genrand_realf();
                            if (randomDrop <= dropRate)
                            {
                                GameState.ItemSpawnSystem.SpawnItemParticle(EntitasContext, dropType, pos.Value + new Vec2f(randXOffset, 0.5f));
                                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, dropType, EntitasContext);
                            }

                            currentDrop++;
                        }
 
                    }
                    
                }
            }

            AgentList.Remove(agentId);

            
        }

        public FloatingTextEntity AddFloatingText(string text, float timeToLive, Vec2f velocity, Vec2f position)
        {
            FloatingTextEntity newEntity = FloatingTextList.Add(GameState.FloatingTextSpawnerSystem.SpawnFloatingText
                (EntitasContext.floatingText, text, timeToLive, velocity, position, -1));
            return newEntity;
        }

        public void RemoveFloatingText(int floatingTextId)
        {
            FloatingTextEntity entity = FloatingTextList.Get(floatingTextId);
            Utils.Assert(entity.isEnabled);
            GameObject.Destroy(entity.floatingTextSprite.GameObject);
            FloatingTextList.Remove(floatingTextId);
        }

        public ParticleEntity AddParticleEmitter(Vec2f position, Particle.ParticleEmitterType type)
        {
            ParticleEntity newEntity = ParticleEmitterList.Add(GameState.ParticleEmitterSpawnerSystem.Spawn(
                EntitasContext.particle, type, position, -1));
            return newEntity;
        }

        public void RemoveParticleEmitter(int particleEmitterId)
        {
            ParticleEntity entity = ParticleEmitterList.Get(particleEmitterId);
            Utils.Assert(entity.isEnabled);
            ParticleEmitterList.Remove(entity.particleEmitterID.ParticleEmitterId);
        }


        public ParticleEntity AddParticle(Vec2f position, Vec2f velocity, Particle.ParticleType type)
        {
            Utils.Assert(ParticleList.Length < PlanetEntityLimits.ParticleLimit);

            ParticleEntity newEntity = ParticleList.Add(GameState.ParticleSpawnerSystem.Spawn(
                EntitasContext.particle, type, position, velocity, -1));
            return newEntity;
        }

        public void AddDebris(Vec2f position, int spriteId, float spriteWidth, float spriteHeight)
        {
            Utils.Assert(ParticleList.Length + 5 < PlanetEntityLimits.ParticleLimit);

            GameState.ParticleSpawnerSystem.SpawnSpriteDebris(this, position, spriteId, spriteWidth, spriteHeight);
        }

        public void RemoveParticle(int particleId)
        {
            ParticleList.Remove(particleId);
        }

        public ProjectileEntity AddProjectile(Vec2f position, Vec2f direction, Enums.ProjectileType projectileType)
        {
            Utils.Assert(ProjectileList.Length < PlanetEntityLimits.ProjectileLimit);
            ProjectileEntity newEntity = ProjectileList.Add(GameState.ProjectileSpawnerSystem.Spawn(EntitasContext.projectile,
                         position, direction, projectileType, -1));
            return newEntity;
        }

        public void RemoveProjectile(int projectileId)
        {
            ProjectileEntity entity = ProjectileList.Get(projectileId);
            Utils.Assert(entity.isEnabled);
            ProjectileList.Remove(entity.projectileID.ID);
        }

        public VehicleEntity AddVehicle(UnityEngine.Material material, Vector2 position)
        {
            Utils.Assert(VehicleList.Length < PlanetEntityLimits.VehicleLimit);

            VehicleEntity newEntity = VehicleList.Add(new VehicleEntity());
            return newEntity;
        }

        public void RemoveVehicle(int vehicleId)
        {
            VehicleList.Remove(vehicleId);
        }

        public ItemParticleEntity AddItemParticle(Vec2f position, ItemType itemType)
        {
            Utils.Assert(ItemParticleList.Length < PlanetEntityLimits.ItemParticlesLimit);

            ItemParticleEntity newEntity = ItemParticleList.Add(GameState.ItemSpawnSystem.SpawnItemParticle(EntitasContext, itemType, position));
            return newEntity;
        }

        public void RemoveItemParticle(int itemParticleId)
        {
            ItemParticleList.Remove(itemParticleId);

        }

        public void RemoveUIElement(int elementID)
        {
            UIElementList.Remove(elementID);
        }

        // updates the entities, must call the systems and so on ..
        public void Update(float deltaTime, Material material, Transform transform, AgentEntity agentEntity)
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
            GameState.AgentMovableSystem.Update(EntitasContext.agent);
            GameState.ItemMovableSystem.Update(EntitasContext.itemParticle);
            GameState.AgentProcessCollisionSystem.Update(EntitasContext.agent, ref TileMap);
            GameState.ItemProcessCollisionSystem.Update(EntitasContext.itemParticle, ref TileMap);
            GameState.EnemyAiSystem.Update(ref this);
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

            GameState.HUDManager.Update(agentEntity);

            TileMap.UpdateTileSprites();

            if (GameState.TGenGrid != null)
            {
                GameState.TGenGrid.Update();
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
            GameState.ElementUpdateSystem.Update(ref this, Time.deltaTime);

            // Draw Frames.
            GameState.TileMapRenderer.DrawLayer(MapLayerType.Back);
            GameState.TileMapRenderer.DrawLayer(MapLayerType.Mid);
            GameState.TileMapRenderer.DrawLayer(MapLayerType.Front);
            GameState.Renderer.DrawFrame(ref GameState.ItemMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle));
            GameState.Renderer.DrawFrame(ref GameState.AgentMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Agent));
            GameState.Renderer.DrawFrame(ref GameState.ProjectileMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle));
            GameState.Renderer.DrawFrame(ref GameState.ParticleMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle));
            GameState.Renderer.DrawFrame(ref GameState.MechMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Mech));
            GameState.HUDManager.Draw();
            GameState.ElementDrawSystem.Draw(EntitasContext.uIElement);

            GameState.FloatingTextDrawSystem.Draw(EntitasContext.floatingText, transform, 10000);
        }
    }
}
