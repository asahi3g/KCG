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
            Utils.Assert(AgentList.Size < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnPlayer(EntitasContext, spriteId, 
                width, height, position, -1, startingAnimation, health, food, water, oxygen, fuel, 0.2f));
            return newEntity;
        }

        public AgentEntity AddPlayer(Vec2f position)
        {
            Utils.Assert(AgentList.Size < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    -1, Agent.AgentType.Player));
            return newEntity;
        }

        // Note(Mahdi): Deprecated will be removed soon
        public AgentEntity AddAgent(int spriteId, int width,
                     int height, Vec2f position, int startingAnimation)
        {
            Utils.Assert(AgentList.Size < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnAgent(EntitasContext, 
                spriteId, width, height, position, -1, startingAnimation));
            return newEntity;
        }

        public AgentEntity AddAgent(Vec2f position)
        {
            Utils.Assert(AgentList.Size < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.Spawn(EntitasContext, position,
                    -1, Agent.AgentType.Agent));
            return newEntity;
        }

        public MechEntity AddMech(Vec2f position, MechType mechType)
        {
            Utils.Assert(MechList.Size < PlanetEntityLimits.MechLimit);

            MechEntity newEntity = MechList.Add(GameState.MechSpawnerSystem.Spawn(EntitasContext, position, -1, mechType));
            return newEntity;
        }

        // Note(Mahdi): Deprecated will be removed soon
        public AgentEntity AddEnemy(int spriteId,
                        int width, int height, Vec2f position, int startingAnimation)
        {
            Utils.Assert(AgentList.Size < PlanetEntityLimits.AgentLimit);

            AgentEntity newEntity = AgentList.Add(GameState.AgentSpawnerSystem.SpawnEnemy(spriteId, width, height, 
                position, -1, startingAnimation));
            return newEntity;
        }

        public AgentEntity AddEnemy(Vec2f position)
        {
            Utils.Assert(AgentList.Size < PlanetEntityLimits.AgentLimit);

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
            Utils.Assert(ParticleList.Size < PlanetEntityLimits.ParticleLimit);

            ParticleEntity newEntity = ParticleList.Add(GameState.ParticleSpawnerSystem.Spawn(
                EntitasContext.particle, type, position, velocity, -1));
            return newEntity;
        }

        public ParticleEntity AddDebris(Vec2f position)
        {
            Utils.Assert(ParticleList.Size < PlanetEntityLimits.ParticleLimit);

            Vector4 spriteCoords = GameState.SpriteAtlasManager.GetSprite(2, Enums.AtlasType.Particle).TextureCoords;
            float x = spriteCoords.x;
            float y = spriteCoords.y;
            float width = spriteCoords.z;
            float height = spriteCoords.w;

            Vec2f[] part1Vertices = new Vec2f[12];
            part1Vertices[0] = new Vec2f(0.0f, 0.0f);
            part1Vertices[1] = new Vec2f(0.73f, 0.0f);
            part1Vertices[2] = new Vec2f(0.66f, 0.26f);

            part1Vertices[3] = new Vec2f(0.0f, 0.0f);
            part1Vertices[4] = new Vec2f(0.66f, 0.26f);
            part1Vertices[5] = new Vec2f(0.5f, 0.4f);

            part1Vertices[6] = new Vec2f(0.0f, 0.0f);
            part1Vertices[7] = new Vec2f(0.5f, 0.4f);
            part1Vertices[8] = new Vec2f(0.5f, 0.5f);

            part1Vertices[9] = new Vec2f(0.0f, 0.0f);
            part1Vertices[10] = new Vec2f(0.5f, 0.5f);
            part1Vertices[11] = new Vec2f(0.0f, 0.4f);


            Vec2f[] part1Coords = new Vec2f[12];
            part1Coords[0] = new Vec2f(x + 0.0f * width, y + 0.0f * height);
            part1Coords[1] = new Vec2f(x + 0.73f * width, y + 0.0f * height);
            part1Coords[2] = new Vec2f(x + 0.66f * width, y + 0.26f * height);

            part1Coords[3] = new Vec2f(x + 0.0f * width, y + 0.0f * height);
            part1Coords[4] = new Vec2f(x + 0.66f * width, y + 0.26f * height);
            part1Coords[5] = new Vec2f(x + 0.5f * width, y + 0.4f * height);

            part1Coords[6] = new Vec2f(x + 0.0f * width, y + 0.0f * height);
            part1Coords[7] = new Vec2f(x + 0.5f * width, y + 0.4f * height);
            part1Coords[8] = new Vec2f(x + 0.5f * width, y + 0.5f * height);

            part1Coords[9] = new Vec2f(x + 0.0f * width, y + 0.0f * height);
            part1Coords[10] = new Vec2f(x + 0.5f * width, y + 0.5f * height);
            part1Coords[11] = new Vec2f(x + 0.0f * width, y + 0.4f * height);



            Vec2f[] part2Vertices = new Vec2f[12];
            part2Vertices[0] = new Vec2f(1.0f, 0.0f);
            part2Vertices[1] = new Vec2f(1.0f, 1.0f);
            part2Vertices[2] = new Vec2f(0.83f, 0.66f);

            part2Vertices[3] = new Vec2f(1.0f, 0.0f);
            part2Vertices[4] = new Vec2f(0.83f, 0.66f);
            part2Vertices[5] = new Vec2f(0.83f, 0.33f);

            part2Vertices[6] = new Vec2f(1.0f, 0.0f);
            part2Vertices[7] = new Vec2f(0.83f, 0.33f);
            part2Vertices[8] = new Vec2f(0.66f, 0.26f);

            part2Vertices[9] = new Vec2f(1.0f, 0.0f);
            part2Vertices[10] = new Vec2f(0.66f, 0.26f);
            part2Vertices[11] = new Vec2f(0.73f, 0.0f);


            Vec2f[] part2Coords = new Vec2f[12];
            part2Coords[0] = new Vec2f(x + 1.0f * width, y + 0.0f * height);
            part2Coords[1] = new Vec2f(x + 1.0f * width, y + 1.0f * height);
            part2Coords[2] = new Vec2f(x + 0.83f * width, y + 0.66f * height);

            part2Coords[3] = new Vec2f(x + 1.0f * width, y + 0.0f * height);
            part2Coords[4] = new Vec2f(x + 0.83f * width, y + 0.66f * height);
            part2Coords[5] = new Vec2f(x + 0.83f * width, y + 0.33f * height);

            part2Coords[6] = new Vec2f(x + 1.0f * width, y + 0.0f * height);
            part2Coords[7] = new Vec2f(x + 0.83f * width, y + 0.33f * height);
            part2Coords[8] = new Vec2f(x + 0.66f * width, y + 0.26f * height);

            part2Coords[9] = new Vec2f(x + 1.0f * width, y + 0.0f * height);
            part2Coords[10] = new Vec2f(x + 0.66f * width, y + 0.26f * height);
            part2Coords[11] = new Vec2f(x + 0.73f * width, y + 0.0f * height);




            Vec2f[] part3Vertices = new Vec2f[9];
            part3Vertices[0] = new Vec2f(0.66f, 0.0f);
            part3Vertices[1] = new Vec2f(0.83f, 0.7f);
            part3Vertices[2] = new Vec2f(0.83f, 0.4f);
            part3Vertices[3] = new Vec2f(0.66f, 0.0f);
            part3Vertices[4] = new Vec2f(0.83f, 0.4f);
            part3Vertices[5] = new Vec2f(0.5f, 0.4f);
            part3Vertices[6] = new Vec2f(0.66f, 0.0f);
            part3Vertices[7] = new Vec2f(0.5f, 0.4f);
            part3Vertices[8] = new Vec2f(0.5f, 0.14f);


            Vec2f[] part3Coords = new Vec2f[9];
            part3Coords[0] = new Vec2f(x + 0.66f * width, y + 0.26f * height);
            part3Coords[1] = new Vec2f(x + 0.83f * width, y + 0.33f * height);
            part3Coords[2] = new Vec2f(x + 0.83f * width, y + 0.66f * height);
            part3Coords[3] = new Vec2f(x + 0.66f * width, y + 0.26f * height);
            part3Coords[4] = new Vec2f(x + 0.83f * width, y + 0.66f * height);
            part3Coords[5] = new Vec2f(x + 0.5f * width, y + 0.66f * height);
            part3Coords[6] = new Vec2f(x + 0.66f * width, y + 0.26f * height);
            part3Coords[7] = new Vec2f(x + 0.5f * width, y + 0.66f * height);
            part3Coords[8] = new Vec2f(x + 0.5f * width, y + 0.4f * height);




            Vec2f[] part4Vertices = new Vec2f[6];
            part4Vertices[0] = new Vec2f(0.5f, 0.0f);
            part4Vertices[1] = new Vec2f(0.83f, 0.0f);
            part4Vertices[2] = new Vec2f(1.0f, 0.33f);

            part4Vertices[3] = new Vec2f(0.5f, 0.0f);
            part4Vertices[4] = new Vec2f(1.0f, 0.33f);
            part4Vertices[5] = new Vec2f(0.0f, 0.33f);


            Vec2f[] part4Coords = new Vec2f[6];
            part4Coords[0] = new Vec2f(x + 0.5f * width, y + 0.66f * height);
            part4Coords[1] = new Vec2f(x + 0.83f * width, y + 0.66f * height);
            part4Coords[2] = new Vec2f(x + 1.0f * width, y + 1.0f * height);

            part4Coords[3] = new Vec2f(x + 0.5f * width, y + 0.66f * height);
            part4Coords[4] = new Vec2f(x + 1.0f * width, y + 1.0f * height);
            part4Coords[5] = new Vec2f(x + 0.0f * width, y + 1.0f * height);



            Vec2f[] part5Vertices = new Vec2f[6];
            part5Vertices[0] = new Vec2f(0.0f, 0.0f);
            part5Vertices[1] = new Vec2f(0.5f, 0.1f);
            part5Vertices[2] = new Vec2f(0.5f, 0.26f);

            part5Vertices[3] = new Vec2f(0.0f, 0.0f);
            part5Vertices[4] = new Vec2f(0.5f, 0.26f);
            part5Vertices[5] = new Vec2f(0.0f, 0.6f);


            Vec2f[] part5Coords = new Vec2f[6];
            part5Coords[0] = new Vec2f(x + 0.0f * width, y + 0.4f * height);
            part5Coords[1] = new Vec2f(x + 0.5f * width, y + 0.5f * height);
            part5Coords[2] = new Vec2f(x + 0.5f * width, y + 0.66f * height);

            part5Coords[3] = new Vec2f(x + 0.0f * width, y + 0.4f * height);
            part5Coords[4] = new Vec2f(x + 0.5f * width, y + 0.66f * height);
            part5Coords[5] = new Vec2f(x + 0.0f * width, y + 1.0f * height);


            float rand1 = KMath.Random.Mt19937.genrand_realf();
            float rand2 = KMath.Random.Mt19937.genrand_realf();

            Vec2f velocity;
            velocity.X = rand1;
            velocity.Y = rand2;

            var cc = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position, 
            part1Vertices, part1Coords, velocity, -1));


            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1;
            velocity.Y = rand2;

            ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position, 
            part2Vertices, part2Coords, velocity, -1));

            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1;
            velocity.Y = rand2;

            ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position + new Vec2f(0.0f, 0.26f), 
            part3Vertices, part3Coords, velocity, -1));

            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1;
            velocity.Y = rand2;

            ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position + new Vec2f(0.0f, 0.66f), 
            part4Vertices, part4Coords, velocity, -1));


            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1;
            velocity.Y = rand2;

            ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position + new Vec2f(0.0f, 0.4f), 
            part5Vertices, part5Coords, velocity, -1));


            return cc;


            Vec2f[] triangle1 = new Vec2f[3];
            triangle1[0] = new Vec2f(0, 0);
            triangle1[1] = new Vec2f(0.5f, 0.5f);
            triangle1[2] = new Vec2f(0.0f, 1.0f);

            Vec2f[] triangle2 = new Vec2f[3];
            triangle2[0] = new Vec2f(0, 0.5f);
            triangle2[1] = new Vec2f(0.5f, 0.0f);
            triangle2[2] = new Vec2f(1.0f, 0.5f);


            Vec2f[] triangle3 = new Vec2f[3];
            triangle3[0] = new Vec2f(1.0f, 1.0f);
            triangle3[1] = new Vec2f(0.5f, 0.5f);
            triangle3[2] = new Vec2f(1.0f, 0.0f);


            Vec2f[] triangle4 = new Vec2f[3];
            triangle4[0] = new Vec2f(1.0f, 0.0f);
            triangle4[1] = new Vec2f(0.5f, 0.5f);
            triangle4[2] = new Vec2f(0.0f, 0.0f);


            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1;
            velocity.Y = rand2;

            Vec2f[] triangle1Coords = new Vec2f[3];
            triangle1Coords[0] = new Vec2f(x, y);
            triangle1Coords[1] = new Vec2f(x + width / 2, y + height / 2);
            triangle1Coords[2] = new Vec2f(x, y + height);

            ParticleEntity particle1 = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position, 
            triangle1, triangle1Coords, velocity, -1));


            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();
            velocity.X = rand1;
            velocity.Y = rand2;

            Vec2f[] triangle2Coords = new Vec2f[3];
            triangle2Coords[0] = new Vec2f(x, y + height);
            triangle2Coords[1] = new Vec2f(x + width / 2, y + height / 2);
            triangle2Coords[2] = new Vec2f(x + width, y + height);

            ParticleEntity particle2 = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position + new Vec2f(0.0f, 0.5f), 
            triangle2, triangle2Coords, velocity, -1));


            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();
            velocity.X = rand1;
            velocity.Y = rand2;

            Vec2f[] triangle3Coords = new Vec2f[3];
            triangle3Coords[0] = new Vec2f(x + width, y + height);
            triangle3Coords[1] = new Vec2f(x + width / 2, y + height / 2);
            triangle3Coords[2] = new Vec2f(x + width, y);

            ParticleEntity particle3 = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position, 
            triangle3, triangle3Coords, velocity, -1));


            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();
            velocity.X = rand1;
            velocity.Y = rand2;

            Vec2f[] triangle4Coords = new Vec2f[3];
            triangle4Coords[0] = new Vec2f(x + width, y);
            triangle4Coords[1] = new Vec2f(x + width / 2, y + height / 2);
            triangle4Coords[2] = new Vec2f(x, y);

            ParticleEntity particle4 = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position, 
            triangle4, triangle4Coords, velocity, -1));

            /*ParticleEntity a = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position, 
            new Vector4(spriteCoords.x, spriteCoords.y, width / 2, height / 2), -1));

            ParticleEntity b = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position + new Vec2f(0.5f, 0.0f), 
            new Vector4(spriteCoords.x + width / 2.0f, spriteCoords.y, width / 2, height / 2), -1));

            ParticleEntity c = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position + new Vec2f(0.0f, -0.5f), 
            new Vector4(spriteCoords.x, spriteCoords.y + height / 2.0f, width / 2, height / 2), -1));

            ParticleEntity d = ParticleList.Add(GameState.ParticleSpawnerSystem.SpawnDebris(EntitasContext.particle, position + new Vec2f(0.5f, -0.5f), 
            new Vector4(spriteCoords.x + width / 2.0f, spriteCoords.y + height / 2.0f, width / 2, height / 2), -1));*/

            return particle4;
        }

        public void RemoveParticle(int particleId)
        {
            ParticleList.Remove(particleId);
        }

        public ProjectileEntity AddProjectile(Vec2f position, Vec2f direction, Enums.ProjectileType projectileType)
        {
            Utils.Assert(ProjectileList.Size < PlanetEntityLimits.ProjectileLimit);
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
            Utils.Assert(VehicleList.Size < PlanetEntityLimits.VehicleLimit);

            VehicleEntity newEntity = VehicleList.Add(new VehicleEntity());
            return newEntity;
        }

        public void RemoveVehicle(int vehicleId)
        {
            VehicleList.Remove(vehicleId);
        }

        public ItemParticleEntity AddItemParticle(Vec2f position, ItemType itemType)
        {
            Utils.Assert(ItemParticleList.Size < PlanetEntityLimits.ItemParticlesLimit);

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
            GameState.PhysicsMovableSystem.Update(EntitasContext.agent);
            GameState.PhysicsMovableSystem.Update(EntitasContext.itemParticle);
            GameState.PhysicsProcessCollisionSystem.Update(EntitasContext.agent, ref TileMap);
            GameState.PhysicsProcessCollisionSystem.Update(EntitasContext.itemParticle, ref TileMap);
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
