//imports UnityEngine

using System.Collections.Generic;
using Entitas;
using KMath;

namespace Particle
{
     public class ParticleUpdateSystem
    {
        List<ParticleEntity> ToDestroy = new List<ParticleEntity>();


        public void Update()
        {
            ToDestroy.Clear();

            ref var planet = ref GameState.Planet;
            float deltaTime = UnityEngine.Time.deltaTime;
            IGroup<ParticleEntity> entities = planet.EntitasContext.particle.GetGroup(ParticleMatcher.ParticleState);
            foreach (var gameEntity in entities)
            {

                if (gameEntity.hasParticleAnimation)
                {
                    var animation = gameEntity.particleAnimation;
                    animation.State.Update(deltaTime, animation.AnimationSpeed);

                    gameEntity.ReplaceParticleAnimation(animation.AnimationSpeed, animation.State);
                }

                var state = gameEntity.particleState;
                var box2d = gameEntity.particleBox2DCollider;
                ParticleProperties properties = GameState.ParticleCreationApi.Get(gameEntity.particleID.ParticleType);


                float r = 1.0f, g = 1.0f, b = 1.0f, a = 1.0f;
                if (properties.ColorUpdateMethod == Enums.ParticleColorUpdateMethod.Linear)
                {
                    r = properties.StartingColor.r * state.Health +  properties.EndColor.r * (1.0f - state.Health);
                    g = properties.StartingColor.g * state.Health +  properties.EndColor.g * (1.0f - state.Health);
                    b = properties.StartingColor.b * state.Health +  properties.EndColor.b * (1.0f - state.Health);
                    a = properties.StartingColor.a * state.Health +  properties.EndColor.a * (1.0f - state.Health);
                }
                
                state.Color = new Vec4f(r, g, b, a); 
                

                



                float newHealth = state.Health - state.DecayRate * deltaTime;
                gameEntity.ReplaceParticleState(newHealth, state.DecayRate, state.DeltaRotation, state.DeltaScale, state.Color);

                var physicsState = gameEntity.particlePhysicsState;
                Vec2f displacement = 
                        0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
                Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;

                Vec2f newPosition = physicsState.Position + displacement;

                float newRotation = physicsState.Rotation + state.DeltaRotation * deltaTime;
                
                gameEntity.ReplaceParticlePhysicsState(newPosition, physicsState.Position, physicsState.Acceleration, newVelocity, newRotation, physicsState.Bounce, physicsState.BounceFactor);

                /*state.GameObject.transform.position = new Vector3(newPosition.x, newPosition.y, 0.0f);
                state.GameObject.transform.Rotate(0.0f, 0.0f, state.DeltaRotation, Space.Self);*/
                
                if (newHealth <= 0)
                {
                    ToDestroy.Add(gameEntity);
                }
            }

            foreach(var gameEntity in ToDestroy)
            {
                //Object.Destroy(gameEntity.particleState.GameObject);
                //gameEntity.Destroy();
                planet.RemoveParticle(gameEntity.particleID.Index);
            }
        }
    }
}


