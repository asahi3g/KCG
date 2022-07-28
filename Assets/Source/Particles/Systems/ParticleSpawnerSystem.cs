using System.Collections.Generic;
using Entitas;
using KMath;
using UnityEngine;

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


         public ParticleEntity SpawnDebris(ParticleContext context, Vec2f position, Vec2f[] triangles, Vec2f[] textureCoords, Vec2f velocity, int particleId)
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

    }
}
