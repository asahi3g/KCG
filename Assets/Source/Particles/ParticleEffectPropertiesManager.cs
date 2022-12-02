using System;
using System.Collections.Generic;
using KMath;


namespace Particle
{
    public class ParticleEffectPropertiesManager
    {
        // Start is called before the first frame update

        private int CurrentIndex; // current index into the Offset Array

        private ParticleEffectProperties[] PropertiesArray; // an array of offsets into the LineSegment array 

        private int CurrentOffset;
        private ParticleEffectElement[] ElementArray;


        public ParticleEffectPropertiesManager()
        {
            PropertiesArray = new ParticleEffectProperties[256];
            ElementArray = new ParticleEffectElement[1024];
            

            CurrentIndex = 0;
            CurrentOffset = 0;
        }

        public void InitStage1()
        {

        }

        public void InitStage2()
        {
            InitializeResources();
        }

        public ParticleEffectProperties GetProperties(Enums.ParticleEffect id)
        {
            Utility.Utils.Assert((int)id >= 0 && (int)id < PropertiesArray.Length);

            return PropertiesArray[(int)id];
        }

        public ParticleEffectElement GetElement(int index)
        {
             Utility.Utils.Assert((int)index >= 0 && (int)index < ElementArray.Length);

             return ElementArray[index];
        }

        public void Create(Enums.ParticleEffect Id)
        {
            if ((int)Id + 1 >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length + 1024);
            }

            CurrentIndex = (int)Id;
            PropertiesArray[CurrentIndex] = new ParticleEffectProperties{Offset=CurrentOffset};
        }
        

        
        public void AddEmitter(ParticleEmitterType type, Vec2f elementOffset, float delay)
        {
            if ((int)type >= ElementArray.Length)
            {
                Array.Resize(ref ElementArray, ElementArray.Length + 1024);
            }

            PropertiesArray[CurrentIndex].Size++;

            ElementArray[CurrentOffset].Offset = elementOffset;
            ElementArray[CurrentOffset].Delay = delay;
            ElementArray[CurrentOffset++].Emitter = type;
        }


        public void End()
        {

        }



        public void InitializeResources()
        {
            ParticleEffectPropertiesManager Api = GameState.ParticleEffectPropertiesManager;

            Api.Create(Enums.ParticleEffect.Blood_Small);
            Api.AddEmitter(ParticleEmitterType.BloodSmoke, Vec2f.Zero, 0.0f);
            Api.AddEmitter(ParticleEmitterType.BloodFog, Vec2f.Zero, 0.0f);
            Api.End();

            Api.Create(Enums.ParticleEffect.Blood_Medium);
            Api.AddEmitter(ParticleEmitterType.Blood, Vec2f.Zero, 0.0f);
            Api.AddEmitter(ParticleEmitterType.Blood2, Vec2f.Zero, 0.0f);
            Api.AddEmitter(ParticleEmitterType.BloodSmoke, Vec2f.Zero, 0.0f);
            Api.AddEmitter(ParticleEmitterType.BloodFog, Vec2f.Zero, 0.0f);
            Api.End();


            Api.Create(Enums.ParticleEffect.Explosion_2);
            Api.AddEmitter(ParticleEmitterType.Explosion_2_Part4, new Vec2f(-0.3f, -0.3f), 0.0f);
            Api.AddEmitter(ParticleEmitterType.Explosion_2_Part3, Vec2f.Zero, 0.0f);
            Api.AddEmitter(ParticleEmitterType.Explosion_2_Part2, Vec2f.Zero, 0.0f);
            Api.AddEmitter(ParticleEmitterType.Explosion_2_Part1, Vec2f.Zero, 0.0f);
            Api.End();


            Api.Create(Enums.ParticleEffect.Smoke_2);
            Api.AddEmitter(ParticleEmitterType.Smoke_2, Vec2f.Zero, 0.0f);
            Api.End();


            Api.Create(Enums.ParticleEffect.Smoke_3);
            Api.AddEmitter(ParticleEmitterType.Smoke_3, Vec2f.Zero, 0.0f);
            Api.End();


            Api.Create(Enums.ParticleEffect.Dust_Jumping);
            for(int i = 0; i < 8; i++)
            {
                Api.AddEmitter(ParticleEmitterType.Dust_Jumping, new Vec2f(0.0f, 0.15f * i), 0.015f * i);
            }
            Api.End();
        }


    }

}
