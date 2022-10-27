using Enums;
using KMath;

namespace Agent
{
    public class ProcessPhysicalState
    {
        public void Update(float deltaTime)
        {
            ref var planet = ref GameState.Planet;
            var entitiesWithMovementState = planet.EntitasContext.agent.GetGroup(
                AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState, AgentMatcher.AgentStats));

            foreach (var entity in entitiesWithMovementState)
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
                    !entity.IsCrouched())
                    {
                        physicsState.MovementState = AgentMovementState.None;
                    }
                }

                


                if (physicsState.MovementState != AgentMovementState.Falling && 
                    physicsState.MovementState != AgentMovementState.Dashing)
                    {
                        if (physicsState.JumpCounter == 1)
                        {
                            physicsState.MovementState = AgentMovementState.Jump;
                            physicsState.AffectedByGravity = true;
                        }
                        else if (physicsState.JumpCounter == 2)
                        {
                            physicsState.MovementState = AgentMovementState.Flip;
                            physicsState.AffectedByGravity = true;
                        }
                    }


                if (physicsState.IdleAfterShootingTime > 0)
                {
                    physicsState.IdleAfterShootingTime -= deltaTime;
                }
                else
                {
                    if (physicsState.MovementState == AgentMovementState.IdleAfterShooting)
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
                        {
                            physicsState.MovementState = AgentMovementState.IdleAfterShooting;
                            physicsState.IdleAfterShootingTime = 0.7f;
                            physicsState.ActionInProgress = false;
                            physicsState.ActionJustEnded = true;
                            break;
                        }
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

                        case AgentMovementState.PickaxeHit:
                        {
                                physicsState.MovementState = AgentMovementState.None;
                                physicsState.ActionInProgress = false;
                                physicsState.ActionJustEnded = true;
                                break;
                        }

                        case AgentMovementState.ChoppingTree:
                        {
                                physicsState.MovementState = AgentMovementState.None;
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

                Vec2f particlesSpawnPosition = physicsState.Position;
                if (physicsState.FacingDirection == 1)
                {
                    particlesSpawnPosition += new Vec2f(-0.44f, 1.2f);
                }
                else if (physicsState.FacingDirection == -1)
                {
                    particlesSpawnPosition += new Vec2f(0.44f, 1.2f);
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
                    planet.AddParticleEmitter(particlesSpawnPosition, Particle.ParticleEmitterType.DustEmitter);
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
                    stats.Fuel -= 30.0f * deltaTime;
                    if (stats.Fuel <= 1.0f)
                    {
                        stats.Fuel -= 10.0f;
                    }
                    planet.AddParticleEmitter(particlesSpawnPosition, Particle.ParticleEmitterType.DustEmitter);

                    //physicsState.MovementState = AgentMovementState.None;
                }
                else
                {
                    // make sure the fuel never goes up more than it should
                    if (stats.Fuel <= 100)
                    {
                        // if we are not JetPackFlying, add fuel to the tank
                        stats.Fuel += 30.0f * deltaTime;
                    }
                    else
                    {
                        stats.Fuel = 100;
                    }
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
                            if (physicsState.MovingDirection != physicsState.FacingDirection)
                            {
                                physicsState.MovementState = AgentMovementState.MoveBackward;
                            }
                            else
                            {
                                physicsState.MovementState = AgentMovementState.Move;
                            }
                            
                        }
                    }
                    else
                    {
                        if (physicsState.IdleAfterShootingTime > 0)
                        {
                            physicsState.MovementState = AgentMovementState.IdleAfterShooting;
                        }
                        else
                        {
                            physicsState.MovementState = AgentMovementState.Idle;
                        }
                    }
                }

                if (entity.IsCrouched())
                {
                    if (physicsState.Velocity.X >= physicsState.Speed * 0.1f ||
                    physicsState.Velocity.X <= -physicsState.Speed * 0.1f)
                    {
                        if (physicsState.MovingDirection != physicsState.FacingDirection)
                        {
                            physicsState.MovementState = AgentMovementState.Crouch_MoveBackward;
                        }
                        else
                        {
                            physicsState.MovementState = AgentMovementState.Crouch_Move;
                        }

                    }
                    else
                    {
                        physicsState.MovementState = AgentMovementState.Crouch;
                    }
                }

                if (entity.IsAffectedByGravity())
                {
                    physicsState.AffectedByGravity = true;
                }




                var IDComponent = entity.agentID;
                var box2DComponent = entity.physicsBox2DCollider;

                AgentProperties properties = GameState.AgentCreationApi.Get((int)IDComponent.Type);

                if (entity.IsCrouched())
                {
                    box2DComponent.Size.Y = properties.CollisionDimensions.Y * 0.65f;
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
