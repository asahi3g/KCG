using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agent
{
    public partial class AgentMovementAnimationTable
    {
        public void InitInsectAnimations()
        {
            SetAnimation(Enums.AgentMovementState.Move, Enums.AgentAnimationType.GroundInsectAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectRun, FadeTime = 0.125f, Looping = true, Speed = 2.0f, MovementSpeedFactor = 1.0f });

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.GroundInsectAnimation, Enums.ItemAnimationSet.Default,
           new AgentAnimation { Animation = Engine3D.AnimationType.InsectRun, FadeTime = 0.125f, Looping = true, Speed = 2.0f, MovementSpeedFactor = 1.0f });

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.GroundInsectAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectIdle, FadeTime = 0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.GroundInsectAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectJump, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.GroundInsectAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectFalling, FadeTime = 0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.MonsterAttack, Enums.AgentAnimationType.GroundInsectAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectAttack, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.GroundInsectAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectDie, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });


            SetAnimation(Enums.AgentMovementState.Move, Enums.AgentAnimationType.GroundInsectHeavyAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectHeavyRun, FadeTime = 0.125f, Looping = true, Speed = 2.0f, MovementSpeedFactor = 1.0f });

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.GroundInsectHeavyAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectHeavyIdle, FadeTime = 0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.GroundInsectHeavyAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectHeavyJump, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.GroundInsectHeavyAnimation, Enums.ItemAnimationSet.Default,
           new AgentAnimation { Animation = Engine3D.AnimationType.InsectHeavyFall, FadeTime = 0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.GroundInsectHeavyAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectHeavyJump, FadeTime = 0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.MonsterAttack, Enums.AgentAnimationType.GroundInsectHeavyAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectHeavyAttack, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.GroundInsectHeavyAnimation, Enums.ItemAnimationSet.Default,
            new AgentAnimation { Animation = Engine3D.AnimationType.InsectHeavyDie, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

        }
    }
}
