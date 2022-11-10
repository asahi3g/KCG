//imports UnityEngine

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
        public ParticleEntity Spawn(UnityEngine.Material material, Vec2f position, Vec2f size,
                                     int spriteId)
        {
            // use an api to create different emitter entities
            ParticleEntity entity = CreateParticleEmitterEntity(ParticleEmitterType.OreFountain, 
                                            position);

            return entity;
        }

        public ParticleEntity Spawn(ParticleEmitterType type, 
                                        Vec2f position)
        {
            ParticleEntity entity = CreateParticleEmitterEntity(type, position);

            return entity;
        }

        private ParticleEntity CreateParticleEmitterEntity(ParticleEmitterType type, Vec2f position)
        {
            ParticleEmitterProperties emitterProperties = 
                        ParticleEmitterCreationApi.Get((int)type);
            ParticleProperties particleProperties = 
                        ParticleCreationApi.Get(emitterProperties.ParticleType);
            var e = GameState.Planet.EntitasContext.particle.CreateEntity();
            e.AddParticleEmitterID(uniqueID++, -1);
            e.AddParticleEmitter2dPosition(new UnityEngine.Vector2(position.X, position.Y), new UnityEngine.Vector2(), new UnityEngine.Vector2());
            e.AddParticleEmitterState(emitterProperties.ParticleType, type, emitterProperties.Duration, 0.0f);

            return e;
        }
    }
}
