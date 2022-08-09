using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Particle
{
    public class ParticleList
    {
        
        // array for storing entities
        public ParticleEntity[] List;

        public int Length;

        // the capacity is just the length of the list
        public int Capacity;

        public ParticleList()
        {
            List = new ParticleEntity[4096];
            Capacity = List.Length;
        }


        public ParticleEntity Add(ParticleEntity entity)
        {
            // if we dont have enough space we expand
            // the capacity
            ExpandArray();


            int LastIndex = Length;
            entity.ReplaceParticleID(LastIndex);
            List[LastIndex] = entity;
            Length++;

             return List[LastIndex];
        }


        public ParticleEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int particleId)
        {
            Utils.Assert(particleId >= 0 && particleId < Length);

            ref ParticleEntity entity = ref List[particleId];
            entity.Destroy();

            if (particleId != Length - 1)
            {
                entity = List[Length - 1];
                entity.ReplaceParticleID(particleId);
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