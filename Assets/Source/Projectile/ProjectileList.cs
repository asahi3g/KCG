using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Projectile
{
    public class ProjectileList
    {
        
        // array for storing entities
        public ProjectileEntity[] List;

        public int Length;
        // used for tracking down an available 
        // index that we can use to insert
        public int LastFreeIndex;

        // the capacity is just the length of the list
        public int Capacity;

        public ProjectileList()
        {
            List = new ProjectileEntity[4096];
            Capacity = List.Length;
        }


        public ProjectileEntity Add(ProjectileEntity entity)
        {
            // if we dont have enough space we expand
            // the capacity
            ExpandArray();


            // trying to find an empty index
            // we use LastFreeIndex for a faster insertion
            int Found = -1;
            for(int index = LastFreeIndex; index < Capacity; index++)
            {
                ProjectileEntity thisEntity = List[index];

                if (thisEntity == null)
                {
                    Found = index;
                    break;
                }
            }
            if (Found == -1)
            {
                for(int index = 0; index < LastFreeIndex; index++)
                {
                    ProjectileEntity thisEntity = List[index];

                    if (thisEntity == null)
                    {
                        Found = index;
                        break;
                    }
                }
            }

            // increment the LastFreeIndex
            LastFreeIndex = (LastFreeIndex + 1) % Capacity;


            // creating the Entity and initializing it
            entity.ReplaceProjectileID(Found);
            List[Found] = entity;
            Length++;

             return List[Found];
        }


        public ProjectileEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int floatingTextId)
        {
            LastFreeIndex = floatingTextId;
            ref ProjectileEntity entity = ref List[floatingTextId];
            entity.Destroy();
            entity = null;
            Length--;
        }




        // used to grow the list
        private void ExpandArray()
        {
            if (Length >= Capacity)
            {
                int NewCapacity = Capacity + 4096;

                // make sure the new capacity 
                // is bigget than the old one
                Utils.Assert(NewCapacity > Capacity);
                Capacity = NewCapacity;
                System.Array.Resize(ref List, Capacity);
            }
        }

    }
}