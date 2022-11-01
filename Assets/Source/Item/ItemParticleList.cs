
using Utility;

namespace Item
{
    public class ItemParticleList
    {
        
        // array for storing entities
        public ItemParticleEntity[] List;

        public int Length;

        // the capacity is just the length of the list
        public int Capacity;

        public ItemParticleList()
        {
            List = new ItemParticleEntity[4096];
            Capacity = List.Length;
        }


        public ItemParticleEntity Add(ItemParticleEntity entity)
        {
            // if we dont have enough space we expand
            // the capacity
            ExpandArray();


            int LastIndex = Length;
            List[LastIndex] = entity;
            Length++;

             return List[LastIndex];
        }


        public ItemParticleEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int id)
        {
            Utils.Assert(id >= 0 && id < Length);

            ref ItemParticleEntity entity = ref List[id];
            entity.Destroy();

            if (id != Length - 1)
            {
                entity = List[Length - 1];
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