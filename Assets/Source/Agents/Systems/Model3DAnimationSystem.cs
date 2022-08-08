using System;
using KMath;
using UnityEngine;

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

                var movementState = entity.agentMovementState;        
                var model3d = entity.agentModel3D;

                Debug.Log(movementState.MovementState);

                switch(movementState.MovementState)
                {
                    case MovementState.Move:
                    {
                        if (movementState.Running)
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
                    case MovementState.Idle:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case MovementState.Flying:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case MovementState.Sliding:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case MovementState.Dashing:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Run);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                        break;
                    }
                    case MovementState.Falling:
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.JumpFall);
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
                if (movementState.MovementState != MovementState.Falling)
                {
                    if (movementState.JumpCounter == 1)
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Jump);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                    }
                    else if (movementState.JumpCounter == 2)
                    {
                        AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Flip);
                        model3d.AnimancerComponent.Play(animation, 0.125f);
                    }
                }
            }
        }
    }
}
