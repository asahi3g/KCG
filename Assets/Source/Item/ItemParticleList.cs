
using Utility;

namespace Item
{
    // Don't use entitas internal lists because they don't have deterministic order.
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
            entity.itemID.Index = Length;
            Length++;

             return List[LastIndex];
        }


        public ItemParticleEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int index)
        {
            Utils.Assert(index >= 0 && index < Length);

            ref ItemParticleEntity entity = ref List[index];
            entity.Destroy();
            Length--;

            if (index != Length)
            {
                entity = List[Length];
                entity.itemID.Index = index;
            }
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