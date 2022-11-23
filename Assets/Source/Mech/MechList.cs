using Utility;

namespace Mech
{
    // Don't use entitas internal lists because they don't have deterministic order.
    public class MechList
    {
        
        // array for storing entities
        public MechEntity[] List;

        public int Length;

        // the capacity is just the length of the list
        public int Capacity;

        public MechList()
        {
            List = new MechEntity[4096];
            Capacity = List.Length;
        }


        public MechEntity Add(MechEntity entity)
        {
            // if we dont have enough space we expand
            // the capacity
            ExpandArray();


            int LastIndex = Length;
            List[LastIndex] = entity;
            entity.mechID.Index = LastIndex;
            Length++;

             return List[LastIndex];
        }


        public MechEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int index)
        {
            Utils.Assert(index >= 0 && index < Length);

            ref MechEntity entity = ref List[index];
            entity.Destroy();
            entity = null;

            if (index != Length - 1)
            {
                entity = List[Length - 1];
                entity.mechID.Index = index;
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
