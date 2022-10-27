//imports UnityEngine

using System.Collections.Generic;
using Entitas;
using KMath;

namespace Particle
{
    public class ParticleEmitterSpawnerSystem
    {

        ParticleEmitterCreationApi ParticleEmitterCreationApi;
        ParticleCreationApi ParticleCreationApi;
        int uniqueID = 0;
        public ParticleEmitterSpawnerSystem(ParticleEmitterCreationApi particleEmitterCreationApi,
                                            ParticleCreationApi particleCreationApi)
        {
            ParticleEmitterCreationApi = particleEmitterCreationApi;
            ParticleCreationApi = particleCreationApi;
        }

        //Note(Mahdi): Deprecated will be removed later
        public ParticleEntity Spawn(ParticleContext context, UnityEngine.Material material, Vec2f position, Vec2f size,
                                     int spriteId)
        {
            // use an api to create different emitter entities
            ParticleEntity entity = CreateParticleEmitterEntity(context, Particle.ParticleEmitterType.OreFountain, 
                                            position);

            return entity;
        }

        public ParticleEntity Spawn(ParticleContext context, Particle.ParticleEmitterType type, 
                                        Vec2f position)
        {
            ParticleEntity entity = CreateParticleEmitterEntity(context, type, position);

            return entity;
        }

        private ParticleEntity CreateParticleEmitterEntity(ParticleContext context, 
                                Particle.ParticleEmitterType type, Vec2f position)
        {
            ParticleEmitterProperties emitterProperties = 
                        ParticleEmitterCreationApi.Get((int)type);
            ParticleProperties particleProperties = 
                        ParticleCreationApi.Get((int)emitterProperties.ParticleType);
            var e = context.CreateEntity();
            e.AddParticleEmitterID(uniqueID++, -1);
            e.AddParticleEmitter2dPosition(new UnityEngine.Vector2(position.X, position.Y), new UnityEngine.Vector2(), new UnityEngine.Vector2());
            e.AddParticleEmitterState(emitterProperties.ParticleType, type, emitterProperties.Duration, 0.0f);

            return e;
        }
    }
}
