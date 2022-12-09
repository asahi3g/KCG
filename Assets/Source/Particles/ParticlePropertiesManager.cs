//imports UnityEngine

using System;
using System.Collections.Generic;
using KMath;

namespace Particle
{
    public class ParticlePropertiesManager
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private ParticleProperties[] PropertiesArray;

        private Dictionary<string, int> NameToID;

        public ParticlePropertiesManager()
        {
            NameToID = new Dictionary<string, int>();
            PropertiesArray = new ParticleProperties[1024];
            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new ParticleProperties();
            }
            CurrentIndex = -1;
        }

        public ParticleProperties Get(ParticleType Id)
        {
            if (Id >= 0 && (int)Id < PropertiesArray.Length)
            {
                return PropertiesArray[(int)Id];
            }

            return new ParticleProperties();
        }

        public ref ParticleProperties GetRef(int Id)
        {
            return ref PropertiesArray[Id];
        }

        public ParticleProperties Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get((ParticleType)value);
            }

            return new ParticleProperties();
        }

        public void Create(int Id)
        {
            while (Id >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length * 2);
            }

            CurrentIndex = Id;
            if (CurrentIndex != -1)
            {
                PropertiesArray[CurrentIndex].PropertiesId = CurrentIndex;
                PropertiesArray[CurrentIndex].StartingScale = 1.0f;
                PropertiesArray[CurrentIndex].EndScale = 1.0f;
            }
        }

        public void SetName(string name)
        {
            if (CurrentIndex == -1) return;

            if (!NameToID.ContainsKey(name))
            {
                NameToID.Add(name, CurrentIndex);
            }

            PropertiesArray[CurrentIndex].Name = name;
        }

        public void SetAnimationType(Animation.AnimationType animationType)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].HasAnimation = true;
                PropertiesArray[CurrentIndex].AnimationType = animationType;
            }
        }
        public void SetDecayRate(float decayRate)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MinDecayRate = decayRate;
                PropertiesArray[CurrentIndex].MaxDecayRate = decayRate;
            }
        }

        public void SetDecayRate(float minDecayRate, float maxDecayRate)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MinDecayRate = minDecayRate;
                PropertiesArray[CurrentIndex].MaxDecayRate = maxDecayRate;
            }
        }

        public void SetAcceleration(Vec2f acceleration)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Acceleration = acceleration;
            }
        }

        public void SetDeltaRotation(float deltaRotation)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DeltaRotation = deltaRotation;
            }
        }

        public void SetDeltaScale(float deltaScale)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DeltaScale = deltaScale;
            }
        }

        public void SetSpriteId(int spriteId)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].SpriteId = spriteId;
            }
        }

        public void SetSize(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MinSize = size;
                PropertiesArray[CurrentIndex].MaxSize = size;
            }
        }

        public void SetSize(Vec2f minSize, Vec2f maxSize)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MinSize = minSize;
                PropertiesArray[CurrentIndex].MaxSize = maxSize;
            }
        }

        public void SetStartingVelocity(Vec2f startingVelocity)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingVelocity = startingVelocity;
            }
        }

        public void SetStartingRotation(float startingRotation)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingRotation = startingRotation;
            }
        }

        public void SetStartingScale(float startingScale)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingScale = startingScale;
            }
        }

        public void SetEndScale(float endScale)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].EndScale = endScale;
            }
        }

        public void SetStartingColor(UnityEngine.Color startingColor)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingColor = startingColor;
            }
        }

        public void SetEndColor(UnityEngine.Color endColor)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].EndColor = endColor;
            }
        }

        public void SetAnimationSpeed(float animationSpeed)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].AnimationSpeed = animationSpeed;
            }
        }

        public void SetIsCollidable(bool isCollidable)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].IsCollidable = isCollidable;
            }
        }

        public void SetBounce(bool bounce)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Bounce = bounce;
            }
        }

        public void SetBounceFactor(Vec2f bounceFactor)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].BounceFactor = bounceFactor;
            }
        }


        public void End()
        {
            CurrentIndex = -1;
        }


        public int OreIcon;
        public int OreSpriteSheet;
        public int WhitePixel;
        public int BloodSpriteSheet;
        public int WoodSprite;
        public int WhiteCircle;
        public int WoodSpriteSheet;
        public int Smoke9_Sprite;
        public int Smoke18_Sprite;
        public int Fire4_Sprite;

        public void InitializeResources()
        {
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            int WhitePixelSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\white_32x32.png", 32, 32);
            int ParticleSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\particle.png", 128, 128);
            int CircleSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\circle.png", 128, 128);
            WoodSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\brown_32x32.png", 32, 32);
            int Smoke9_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\smoke_9.png", 512, 512);
            int Smoke18_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\smoke_18.png", 512, 512);
            int Fire4_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\fire_4.png", 512, 512);
            int EmptyCircle_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\Circle_Empty.png", 512, 512);
            int SpikyCircle_SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\spiky_20.png", 512, 512);

            OreIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(OreSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            WhitePixel = GameState.SpriteAtlasManager.CopySpriteToAtlas(WhitePixelSheet, 0, 0, Enums.AtlasType.Particle);
            WoodSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(WoodSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            int ParticleSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(ParticleSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            WhiteCircle = GameState.SpriteAtlasManager.CopySpriteToAtlas(CircleSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            Smoke9_Sprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(Smoke9_SpriteSheet, 0, 0, Enums.AtlasType.Particle);
            Smoke18_Sprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(Smoke18_SpriteSheet, 0, 0, Enums.AtlasType.Particle);
            Fire4_Sprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(Fire4_SpriteSheet, 0, 0, Enums.AtlasType.Particle);
            int EmptyCircleSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(EmptyCircle_SpriteSheet, 0, 0, Enums.AtlasType.Particle);
            int SpikyCircleSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(SpikyCircle_SpriteSheet, 0, 0, Enums.AtlasType.Particle);

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Ore);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -20.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(90.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(OreIcon);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(1.0f, 10.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.OreExplosionParticle);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(130.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(-1.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(OreIcon);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.DustParticle);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetDeltaScale(-1.0f);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.Dust);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.35f, 0.35f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.Debris);
            GameState.ParticlePropertiesManager.SetDecayRate(0.5f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -15.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticlePropertiesManager.SetIsCollidable(true);
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.GasParticle);
            GameState.ParticlePropertiesManager.SetDecayRate(0.17f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetDeltaScale(-1.0f);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.Smoke);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(4.5f, 4.5f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(10.3f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(255f, 72f, 0f, 255.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.Blood);
            GameState.ParticlePropertiesManager.SetDecayRate(1.5f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(90.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(ParticleSprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.075f, 0.075f), new Vec2f(0.575f, 0.575f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 2.5f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(0.7f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.6f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.1f));
            GameState.ParticlePropertiesManager.SetIsCollidable(true);
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.MuzzleFlash);
            GameState.ParticlePropertiesManager.SetDecayRate(6.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(90.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(ParticleSprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.075f, 0.075f), new Vec2f(0.575f, 0.575f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 2.5f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(0.7f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(0.990f, 0.660f, 0.228f, 0.6f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(0.740f, 0.448f, 0.0666f, 0.1f));
            GameState.ParticlePropertiesManager.SetIsCollidable(true);
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.Blood2);
            GameState.ParticlePropertiesManager.SetName("Blood");
            GameState.ParticlePropertiesManager.SetDecayRate(0.3f, 1.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -15.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(90.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhitePixel);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.15f, 0.15f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(0.7f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.7f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.2f));
            GameState.ParticlePropertiesManager.SetIsCollidable(true);
            GameState.ParticlePropertiesManager.SetBounce(true);
            GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(0.3f, 0.3f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.BloodSmoke);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(90.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(ParticleSprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.7f, 0.70f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.5f);
            GameState.ParticlePropertiesManager.SetEndScale(0.5f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.3f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.1f));
            GameState.ParticlePropertiesManager.SetIsCollidable(true);
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.BloodFog);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(90.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(ParticleSprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.9f, 0.9f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.5f);
            GameState.ParticlePropertiesManager.SetEndScale(0.5f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.1f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.1f));
            GameState.ParticlePropertiesManager.End();
            

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Wood);
            GameState.ParticlePropertiesManager.SetDecayRate(0.5f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(90.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WoodSprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(1.0f, 5.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticlePropertiesManager.SetIsCollidable(true);
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Explosion);
            GameState.ParticlePropertiesManager.SetDecayRate(2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetDeltaScale(-1.0f);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.Explosion);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Shrapnel);
            GameState.ParticlePropertiesManager.SetDecayRate(2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(130.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(-1.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(OreIcon);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.125f, 0.125f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticlePropertiesManager.SetIsCollidable(true);
            GameState.ParticlePropertiesManager.SetBounce(true);
            GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.MetalBulletImpact);
            GameState.ParticlePropertiesManager.SetDecayRate(6.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(130.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(-1.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhitePixel);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.2f, 0.2f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetBounce(true);
            GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(0.8f, 0.8f, 0.8f, 0.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.RockBulletImpact);
            GameState.ParticlePropertiesManager.SetDecayRate(6.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(130.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(-1.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhitePixel);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.2f, 0.2f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetBounce(true);
            GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 0.64f, 0.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 0.64f, 0.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.BloodImpact);
            GameState.ParticlePropertiesManager.SetDecayRate(6.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(130.0f);
            GameState.ParticlePropertiesManager.SetDeltaScale(-1.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhitePixel);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingRotation(0.0f);
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetBounce(true);
            GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.BulletTrail);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(ParticleSprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.5f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();



            GameState.ParticlePropertiesManager.Create((int)ParticleType.Explosion_2_Part1);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f, 2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 10.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhitePixel);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.4f, 0.4f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(0.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 0.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 0.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Explosion_2_Part2);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f, 2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 7.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhitePixel);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.3f, 0.3f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(0.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 0.7f, 0.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 0.7f, 0.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Explosion_2_Part3);
            GameState.ParticlePropertiesManager.SetDecayRate(2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhitePixel);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.25f, 0.25f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(0.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(0.5f, 0.5f, 0.5f, 0.5f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Explosion_2_Smoke);
            GameState.ParticlePropertiesManager.SetDecayRate(0.5f, 1.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 7.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(Smoke18_Sprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(2.0f, 2.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.0f);
            GameState.ParticlePropertiesManager.SetEndScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.6f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(0.0f, 0.0f, 0.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Explosion_2_Shrapnel);
            GameState.ParticlePropertiesManager.SetDecayRate(2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(130.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhitePixel);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.125f, 0.125f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 0.7f, 0.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 0.7f, 0.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.Explosion_2_Impact);
            GameState.ParticlePropertiesManager.SetDecayRate(5.2f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -0.0f));
            GameState.ParticlePropertiesManager.SetSpriteId(EmptyCircleSprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.0f);
            GameState.ParticlePropertiesManager.SetEndScale(2.4f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();
            

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Explosion_2_Flash);
            GameState.ParticlePropertiesManager.SetDecayRate(6.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhiteCircle);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(1.6f, 1.6f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.8f);
            GameState.ParticlePropertiesManager.SetEndScale(2.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.2f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.Dust_2);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f, 2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -1.0f));
           // GameState.ParticlePropertiesManager.SetDeltaRotation(180.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhiteCircle);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.0f);
            GameState.ParticlePropertiesManager.SetEndScale(5.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
          //  GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(0.79f, 0.7f, 0.53f, 1.0f));
          //  GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(0.79f, 0.7f, 0.53f, 1.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1f, 1f, 1f, 0.35f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f));

          //  GameState.ParticlePropertiesManager.SetIsCollidable(true);
          //  GameState.ParticlePropertiesManager.SetBounce(true);
           // GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(0.5f, 1.0f));
            GameState.ParticlePropertiesManager.End();



            GameState.ParticlePropertiesManager.Create((int)ParticleType.Dust_3);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f, 2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -1.0f));
           // GameState.ParticlePropertiesManager.SetDeltaRotation(180.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhiteCircle);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.0f);
            GameState.ParticlePropertiesManager.SetEndScale(5.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
           // GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(0.79f, 0.7f, 0.53f, 1.0f));
           // GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(0.79f, 0.7f, 0.53f, 1.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1f, 1f, 1f, 0.35f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f));
         //   GameState.ParticlePropertiesManager.SetIsCollidable(true);
           // GameState.ParticlePropertiesManager.SetBounce(true);
           // GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(0.5f, 1.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.Dust_SwordAttack);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f, 2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -1.0f));
           // GameState.ParticlePropertiesManager.SetDeltaRotation(180.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(Smoke18_Sprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.0f);
            GameState.ParticlePropertiesManager.SetEndScale(10.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(2f, 2f, 2f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(2f, 2f, 2f, 0.0f));

          //  GameState.ParticlePropertiesManager.SetIsCollidable(true);
          //  GameState.ParticlePropertiesManager.SetBounce(true);
           // GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(0.5f, 1.0f));
            GameState.ParticlePropertiesManager.End();




          /*  GameState.ParticlePropertiesManager.Create((int)ParticleType.Dust_2);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f, 2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -1.0f));
           // GameState.ParticlePropertiesManager.SetDeltaRotation(180.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(Smoke18_Sprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.0f);
            GameState.ParticlePropertiesManager.SetEndScale(10.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(2f, 2f, 2f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(2f, 2f, 2f, 0.0f));

          //  GameState.ParticlePropertiesManager.SetIsCollidable(true);
          //  GameState.ParticlePropertiesManager.SetBounce(true);
           // GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(0.5f, 1.0f));
            GameState.ParticlePropertiesManager.End();



            GameState.ParticlePropertiesManager.Create((int)ParticleType.Dust_3);
            GameState.ParticlePropertiesManager.SetDecayRate(1.0f, 2.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -1.0f));
           // GameState.ParticlePropertiesManager.SetDeltaRotation(180.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(Smoke18_Sprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.0f);
            GameState.ParticlePropertiesManager.SetEndScale(10.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(2f, 2f, 2f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(2f, 2f, 2f, 0.0f));
          //  GameState.ParticlePropertiesManager.SetIsCollidable(true);
           // GameState.ParticlePropertiesManager.SetBounce(true);
           // GameState.ParticlePropertiesManager.SetBounceFactor(new Vec2f(0.5f, 1.0f));
            GameState.ParticlePropertiesManager.End();*/



            GameState.ParticlePropertiesManager.Create((int)ParticleType.Smoke_2);
            GameState.ParticlePropertiesManager.SetDecayRate(1.5f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 5.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhiteCircle);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(8.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.5f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.Smoke_3);
            GameState.ParticlePropertiesManager.SetDecayRate(0.7f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0.0f);
            GameState.ParticlePropertiesManager.SetSpriteId(WhiteCircle);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetEndScale(8.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.2f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f));
            GameState.ParticlePropertiesManager.End();



            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_1_Right);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_1_Right);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_1_Left);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_1_Left);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();








            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_2_Right);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_2_Right);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 2.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_2_Left);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_2_Left);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 2.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();




            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_3_Right);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_3_Right);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 1.5f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();


            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_3_Left);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_3_Left);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 1.5f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();




            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_1_Up_Right);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_1_Up_Right);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_2_Up_Right);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_2_Up_Right);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();

            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordSlash_3_Up_Right);
            GameState.ParticlePropertiesManager.SetDecayRate(4.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetDeltaRotation(0);
            GameState.ParticlePropertiesManager.SetAnimationType(Animation.AnimationType.SwordSlash_3_Up_Right);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();



            GameState.ParticlePropertiesManager.Create((int)ParticleType.SwordAttack_Impact);
            GameState.ParticlePropertiesManager.SetDecayRate(8.0f);
            GameState.ParticlePropertiesManager.SetAcceleration(new Vec2f(0.0f, -0.0f));
            GameState.ParticlePropertiesManager.SetSpriteId(SpikyCircleSprite);
            GameState.ParticlePropertiesManager.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticlePropertiesManager.SetStartingScale(0.0f);
            GameState.ParticlePropertiesManager.SetEndScale(1.0f);
            GameState.ParticlePropertiesManager.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticlePropertiesManager.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            GameState.ParticlePropertiesManager.End();


            
        }

        public void InitializeEmitterResources()
        {
            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.OreFountain);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Ore);
            GameState.ParticleEmitterPropertiesManager.SetDuration(0.5f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(0.05f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-1.0f, 0.0f), new Vec2f(1.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.OreExplosion);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.OreExplosionParticle);
            GameState.ParticleEmitterPropertiesManager.SetDuration(0.15f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(15);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(1.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-10.0f, -10.0f), new Vec2f(10.0f, 10.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.DustEmitter);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.DustParticle);
            GameState.ParticleEmitterPropertiesManager.SetDuration(0.1f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(1.02f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.GasEmitter);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.GasParticle);
            GameState.ParticleEmitterPropertiesManager.SetDuration(0.5f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.25f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(1.02f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Blood);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Blood);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.4f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(10);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.6f, -0.5f), new Vec2f(0.6f, 0.5f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Blood2);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Blood2);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(10);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-4.0f, -10.0f), new Vec2f(4.0f, 2.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.BloodSmoke);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.BloodSmoke);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.3f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(6);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.BloodFog);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.BloodFog);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.7f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(15);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.1f, -0.1f), new Vec2f(0.1f, 0.1f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.WoodEmitter);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Wood);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(10);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-1.0f, -1.0f), new Vec2f(1.0f, 1.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.ExplosionEmitter);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Explosion);
            GameState.ParticleEmitterPropertiesManager.SetDuration(4.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.7f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(5);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(0.0f, 0.0f), new Vec2f(0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.ShrapnelEmitter);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Shrapnel);
            GameState.ParticleEmitterPropertiesManager.SetDuration(0.15f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(30);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(1.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-5.0f, -5.0f), new Vec2f(5.0f, 5.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.MetalBulletImpact);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.MetalBulletImpact);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(6);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.RockBulletImpact);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.RockBulletImpact);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(6);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.BloodImpact);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.BloodImpact);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(6);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.MuzzleFlash);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.MuzzleFlash);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.2f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(10);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.3f, -0.3f), new Vec2f(0.3f, 0.3f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Explosion_2_Part1);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Explosion_2_Part1);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(15);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-2.5f, -2.5f), new Vec2f(2.5f, 2.5f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Explosion_2_Part2);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Explosion_2_Part2);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(10);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-1.5f, -1.5f), new Vec2f(1.5f, 1.5f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Explosion_2_Part3);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Explosion_2_Part3);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(20);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-1.5f, -1.5f), new Vec2f(1.5f, 1.5f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Explosion_2_Shrapnel);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Explosion_2_Shrapnel);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(60);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-4.5f, -4.5f), new Vec2f(4.5f, 4.5f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Explosion_2_Smoke);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Explosion_2_Smoke);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.2f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(20);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.75f, -0.75f), new Vec2f(0.75f, 0.75f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Explosion_2_Flash);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Explosion_2_Flash);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(10);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-1.5f, -1.5f), new Vec2f(1.5f, 1.5f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Explosion_2_Impact);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Explosion_2_Impact);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-1.5f, -1.5f), new Vec2f(1.5f, 1.5f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Dust_2);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Dust_2);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.15f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(60 * 2);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Dust_SwordAttack);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Dust_SwordAttack);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.25f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(60 * 2);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Dust_Jumping);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Dust_2);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.15f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(7 * 2);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Dust_Landing);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Dust_3);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(10 * 2);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0f));
            GameState.ParticleEmitterPropertiesManager.End();



            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Smoke_2);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Smoke_2);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.15f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(7);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0.0f, -0f), new Vec2f(-0.0f, 0f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.Smoke_3);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.Smoke_3);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.10f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(7);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(0.01f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-1.5f, -0f), new Vec2f(-1.0f, 0.3f));
            GameState.ParticleEmitterPropertiesManager.End();



            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.SwordSlash_1_Right);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.SwordSlash_1_Right);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.SwordSlash_1_Left);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.SwordSlash_1_Left);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.SwordSlash_2_Right);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.SwordSlash_2_Right);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.SwordSlash_2_Left);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.SwordSlash_2_Left);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.SwordSlash_3_Right);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.SwordSlash_3_Right);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();

            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.SwordSlash_3_Left);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.SwordSlash_3_Left);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();


            GameState.ParticleEmitterPropertiesManager.Create((int)ParticleEmitterType.SwordAttack_Impact);
            GameState.ParticleEmitterPropertiesManager.SetParticleType(ParticleType.SwordAttack_Impact);
            GameState.ParticleEmitterPropertiesManager.SetDuration(2.0f);
            GameState.ParticleEmitterPropertiesManager.SetSpawnRadius(0.3f);
            GameState.ParticleEmitterPropertiesManager.SetParticleCount(1);
            GameState.ParticleEmitterPropertiesManager.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterPropertiesManager.SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            GameState.ParticleEmitterPropertiesManager.End();
        }
    }

}
