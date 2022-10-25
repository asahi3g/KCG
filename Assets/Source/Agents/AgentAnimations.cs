using Engine3D;

namespace Agent
{
    public class AgentAnimations
    {
        private AnimationLoader animationLoader;

        public AgentAnimations(AnimationLoader animationLoader)
        {
            this.animationLoader = animationLoader;
        }

        public void LoadAnimations()
        {
            // Humanoid
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Idle", AnimationType.Idle);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Run", AnimationType.Run);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Walk_F", AnimationType.Walk);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Walk_B", AnimationType.WalkBack);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jump", AnimationType.Jump);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jump_Roll", AnimationType.Flip);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jog", AnimationType.Jog);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Dodge", AnimationType.Dash);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@DodgeRoll", AnimationType.Roll);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Crouch", AnimationType.Crouch);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Crouch_Walk", AnimationType.Crouch_Walk);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@Slowed", AnimationType.Limp);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.Drink);
            //animationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.PickaxeHit);
            //animationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.ChopTree);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@JumpFall", AnimationType.JumpFall);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/10_Rapier/Stander@Rapier_Attack1", AnimationType.SwordSlash);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/05_Pistol/Stander@Pistol_Attack1", AnimationType.FireGun);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Throw1", AnimationType.UseTool);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Idle2", AnimationType.Sliding);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Sit1", AnimationType.JetPack);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@Damage2", AnimationType.Stagger);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@KnockDown_F_Light", AnimationType.KnockedDownFront);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@LyingFront", AnimationType.LyingFront);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@KnockDown_B_Light", AnimationType.KnockedDownBack);
            animationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@LyingBack", AnimationType.LyingBack);

            // Insect
            animationLoader.Load("ArtistAnimations/Insect/InsectIdle", AnimationType.InsectIdle);
            animationLoader.Load("ArtistAnimations/Insect/InsectRun", AnimationType.InsectRun);
            animationLoader.Load("ArtistAnimations/Insect/InsectAttack", AnimationType.InsectAttack);
            animationLoader.Load("ArtistAnimations/Insect/InsectJump", AnimationType.InsectJump);
            animationLoader.Load("ArtistAnimations/Insect/InsectDeath", AnimationType.InsectDie);
            animationLoader.Load("ArtistAnimations/Insect/InsectFalling", AnimationType.InsectFalling);

            // Heavy Insect
            animationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyIdle", AnimationType.InsectHeavyIdle);
            animationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyRun", AnimationType.InsectHeavyRun);
            animationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyAttack", AnimationType.InsectHeavyAttack);
            animationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyJump", AnimationType.InsectHeavyJump);
            animationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyFall", AnimationType.InsectHeavyFall);
            animationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyDead", AnimationType.InsectHeavyDie);


            // Medium Marine
            animationLoader.Load("ArtistAnimations/MediumMarine/Idle", AnimationType.SpaceMarineIdle);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunIdle", AnimationType.SpaceMarineRifleIdle);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolIdle", AnimationType.SpaceMarinePistolIdle);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunAlerted", AnimationType.SpaceMarineRifleIdleAlerted);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolAlerted", AnimationType.SpaceMarinePistolIdleAlerted);
            animationLoader.Load("ArtistAnimations/MediumMarine/Run", AnimationType.SpaceMarineRun);
            animationLoader.Load("ArtistAnimations/MediumMarine/DEFORM_Walking", AnimationType.SpaceMarineWalk);
            animationLoader.Load("ArtistAnimations/MediumMarine/Jump", AnimationType.SpaceMarineJump);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunJump", AnimationType.SpaceMarineRifleJump);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolJump", AnimationType.SpaceMarinePistolJump);
            animationLoader.Load("ArtistAnimations/MediumMarine/JumpRoll", AnimationType.SpaceMarineFlip);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunJumpRoll", AnimationType.SpaceMarineRifleFlip);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolJumpRoll", AnimationType.SpaceMarinePistolFlip);
            animationLoader.Load("ArtistAnimations/MediumMarine/Walking", AnimationType.SpaceMarineJog);
            animationLoader.Load("ArtistAnimations/MediumMarine/WalkingBackwards", AnimationType.SpaceMarineWalkBackward);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunWalking", AnimationType.SpaceMarineRifleJog);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolWalking", AnimationType.SpaceMarinePistolJog);
            animationLoader.Load("ArtistAnimations/MediumMarine/Dodge", AnimationType.SpaceMarineDash);
            animationLoader.Load("ArtistAnimations/MediumMarine/Roll", AnimationType.SpaceMarineRoll);
            animationLoader.Load("ArtistAnimations/MediumMarine/CrouchIdle", AnimationType.SpaceMarineCrouch);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunCrouchIdle", AnimationType.SpaceMarineRifleCrouch);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolCrouchIdle", AnimationType.SpaceMarinePistolCrouch);
            animationLoader.Load("ArtistAnimations/MediumMarine/CrouchWalk", AnimationType.SpaceMarineCrouch_Walk);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunCrouchWalk", AnimationType.SpaceMarineRifleCrouch_Walk);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolCrouchWalk", AnimationType.SpaceMarinePistolCrouch_Walk);
            animationLoader.Load("ArtistAnimations/MediumMarine/CrouchWalkBackwards", AnimationType.SpaceMarineCrouch_WalkBackwards);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunCrouchWalkBackwards", AnimationType.SpaceMarineRifleCrouch_WalkBackwards);
            animationLoader.Load("ArtistAnimations/MediumMarine/Limp", AnimationType.SpaceMarineLimp);
            animationLoader.Load("ArtistAnimations/MediumMarine/Drink", AnimationType.SpaceMarineDrink);
            animationLoader.Load("ArtistAnimations/MediumMarine/JumpFall", AnimationType.SpaceMarineJumpFall);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunJumpFall", AnimationType.SpaceMarineRifleJumpFall);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolJumpFall", AnimationType.SpaceMarinePistolJumpFall);
            animationLoader.Load("ArtistAnimations/MediumMarine/DEFORM_SwordAttack", AnimationType.SpaceMarineSwordSlash);
            animationLoader.Load("ArtistAnimations/MediumMarine/DEFORM_Shooting", AnimationType.SpaceMarineFireGun);
            animationLoader.Load("ArtistAnimations/MediumMarine/GunShoot", AnimationType.SpaceMarineRifleFireGun);
            animationLoader.Load("ArtistAnimations/MediumMarine/PistolShoot", AnimationType.SpaceMarinePistolFireGun);
            animationLoader.Load("ArtistAnimations/MediumMarine/Throw", AnimationType.SpaceMarineUseTool);
            animationLoader.Load("ArtistAnimations/MediumMarine/Slide", AnimationType.SpaceMarineSliding);
            animationLoader.Load("ArtistAnimations/MediumMarine/Jetpack", AnimationType.SpaceMarineJetPack);
            animationLoader.Load("ArtistAnimations/MediumMarine/Stagger", AnimationType.SpaceMarineStagger);
            animationLoader.Load("ArtistAnimations/MediumMarine/DEFORM_DeathKnockback", AnimationType.SpaceMarineKnockedDownFront);
            animationLoader.Load("ArtistAnimations/MediumMarine/Death2", AnimationType.SpaceMarineLyingFront);
            //animationLoader.Load("ArtistAnimations/DEFORM_DeathKnockback", AnimationType.SpaceMarineKnockedDownBack);
            //animationLoader.Load("ArtistAnimations/DEFORM_Death", AnimationType.SpaceMarineLyingBack);
        }
    }
}
