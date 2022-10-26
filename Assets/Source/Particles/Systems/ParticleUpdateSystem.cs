//imports UnityEngine

using System.Collections.Generic;
using Entitas;
using KMath;

namespace Particle
{
     public class ParticleUpdateSystem
    {
        List<ParticleEntity> ToDestroy = new();


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

                float newHealth = state.Health - state.DecayRate * deltaTime;
                gameEntity.ReplaceParticleState(newHealth, state.DecayRate, state.DeltaRotation, state.DeltaScale);

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


