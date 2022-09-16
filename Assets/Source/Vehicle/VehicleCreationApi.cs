using System;
using System.Collections.Generic;
using UnityEngine;
using KMath;


namespace Vehicle
{
    public class VehicleCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private VehicleProperties[] PropertiesArray;

        private Dictionary<string, int> NameToID;

        public VehicleCreationApi()
        {
            NameToID = new Dictionary<string, int>();
            PropertiesArray = new VehicleProperties[1024];
            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new VehicleProperties();
            }
            CurrentIndex = -1;
        }

        public VehicleProperties Get(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id];
            }

            return new VehicleProperties();
        }

        public ref VehicleProperties GetRef(int Id)
        {
            return ref PropertiesArray[Id];
        }

        public VehicleProperties Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get(value);
            }

            return new VehicleProperties();
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

        public void SetSpriteId(int SpriteId)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].SpriteId = SpriteId;
            }
        }

        public void SetSize(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].SpriteSize = size;
            }
        }

        public void SetCollisionSize(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].CollisionSize = size;
            }
        }

        public void SetCollisionOffset(Vec2f offset)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].CollisionOffset = offset;
            }
        }

        public void SetScale(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Scale = size;
            }
        }

        public void SetRotation(float newRotation)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Rotation = newRotation;
            }
        }

        public void SetAngularVelocity(Vec2f velocity)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].AngularVelocity = velocity;
            }
        }

        public void SetAngularMass(float AngularMass)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].AngularMass = AngularMass;
            }
        }

        public void SetAngularAcceleration(float AngularAcceleration)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].AngularAcceleration = AngularAcceleration;
            }
        }

        public void SetCenterOfGravity(float centerOfGravity)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].CenterOfGravity = centerOfGravity;
            }
        }

        public void SetCenterOfRotation(Vec2f CenterOfRotation)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].CenterOfRotation = CenterOfRotation;
            }
        }

        public void SetAffectedByGravity(bool AffectedByGravity)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].AffectedByGravity = AffectedByGravity;
            }
        }

        public void End()
        {
            CurrentIndex = -1;
        }

        public int JetChassis;

        public void InitializeResources()
        {
            JetChassis = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Vehicles\\Jet\\Chassis\\Jet_chassis.png", 144, 96);
            JetChassis = GameState.SpriteAtlasManager.CopySpriteToAtlas(JetChassis, 0, 0, Enums.AtlasType.Vehicle);

            GameState.VehicleCreationApi.Create((int)Enums.VehicleType.Jet);
            GameState.VehicleCreationApi.SetName("Car");
            GameState.VehicleCreationApi.SetSpriteId(JetChassis);
            GameState.VehicleCreationApi.SetSize(new Vec2f(3.0f, 3.0f));
            GameState.VehicleCreationApi.SetCollisionSize(new Vec2f(2.0f, 2.0f));
            GameState.VehicleCreationApi.SetCollisionOffset(new Vec2f(0, -3.0f));
            GameState.VehicleCreationApi.SetScale(new Vec2f(1.0f, 1.0f));
            GameState.VehicleCreationApi.SetRotation(-90.0f);
            GameState.VehicleCreationApi.SetAngularVelocity(Vec2f.Zero);
            GameState.VehicleCreationApi.SetAngularMass(14f);
            GameState.VehicleCreationApi.SetAngularAcceleration(4f);
            GameState.VehicleCreationApi.SetCenterOfGravity(-6f);
            GameState.VehicleCreationApi.SetCenterOfRotation(Vec2f.Zero);
            GameState.VehicleCreationApi.SetAffectedByGravity(true);
            GameState.VehicleCreationApi.End();
        }
    }

}
