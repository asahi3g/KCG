namespace Agent
{



    public partial class AgentMovementAnimationTable
    {


        public void InitHumanLightAnimations()
        {
            SetAnimation(Enums.AgentMovementState.Move, Enums. AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJog, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightWalkBackward, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightIdle, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightCrouch_Walk, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_MoveBackward, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightCrouch_WalkBackwards, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDash, FadeTime=0.015f, Looping = false, Speed = 2.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.PickaxeHit, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0,
                new AgentAnimation { Animation = Engine3D.AnimationType.HumanLightPickaxeHit, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJetPack, FadeTime=0.125f, Looping = true, Speed = 3.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightKnockedDownFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightLyingFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.Default, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});






            // sword
            SetAnimation(Enums.AgentMovementState.Move, Enums. AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJog, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightWalkBackward, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightIdle, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightCrouch_Walk, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_MoveBackward, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightCrouch_WalkBackwards, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDash, FadeTime=0.015f, Looping = false, Speed = 2.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword,  0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.PickaxeHit, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
                new AgentAnimation { Animation = Engine3D.AnimationType.HumanLightPickaxeHit, FadeTime = 0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f });

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJetPack, FadeTime=0.125f, Looping = true, Speed = 3.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightKnockedDownFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightLyingFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0,
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SwordSlash, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSwordAttack_1, FadeTime=0.00f, Looping = false, Speed = 4.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SwordSlash, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 1, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSwordAttack_1, FadeTime=0.0f, Looping = false, Speed = 4.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SwordSlash, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingSword, 2, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDash, FadeTime=0.0f, Looping = false, Speed = 4.0f, MovementSpeedFactor = 0.0f});




            //rifle


            SetAnimation(Enums.AgentMovementState.Move, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleJog, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightWalkBackward, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleIdle, FadeTime=0.125f, Looping = false, Speed = 1.0f, StartTime = 0.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.IdleAfterShooting, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleIdleAlerted, FadeTime=0.125f, Looping = false, Speed = 1.0f, StartTime = 0.43f, MovementSpeedFactor = 0.0f});

             SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});


            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleCrouch_Walk, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_MoveBackward, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleCrouch_WalkBackwards, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRifleJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDash, FadeTime=0.015f, Looping = false, Speed = 2.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.FireGun, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0,
            new AgentAnimation
            {
                Animation = Engine3D.AnimationType.HumanLightRifleFireGun,
                FadeTime = 9999.075f,
                Looping = true,
                Speed = 0.1f,
                StartTime = 0.0f,
                MovementSpeedFactor = 0.1f,
                UseActionDurationForSpeed = true
            });

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightJetPack, FadeTime=0.125f, Looping = true, Speed = 3.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightKnockedDownFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightLyingFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingRifle, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});
















            // pistol
            SetAnimation(Enums.AgentMovementState.Move, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightPistolJog, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.MoveBackward, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightWalkBackward, FadeTime=0.125f, Looping = true, Speed = 0.5f, MovementSpeedFactor = 1.0f});

            SetAnimation(Enums.AgentMovementState.Idle, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightPistolIdle, FadeTime=0.125f, Looping = false, Speed = 1.0f, StartTime = 0.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.IdleAfterShooting, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightPistolIdleAlerted, FadeTime=0.125f, Looping = false, Speed = 1.0f, StartTime = 0.43f, MovementSpeedFactor = 0.0f});

             SetAnimation(Enums.AgentMovementState.Rolling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightRoll, FadeTime=0.125f, Looping = false, Speed = 1.5f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.StandingUpAfterRolling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightIdle, FadeTime=1.425f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightPistolCrouch, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Crouch_Move, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0,
            new AgentAnimation { Animation = Engine3D.AnimationType.HumanLightPistolCrouch_Walk, FadeTime = 0.175f, Looping = false, Speed =
                1.0f, MovementSpeedFactor = 1.0f, UseActionDurationForSpeed = true });

            SetAnimation(Enums.AgentMovementState.Crouch_MoveBackward, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightCrouch_WalkBackwards, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Jump, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightPistolJump, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Flip, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightPistolFlip, FadeTime=0.125f, Looping = false, Speed = 1.2f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Falling, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightPistolJumpFall, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Dashing, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDash, FadeTime=0.015f, Looping = false, Speed = 2.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Drink, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightDrink, FadeTime=0.125f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.Limp, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightLimp, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.FireGun, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightPistolFireGun, FadeTime=0.175f, Looping = false, Speed = 1.0f, MovementSpeedFactor = 1.0f, UseActionDurationForSpeed = true});

            SetAnimation(Enums.AgentMovementState.JetPackFlying, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0,
            new AgentAnimation { Animation = Engine3D.AnimationType.HumanLightPistolFireGun, FadeTime = 0.175f, Looping = false,
                Speed = 1.0f, MovementSpeedFactor = 1.0f, UseActionDurationForSpeed = true });

            SetAnimation(Enums.AgentMovementState.KnockedDownFront, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightKnockedDownFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.LyingFront, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightLyingFront, FadeTime=1.125f, Looping = false, Speed = 0.6f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingLeft, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

            SetAnimation(Enums.AgentMovementState.SlidingRight, Enums.AgentAnimationType.HumanLightAnimations, Enums.ItemAnimationSet.HoldingPistol, 0, 
            new AgentAnimation{Animation=Engine3D.AnimationType.HumanLightSliding, FadeTime=0.125f, Looping = true, Speed = 1.0f, MovementSpeedFactor = 0.0f});

        }
    }
}