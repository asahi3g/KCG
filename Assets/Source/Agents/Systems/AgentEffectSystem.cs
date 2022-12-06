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
                AgentMatcher.AllOf(AgentMatcher.AgentID));

            foreach (var entity in entitiesWithMovementState)
            {
                var physicsState = entity.agentPhysicsState;
                var box2DCollider = entity.physicsBox2DCollider;
                var model3d = entity.agentAgent3DModel;

                Vec2f feetPosition = physicsState.Position + new Vec2f(0.125f, 0.0f) + new Vec2f(0.125f, 0.0f) * physicsState.MovingDirection;

                if (physicsState.MovementState == AgentMovementState.Dashing)
                {
                    // if we are dashing we add some particles
                    var emitter = planet.AddParticleEmitter(feetPosition, Particle.ParticleEmitterType.DustEmitter); 
                    emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(-3.0f * physicsState.MovingDirection, 0.0f);
                }

                if (physicsState.MovementState == AgentMovementState.Jump && physicsState.JumpedFromGround)
                {
                    physicsState.JumpingTime += deltaTime;

                    if (physicsState.JumpingTime < Agent.Constants.JumpingParticlesMaximumTime)
                    {
                        var emitter = GameState.Planet.AddParticleEmitter(physicsState.Position + new Vec2f(0.125f, 0.0f), Particle.ParticleEmitterType.Dust_Jumping);
                        Vec2f emitterVelocity = physicsState.Velocity.Normalized * 1.0f;
                        emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(emitterVelocity.X, emitterVelocity.Y);
                    }
                }
                else
                {
                    physicsState.JumpingTime = 0;
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
                physicsState.OnGrounded && physicsState.MovementState != Enums.AgentMovementState.SlidingLeft && 
                physicsState.MovementState != Enums.AgentMovementState.SlidingRight)
                {
                    Vec2f particleSpawnPosition = physicsState.Position + -0.25f * new Vec2f(physicsState.GroundNormal.Y, -physicsState.GroundNormal.X);

                    for(int i = 0; i < 4; i++)
                    {
                        var emitter = planet.AddParticleEmitter(particleSpawnPosition, Particle.ParticleEmitterType.Dust_Landing);
                        Vec2f velocity = -0.4f * (i + 1) * new Vec2f(physicsState.GroundNormal.Y, -physicsState.GroundNormal.X) + new Vec2f(0.0f, 0.125f * i);
                        emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(velocity.X, velocity.Y);
                    }

                    particleSpawnPosition = physicsState.Position + 0.25f * new Vec2f(physicsState.GroundNormal.Y, -physicsState.GroundNormal.X);
                    for(int i = 0; i < 4; i++)
                    {
                        var emitter = planet.AddParticleEmitter(particleSpawnPosition, Particle.ParticleEmitterType.Dust_Landing);
                        Vec2f velocity = 0.4f * (i + 1) * new Vec2f(physicsState.GroundNormal.Y, -physicsState.GroundNormal.X) + new Vec2f(0.0f, 0.125f * i);
                        emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(velocity.X, velocity.Y);
                    }
                }



                
                if (model3d.FlashDuration > 0.0f)
                {
                    model3d.FlashDuration -= deltaTime;

                    if (model3d.FlashDuration <= 0.0f)
                    {
                        model3d.Renderer.GetModelMesh().GetComponent<UnityEngine.SkinnedMeshRenderer>().sharedMaterial = model3d.Material;
                    }
                }


            }
        }
    }
}
