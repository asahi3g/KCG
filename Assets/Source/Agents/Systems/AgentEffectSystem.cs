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

                Vec2f feetPosition = physicsState.Position + new Vec2f(0.25f, 0.0f) + new Vec2f(0.25f, 0.0f) * physicsState.MovingDirection;

                if (physicsState.MovementState == AgentMovementState.Dashing)
                {
                    // if we are dashing we add some particles
                    var emitter = planet.AddParticleEmitter(feetPosition, Particle.ParticleEmitterType.DustEmitter); 
                    emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(-3.0f * physicsState.MovingDirection, 0.0f);
                }


                UnityEngine.Debug.Log(physicsState.MovingDistance);
                if (physicsState.LastMovingDirection != physicsState.MovingDirection)
                {
                    if (physicsState.MovingDistance >= Agent.Constants.MinimumDistanceToSpawnParticlesOnTurn)
                    {
                        var emitter = planet.AddParticleEmitter(feetPosition, Particle.ParticleEmitterType.Dust_2);
                        emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(-3.0f * physicsState.MovingDirection, 0.0f);
                    }


                    physicsState.MovingDistance = 0f;
                }

            }
        }
    }
}
