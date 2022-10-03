using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Pod
{
    public class PodList
    {
        // array for storing entities
        public PodEntity[] List;

        public int Length;

        // the capacity is just the length of the list
        public int Capacity;

        public PodList()
        {
            List = new PodEntity[4096];
            Capacity = List.Length;
        }


        public PodEntity Add(PodEntity entity)
        {
            // if we dont have enough space we expand
            // the capacity
            ExpandArray();


            int LastIndex = Length;
            List[LastIndex] = entity;
            entity.podID.Index = LastIndex;
            Length++;

            return List[LastIndex];
        }


        public PodEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int index)
        {
            Utils.Assert(index >= 0 && index < Length);

            ref PodEntity entity = ref List[index];
            entity.Destroy();

            if (index != Length - 1)
            {
                entity = List[Length - 1];
                entity.podID.Index = index;
            }
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