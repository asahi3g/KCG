using System;
using System.Collections.Generic;
using UnityEngine;
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

        public ParticleProperties Get(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id];
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
                return Get(value);
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
                PropertiesArray[CurrentIndex].DecayRate = decayRate;
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
                PropertiesArray[CurrentIndex].Size = size;
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

        public void SetStartingColor(Color startingColor)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingColor = startingColor;
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
        public int BloodSprite;
        public int BloodSpriteSheet;
        public int WoodSprite;
        public int WoodSpriteSheet;


        public void InitializeResources()
        {
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            BloodSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\red_32x32.png", 32, 32);
            WoodSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\brown_32x32.png", 32, 32);

            OreIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(OreSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            BloodSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(BloodSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            WoodSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(WoodSpriteSheet, 0, 0, Enums.AtlasType.Particle);

            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Ore);
            GameState.ParticleCreationApi.SetName("Ore");
            GameState.ParticleCreationApi.SetDecayRate(1.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -20.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(OreIcon);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(1.0f, 10.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.OreExplosionParticle);
            GameState.ParticleCreationApi.SetName("ore-explosion-particle");
            GameState.ParticleCreationApi.SetDecayRate(1.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(130.0f);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetSpriteId(OreIcon);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.DustParticle);
            GameState.ParticleCreationApi.SetName("dust-particle");
            GameState.ParticleCreationApi.SetDecayRate(4.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(0);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Dust);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.35f, 0.35f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.End();


            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Debris);
            GameState.ParticleCreationApi.SetName("debris");
            GameState.ParticleCreationApi.SetDecayRate(0.5f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -15.0f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.GasParticle);
            GameState.ParticleCreationApi.SetName("gas-particle");
            GameState.ParticleCreationApi.SetDecayRate(0.17f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(0);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Smoke);
            GameState.ParticleCreationApi.SetSize(new Vec2f(4.5f, 4.5f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(10.3f);
            GameState.ParticleCreationApi.SetStartingScale(20.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255f, 72f, 0f, 255.0f));
            GameState.ParticleCreationApi.End();


            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Blood);
            GameState.ParticleCreationApi.SetName("Blood");
            GameState.ParticleCreationApi.SetDecayRate(0.5f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(BloodSprite);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.075f, 0.075f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(1.0f, 5.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Wood);
            GameState.ParticleCreationApi.SetName("Wood");
            GameState.ParticleCreationApi.SetDecayRate(0.5f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
            GameState.ParticleCreationApi.SetDeltaScale(0.0f);
            GameState.ParticleCreationApi.SetSpriteId(WoodSprite);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(1.0f, 5.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Explosion);
            GameState.ParticleCreationApi.SetName("explosion");
            GameState.ParticleCreationApi.SetDecayRate(2.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(0);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Explosion);
            GameState.ParticleCreationApi.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.End();

            GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Shrapnel);
            GameState.ParticleCreationApi.SetName("Shrapnel");
            GameState.ParticleCreationApi.SetDecayRate(2.0f);
            GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetDeltaRotation(130.0f);
            GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
            GameState.ParticleCreationApi.SetSpriteId(OreIcon);
            GameState.ParticleCreationApi.SetSize(new Vec2f(0.125f, 0.125f));
            GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            GameState.ParticleCreationApi.SetStartingRotation(0.0f);
            GameState.ParticleCreationApi.SetStartingScale(1.0f);
            GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
            GameState.ParticleCreationApi.SetIsCollidable(true);
            GameState.ParticleCreationApi.SetBounce(true);
            GameState.ParticleCreationApi.SetBounceFactor(new Vec2f(1.0f, 0.25f));
            GameState.ParticleCreationApi.End();
        }

        public void InitializeEmitterResources()
        {
            GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.OreFountain);
            GameState.ParticleEmitterCreationApi.SetName("ore-fountain");
            GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.Ore);
            GameState.ParticleEmitterCreationApi.SetDuration(0.5f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(1);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(0.05f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-1.0f, 0.0f), new Vec2f(1.0f, 0.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.OreExplosion);
            GameState.ParticleEmitterCreationApi.SetName("ore-explosion");
            GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.OreExplosionParticle);
            GameState.ParticleEmitterCreationApi.SetDuration(0.15f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(15);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-10.0f, -10.0f), new Vec2f(10.0f, 10.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.DustEmitter);
            GameState.ParticleEmitterCreationApi.SetName("dust-emitter");
            GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.DustParticle);
            GameState.ParticleEmitterCreationApi.SetDuration(0.1f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(1);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.02f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.GasEmitter);
            GameState.ParticleEmitterCreationApi.SetName("gas-emitter");
            GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.GasParticle);
            GameState.ParticleEmitterCreationApi.SetDuration(0.5f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.25f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(1);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.02f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.Blood);
            GameState.ParticleEmitterCreationApi.SetName("blood");
            GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.Blood);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(10);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-1.0f, -1.0f), new Vec2f(1.0f, 1.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.WoodEmitter);
            GameState.ParticleEmitterCreationApi.SetName("wood");
            GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.Wood);
            GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(10);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-1.0f, -1.0f), new Vec2f(1.0f, 1.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.ExplosionEmitter);
            GameState.ParticleEmitterCreationApi.SetName("blood");
            GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.Explosion);
            GameState.ParticleEmitterCreationApi.SetDuration(4.0f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.7f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(5);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(10.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0.0f), new Vec2f(0.0f, 0.0f));
            GameState.ParticleEmitterCreationApi.End();

            GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.ShrapnelEmitter);
            GameState.ParticleEmitterCreationApi.SetName("shrapnel-emitter");
            GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.Shrapnel);
            GameState.ParticleEmitterCreationApi.SetDuration(0.15f);
            GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
            GameState.ParticleEmitterCreationApi.SetParticleCount(30);
            GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.0f);
            GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-5.0f, -5.0f), new Vec2f(5.0f, 5.0f));
            GameState.ParticleEmitterCreationApi.End();
        }
    }

}
