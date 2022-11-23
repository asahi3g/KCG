//imports UnityEngine

using System;
using System.Collections.Generic;
using KMath;

namespace Particle
{
    public class ParticleCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private ParticleProperties[] PropertiesArray;

        private Dictionary<string, int> NameToID;

        public ParticleCreationApi()
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
        public int WoodSpriteSheet;


        public void InitializeResources()
        {
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            int WhitePixelSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\white_32x32.png", 32, 32);
            int ParticleSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\particle.png", 128, 128);
            WoodSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\brown_32x32.png", 32, 32);

            OreIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(OreSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            WhitePixel = GameState.SpriteAtlasManager.CopySpriteToAtlas(WhitePixelSheet, 0, 0, Enums.AtlasType.Particle);
            WoodSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(WoodSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            int ParticleSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(ParticleSpriteSheet, 0, 0, Enums.AtlasType.Particle);

            GameState.ParticleCreationApi.Create((int)ParticleType.Ore);
            GameState.ParticleCreationApi.SetDecayRate(1.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -20.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(OreIcon);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(1.0f, 10.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.OreExplosionParticle);
            GameState.ParticleCreationApi.SetDecayRate(1.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(130.0f);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetSpriteId(OreIcon);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.DustParticle);
            GameState.ParticleCreationApi.SetDecayRate(4.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(0);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Dust);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.35f, 0.35f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.End();


            GameState.ParticleCreationApi.Create((int)ParticleType.Debris);
            GameState.ParticleCreationApi.SetDecayRate(0.5f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -15.0f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.GasParticle);
            GameState.ParticleCreationApi.SetDecayRate(0.17f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(0);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Smoke);
            GameState.ParticleCreationApi.SetSize(new Vec2f(4.5f, 4.5f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(10.3f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(255f, 72f, 0f, 255.0f));
            GameState.ParticleCreationApi.End();


            GameState.ParticleCreationApi.Create((int)ParticleType.Blood);
            GameState.ParticleCreationApi.SetDecayRate(1.5f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(ParticleSprite);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.075f, 0.075f), new Vec2f(0.575f, 0.575f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 2.5f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetEndScale(0.7f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.6f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.1f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.MuzzleFlash);
            GameState.ParticleCreationApi.SetDecayRate(6.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(ParticleSprite);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.075f, 0.075f), new Vec2f(0.575f, 0.575f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 2.5f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetEndScale(0.7f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(0.990f, 0.660f, 0.228f, 0.6f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(0.740f, 0.448f, 0.0666f, 0.1f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.End();


            GameState.ParticleCreationApi.Create((int)ParticleType.Blood2);
            GameState.ParticleCreationApi.SetName("Blood");
            GameState.ParticleCreationApi.SetDecayRate(0.3f, 1.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -15.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(WhitePixel);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.15f, 0.15f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetEndScale(0.7f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.7f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.2f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.SetBounce(true);
            GameState.ParticleCreationApi.SetBounceFactor(new Vec2f(0.3f, 0.3f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.BloodSmoke);
            GameState.ParticleCreationApi.SetDecayRate(4.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(ParticleSprite);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.7f, 0.70f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.5f);
            GameState.ParticleCreationApi.SetEndScale(0.5f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.3f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.1f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.BloodFog);
            GameState.ParticleCreationApi.SetDecayRate(1.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(ParticleSprite);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.9f, 0.9f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.5f);
            GameState.ParticleCreationApi.SetEndScale(0.5f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.1f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.1f));
            GameState.ParticleCreationApi.End();
            

            GameState.ParticleCreationApi.Create((int)ParticleType.Wood);
            GameState.ParticleCreationApi.SetDecayRate(0.5f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(WoodSprite);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(1.0f, 5.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.Explosion);
            GameState.ParticleCreationApi.SetDecayRate(2.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(0);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Explosion);
            GameState.ParticleCreationApi.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.Shrapnel);
            GameState.ParticleCreationApi.SetDecayRate(2.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(130.0f);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetSpriteId(OreIcon);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.125f, 0.125f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.SetBounce(true);
            GameState.ParticleCreationApi.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticleCreationApi.End();


            GameState.ParticleCreationApi.Create((int)ParticleType.MetalBulletImpact);
            GameState.ParticleCreationApi.SetDecayRate(6.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(130.0f);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetSpriteId(WhitePixel);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.2f, 0.2f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetBounce(true);
            GameState.ParticleCreationApi.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1.0f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(0.8f, 0.8f, 0.8f, 0.0f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.RockBulletImpact);
            GameState.ParticleCreationApi.SetDecayRate(6.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(130.0f);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetSpriteId(WhitePixel);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.2f, 0.2f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetBounce(true);
            GameState.ParticleCreationApi.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(1.0f, 0.64f, 0.0f, 1.0f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(1.0f, 0.64f, 0.0f, 0.0f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)ParticleType.BloodImpact);
            GameState.ParticleCreationApi.SetDecayRate(6.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(130.0f);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetSpriteId(WhitePixel);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetBounce(true);
            GameState.ParticleCreationApi.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 1.0f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.0f));
            GameState.ParticleCreationApi.End();


            GameState.ParticleCreationApi.Create((int)ParticleType.BulletTrail);
            GameState.ParticleCreationApi.SetDecayRate(4.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(ParticleSprite);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.5f));
            GameState.ParticleCreationApi.SetEndColor(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f));
            GameState.ParticleCreationApi.End();
        }

        public void InitializeEmitterResources()
        {
            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.OreFountain);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.Ore);
            GameState.ParticleEmitterCreationApi.SetDuration(0.5f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(1);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(0.05f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-1.0f, 0.0f), new Vec2f(1.0f, 0.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.OreExplosion);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.OreExplosionParticle);
            GameState.ParticleEmitterCreationApi.SetDuration(0.15f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(15);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-10.0f, -10.0f), new Vec2f(10.0f, 10.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.DustEmitter);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.DustParticle);
            GameState.ParticleEmitterCreationApi.SetDuration(0.1f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(1);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.02f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.GasEmitter);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.GasParticle);
            GameState.ParticleEmitterCreationApi.SetDuration(0.5f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.25f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(1);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.02f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.Blood);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.Blood);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.4f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(10);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-0.6f, -0.5f), new Vec2f(0.6f, 0.5f));
            GameState.ParticleEmitterCreationApi.End();


            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.Blood2);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.Blood2);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(10);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-4.0f, -10.0f), new Vec2f(4.0f, 2.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.BloodSmoke);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.BloodSmoke);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.3f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(6);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.BloodFog);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.BloodFog);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.7f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(15);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-0.1f, -0.1f), new Vec2f(0.1f, 0.1f));
            GameState.ParticleEmitterCreationApi.End();


            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.WoodEmitter);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.Wood);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(10);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-1.0f, -1.0f), new Vec2f(1.0f, 1.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.ExplosionEmitter);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.Explosion);
            GameState.ParticleEmitterCreationApi.SetDuration(4.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.7f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(5);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0.0f), new Vec2f(0.0f, 0.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.ShrapnelEmitter);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.Shrapnel);
            GameState.ParticleEmitterCreationApi.SetDuration(0.15f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(30);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-5.0f, -5.0f), new Vec2f(5.0f, 5.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.MetalBulletImpact);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.MetalBulletImpact);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(6);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.RockBulletImpact);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.RockBulletImpact);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(6);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.BloodImpact);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.BloodImpact);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.0f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(6);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)ParticleEmitterType.MuzzleFlash);
            GameState.ParticleEmitterCreationApi.SetParticleType(ParticleType.MuzzleFlash);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.2f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(10);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-0.3f, -0.3f), new Vec2f(0.3f, 0.3f));
            GameState.ParticleEmitterCreationApi.End();
        }
    }

}
