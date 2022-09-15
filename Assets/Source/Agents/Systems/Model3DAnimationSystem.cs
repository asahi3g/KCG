using System;
using KMath;
using UnityEngine;
using Enums;

namespace Agent
{
    public class Model3DAnimationSystem
    {
        public void Update(AgentContext agentContext)
        {
            
            float deltaTime = Time.deltaTime;
            var entities = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
            foreach (var entity in entities)
            {
                var physicsState = entity.agentPhysicsState;        
                var model3d = entity.agentModel3D;


                /*if (entity.isAgentPlayer)
                {
                    Debug.Log(physicsState.MovementState);
                }*/



                Animancer.AnimancerState currentClip = null;
                if (model3d.AnimationType == Enums.AgentAnimationType.HumanoidAnimation)
                {
                    switch(physicsState.MovementState)
                    {
                        case AgentMovementState.Move:
                        {
                            if (System.Math.Abs(physicsState.Velocity.X) > 7.0f)
                            {
                                if (physicsState.MovingDirection != physicsState.FacingDirection)
                                {
                                    AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.WalkBack);
                                    currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                                }
                                else
                                {
                                    AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Run);
                                    currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                                }
                            }
                            else
                            {
                                if (physicsState.MovingDirection != physicsState.FacingDirection)
                                {
                                    AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.WalkBack);
                                    currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                                }
                                else
                                {
                                    AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Jog);
                                    currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                                }
                            }
                            break;
                        }
                        case AgentMovementState.Idle:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Limp:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Limp);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Drink:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Drink);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.JetPackFlying:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.JetPack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.SlidingLeft:
                        case AgentMovementState.SlidingRight:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Sliding);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Dashing:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Dash);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.Rolling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Roll);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Crouch:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Crouch);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Crouch_Move:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Crouch_Walk);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.StandingUpAfterRolling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 1.425f);
                            break;
                        }
                        case AgentMovementState.Stagger:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Stagger);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.Falling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.JumpFall);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.SwordSlash:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SwordSlash);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.FireGun:
                        {
                            float speed =  0.6f / physicsState.ActionCooldown;
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.FireGun);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            currentClip.Speed = speed;
                            break;
                        }
                        case AgentMovementState.UseTool:
                        {
                            float speed =  0.6f / physicsState.ActionCooldown;
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.UseTool);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            currentClip.Speed = speed;
                            break;
                        }
                        case AgentMovementState.KnockedDownFront:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.KnockedDownFront);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.LyingFront:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.LyingFront);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.KnockedDownBack:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.KnockedDownBack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.LyingBack:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.LyingBack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        default:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                    }

                    if (physicsState.MovementState != AgentMovementState.Falling && 
                    physicsState.MovementState != AgentMovementState.Dashing)
                    {
                        if (physicsState.JumpCounter == 1)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Jump);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                        else if (physicsState.JumpCounter == 2)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Flip);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                    }
                }
                else if (model3d.AnimationType == Enums.AgentAnimationType.GroundInsectAnimation)
                {
                    switch(physicsState.MovementState)
                    {
                        case AgentMovementState.Move:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectRun);  
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);  
                            break;
                        }
                        case AgentMovementState.Limp:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectRun);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Idle:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.MonsterAttack:
                        case AgentMovementState.SwordSlash:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectAttack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.Falling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectRun);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.LyingFront:
                        case AgentMovementState.LyingBack:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectDie);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                    }

                     if (physicsState.MovementState != AgentMovementState.Falling && 
                    physicsState.MovementState != AgentMovementState.Dashing)
                    {
                        if (physicsState.JumpCounter == 1)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectJump);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                        else if (physicsState.JumpCounter == 2)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectJump);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                    }
                }
                else if (model3d.AnimationType == Enums.AgentAnimationType.GroundInsectHeavyAnimation)
                {
                    switch(physicsState.MovementState)
                    {
                        case AgentMovementState.Move:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectHeavyRun);  
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);  
                            break;
                        }
                        case AgentMovementState.Limp:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectHeavyRun);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Idle:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectHeavyIdle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.MonsterAttack:
                        case AgentMovementState.SwordSlash:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectHeavyAttack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.Falling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectHeavyRun);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.LyingFront:
                        case AgentMovementState.LyingBack:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectHeavyDie);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                    }

                     if (physicsState.MovementState != AgentMovementState.Falling && 
                    physicsState.MovementState != AgentMovementState.Dashing)
                    {
                        if (physicsState.JumpCounter == 1)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectHeavyJump);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                        else if (physicsState.JumpCounter == 2)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectHeavyJump);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                    }
                }
                else if (model3d.AnimationType == Enums.AgentAnimationType.SpaceMarineAnimations)
                {
                    switch(physicsState.MovementState)
                    {
                        case AgentMovementState.Move:
                        {
                            if (System.Math.Abs(physicsState.Velocity.X) > 7.0f)
                            {
                                AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineRun);
                                currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            }
                            else
                            {
                                AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineJog);
                                currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            }
                            break;
                        }
                        case AgentMovementState.Idle:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineIdle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Limp:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineLimp);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Drink:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineDrink);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.JetPackFlying:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineJetPack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.SlidingLeft:
                        case AgentMovementState.SlidingRight:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineSliding);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Dashing:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineDash);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.Rolling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineRoll);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            currentClip.Speed = 1.0f;
                            break;
                        }
                        case AgentMovementState.Crouch:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineCrouch);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Crouch_Move:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineCrouch_Walk);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.StandingUpAfterRolling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineIdle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 1.425f);
                            break;
                        }
                        case AgentMovementState.Stagger:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineStagger);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.Falling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineJumpFall);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.SwordSlash:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineSwordSlash);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.FireGun:
                        {
                            float speed =  0.6f / physicsState.ActionCooldown;
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineFireGun);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            currentClip.Speed = speed;
                            break;
                        }
                        case AgentMovementState.UseTool:
                        {
                            float speed =  0.6f / physicsState.ActionCooldown;
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineUseTool);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            currentClip.Speed = speed;
                            break;
                        }
                        case AgentMovementState.KnockedDownFront:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineKnockedDownFront);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.LyingFront:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineLyingFront);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.KnockedDownBack:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineKnockedDownBack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.LyingBack:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineLyingBack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        default:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineIdle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                    }

                    if (physicsState.MovementState != AgentMovementState.Falling && 
                    physicsState.MovementState != AgentMovementState.Dashing)
                    {
                        if (physicsState.JumpCounter == 1)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineJump);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                        else if (physicsState.JumpCounter == 2)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineFlip);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                    }
                }
                else if (model3d.AnimationType == Enums.AgentAnimationType.GroundInsectAnimation)
                {
                    switch(physicsState.MovementState)
                    {
                        case AgentMovementState.Move:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectRun);  
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);  
                            break;
                        }
                        case AgentMovementState.Limp:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectRun);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.Idle:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                            break;
                        }
                        case AgentMovementState.MonsterAttack:
                        case AgentMovementState.SwordSlash:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectAttack);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.Falling:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectRun);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                        case AgentMovementState.LyingFront:
                        case AgentMovementState.LyingBack:
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectDie);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.075f);
                            break;
                        }
                    }

                     if (physicsState.MovementState != AgentMovementState.Falling && 
                    physicsState.MovementState != AgentMovementState.Dashing)
                    {
                        if (physicsState.JumpCounter == 1)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectJump);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                        else if (physicsState.JumpCounter == 2)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.InsectJump);
                            currentClip = model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                    }
                }

                if (currentClip != null && physicsState.SetMovementState)
                {
                    physicsState.SetMovementState = false;
                    currentClip.Time = 0.0f;
                }
            }
        }
    }
}
