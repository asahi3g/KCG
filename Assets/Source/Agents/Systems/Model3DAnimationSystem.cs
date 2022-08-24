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

                switch(physicsState.MovementState)
                {
                    case AgentMovementState.Move:
                    {
                        if (System.Math.Abs(physicsState.Velocity.X) > 7.0f)
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Run);
                            model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                        else
                        {
                            AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Jog);
                            model3d.AnimancerComponent.Play(animation, 0.125f);
                        }
                        break;
                    }
                    case AgentMovementState.Idle:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.Limp:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Limp);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.Drink:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Drink);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.JetPackFlying:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.JetPack);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.SlidingLeft:
                    case AgentMovementState.SlidingRight:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Sliding);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.Dashing:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Dash);
                        model3d.AnimancerComponent.Play(animation, 0.075f);
                        break;
                    }
                    case AgentMovementState.Rolling:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Roll);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.Crouch:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Crouch);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.Crouch_Move:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Crouch_Walk);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.StandingUpAfterRolling:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 1.425f);
                        break;
                    }
                    case AgentMovementState.Stagger:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Stagger);
                        model3d.AnimancerComponent.Play(animation, 0.075f);
                        break;
                    }
                    case AgentMovementState.Falling:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.JumpFall);
                        model3d.AnimancerComponent.Play(animation, 0.075f);
                        break;
                    }
                    case AgentMovementState.SwordSlash:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SwordSlash);
                        model3d.AnimancerComponent.Play(animation, 0.075f);
                        break;
                    }
                    case AgentMovementState.FireGun:
                    {
                        float speed =  0.6f / physicsState.GunCooldown;
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.FireGun);
                        var state = model3d.AnimancerComponent.Play(animation, 0.075f);
                        state.Speed = speed;
                        break;
                    }
                    case AgentMovementState.UseTool:
                    {
                        float speed =  0.6f / physicsState.ToolCooldown;
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.UseTool);
                        var state = model3d.AnimancerComponent.Play(animation, 0.075f);
                        state.Speed = speed;
                        break;
                    }
                    case AgentMovementState.KnockedDownFront:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.KnockedDownFront);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.LyingFront:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.LyingFront);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.KnockedDownBack:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.KnockedDownBack);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.LyingBack:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.LyingBack);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    default:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                }

                if (physicsState.MovementState != AgentMovementState.Falling && 
                physicsState.MovementState != AgentMovementState.Dashing)
                {
                    if (physicsState.JumpCounter == 1)
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Jump);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                    }
                    else if (physicsState.JumpCounter == 2)
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Flip);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                    }
                }
            }
        }
    }
}
