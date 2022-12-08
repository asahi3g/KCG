using Engine3D;
using Loader;

namespace Agent
{
    public class AgentAnimations
    {
        private AnimationLoader AnimationLoader;

        public AgentAnimations(AnimationLoader animationLoader)
        {
            this.AnimationLoader = animationLoader;
        }

        public void LoadAnimations()
        {
            // Humanoid
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Idle", AnimationType.Idle);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Run", AnimationType.Run);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Walk_F", AnimationType.Walk);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Walk_B", AnimationType.WalkBack);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jump", AnimationType.Jump);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jump_Roll", AnimationType.Flip);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jog", AnimationType.Jog);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Dodge", AnimationType.Dash);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@DodgeRoll", AnimationType.Roll);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Crouch", AnimationType.Crouch);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Crouch_Walk", AnimationType.Crouch_Walk);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@Slowed", AnimationType.Limp);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.Drink);
            //animationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.PickaxeHit);
            //animationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.ChopTree);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@JumpFall", AnimationType.JumpFall);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/10_Rapier/Stander@Rapier_Attack1", AnimationType.SwordSlash);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/05_Pistol/Stander@Pistol_Attack1", AnimationType.FireGun);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Throw1", AnimationType.UseTool);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Idle2", AnimationType.Sliding);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Sit1", AnimationType.JetPack);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@Damage2", AnimationType.Stagger);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@KnockDown_F_Light", AnimationType.KnockedDownFront);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@LyingFront", AnimationType.LyingFront);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@KnockDown_B_Light", AnimationType.KnockedDownBack);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@LyingBack", AnimationType.LyingBack);

            // Insect
            AnimationLoader.Load("ArtistAnimations/Insect/InsectIdle", AnimationType.InsectIdle);
            AnimationLoader.Load("ArtistAnimations/Insect/InsectRun", AnimationType.InsectRun);
            AnimationLoader.Load("ArtistAnimations/Insect/InsectAttack", AnimationType.InsectAttack);
            AnimationLoader.Load("ArtistAnimations/Insect/InsectJump", AnimationType.InsectJump);
            AnimationLoader.Load("ArtistAnimations/Insect/InsectDeath", AnimationType.InsectDie);
            AnimationLoader.Load("ArtistAnimations/Insect/InsectFalling", AnimationType.InsectFalling);

            // Heavy Insect
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyIdle", AnimationType.InsectHeavyIdle);
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyRun", AnimationType.InsectHeavyRun);
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyAttackAttack", AnimationType.InsectHeavyAttack);
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyJump", AnimationType.InsectHeavyJump);
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyFall", AnimationType.InsectHeavyFall);
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyDead", AnimationType.InsectHeavyDie);


            // Medium Marine
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Idle", AnimationType.SpaceMarineIdle);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunIdle", AnimationType.SpaceMarineRifleIdle);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolIdle2", AnimationType.SpaceMarinePistolIdle);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunAlerted", AnimationType.SpaceMarineRifleIdleAlerted);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolAlerted2", AnimationType.SpaceMarinePistolIdleAlerted);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Run", AnimationType.SpaceMarineRun);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/DEFORM_Walking", AnimationType.SpaceMarineWalk);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Jump", AnimationType.SpaceMarineJump);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunJump", AnimationType.SpaceMarineRifleJump);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolJump2", AnimationType.SpaceMarinePistolJump);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/JumpRoll", AnimationType.SpaceMarineFlip);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunJumpRoll", AnimationType.SpaceMarineRifleFlip);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolJumpRoll2", AnimationType.SpaceMarinePistolFlip);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Walking", AnimationType.SpaceMarineJog);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/WalkingBackwards", AnimationType.SpaceMarineWalkBackward);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunWalking", AnimationType.SpaceMarineRifleJog);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolWalking2", AnimationType.SpaceMarinePistolJog);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Dodge", AnimationType.SpaceMarineDash);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Roll", AnimationType.SpaceMarineRoll);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/CrouchIdle", AnimationType.SpaceMarineCrouch);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunCrouchIdle", AnimationType.SpaceMarineRifleCrouch);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolCrouchIdle2", AnimationType.SpaceMarinePistolCrouch);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/CrouchWalk", AnimationType.SpaceMarineCrouch_Walk);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunCrouchWalk", AnimationType.SpaceMarineRifleCrouch_Walk);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolCrouchWalk2", AnimationType.SpaceMarinePistolCrouch_Walk);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/CrouchWalkBackwards", AnimationType.SpaceMarineCrouch_WalkBackwards);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunCrouchWalkBackwards", AnimationType.SpaceMarineRifleCrouch_WalkBackwards);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Limp", AnimationType.SpaceMarineLimp);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Drink", AnimationType.SpaceMarineDrink);
            //AnimationLoader.Load("ArtistAnimations/SpaceMarine/Drink", AnimationType.SpaceMarinePickaxeHit);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/JumpFall", AnimationType.SpaceMarineJumpFall);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunJumpFall", AnimationType.SpaceMarineRifleJumpFall);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolJumpFall2", AnimationType.SpaceMarinePistolJumpFall);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/DEFORM_SwordAttack", AnimationType.SpaceMarineSwordSlash);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/DEFORM_Shooting", AnimationType.SpaceMarineFireGun);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/GunShoot", AnimationType.SpaceMarineRifleFireGun);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SpaceMarinePistolShoot2", AnimationType.SpaceMarinePistolFireGun);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Throw", AnimationType.SpaceMarineUseTool);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Slide", AnimationType.SpaceMarineSliding);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/SwordAttack", AnimationType.SpaceMarineSwordAttack_1);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Jetpack", AnimationType.SpaceMarineJetPack);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Stagger", AnimationType.SpaceMarineStagger);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/DEFORM_DeathKnockback", AnimationType.SpaceMarineKnockedDownFront);
            AnimationLoader.Load("ArtistAnimations/SpaceMarine/Death2", AnimationType.SpaceMarineLyingFront);
            //AnimationLoader.Load("ArtistAnimations/DEFORM_DeathKnockback", AnimationType.SpaceMarineKnockedDownBack);
            //AnimationLoader.Load("ArtistAnimations/DEFORM_Death", AnimationType.SpaceMarineLyingBack);



            // Human Light
            AnimationLoader.Load("Animations/Human Light/Idle", AnimationType.HumanLightIdle);
            AnimationLoader.Load("Animations/Human Light/GunIdle", AnimationType.HumanLightRifleIdle);
            AnimationLoader.Load("Animations/Human Light/PistolIdle", AnimationType.HumanLightPistolIdle);
            AnimationLoader.Load("Animations/Human Light/GunAlerted", AnimationType.HumanLightRifleIdleAlerted);
            AnimationLoader.Load("Animations/Human Light/PistolAlerted", AnimationType.HumanLightPistolIdleAlerted);
            AnimationLoader.Load("Animations/Human Light/Run", AnimationType.HumanLightRun);
            //AnimationLoader.Load("Animations/Human Light/Walking", AnimationType.HumanLightWalk);
            AnimationLoader.Load("Animations/Human Light/Jump", AnimationType.HumanLightJump);
            AnimationLoader.Load("Animations/Human Light/GunJump", AnimationType.HumanLightRifleJump);
            AnimationLoader.Load("Animations/Human Light/PistolJump", AnimationType.HumanLightPistolJump);
            AnimationLoader.Load("Animations/Human Light/JumpRoll", AnimationType.HumanLightFlip);
            AnimationLoader.Load("Animations/Human Light/GunJumpRoll", AnimationType.HumanLightRifleFlip);
            AnimationLoader.Load("Animations/Human Light/PistolJumpRoll", AnimationType.HumanLightPistolFlip);
            AnimationLoader.Load("Animations/Human Light/Walking", AnimationType.HumanLightJog);
            AnimationLoader.Load("Animations/Human Light/WalkingBackwards", AnimationType.HumanLightWalkBackward);
            AnimationLoader.Load("Animations/Human Light/GunWalking", AnimationType.HumanLightRifleJog);
            AnimationLoader.Load("Animations/Human Light/PistolWalking", AnimationType.HumanLightPistolJog);
            AnimationLoader.Load("Animations/Human Light/Dodge", AnimationType.HumanLightDash);
            AnimationLoader.Load("Animations/Human Light/Roll", AnimationType.HumanLightRoll);
            AnimationLoader.Load("Animations/Human Light/CrouchIdle", AnimationType.HumanLightCrouch);
            AnimationLoader.Load("Animations/Human Light/GunCrouchIdle", AnimationType.HumanLightRifleCrouch);
            AnimationLoader.Load("Animations/Human Light/PistolCrouchIdle", AnimationType.HumanLightPistolCrouch);
            AnimationLoader.Load("Animations/Human Light/CrouchWalk", AnimationType.HumanLightCrouch_Walk);
            AnimationLoader.Load("Animations/Human Light/GunCrouchWalk", AnimationType.HumanLightRifleCrouch_Walk);
            AnimationLoader.Load("Animations/Human Light/PistolCrouchWalk", AnimationType.HumanLightPistolCrouch_Walk);
            AnimationLoader.Load("Animations/Human Light/CrouchWalkBackwards", AnimationType.HumanLightCrouch_WalkBackwards);
            AnimationLoader.Load("Animations/Human Light/GunCrouchWalkBackwards", AnimationType.HumanLightRifleCrouch_WalkBackwards);
            AnimationLoader.Load("Animations/Human Light/Limp", AnimationType.HumanLightLimp);
            AnimationLoader.Load("Animations/Human Light/Drink", AnimationType.HumanLightDrink);
            //AnimationLoader.Load("Animations/Human Light/Drink", AnimationType.HumanLightPickaxeHit);
            AnimationLoader.Load("Animations/Human Light/JumpFall", AnimationType.HumanLightJumpFall);
            AnimationLoader.Load("Animations/Human Light/GunJumpFall", AnimationType.HumanLightRifleJumpFall);
            AnimationLoader.Load("Animations/Human Light/PistolJumpFall", AnimationType.HumanLightPistolJumpFall);
            //AnimationLoader.Load("Animations/Human Light/SwordFirst", AnimationType.HumanLightSwordSlash);
            AnimationLoader.Load("Animations/Human Light/GunShoot", AnimationType.HumanLightRifleFireGun);
            AnimationLoader.Load("Animations/Human Light/PistolShoot", AnimationType.HumanLightPistolFireGun);
            AnimationLoader.Load("Animations/Human Light/Throw", AnimationType.HumanLightUseTool);
            AnimationLoader.Load("Animations/Human Light/Slide", AnimationType.HumanLightSliding);
            AnimationLoader.Load("Animations/Human Light/SwordFirst", AnimationType.HumanLightSwordAttack_1);
            AnimationLoader.Load("Animations/Human Light/SwordSecond", AnimationType.HumanLightSwordAttack_2);
            AnimationLoader.Load("Animations/Human Light/SwordThird", AnimationType.HumanLightSwordAttack_3);
            AnimationLoader.Load("Animations/Human Light/Jetpack", AnimationType.HumanLightJetPack);
            AnimationLoader.Load("Animations/Human Light/Stagger", AnimationType.HumanLightStagger);
            AnimationLoader.Load("Animations/Human Light/Death", AnimationType.HumanLightKnockedDownFront);
        }
    }
}
