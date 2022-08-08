using System;
using Enums;
using Planet;
using KMath;

namespace Agent
{
    public class ProcessPhysicalState
    {
        public void Update(ref PlanetState planet, float deltaTime)
        {
            var EntitiesWithMovementState = planet.EntitasContext.agent.GetGroup(
                AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState));

            foreach (var entity in EntitiesWithMovementState)
            {
                var physicsState = entity.agentPhysicsState;
                var stats = entity.agentStats;

                // decrease the dash cooldown
                if (physicsState.DashCooldown > 0)
                    physicsState.DashCooldown -= deltaTime;

                // if we are on the ground we reset the jump counter.
                if (physicsState.OnGrounded)
                {
                    physicsState.JumpCounter = 0;
                    if (physicsState.MovementState == AgentMovementState.SlidingRight || physicsState.MovementState == AgentMovementState.SlidingLeft)
                        physicsState.MovementState = AgentMovementState.None;
                }


                // the end of dashing
                // we can do this using a fixed amount of time.
                if (System.Math.Abs(physicsState.Velocity.X) <= 6.0f && physicsState.MovementState == AgentMovementState.Dashing)
                {
                    physicsState.MovementState = AgentMovementState.None;
                    physicsState.Invulnerable = false;
                    physicsState.AffectedByGravity = true;
                }

                // if we are dashing we add some particles
                if (physicsState.MovementState == AgentMovementState.Dashing)
                {
                    planet.AddParticleEmitter(physicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                }

                // if we are sliding
                // spawn some particles and limit vertical movement
                if (physicsState.MovementState == AgentMovementState.SlidingRight)
                {
                    physicsState.JumpCounter = 0;
                    physicsState.Acceleration.Y = 0.0f;
                    physicsState.Velocity.Y = -1.75f;
                    planet.AddParticleEmitter(physicsState.Position + new Vec2f(0.0f, -0.5f), Particle.ParticleEmitterType.DustEmitter);
                }
                else if (physicsState.MovementState == AgentMovementState.SlidingLeft)
                {
                    physicsState.JumpCounter = 0;
                    physicsState.Acceleration.Y = 0.0f;
                    physicsState.Velocity.Y = -1.75f;
                    planet.AddParticleEmitter(physicsState.Position + new Vec2f(0.5f, -0.5f), Particle.ParticleEmitterType.DustEmitter);
                }

                if (physicsState.MovementState == AgentMovementState.JetPackFlying)
                {
                    // if we are using the jetpack
                    // set the Y velocity to a given value.
                    physicsState.Velocity.Y = 3.5f;

                    // Reduce the fuel and spawn particles
                    stats.Fuel -= 1.0f;
                    if (stats.Fuel <= 1.0f)
                    {
                        stats.Fuel -= 30.0f;
                    }
                    planet.AddParticleEmitter(physicsState.Position, Particle.ParticleEmitterType.DustEmitter);

                    physicsState.MovementState = AgentMovementState.None;
                }
                else
                {
                    // make sure the fuel never goes up more than it should
                    if (stats.Fuel <= 100)
                        // if we are not JetPackFlying, add fuel to the tank
                        stats.Fuel += 1.0f;
                    else
                        stats.Fuel = 100;
                }
            }
        }

        public void JumpVelocity(AgentEntity agentEntity, float velocity)
        {
            var physicsState = agentEntity.agentPhysicsState;

            // we can start jumping only if the jump counter is 0
            if (physicsState.JumpCounter == 0 && physicsState.MovementState != AgentMovementState.Dashing)
            {
                // first jump

                // if we are sticking to a wall 
                // throw the agent in the opphysicsStateite direction
                // Inpulse so use immediate speed intead of acceleration.
                if (physicsState.MovementState == AgentMovementState.SlidingLeft)
                {
                    physicsState.MovementState = AgentMovementState.None;
                    physicsState.Velocity.X = physicsState.Speed / 2;
                }
                else if (physicsState.MovementState == AgentMovementState.SlidingRight)
                {
                    physicsState.MovementState = AgentMovementState.None;
                    physicsState.Acceleration.X = - physicsState.Speed / 2;
                }

                // jumping
                physicsState.Velocity.Y = velocity;
                physicsState.JumpCounter++;
            }
            else
            {
                // double jump
                if (physicsState.JumpCounter <= 1)
                {
                    physicsState.Velocity.Y = velocity * 0.75f;
                    physicsState.JumpCounter++;
                }
            }
        }

        public void Dash(AgentEntity agentEntity, float horizontalDir)
        {
            var PhysicsState = agentEntity.agentPhysicsState;

            if (PhysicsState.DashCooldown <= 0.0f)
            {
                PhysicsState.Velocity.X = 4 * PhysicsState.Speed * horizontalDir;
                PhysicsState.Velocity.Y = 0.0f;

                PhysicsState.Invulnerable = true;
                PhysicsState.AffectedByGravity = false;
                PhysicsState.MovementState = AgentMovementState.Dashing;
                PhysicsState.DashCooldown = 1.0f;
            }
        }

        public void JetPackFlying(AgentEntity agentEntity)
        {
            var stats = agentEntity.agentStats;
            var PhysicsState = agentEntity.agentPhysicsState;

            // if the fly button is pressed
            if (stats.Fuel > 0.0f)
            {
                PhysicsState.MovementState = AgentMovementState.JetPackFlying;
            }
        }

        public void Run(AgentEntity agentEntity, float horizontalDir)
        {
            var PhysicsState = agentEntity.agentPhysicsState;

            // handling horizontal movement (left/right)
            if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed)
                PhysicsState.Acceleration.X = horizontalDir * 2 * PhysicsState.Speed / Physics.Constants.TimeToMax;

            if (horizontalDir > 0 && PhysicsState.MovementState == AgentMovementState.SlidingLeft)
            {
                // if we move to the right
                // that means we are no longer sliding down on the left
                PhysicsState.MovementState = AgentMovementState.None;
            }
            else if (horizontalDir < -1.0f && PhysicsState.MovementState == AgentMovementState.SlidingRight)
            {
                // if we move to the left
                // that means we are no longer sliding down on the right
                PhysicsState.MovementState = AgentMovementState.None;
            }

        }

        public void Walk(AgentEntity agentEntity, float horizontalDir)
        {
            var PhysicsState = agentEntity.agentPhysicsState;
            
            if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed/2) 
                PhysicsState.Acceleration.X = 2 * horizontalDir * PhysicsState.Speed / Physics.Constants.TimeToMax;
            else if (Math.Abs(PhysicsState.Velocity.X) == PhysicsState.Speed/2) // Velocity equal drag.
                PhysicsState.Acceleration.X = horizontalDir * PhysicsState.Speed / Physics.Constants.TimeToMax;

            if (horizontalDir > 0 && PhysicsState.MovementState == AgentMovementState.SlidingLeft)
            {   
                // if we move to the right
                // that means we are no longer sliding down on the left
                PhysicsState.MovementState = AgentMovementState.None;
            }
            else if (horizontalDir < -1.0f && PhysicsState.MovementState == AgentMovementState.SlidingRight)
            {
                // if we move to the left
                // that means we are no longer sliding down on the right
                PhysicsState.MovementState = AgentMovementState.None;
            }
        }
    }
}
