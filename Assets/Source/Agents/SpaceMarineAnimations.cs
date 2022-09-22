namespace Agent
{



    public partial class AgentMovementAnimationTable
    {


        public void InitSpaceMarineAnimations()
        {
            SetAnimation(Enums.AgentMovementState.Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJog, FadeTime=0.125f, Looping = true, Speed = 2.0f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineWalkBackward, FadeTime=0.125f, Looping = true, Speed = 2.0f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineIdle, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch_Walk, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDash, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJetPack, FadeTime=0.125f, Looping = true, Speed = 3.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineKnockedDownFront, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLyingFront, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});








            //rifle


            SetAnimation(Enums.AgentMovementState.Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleJog, FadeTime=0.125f, Looping = true, Speed = 2.0f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineWalkBackward, FadeTime=0.125f, Looping = true, Speed = 2.0f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleIdle, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.IdleAfterShooting, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleIdleAlerted, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleCrouch_Walk, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDash, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.FireGun, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleFireGun, FadeTime=0.075f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 1.0f, UseActionDurationForSpeed = true});

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJetPack, FadeTime=0.125f, Looping = true, Speed = 3.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineKnockedDownFront, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLyingFront, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

        }
    }
}