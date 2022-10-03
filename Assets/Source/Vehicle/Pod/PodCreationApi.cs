using System;
using System.Collections.Generic;
using UnityEngine;
using KMath;


namespace Pod
{
    public class PodCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private PodProperties[] PropertiesArray;

        private Dictionary<string, int> NameToID;

        public PodCreationApi()
        {
            NameToID = new Dictionary<string, int>();
            PropertiesArray = new PodProperties[1024];
            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new PodProperties();
            }
            CurrentIndex = -1;
        }

        public PodProperties Get(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id];
            }

            return new PodProperties();
        }

        public ref PodProperties GetRef(int Id)
        {
            return ref PropertiesArray[Id];
        }

        public PodProperties Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get(value);
            }

            return new PodProperties();
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

        public int Pod;

        public void InitializeResources()
        {
            
        }
    }

}
