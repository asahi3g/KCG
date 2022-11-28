using Enums;
using KMath;

namespace Agent
{
    public class ProcessPhysicalState
    {
        public void Update(float deltaTime)
        {
         var planet = GameState.Planet;
            var entitiesWithMovementState = planet.EntitasContext.agent.GetGroup(
                AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState, AgentMatcher.AgentStats));

            foreach (var entity in entitiesWithMovementState)
            {
                var physicsState = entity.agentPhysicsState;
                var stats = entity.agentStats;

                float epsilon = 4.0f;

                if (physicsState.MovementState != AgentMovementState.SlidingLeft &&
                physicsState.MovementState != AgentMovementState.SlidingRight)
                {
                    if (physicsState.Velocity.Y <= -epsilon && entity.IsStateFree() && !physicsState.OnGrounded)
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
                else
                {
                    if (physicsState.MovementState == AgentMovementState.SwordSlash)
                    {
                        physicsState.MovementState = AgentMovementState.None;
                    }
                }

                if (physicsState.DashDuration > 0)
                {
                    physicsState.DashDuration -= deltaTime;
                }
                else
                {
                    if (physicsState.MovementState == AgentMovementState.Dashing)
                    {
                        physicsState.MovementState = AgentMovementState.None;
                        GameState.AgentIKSystem.SetIKEnabled(true);
                        if (entity.agentModel3D.RifleIKBodyParts[4] != null ||
                            entity.agentModel3D.PistolIKBodyParts[4] != null)
                        {
                            entity.agentModel3D.RifleIKBodyParts[4].gameObject.SetActive(true);
                            entity.agentModel3D.PistolIKBodyParts[4].gameObject.SetActive(true);
                        }
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
                        case AgentMovementState.MonsterAttack:
                        {
                            physicsState.MovementState = AgentMovementState.None;
                            physicsState.ActionInProgress = false;
                            physicsState.ActionJustEnded = true;
                            break;
                        }
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
                /*if (System.Math.Abs(physicsState.Velocity.X) <= 6.0f && physicsState.MovementState == AgentMovementState.Dashing)
                {
                    physicsState.MovementState = AgentMovementState.None;
                    physicsState.Invulnerable = false;
                    physicsState.AffectedByGravity = true;
                }*/

                if (physicsState.MovementState == AgentMovementState.Dashing)
                {
                    if (physicsState.OnGrounded)
                    {
                        if (physicsState.MovingDirection != 0)
                        {
                            physicsState.Velocity = (Physics.Constants.DashSpeedMultiplier - 1.0f * (1.0f - (Physics.Constants.DashTime - physicsState.DashDuration))) * physicsState.Speed * new Vec2f(physicsState.GroundNormal.Y, -physicsState.GroundNormal.X);
                            physicsState.Velocity *= physicsState.MovingDirection;
                        }
                        else
                        {
                            physicsState.Velocity = (Physics.Constants.DashSpeedMultiplier - 1.0f * (1.0f - (Physics.Constants.DashTime  - physicsState.DashDuration))) * physicsState.Speed * new Vec2f(physicsState.GroundNormal.Y, -physicsState.GroundNormal.X);
                            physicsState.Velocity *= 1;
                        }
                    }
                    else
                    {
                        physicsState.Velocity.X = (Physics.Constants.DashSpeedMultiplier - 1.0f * (1.0f - (Physics.Constants.DashTime  - physicsState.DashDuration))) * physicsState.Speed * physicsState.MovingDirection;
                        physicsState.Velocity.Y = 0.0f;
                    }        
                }

                // if we are dashing we add some particles
                if (physicsState.MovementState == AgentMovementState.Dashing)
                {
                    //planet.AddParticleEmitter(particlesSpawnPosition, Particle.ParticleEmitterType.DustEmitter);
                    planet.AddParticleEffect(particlesSpawnPosition, Enums.ParticleEffect.Smoke_2);
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

                    var FuelUsageRate = 30f * deltaTime;
                    stats.Fuel -= (int)FuelUsageRate;
                    
                    if (stats.Fuel <= 1)
                    {
                        stats.Fuel -= 10;
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
                        var FuelAddRate = 30f * deltaTime;
                        stats.Fuel += (int)FuelAddRate;
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

                AgentPropertiesTemplate properties = GameState.AgentCreationApi.Get((int)IDComponent.Type);

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
    }
}
