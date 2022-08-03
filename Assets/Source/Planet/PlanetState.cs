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
            cameraFollow = new CameraFollow();

            EntitasContext = new Contexts();
        }

        public void InitializeSystems(Material material, Transform transform)
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

        // Note(Mahdi): Deprecated will be removed soon
        public AgentEntity AddAgent(int spriteId, int width,
                     int height, Vec2f position, int startingAnimation)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnAgent(EntitasContext, 
                spriteId, width, height, position, -1, startingAnimation));
            return newEntity;
        }

        public AgentEntity AddAgent(Vec2f position)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    -1, Agent.AgentType.Agent));
            return newEntity;
        }

        public MechEntity AddMech(Vec2f position, MechType mechType)
        {
            Utils.Assert(MechList.Length < PlanetEntityLimits.MechLimit);

            MechEntity newEntity = MechList.Add(GameState.MechSpawnerSystem.Spawn(EntitasContext, position, -1, mechType));
            return newEntity;
        }

        // Note(Mahdi): Deprecated will be removed soon
        public AgentEntity AddEnemy(int spriteId,
                        int width, int height, Vec2f position, int startingAnimation)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnEnemy(spriteId, width, height, 
                position, -1, startingAnimation));
            return newEntity;
        }

        public AgentEntity AddEnemy(Vec2f position)
        {
            Utils.Assert(AgentList.Length < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    -1, Agent.AgentType.Enemy));
            return newEntity;
        }

        public void RemoveAgent(int agentId)
        {
            AgentEntity entity = AgentList.Get(agentId);
            Utils.Assert(entity.isEnabled);
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

            TileMap.UpdateTileSprites();
            
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
    }
}
