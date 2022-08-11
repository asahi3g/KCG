using Enums;
using KMath;
using System;
using System.Collections.Generic;
using UnityEngine;
using PlanetTileMap;

namespace Mech
{
    //https://github.com/kk-digital/kcg/issues/397

    public class MechCreationApi
    {
        public MechProperties[] PropertiesArray;

        public int CurrentIndex;

        private Dictionary<string, int> NameToID;

        public MechCreationApi()
        {
            NameToID = new Dictionary<string, int>();

            PropertiesArray = new MechProperties[1024];

            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new MechProperties();
            }

            CurrentIndex = -1;
        }

        public MechProperties Get(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id];
            }

            return new MechProperties();
        }

        public ref MechProperties GetRef(int Id)
        {
            return ref PropertiesArray[Id];
        }

        public MechProperties Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get(value);
            }

            return new MechProperties();
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
                PropertiesArray[CurrentIndex].MechID = CurrentIndex;
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

        public void SetDropTableID(int dropTableID)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DropTableID = dropTableID;
            }
        }

        public void SetTexture(int spriteId)
        {
            PropertiesArray[CurrentIndex].SpriteID = spriteId;
        }

        public void SetSpriteSize(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].SpriteSize = size;
            }
        }

        public void SetAction(Enums.ActionType actionType)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Action = actionType;
            }
        }

        public void SetInventory(int inventoryModelID)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].InventoryModelID = inventoryModelID;
                PropertiesArray[CurrentIndex].MechFlags |= MechProperties.Flags.HasInventory;
            }
        }

        public void SetDurability(int durability)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Durability = durability;
                PropertiesArray[CurrentIndex].MechFlags |= MechProperties.Flags.IsBreakable;
            }
        }

        public void End()
        {
            CurrentIndex = -1;
        }

    }
}