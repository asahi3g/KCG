using System;
using System.Collections.Generic;

namespace Animation
{
    public class AnimationManager
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private AnimationProperties[] TypeArray;

        private Dictionary<string, int> NameToID;

        public AnimationManager()
        {
            NameToID = new Dictionary<string, int>();
            TypeArray = new AnimationProperties[1024];
            for (int i = 0; i < TypeArray.Length; i++)
            {
                TypeArray[i] = new AnimationProperties();
            }
            CurrentIndex = -1;
        }

        public AnimationProperties Get(int index)
        {
            if (index >= 0 && index < TypeArray.Length)
            {
                return TypeArray[index];
            }

            return new AnimationProperties();
        }

        public AnimationProperties Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get(value);
            }

            return new AnimationProperties();
        }

        public void CreateAnimation(int index)
        {
            while (index >= TypeArray.Length)
            {
                Array.Resize(ref TypeArray, TypeArray.Length * 2);
            }

            CurrentIndex = index;
            if (CurrentIndex != -1)
            {
                TypeArray[CurrentIndex].Index = CurrentIndex;
            }
        }

        public void SetName(string name)
        {
            if (CurrentIndex == -1) return;

            if (!NameToID.ContainsKey(name))
            {
                NameToID.Add(name, CurrentIndex);
            }

            TypeArray[CurrentIndex].Name = name;
        }

        public void SetTimePerFrame(float timePerFrame)
        {
            if (CurrentIndex == -1) return;

            TypeArray[CurrentIndex].TimePerFrame = timePerFrame;
        }

        public void SetFrameCount(int frameCount)
        {
            if (CurrentIndex == -1) return;

            TypeArray[CurrentIndex].FrameCount = frameCount;
        }

        public void SetBaseSpriteID(int baseSpriteId)
        {
            if (CurrentIndex == -1) return;

            TypeArray[CurrentIndex].BaseSpriteId = baseSpriteId;
        }

        public void EndAnimation()
        {
            CurrentIndex = -1;
        }

        public int CharacterSpriteId;
        public int CharacterSpriteSheet;
        public int SlimeMoveLeftBaseSpriteId;
        public int SlimeSpriteSheet;
        public int DustBaseSpriteId;
        public int DustSpriteSheet;
        public int ExplosionBaseSpriteId;
        public int ExplosionSpriteSheet;

        public void InitializeResources()
        {
            CharacterSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Characters\\Player\\character.png", 32, 48);
            SlimeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Enemies\\Slime\\slime.png", 32, 32);
            DustSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\Dust\\dust1.png", 16, 16);
            ExplosionSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\explosion.png", 182, 182);
            int SwordSlash_1_Right_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\BakedEffects\\Sword_Slash_1_Right.png", 38, 70);
            int SwordSlash_1_Left_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\BakedEffects\\Sword_Slash_1_Left.png", 38, 70);
            int SwordSlash_2_Right_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\BakedEffects\\Sword_Slash_2_Right.png", 38, 70);
            int SwordSlash_2_Left_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\BakedEffects\\Sword_Slash_2_Left.png", 38, 70);


            CharacterSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(CharacterSpriteSheet, 0, 0, Enums.AtlasType.Agent);
            SlimeMoveLeftBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(SlimeSpriteSheet, 0, 0, 3, 0, Enums.AtlasType.Agent);
            DustBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(DustSpriteSheet, 0, 0, 5, 0, Enums.AtlasType.Particle);
            ExplosionBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(ExplosionSpriteSheet, 0, 0, 4, 1, Enums.AtlasType.Particle);
            int SwordSlash_1_RightBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(SwordSlash_1_Right_SpriteSheet, 0, 0, 2, 0, Enums.AtlasType.Particle);
            int SwordSlash_1_LeftBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(SwordSlash_1_Left_SpriteSheet, 0, 0, 2, 0, Enums.AtlasType.Particle);
            int SwordSlash_2_RightBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(SwordSlash_2_Right_SpriteSheet, 0, 0, 2, 0, Enums.AtlasType.Particle);
            int SwordSlash_2_LeftBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(SwordSlash_2_Left_SpriteSheet, 0, 0, 2, 0, Enums.AtlasType.Particle);

            GameState.AnimationManager.CreateAnimation((int)AnimationType.CharacterMoveLeft);
            GameState.AnimationManager.SetName("character-move-left");
            GameState.AnimationManager.SetTimePerFrame(0.15f);
            GameState.AnimationManager.SetBaseSpriteID(CharacterSpriteId);
            GameState.AnimationManager.SetFrameCount(1);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation((int)AnimationType.CharacterMoveLeft);
            GameState.AnimationManager.SetName("character-move-right");
            GameState.AnimationManager.SetTimePerFrame(0.15f);
            GameState.AnimationManager.SetBaseSpriteID(CharacterSpriteId);
            GameState.AnimationManager.SetFrameCount(1);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation((int)AnimationType.SlimeMoveLeft);
            GameState.AnimationManager.SetName("slime-move-left");
            GameState.AnimationManager.SetTimePerFrame(0.35f);
            GameState.AnimationManager.SetBaseSpriteID(SlimeMoveLeftBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(4);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation((int)AnimationType.Dust);
            GameState.AnimationManager.SetName("dust");
            GameState.AnimationManager.SetTimePerFrame(0.075f);
            GameState.AnimationManager.SetBaseSpriteID(DustBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(6);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation((int)AnimationType.Smoke);
            GameState.AnimationManager.SetName("smoke");
            GameState.AnimationManager.SetTimePerFrame(0.075f);
            GameState.AnimationManager.SetBaseSpriteID(DustBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(6);
            GameState.AnimationManager.EndAnimation();


            GameState.AnimationManager.CreateAnimation((int)AnimationType.Explosion);
            GameState.AnimationManager.SetName("explosion");
            GameState.AnimationManager.SetTimePerFrame(0.075f);
            GameState.AnimationManager.SetBaseSpriteID(ExplosionBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(7);
            GameState.AnimationManager.EndAnimation();


            GameState.AnimationManager.CreateAnimation((int)AnimationType.SwordSlash_1_Right);
            GameState.AnimationManager.SetTimePerFrame(0.075f);
            GameState.AnimationManager.SetBaseSpriteID(SwordSlash_1_RightBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(3);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation((int)AnimationType.SwordSlash_1_Left);
            GameState.AnimationManager.SetTimePerFrame(0.075f);
            GameState.AnimationManager.SetBaseSpriteID(SwordSlash_1_LeftBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(3);
            GameState.AnimationManager.EndAnimation();


            GameState.AnimationManager.CreateAnimation((int)AnimationType.SwordSlash_2_Right);
            GameState.AnimationManager.SetTimePerFrame(0.075f);
            GameState.AnimationManager.SetBaseSpriteID(SwordSlash_2_RightBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(3);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation((int)AnimationType.SwordSlash_2_Left);
            GameState.AnimationManager.SetTimePerFrame(0.075f);
            GameState.AnimationManager.SetBaseSpriteID(SwordSlash_2_LeftBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(3);
            GameState.AnimationManager.EndAnimation();
        }

    }

}
