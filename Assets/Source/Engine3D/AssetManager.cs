using Animancer;
using UnityEngine;
using System;

namespace Engine3D
{


    //TODO(): allow assets to be loaded on gameplay instead of loading at start
    //TODO(): has to print the time to load all the assets
    public class AssetManager
    {

        public static AssetManager AssetManagerSingelton;

        public static AssetManager Singelton
        {
            get
            {
                if (AssetManagerSingelton == null)
                {
                    AssetManagerSingelton = new AssetManager();
                }

                return AssetManagerSingelton;
            }
        }




        AnimationLoader AnimationLoader;
        ModelLoader ModelLoader;
        MaterialLoader MaterialLoader;

        

        public AssetManager()
        {
            AnimationLoader = new AnimationLoader();
            ModelLoader = new ModelLoader();
            MaterialLoader = new MaterialLoader();

            long beginTime = DateTime.Now.Ticks;
            LoadMaterials();
            LoadAnimations();
            LoadModels();
            Debug.Log("3d Assets Loading Time: " + (DateTime.Now.Ticks - beginTime) / TimeSpan.TicksPerMillisecond + " miliseconds");
        }

        public ref AnimationClip GetAnimationClip(AnimationType animationType)
        {
            return ref AnimationLoader.GetAnimationClip(animationType);
        }

        public ref GameObject GetModel(ModelType modelType)
        {
            return ref ModelLoader.GetModel(modelType);
        }

        public ref Material GetMaterial(MaterialType materialType)
        {
            return ref MaterialLoader.GetMaterial(materialType);
        }

        private void LoadMaterials()
        {

        }

        private void LoadAnimations()
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
            //AnimationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.PickaxeHit);
            //AnimationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.ChopTree);
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
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyAttack", AnimationType.InsectHeavyAttack);
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyJump", AnimationType.InsectHeavyJump);
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyFall", AnimationType.InsectHeavyFall);
            AnimationLoader.Load("ArtistAnimations/InsectHeavy/InsectHeavyDead", AnimationType.InsectHeavyDie);


            AnimationLoader.Load("ArtistAnimations/MediumMarine/Idle", AnimationType.SpaceMarineIdle);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunIdle", AnimationType.SpaceMarineRifleIdle);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolIdle", AnimationType.SpaceMarinePistolIdle);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunAlerted", AnimationType.SpaceMarineRifleIdleAlerted);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolAlerted", AnimationType.SpaceMarinePistolIdleAlerted);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Run", AnimationType.SpaceMarineRun);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/DEFORM_Walking", AnimationType.SpaceMarineWalk);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Jump", AnimationType.SpaceMarineJump);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunJump", AnimationType.SpaceMarineRifleJump);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolJump", AnimationType.SpaceMarinePistolJump);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/JumpRoll", AnimationType.SpaceMarineFlip);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunJumpRoll", AnimationType.SpaceMarineRifleFlip);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolJumpRoll", AnimationType.SpaceMarinePistolFlip);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Walking", AnimationType.SpaceMarineJog);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/WalkingBackwards", AnimationType.SpaceMarineWalkBackward);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunWalking", AnimationType.SpaceMarineRifleJog);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolWalking", AnimationType.SpaceMarinePistolJog);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Dodge", AnimationType.SpaceMarineDash);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Roll", AnimationType.SpaceMarineRoll);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/CrouchIdle", AnimationType.SpaceMarineCrouch);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunCrouchIdle", AnimationType.SpaceMarineRifleCrouch);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolCrouchIdle", AnimationType.SpaceMarinePistolCrouch);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/CrouchWalk", AnimationType.SpaceMarineCrouch_Walk);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunCrouchWalk", AnimationType.SpaceMarineRifleCrouch_Walk);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolCrouchWalk", AnimationType.SpaceMarinePistolCrouch_Walk);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/CrouchWalkBackwards", AnimationType.SpaceMarineCrouch_WalkBackwards);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunCrouchWalkBackwards", AnimationType.SpaceMarineRifleCrouch_WalkBackwards);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Limp", AnimationType.SpaceMarineLimp);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Drink", AnimationType.SpaceMarineDrink);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/JumpFall", AnimationType.SpaceMarineJumpFall);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunJumpFall", AnimationType.SpaceMarineRifleJumpFall);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolJumpFall", AnimationType.SpaceMarinePistolJumpFall);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/DEFORM_SwordAttack", AnimationType.SpaceMarineSwordSlash);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/DEFORM_Shooting", AnimationType.SpaceMarineFireGun);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/GunShoot", AnimationType.SpaceMarineRifleFireGun);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/PistolShoot", AnimationType.SpaceMarinePistolFireGun);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Throw", AnimationType.SpaceMarineUseTool);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Slide", AnimationType.SpaceMarineSliding);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Jetpack", AnimationType.SpaceMarineJetPack);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Stagger", AnimationType.SpaceMarineStagger);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/DEFORM_DeathKnockback", AnimationType.SpaceMarineKnockedDownFront);
            AnimationLoader.Load("ArtistAnimations/MediumMarine/Death2", AnimationType.SpaceMarineLyingFront);
            //AnimationLoader.Load("ArtistAnimations/DEFORM_DeathKnockback", AnimationType.SpaceMarineKnockedDownBack);
            //AnimationLoader.Load("ArtistAnimations/DEFORM_Death", AnimationType.SpaceMarineLyingBack);
        }

        private void LoadModels()
        {
            ModelLoader.Load("DefaultHumanoid", ModelType.DefaultHumanoid);
            ModelLoader.Load("SmallInsect", ModelType.SmallInsect);
            ModelLoader.Load("HeavyInsect", ModelType.HeavyInsect);
            ModelLoader.Load("Stander", ModelType.Stander);
            ModelLoader.Load("MediumMarinePrefab", ModelType.SpaceMarine);
            ModelLoader.Load("Pistol", ModelType.Pistol);
            ModelLoader.Load("Rapier", ModelType.Rapier);

            ModelLoader.Load("Sword", ModelType.Sword);
            ModelLoader.Load("SpaceGun", ModelType.SpaceGun);
        }

    }
}