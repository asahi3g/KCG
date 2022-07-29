using System.Collections.Generic;
using Entitas;
using KMath;
using UnityEngine;
using Planet;

namespace Particle
{
    public class ParticleSpawnerSystem
    {

        ParticleCreationApi ParticleCreationApi;
        public ParticleSpawnerSystem(ParticleCreationApi particleCreationApi)
        {
            ParticleCreationApi = particleCreationApi;
        }

        public ParticleEntity Spawn(ParticleContext context, Particle.ParticleType particleType, 
                                        Vec2f position, Vec2f velocity, int particleId)
        {
            ParticleProperties particleProperties = 
                        ParticleCreationApi.Get((int)particleType);

            var entity = context.CreateEntity();
            entity.AddParticleID(particleId);
            entity.AddParticleState(1.0f, particleProperties.DecayRate, particleProperties.DeltaRotation, particleProperties.DeltaScale);
            entity.AddParticlePosition2D(new Vec2f(position.X, position.Y), new Vec2f(position.X, position.Y), particleProperties.Acceleration,
                             new Vec2f(velocity.X, velocity.Y), 0);
            
            entity.AddParticleSprite2D(particleProperties.SpriteId, null, null, particleProperties.Size);

            if (particleProperties.HasAnimation)
            {
                entity.AddParticleAnimation(1.0f, new Animation.Animation{Type=(int)particleProperties.AnimationType});
            }

            if (particleProperties.IsCollidable)
            {
                entity.AddParticleBox2DCollider(particleProperties.Size, new Vec2f());
            }

            return entity;
        }


        public ParticleEntity SpawnDebrisParticle(ParticleContext context, Vec2f position, Vec2f[] triangles, Vec2f[] textureCoords, Vec2f velocity, int particleId)
        {
            ParticleProperties particleProperties = 
                        ParticleCreationApi.Get((int)Particle.ParticleType.Debris);

            var entity = context.CreateEntity();
            entity.AddParticleID(particleId);
            entity.AddParticleState(1.0f, particleProperties.DecayRate, particleProperties.DeltaRotation, particleProperties.DeltaScale);
            entity.AddParticlePosition2D(new Vec2f(position.X, position.Y), new Vec2f(position.X, position.Y), particleProperties.Acceleration,
                             new Vec2f(velocity.X, velocity.Y), 0);
            
            entity.AddParticleSprite2D(-1, triangles, textureCoords, particleProperties.Size);

            if (particleProperties.HasAnimation)
            {
                entity.AddParticleAnimation(1.0f, new Animation.Animation{Type=(int)particleProperties.AnimationType});
            }

            if (particleProperties.IsCollidable)
            {
                entity.AddParticleBox2DCollider(particleProperties.Size, new Vec2f());
            }

            return entity;
        }

        public void SpawnSpriteDebris(PlanetState planetState, Vec2f position, int spriteId, float spriteWidth, float spriteHeight)
        {
            Vector4 spriteCoords = GameState.SpriteAtlasManager.GetSprite(spriteId, Enums.AtlasType.Particle).TextureCoords;
            float x = spriteCoords.x;
            float y = spriteCoords.y;
            float width = spriteCoords.z;
            float height = spriteCoords.w;
            

            float velocityValue = 1.0f;

            // the box debris is composed of 
            // 5 parts, each part will have some vertices and 
            // texture coordinates

            // part 1
            Vec2f[] part1Vertices = new Vec2f[12];
            part1Vertices[0] = new Vec2f(0.0f * spriteWidth, 0.0f * spriteHeight);
            part1Vertices[1] = new Vec2f(0.73f * spriteWidth, 0.0f * spriteHeight);
            part1Vertices[2] = new Vec2f(0.66f * spriteWidth, 0.26f * spriteHeight);

            part1Vertices[3] = new Vec2f(0.0f * spriteWidth, 0.0f * spriteHeight);
            part1Vertices[4] = new Vec2f(0.66f * spriteWidth, 0.26f * spriteHeight);
            part1Vertices[5] = new Vec2f(0.5f * spriteWidth, 0.4f * spriteHeight);

            part1Vertices[6] = new Vec2f(0.0f * spriteWidth, 0.0f * spriteHeight);
            part1Vertices[7] = new Vec2f(0.5f * spriteWidth, 0.4f * spriteHeight);
            part1Vertices[8] = new Vec2f(0.5f * spriteWidth, 0.5f * spriteHeight);

            part1Vertices[9] = new Vec2f(0.0f * spriteWidth, 0.0f * spriteHeight);
            part1Vertices[10] = new Vec2f(0.5f * spriteWidth, 0.5f * spriteHeight);
            part1Vertices[11] = new Vec2f(0.0f * spriteWidth, 0.4f * spriteHeight);


            Vec2f[] part1Coords = new Vec2f[12];
            part1Coords[0] = new Vec2f(x + 0.0f * width, ((y + height) - 0.0f * height));
            part1Coords[1] = new Vec2f(x + 0.73f * width, ((y + height) - 0.0f * height));
            part1Coords[2] = new Vec2f(x + 0.66f * width, ((y + height) - 0.26f * height));

            part1Coords[3] = new Vec2f(x + 0.0f * width, ((y + height) - 0.0f * height));
            part1Coords[4] = new Vec2f(x + 0.66f * width, ((y + height) - 0.26f * height));
            part1Coords[5] = new Vec2f(x + 0.5f * width, ((y + height) - 0.4f * height));

            part1Coords[6] = new Vec2f(x + 0.0f * width, ((y + height) - 0.0f * height));
            part1Coords[7] = new Vec2f(x + 0.5f * width, ((y + height) - 0.4f * height));
            part1Coords[8] = new Vec2f(x + 0.5f * width, ((y + height) - 0.5f * height));

            part1Coords[9] = new Vec2f(x + 0.0f * width, ((y + height) - 0.0f * height));
            part1Coords[10] = new Vec2f(x + 0.5f * width, ((y + height) - 0.5f * height));
            part1Coords[11] = new Vec2f(x + 0.0f * width, ((y + height) - 0.4f * height));



            // part 2
            Vec2f[] part2Vertices = new Vec2f[12];
            part2Vertices[0] = new Vec2f(1.0f * spriteWidth, 0.0f * spriteHeight);
            part2Vertices[1] = new Vec2f(1.0f * spriteWidth, 1.0f * spriteHeight);
            part2Vertices[2] = new Vec2f(0.83f * spriteWidth, 0.66f * spriteHeight);

            part2Vertices[3] = new Vec2f(1.0f * spriteWidth, 0.0f * spriteHeight);
            part2Vertices[4] = new Vec2f(0.83f * spriteWidth, 0.66f * spriteHeight);
            part2Vertices[5] = new Vec2f(0.83f * spriteWidth, 0.33f * spriteHeight);

            part2Vertices[6] = new Vec2f(1.0f * spriteWidth, 0.0f * spriteHeight);
            part2Vertices[7] = new Vec2f(0.83f * spriteWidth, 0.33f * spriteHeight);
            part2Vertices[8] = new Vec2f(0.66f * spriteWidth, 0.26f * spriteHeight);

            part2Vertices[9] = new Vec2f(1.0f * spriteWidth, 0.0f * spriteHeight);
            part2Vertices[10] = new Vec2f(0.66f * spriteWidth, 0.26f * spriteHeight);
            part2Vertices[11] = new Vec2f(0.73f * spriteWidth, 0.0f * spriteHeight);


            Vec2f[] part2Coords = new Vec2f[12];
            part2Coords[0] = new Vec2f(x + 1.0f * width, ((y + height) - 0.0f * height));
            part2Coords[1] = new Vec2f(x + 1.0f * width, ((y + height) - 1.0f * height));
            part2Coords[2] = new Vec2f(x + 0.83f * width, ((y + height) - 0.66f * height));

            part2Coords[3] = new Vec2f(x + 1.0f * width, ((y + height) - 0.0f * height));
            part2Coords[4] = new Vec2f(x + 0.83f * width, ((y + height) - 0.66f * height));
            part2Coords[5] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));

            part2Coords[6] = new Vec2f(x + 1.0f * width, ((y + height) - 0.0f * height));
            part2Coords[7] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));
            part2Coords[8] = new Vec2f(x + 0.66f * width, ((y + height) - 0.26f * height));

            part2Coords[9] = new Vec2f(x + 1.0f * width, ((y + height) - 0.0f * height));
            part2Coords[10] = new Vec2f(x + 0.66f * width, ((y + height) - 0.26f * height));
            part2Coords[11] = new Vec2f(x + 0.73f * width, ((y + height) - 0.0f * height));




            // part 3
            Vec2f[] part3Vertices = new Vec2f[9];
            part3Vertices[0] = new Vec2f(0.66f * spriteWidth, 0.0f * spriteHeight);
            part3Vertices[1] = new Vec2f(0.83f * spriteWidth, 0.07f * spriteHeight);
            part3Vertices[2] = new Vec2f(0.83f * spriteWidth, 0.4f * spriteHeight);

            part3Vertices[3] = new Vec2f(0.66f * spriteWidth, 0.0f * spriteHeight);
            part3Vertices[4] = new Vec2f(0.83f * spriteWidth, 0.4f * spriteHeight);
            part3Vertices[5] = new Vec2f(0.5f * spriteWidth, 0.4f * spriteHeight);

            part3Vertices[6] = new Vec2f(0.66f * spriteWidth, 0.0f * spriteHeight);
            part3Vertices[7] = new Vec2f(0.5f * spriteWidth, 0.4f * spriteHeight);
            part3Vertices[8] = new Vec2f(0.5f * spriteWidth, 0.14f * spriteHeight);


            Vec2f[] part3Coords = new Vec2f[9];
            part3Coords[0] = new Vec2f(x + 0.66f * width, ((y + height) - 0.26f * height));
            part3Coords[1] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));
            part3Coords[2] = new Vec2f(x + 0.83f * width, ((y + height) - 0.66f * height));

            part3Coords[3] = new Vec2f(x + 0.66f * width, ((y + height) - 0.26f * height));
            part3Coords[4] = new Vec2f(x + 0.83f * width, ((y + height) - 0.66f * height));
            part3Coords[5] = new Vec2f(x + 0.5f * width, ((y + height) - 0.66f * height));

            part3Coords[6] = new Vec2f(x + 0.66f * width, ((y + height) - 0.26f * height));
            part3Coords[7] = new Vec2f(x + 0.5f * width, ((y + height) - 0.66f * height));
            part3Coords[8] = new Vec2f(x + 0.5f * width, ((y + height) - 0.4f * height));



            // part 4
            Vec2f[] part4Vertices = new Vec2f[6];
            part4Vertices[0] = new Vec2f(0.5f * spriteWidth, 0.0f * spriteHeight);
            part4Vertices[1] = new Vec2f(0.83f * spriteWidth, 0.0f * spriteHeight);
            part4Vertices[2] = new Vec2f(1.0f * spriteWidth, 0.33f * spriteHeight);

            part4Vertices[3] = new Vec2f(0.5f * spriteWidth, 0.0f * spriteHeight);
            part4Vertices[4] = new Vec2f(1.0f * spriteWidth, 0.33f * spriteHeight);
            part4Vertices[5] = new Vec2f(0.0f * spriteWidth, 0.33f * spriteHeight);


            Vec2f[] part4Coords = new Vec2f[6];
            part4Coords[0] = new Vec2f(x + 0.5f * width, ((y + height) - 0.66f * height));
            part4Coords[1] = new Vec2f(x + 0.83f * width, ((y + height) - 0.66f * height));
            part4Coords[2] = new Vec2f(x + 1.0f * width, ((y + height) - 1.0f * height));

            part4Coords[3] = new Vec2f(x + 0.5f * width, ((y + height) - 0.66f * height));
            part4Coords[4] = new Vec2f(x + 1.0f * width, ((y + height) - 1.0f * height));
            part4Coords[5] = new Vec2f(x + 0.0f * width, ((y + height) - 1.0f * height));



            // part 5
            Vec2f[] part5Vertices = new Vec2f[6];
            part5Vertices[0] = new Vec2f(0.0f * spriteWidth, 0.0f * spriteHeight);
            part5Vertices[1] = new Vec2f(0.5f * spriteWidth, 0.1f * spriteHeight);
            part5Vertices[2] = new Vec2f(0.5f * spriteWidth, 0.26f * spriteHeight);

            part5Vertices[3] = new Vec2f(0.0f * spriteWidth, 0.0f * spriteHeight);
            part5Vertices[4] = new Vec2f(0.5f * spriteWidth, 0.26f * spriteHeight);
            part5Vertices[5] = new Vec2f(0.0f * spriteWidth, 0.6f * spriteHeight);


            Vec2f[] part5Coords = new Vec2f[6];
            part5Coords[0] = new Vec2f(x + 0.0f * width, ((y + height) - 0.4f * height));
            part5Coords[1] = new Vec2f(x + 0.5f * width, ((y + height) - 0.5f * height));
            part5Coords[2] = new Vec2f(x + 0.5f * width, ((y + height) - 0.66f * height));

            part5Coords[3] = new Vec2f(x + 0.0f * width, ((y + height) - 0.4f * height));
            part5Coords[4] = new Vec2f(x + 0.5f * width, ((y + height) - 0.66f * height));
            part5Coords[5] = new Vec2f(x + 0.0f * width, ((y + height) - 1.0f * height));


            // random velocity for each part
            float rand1 = KMath.Random.Mt19937.genrand_realf();
            float rand2 = KMath.Random.Mt19937.genrand_realf();

            Vec2f velocity;
            velocity.X = rand1 * velocityValue;
            velocity.Y = rand2 * velocityValue;

            planetState.ParticleList.Add(SpawnDebrisParticle(planetState.EntitasContext.particle, position, 
            part1Vertices, part1Coords, velocity, -1));


            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1 * velocityValue;
            velocity.Y = rand2 * velocityValue;

            planetState.ParticleList.Add(SpawnDebrisParticle(planetState.EntitasContext.particle, position, 
            part2Vertices, part2Coords, velocity, -1));

            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1 * velocityValue;
            velocity.Y = rand2 * velocityValue;

            planetState.ParticleList.Add(SpawnDebrisParticle(planetState.EntitasContext.particle, position + new Vec2f(0.0f, 0.26f), 
            part3Vertices, part3Coords, velocity, -1));

            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1 * velocityValue;
            velocity.Y = rand2 * velocityValue;

            planetState.ParticleList.Add(SpawnDebrisParticle(planetState.EntitasContext.particle, position + new Vec2f(0.0f, 0.66f), 
            part4Vertices, part4Coords, velocity, -1));


            rand1 = KMath.Random.Mt19937.genrand_realf();
            rand2 = KMath.Random.Mt19937.genrand_realf();

            velocity.X = rand1 * velocityValue;
            velocity.Y = rand2 * velocityValue;

            planetState.ParticleList.Add(SpawnDebrisParticle(planetState.EntitasContext.particle, position + new Vec2f(0.0f, 0.4f), 
            part5Vertices, part5Coords, velocity, -1));
        }

    }
}
