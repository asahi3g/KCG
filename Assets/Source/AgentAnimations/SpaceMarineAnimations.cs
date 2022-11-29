namespace Agent
{



    public partial class AgentMovementAnimationTable
    {


        public void InitSpaceMarineAnimations()
        {
            SetAnimation(Enums.AgentMovementState.Move, Enums. AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJog, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineWalkBackward, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineIdle, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch_Walk, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch_WalkBackwards, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDash, FadeTime=0.015f, Looping = false, Speed = 2.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.PickaxeHit, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0,
                new AgentAnimation { Animation = Engine3D.AnimationType.SpaceMarinePickaxeHit, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJetPack, FadeTime=0.125f, Looping = true, Speed = 3.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineKnockedDownFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLyingFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});






            // sword
            SetAnimation(Enums.AgentMovementState.Move, Enums. AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJog, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineWalkBackward, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineIdle, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch_Walk, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch_WalkBackwards, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDash, FadeTime=0.015f, Looping = false, Speed = 2.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.PickaxeHit, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
                new AgentAnimation { Animation = Engine3D.AnimationType.SpaceMarinePickaxeHit, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJetPack, FadeTime=0.125f, Looping = true, Speed = 3.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineKnockedDownFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLyingFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SwordSlash, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.0f, Looping = true, Speed = 5.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SwordSlash, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 1, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.0f, Looping = true, Speed = 5.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SwordSlash, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingSword, 2, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.0f, Looping = true, Speed = 5.0f, MovementSpeedFactor = 0.0f});




            //rifle


            SetAnimation(Enums.AgentMovementState.Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleJog, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineWalkBackward, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleIdle, FadeTime=0.125f, Looping = false, Speed = 1.0f, StartTime = 0.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.IdleAfterShooting, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleIdleAlerted, FadeTime=0.125f, Looping = false, Speed = 1.0f, StartTime = 0.43f, MovementSpeedFactor = 0.0f});

             SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});


            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleCrouch_Walk, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleCrouch_WalkBackwards, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRifleJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDash, FadeTime=0.015f, Looping = false, Speed = 2.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.FireGun, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0,
            new AgentAnimation
            {
                Animation = Engine3D.AnimationType.SpaceMarineRifleFireGun,
                FadeTime = 9999.075f,
                Looping = true,
                Speed = 0.1f,
                StartTime = 0.0f,
                MovementSpeedFactor = 0.1f,
                UseActionDurationForSpeed = true
            });

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineJetPack, FadeTime=0.125f, Looping = true, Speed = 3.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineKnockedDownFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLyingFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});
















            // pistol
            SetAnimation(Enums.AgentMovementState.Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarinePistolJog, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineWalkBackward, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarinePistolIdle, FadeTime=0.125f, Looping = false, Speed = 1.0f, StartTime = 0.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.IdleAfterShooting, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarinePistolIdleAlerted, FadeTime=0.125f, Looping = false, Speed = 1.0f, StartTime = 0.43f, MovementSpeedFactor = 0.0f});

             SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarinePistolCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0,
            new AgentAnimation { Animation = Engine3D.AnimationType.SpaceMarinePistolCrouch_Walk, FadeTime = 0.175f, Looping = false, Speed =
                1.0f, MovementSpeedFactor = 1.0f, UseActionDurationForSpeed = true });

            SetAnimation(Enums.AgentMovementState.Crouch_MoveBackward, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineCrouch_WalkBackwards, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarinePistolJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarinePistolFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarinePistolJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDash, FadeTime=0.015f, Looping = false, Speed = 2.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.FireGun, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarinePistolFireGun, FadeTime=0.175f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 1.0f, UseActionDurationForSpeed = true});

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0,
            new AgentAnimation { Animation = Engine3D.AnimationType.SpaceMarinePistolFireGun, FadeTime = 0.175f, Looping = false,
                Speed = 1.0f, MovementSpeedFactor = 1.0f, UseActionDurationForSpeed = true });

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineKnockedDownFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineLyingFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.SpaceMarineSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

        }
    }
}