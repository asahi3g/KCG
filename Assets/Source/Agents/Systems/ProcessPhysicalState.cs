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


                if (entity.isAgentPlayer)
                {
//                    UnityEngine.Debug.Log(physicsState.MovementState);
                }

                float MaximumVelocityToFall = Physics.Constants.MaximumVelocityToFall;

                if (physicsState.MovementState != AgentMovementState.SlidingLeft &&
                physicsState.MovementState != AgentMovementState.SlidingRight)
                {
                    if (physicsState.Velocity.Y <= -MaximumVelocityToFall && entity.IsStateFree() && !physicsState.OnGrounded)
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

                

                // update the jumping state
                if (physicsState.MovementState != AgentMovementState.Falling && 
                    entity.IsStateFree())
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

                // decrease the roll cooldown
                if (physicsState.RollCooldown > 0)
                {
                    physicsState.RollCooldown -= deltaTime;
                }
                else
                {
                    if (physicsState.MovementState == AgentMovementState.Rolling)
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
                        if (GameState.Planet.Player.agentAgent3DModel.Renderer.GetPivotRifle() != null ||
                            GameState.Planet.Player.agentAgent3DModel.Renderer.GetPivotPistol() != null)
                        {
                            GameState.Planet.Player.agentAgent3DModel.Renderer.GetPivotRifle().gameObject.SetActive(true);
                            GameState.Planet.Player.agentAgent3DModel.Renderer.GetPivotPistol().gameObject.SetActive(true);
                        }
                    }
                }

                physicsState.ActionCooldown -= deltaTime;
                physicsState.TimeBetweenMoves += deltaTime; 


                if (physicsState.CurerentMoveList != Enums.AgentMoveList.Error)
                {
                    var moveList = GameState.AgentMoveListPropertiesManager.GetPosition(physicsState.CurerentMoveList);
                    var moveListProperties = GameState.AgentMoveListPropertiesManager.Get(moveList.Offset + physicsState.MoveIndex);
                    if (physicsState.TimeBetweenMoves > moveListProperties.MaxDelay)
                    {
                        physicsState.MoveIndex = 0;
                        physicsState.CurerentMoveList = Enums.AgentMoveList.Error;
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
                        case AgentMovementState.UseTool:
                        case AgentMovementState.Drink:
                        case AgentMovementState.PickaxeHit:
                        case AgentMovementState.ChoppingTree:
                        {
                            physicsState.MovementState = AgentMovementState.None;
                            physicsState.ActionInProgress = false;
                            physicsState.ActionJustEnded = true;
                            break;
                        }
                        case AgentMovementState.FireGun:
                        {
                            physicsState.MovementState = AgentMovementState.IdleAfterShooting;
                            physicsState.IdleAfterShootingTime = Agent.Constants.IdleAfterShootingTime;
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
                        case AgentMovementState.SwordSlash:
                        {
                            var box2dCollider = entity.physicsBox2DCollider;

                            if (!physicsState.OnGrounded)
                            {
                                physicsState.MovementState = Enums.AgentMovementState.Falling;
                            }
                            else
                            {
                                physicsState.MovementState = AgentMovementState.None;
                            }
                            
                            physicsState.ActionInProgress = false;
                            physicsState.ActionJustEnded = true;
                            physicsState.AffectedByGravity = true;

                            physicsState.TimeBetweenMoves = 0.0f;

                            if (physicsState.CurerentMoveList != Enums.AgentMoveList.Error)
                            {
                                var moveList = GameState.AgentMoveListPropertiesManager.GetPosition(physicsState.CurerentMoveList);
                                var moveListProperties = GameState.AgentMoveListPropertiesManager.Get(moveList.Offset + physicsState.MoveIndex);
                                    if (physicsState.MoveIndex == (moveList.Size - 1))
                                {
                                    physicsState.MoveIndex = 0;
                                    physicsState.CurerentMoveList = Enums.AgentMoveList.Error;
                                }
                            }

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
                if (physicsState.OnGrounded && physicsState.Velocity.Y < Physics.Constants.MinimumJumpThreshold)
                {
                    physicsState.JumpCounter = 0;
                }


                Vec2f particlesSpawnPosition = entity.GetFeetParticleSpawnPosition();



                // move the agent along the normal of the surface
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


                // if we are sliding
                // spawn some particles and limit vertical movement
                if (physicsState.MovementState == AgentMovementState.SlidingRight)
                {
                    physicsState.SlidingTime += deltaTime;
                    physicsState.JumpCounter = 0;
                    physicsState.Acceleration.Y = 0.0f;
                    if (physicsState.SlidingTime < Agent.Constants.TimeToStartSliding)
                    {
                        physicsState.Velocity.Y = 0.0f;
                    }
                    else
                    {
                        physicsState.Velocity.Y = Agent.Constants.SlidingYVelocity;
                        //planet.AddParticleEmitter(physicsState.Position + new Vec2f(0.0f, -0.5f), Particle.ParticleEmitterType.DustEmitter);
                        planet.AddParticleEmitter(physicsState.Position + new Vec2f(0.0f, -0.5f), Particle.ParticleEmitterType.DustEmitter); 
                    }
                    physicsState.AffectedByGravity = false;
                    
                }
                else if (physicsState.MovementState == AgentMovementState.SlidingLeft)
                {
                    physicsState.SlidingTime += deltaTime;

                    physicsState.JumpCounter = 0;
                    physicsState.Acceleration.Y = 0.0f;
                    if (physicsState.SlidingTime < Agent.Constants.TimeToStartSliding)
                    {
                        physicsState.Velocity.Y = 0.0f;
                    }
                    else
                    {
                        physicsState.Velocity.Y = Agent.Constants.SlidingYVelocity;
                        //planet.AddParticleEmitter(physicsState.Position + new Vec2f(-0.5f, -0.35f), Particle.ParticleEmitterType.DustEmitter);
                    }

                    if (physicsState.Velocity.Y >= Physics.Constants.MaximumVelocityToFall ||
                    physicsState.Velocity.Y <= -Physics.Constants.MaximumVelocityToFall)
                    {
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
                    physicsState.Velocity.Y = Agent.Constants.JetPackYVelocity;

                    // Reduce the fuel and spawn particles

                    var FuelUsage = Agent.Constants.StandardFuelConsumptionPerSecond * deltaTime;
                    stats.Fuel.Remove((int)FuelUsage);
                    
                    if (stats.Fuel.GetValue() <= 1f)
                    {
                        stats.Fuel.Remove(10);
                    }

                    //planet.AddParticleEmitter(particlesSpawnPosition, Particle.ParticleEmitterType.DustEmitter);
                    planet.AddParticleEmitter(particlesSpawnPosition, Particle.ParticleEmitterType.DustEmitter); 

                }
                else
                {
                    // make sure the fuel never goes up more than it should
                    if (stats.Fuel.GetValue() <= stats.Fuel.GetMax())
                    {
                        // if we are not JetPackFlying, add fuel to the tank
                        var FuelUsage = Agent.Constants.StandardFuelConsumptionPerSecond * deltaTime;
                        stats.Fuel.Add((int)FuelUsage);
                    }
                    else
                    {
                        stats.Fuel.SetAsMax();
                    }
                }
                


                if (physicsState.MovementState == AgentMovementState.Idle || 
                physicsState.MovementState == AgentMovementState.None &&
                 entity.IsStateFree())
                {
                    if (physicsState.Velocity.X >= physicsState.Speed * Physics.Constants.MinimumVelocitySpeedRatioForMovement||
                    physicsState.Velocity.X <= -physicsState.Speed * Physics.Constants.MinimumVelocitySpeedRatioForMovement)
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
                    if (physicsState.Velocity.X >= physicsState.Speed * Physics.Constants.MinimumVelocitySpeedRatioForMovement ||
                    physicsState.Velocity.X <= -physicsState.Speed * Physics.Constants.MinimumVelocitySpeedRatioForMovement)
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
                    box2DComponent.Size.Y = properties.CollisionDimensions.Y * Agent.Constants.CrouchingHeightRatio;
                }
                else if (physicsState.MovementState == AgentMovementState.Rolling)
                {
                    box2DComponent.Size.Y = properties.CollisionDimensions.Y * Agent.Constants.RollingHeightRatio;
                }
                else
                {
                    box2DComponent.Size.Y = properties.CollisionDimensions.Y * 1.0f;
                }
            }
        }
    }
}
