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

                Debug.Log(physicsState.MovementState);

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
                    case AgentMovementState.JetPackFlying:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.SlidingLeft:
                    case AgentMovementState.SlidingRight:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case AgentMovementState.Dashing:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Run);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
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
                        Debug.Log(animation);
                        model3d.AnimancerComponent.Play(animation, 0.075f);
                        break;
                    }
                    default:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                }

                if (physicsState.MovementState != AgentMovementState.Falling)
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
