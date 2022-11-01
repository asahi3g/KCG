using Utility;

namespace Inventory
{
    public class InventoryList
    {
        
        // Array for storing entities
        public InventoryEntity[] List;

        public int Length;

        // the capacity is just the length of the list
        public int Capacity;

        public InventoryList()
        {
            List = new InventoryEntity[4096];
            Capacity = List.Length;
        }

        public InventoryEntity Add(InventoryEntity entity)
        {
            // if we dont have enough space we expand
            // the capacity
            ExpandArray();

            int LastIndex = Length;
            entity.inventoryEntity.Index = LastIndex;
            List[LastIndex] = entity;
            Length++;

             return List[LastIndex];
        }


        public InventoryEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int index)
        {
            Utils.Assert(index >= 0 && index < Length);

            ref InventoryEntity entity = ref List[index];
            entity.Destroy();

            if (index != Length - 1)
            {
                entity = List[Length - 1];
                entity.inventoryEntity.Index = index;
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