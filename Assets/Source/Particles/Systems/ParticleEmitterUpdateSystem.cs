//imports UnityEngine

using System.Collections.Generic;
using Entitas;
using KMath;

namespace Particle
{

    public class ParticleEmitterUpdateSystem
    {
        List<ParticleEntity> ToDestroy = new();

        ParticleEmitterCreationApi ParticleEmitterCreationApi;
        ParticleCreationApi ParticleCreationApi;
        public ParticleEmitterUpdateSystem(ParticleEmitterCreationApi particleEmitterCreationApi,
                                            ParticleCreationApi particleCreationApi)
        {
            ParticleEmitterCreationApi = particleEmitterCreationApi;
            ParticleCreationApi = particleCreationApi;
        }

        public void Update()
        {
            ToDestroy.Clear();

            ParticleContext context = GameState.Planet.EntitasContext.particle;

            float deltaTime = UnityEngine.Time.deltaTime;
            IGroup<ParticleEntity> entities = context.GetGroup(ParticleMatcher.ParticleEmitterState);
            foreach (var gameEntity in entities)
            {
                var state = gameEntity.particleEmitterState;
                var position = gameEntity.particleEmitter2dPosition;
                state.Duration -= UnityEngine.Time.deltaTime;
                ParticleEmitterProperties emitterProperties = 
                        ParticleEmitterCreationApi.Get((int)state.ParticleEmitterType);
                ParticleProperties particleProperties = 
                        ParticleCreationApi.Get((int)state.ParticleType);
                if (state.Duration >= 0)
                {
                    if (state.CurrentTime <= 0.0f)
                    {
                        for(int i = 0; i < emitterProperties.ParticleCount; i++)
                        {
                            System.Random random = new System.Random(); 

                            float x = position.Position.x;
                            float y = position.Position.y;

                            Vec2f Velocity = new Vec2f(particleProperties.StartingVelocity.X,
                                                        particleProperties.StartingVelocity.Y);

                            float rand1 = KMath.Random.Mt19937.genrand_realf();
                            float rand2 = KMath.Random.Mt19937.genrand_realf() ;

                            x += rand1 * emitterProperties.SpawnRadius * 2 - emitterProperties.SpawnRadius;
                            y += rand2 * emitterProperties.SpawnRadius * 2 - emitterProperties.SpawnRadius;

                            Velocity.X += rand1 * (emitterProperties.VelocityIntervalEnd.X - 
                                                   emitterProperties.VelocityIntervalBegin.X) -
                                                        emitterProperties.VelocityIntervalEnd.X;
                            Velocity.Y += rand2 * (emitterProperties.VelocityIntervalEnd.Y - 
                                                   emitterProperties.VelocityIntervalBegin.Y) -
                                                        emitterProperties.VelocityIntervalEnd.Y;

                            GameState.Planet.AddParticle(new Vec2f(x, y), Velocity, state.ParticleType);
                        }

                        state.CurrentTime = emitterProperties.TimeBetweenEmissions;
                    }
                    else
                    {
                        state.CurrentTime -= UnityEngine.Time.deltaTime;
                    }

                    gameEntity.ReplaceParticleEmitterState(state.ParticleType, state.ParticleEmitterType,
                                    state.Duration, state.CurrentTime);
                }
                else
                {
                    ToDestroy.Add(gameEntity);
                }
            }

            foreach(var entity in ToDestroy)
            {
                GameState.Planet.RemoveParticleEmitter(entity.particleEmitterID.Index);
            }
        }
    }
}


