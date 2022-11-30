using Enums;
using KMath;

namespace Agent
{
    public class AgentEffectSystem
    {

        public void InitStage1()
        {

        }

        public void InitStage2()
        {

        }

        public void Update(float deltaTime)
        {
         var planet = GameState.Planet;
            var entitiesWithMovementState = planet.EntitasContext.agent.GetGroup(
                AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState, AgentMatcher.AgentStats));

            foreach (var entity in entitiesWithMovementState)
            {
                var physicsState = entity.agentPhysicsState;
                var box2DCollider = entity.physicsBox2DCollider;

                Vec2f feetPosition = physicsState.Position + new Vec2f(0.125f, 0.0f) + new Vec2f(0.125f, 0.0f) * physicsState.MovingDirection;

                if (physicsState.MovementState == AgentMovementState.Dashing)
                {
                    // if we are dashing we add some particles
                    var emitter = planet.AddParticleEmitter(feetPosition, Particle.ParticleEmitterType.DustEmitter); 
                    emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(-3.0f * physicsState.MovingDirection, 0.0f);
                }

                if (physicsState.MovementState == AgentMovementState.Jump)
                {
                    GameState.Planet.AddParticleEmitter(physicsState.Position + new Vec2f(0.125f, 0.0f), Particle.ParticleEmitterType.Dust_Jumping);
                }


                if (physicsState.LastMovingDirection != physicsState.MovingDirection)
                {
                    if (physicsState.MovingDistance >= Agent.Constants.MinimumDistanceToSpawnParticlesOnTurn && 
                    physicsState.OnGrounded)
                    {
                        var emitter = planet.AddParticleEmitter(feetPosition, Particle.ParticleEmitterType.Dust_2);
                        emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(-3.0f * physicsState.MovingDirection, 0.0f);
                    }


                    physicsState.MovingDistance = 0f;
                }


                if (physicsState.LastMovementState == Enums.AgentMovementState.Falling && 
                physicsState.OnGrounded)
                {
                    Vec2f particleSpawnPosition = physicsState.Position + new Vec2f(-0.25f, 0.0f);

                    for(int i = 0; i < 4; i++)
                    {
                        var emitter = planet.AddParticleEmitter(particleSpawnPosition, Particle.ParticleEmitterType.Dust_Landing);
                        emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(-0.4f * (i + 1), 0.125f * i);
                    }

                    particleSpawnPosition = physicsState.Position + new Vec2f(0.25f, 0.0f);
                    for(int i = 0; i < 4; i++)
                    {
                        var emitter = planet.AddParticleEmitter(particleSpawnPosition, Particle.ParticleEmitterType.Dust_Landing);
                        emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(0.4f * (i + 1), 0.125f * i);
                    }
                }

            }
        }
    }
}
