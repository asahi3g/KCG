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
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jump", AnimationType.Jump);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jump_Roll", AnimationType.Flip);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jog", AnimationType.Jog);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Dodge", AnimationType.Dash);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@DodgeRoll", AnimationType.Roll);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Crouch", AnimationType.Crouch);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Crouch_Walk", AnimationType.Crouch_Walk);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/98_Damage/Stander@Slowed", AnimationType.Limp);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/99_Sub/Stander@Sub_Drink", AnimationType.Drink);
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
            AnimationLoader.Load("ArtistAnimations/InsectRun", AnimationType.InsectRun);
            AnimationLoader.Load("ArtistAnimations/InsectAttack", AnimationType.InsectAttack);
            AnimationLoader.Load("ArtistAnimations/InsectJump", AnimationType.InsectJump);
            AnimationLoader.Load("ArtistAnimations/InsectDeath", AnimationType.InsectDie);

             // Heavy Insect
            AnimationLoader.Load("ArtistAnimations/InsectHeavyIdle", AnimationType.InsectHeavyIdle);
            AnimationLoader.Load("ArtistAnimations/InsectHeavyRun", AnimationType.InsectHeavyRun);
            AnimationLoader.Load("ArtistAnimations/InsectHeavyAttack", AnimationType.InsectHeavyAttack);
            AnimationLoader.Load("ArtistAnimations/InsectHeavyJump", AnimationType.InsectHeavyJump);
            AnimationLoader.Load("ArtistAnimations/InsectHeavyDeath", AnimationType.InsectHeavyDie);


            AnimationLoader.Load("ArtistAnimations/SpaceMarineWalk", AnimationType.SpaceMarineRun);
            AnimationLoader.Load("ArtistAnimations/SpaceMarineStagger", AnimationType.SpaceMarineStagger);
            AnimationLoader.Load("ArtistAnimations/SpaceMarineDeath", AnimationType.SpaceMarineDead);
            AnimationLoader.Load("ArtistAnimations/SpaceMarineIdle", AnimationType.SpaceMarineIdle);
        }

        private void LoadModels()
        {
            ModelLoader.Load("DefaultHumanoid", ModelType.DefaultHumanoid);
            ModelLoader.Load("SmallInsect", ModelType.SmallInsect);
            ModelLoader.Load("HeavyInsect", ModelType.HeavyInsect);
            ModelLoader.Load("Stander", ModelType.Stander);
            ModelLoader.Load("SpaceMarine", ModelType.SpaceMarine);
            ModelLoader.Load("Pistol", ModelType.Pistol);
            ModelLoader.Load("Rapier", ModelType.Rapier);
        }

    }
}