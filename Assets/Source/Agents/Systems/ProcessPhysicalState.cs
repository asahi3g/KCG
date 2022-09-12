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
                AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState, AgentMatcher.AgentStats));

            foreach (var entity in EntitiesWithMovementState)
            {
                var physicsState = entity.agentPhysicsState;
                var stats = entity.agentStats;

                stats.IsLimping = stats.Health <= 50.0f;

                float epsilon = 4.0f;

                if (physicsState.MovementState != AgentMovementState.SlidingLeft &&
                physicsState.MovementState != AgentMovementState.SlidingRight)
                {
                    if (physicsState.Velocity.Y <= -epsilon && entity.IsStateFree())
                    {
                        physicsState.MovementState = AgentMovementState.Falling;
                    }
                    else if (entity.IsStateFree() &&
                    physicsState.MovementState != AgentMovementState.JetPackFlying &&
                    physicsState.MovementState != AgentMovementState.Crouch &&
                    physicsState.MovementState != AgentMovementState.Crouch_Move)
                    {
                        physicsState.MovementState = AgentMovementState.None;
                    }
                }

                // decrease the dash cooldown
                if (physicsState.DashCooldown > 0)
                {
                    physicsState.DashCooldown -= deltaTime;
                }

                // decrease the dash cooldown
                if (physicsState.RollCooldown > 0)
                {
                    physicsState.RollCooldown -= deltaTime;
                }

                if (physicsState.SlashCooldown > 0)
                {
                    physicsState.SlashCooldown -= deltaTime;
                }
                else
                {
                    if (physicsState.MovementState == AgentMovementState.SwordSlash)
                    {
                        physicsState.MovementState = AgentMovementState.None;
                    }
                }

                if (physicsState.ActionDuration > 0)
                {
                    physicsState.ActionDuration -= deltaTime;
                }
                else
                {
                    switch(physicsState.MovementState)
                    {
                        //case AgentMovementState.MonsterAttack:
                        case AgentMovementState.FireGun:
                        case AgentMovementState.UseTool:
                        case AgentMovementState.Drink:
                        {
                            physicsState.MovementState = AgentMovementState.None;
                            physicsState.ActionInProgress = false;
                            physicsState.ActionJustEnded = true;
                            break;
                        }

                        case AgentMovementState.Rolling:
                        {
                            physicsState.MovementState = AgentMovementState.StandingUpAfterRolling;
                            physicsState.AffectedByFriction = true;
                            physicsState.RollImpactDuration = 0.8f;
                            physicsState.ActionInProgress = false;
                            physicsState.ActionJustEnded = true;
                            break;
                        }
                    }
                }

                if (physicsState.RollImpactDuration > 0)
                {
                    physicsState.RollImpactDuration -= deltaTime;
                }
                else
                {
                    if (physicsState.MovementState == AgentMovementState.StandingUpAfterRolling)
                    {
                        physicsState.MovementState = AgentMovementState.None;
                    }
                }

                if (physicsState.StaggerDuration > 0)
                {
                    physicsState.StaggerDuration -= deltaTime;
                    if (physicsState.StaggerDuration <= 0 &&
                     physicsState.MovementState == AgentMovementState.Stagger)
                    {
                        physicsState.MovementState = AgentMovementState.None;
                    }
                }

                if (physicsState.DyingDuration > 0)
                {
                    physicsState.DyingDuration -= deltaTime;
                    if (physicsState.DyingDuration <= 0)
                    {
                        if (physicsState.MovementState == AgentMovementState.KnockedDownFront)
                        {
                            physicsState.MovementState = AgentMovementState.LyingFront;
                        }
                        else if (physicsState.MovementState == AgentMovementState.KnockedDownBack)
                        {
                            physicsState.MovementState = AgentMovementState.LyingBack;
                        }
                    }
                }

                // if we are on the ground we reset the jump counter.
                if (physicsState.OnGrounded && physicsState.Velocity.Y < 0.5f)
                {
                    physicsState.JumpCounter = 0;
                    /*if (physicsState.MovementState == AgentMovementState.SlidingRight || physicsState.MovementState == AgentMovementState.SlidingLeft)
                    {*/
                    /*if (entity.IsStateFree())
                    {
                        physicsState.MovementState = AgentMovementState.None;
                    }*/
                 //   }
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
                    planet.AddParticleEmitter(physicsState.Position + new Vec2f(0.0f, 0.5f), Particle.ParticleEmitterType.DustEmitter);
                }

                // if we are sliding
                // spawn some particles and limit vertical movement
                if (physicsState.MovementState == AgentMovementState.SlidingRight)
                {
                    physicsState.SlidingTime += deltaTime;
                    physicsState.JumpCounter = 0;
                    physicsState.Acceleration.Y = 0.0f;
                    if (physicsState.SlidingTime < 0.75f)
                    {
                        physicsState.Velocity.Y = 0.0f;//-1.75f;
                    }
                    else
                    {
                        physicsState.Velocity.Y = -1.75f;
                        planet.AddParticleEmitter(physicsState.Position + new Vec2f(0.0f, -0.5f), Particle.ParticleEmitterType.DustEmitter);
                    }
                    physicsState.AffectedByGravity = false;
                    
                }
                else if (physicsState.MovementState == AgentMovementState.SlidingLeft)
                {
                    physicsState.SlidingTime += deltaTime;

                    physicsState.JumpCounter = 0;
                    physicsState.Acceleration.Y = 0.0f;
                    if (physicsState.SlidingTime < 0.75f)
                    {
                        physicsState.Velocity.Y = 0.0f;//-1.75f;
                    }
                    else
                    {
                        physicsState.Velocity.Y = -1.75f;
                        planet.AddParticleEmitter(physicsState.Position + new Vec2f(-0.5f, -0.35f), Particle.ParticleEmitterType.DustEmitter);
                    }
                    physicsState.AffectedByGravity = false;
                }
                else
                {
                    physicsState.SlidingTime = 0.0f;
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
                


                if (physicsState.MovementState == AgentMovementState.Idle || 
                physicsState.MovementState == AgentMovementState.None)
                {
                    if (physicsState.Velocity.X >= physicsState.Speed * 0.1f ||
                    physicsState.Velocity.X <= -physicsState.Speed * 0.1f)
                    {
                        if (stats.IsLimping)
                        {
                            physicsState.MovementState = AgentMovementState.Limp;
                        }
                        else
                        {
                            physicsState.MovementState = AgentMovementState.Move;
                        }
                    }
                    else
                    {
                        physicsState.MovementState = AgentMovementState.Idle;
                    }
                }

                if (physicsState.MovementState == AgentMovementState.Crouch ||
                physicsState.MovementState == AgentMovementState.Crouch_Move)
                {
                    if (physicsState.Velocity.X >= physicsState.Speed * 0.1f ||
                    physicsState.Velocity.X <= -physicsState.Speed * 0.1f)
                    {
                        physicsState.MovementState = AgentMovementState.Crouch_Move;
                    }
                    else
                    {
                        physicsState.MovementState = AgentMovementState.Crouch;
                    }
                }

                if (physicsState.MovementState == AgentMovementState.Idle ||
                physicsState.MovementState == AgentMovementState.None ||
                physicsState.MovementState == AgentMovementState.Move ||
                physicsState.MovementState == AgentMovementState.JetPackFlying)
                {
                    physicsState.AffectedByGravity = true;
                }




                var IDComponent = entity.agentID;
                var box2DComponent = entity.physicsBox2DCollider;

                AgentProperties properties = GameState.AgentCreationApi.Get((int)IDComponent.Type);

                if (physicsState.MovementState == AgentMovementState.Crouch ||
                physicsState.MovementState == AgentMovementState.Crouch_Move)
                {
                    box2DComponent.Size.Y = properties.CollisionDimensions.Y * 0.75f;
                }
                else if (physicsState.MovementState == AgentMovementState.Rolling)
                {
                    box2DComponent.Size.Y = properties.CollisionDimensions.Y * 0.5f;
                }
                else
                {
                    box2DComponent.Size.Y = properties.CollisionDimensions.Y * 1.0f;
                }
            }
        }

        public void Knockback(AgentEntity agentEntity, float velocity, int horizontalDir)
        {
            var physicsState = agentEntity.agentPhysicsState;

            physicsState.Velocity.X = velocity * horizontalDir;
            physicsState.MovementState = AgentMovementState.Stagger;
            physicsState.StaggerDuration = 1.0f;
        }

        public void Jump(AgentEntity agentEntity)
        {
            var physicsState = agentEntity.agentPhysicsState;
            if (agentEntity.IsStateFree())
            {
                // we can start jumping only if the jump counter is 0
                if (physicsState.JumpCounter == 0)
                {
                    
                        // first jump

                        // if we are sticking to a wall 
                        // throw the agent in the opphysicsStateite direction
                        // Inpulse so use immediate speed intead of acceleration.
                        if (physicsState.MovementState == AgentMovementState.SlidingLeft)
                        {
                            physicsState.MovementState = AgentMovementState.None;
                            physicsState.Velocity.X = physicsState.Speed * 1.0f;
                        }
                        else if (physicsState.MovementState == AgentMovementState.SlidingRight)
                        {
                            physicsState.MovementState = AgentMovementState.None;
                            physicsState.Velocity.X = - physicsState.Speed * 1.0f;
                        }

                        // jumping
                        physicsState.Velocity.Y = physicsState.InitialJumpVelocity;
                        physicsState.JumpCounter++;
                
                }
                else
                {
                    // double jump
                    if (physicsState.JumpCounter <= 1)
                    {
                        physicsState.Velocity.Y = physicsState.InitialJumpVelocity * 0.75f;
                        physicsState.JumpCounter++;
                    }
                }
            }
        }

        public void SwordSlash(AgentEntity agentEntity)
        {
            var PhysicsState = agentEntity.agentPhysicsState;

            if (PhysicsState.SlashCooldown <= 0.0f && 
            agentEntity.IsStateFree())
            {
                //PhysicsState.Velocity.X = 4 * PhysicsState.Speed * horizontalDir;
                //PhysicsState.Velocity.Y = 0.0f;

                //PhysicsState.Invulnerable = false;
                //PhysicsState.AffectedByGravity = true;
                PhysicsState.MovementState = AgentMovementState.SwordSlash;
                PhysicsState.SlashCooldown = 0.6f;
            }
        }

        public void JetPackFlying(AgentEntity agentEntity)
        {
            var stats = agentEntity.agentStats;
            var PhysicsState = agentEntity.agentPhysicsState;

            // if the fly button is pressed
            if (stats.Fuel > 0.0f && 
            agentEntity.IsStateFree())
            {
                PhysicsState.MovementState = AgentMovementState.JetPackFlying;
            }
        }

        

        public void DieInPlace(AgentEntity agentEntity)
        {

        }
    }
}
