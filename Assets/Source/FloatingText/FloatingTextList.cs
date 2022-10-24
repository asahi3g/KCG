namespace FloatingText
{
    public class FloatingTextList
    {
        
        // array for storing entities
        public FloatingTextEntity[] List;

        public int Length;

        // the capacity is just the length of the list
        public int Capacity;

        public FloatingTextList()
        {
            List = new FloatingTextEntity[4096];
            Capacity = List.Length;
        }


        public FloatingTextEntity Add(FloatingTextEntity entity)
        {
            // if we dont have enough space we expand
            // the capacity
            ExpandArray();


            int LastIndex = Length;
            List[LastIndex] = entity;
            entity.floatingTextID.Index = LastIndex;
            Length++;

             return List[LastIndex];
        }


        public FloatingTextEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int index)
        {
            Utils.Assert(index >= 0 && index < Length);

            ref FloatingTextEntity entity = ref List[index];
            entity.Destroy();

            if (index != Length - 1)
            {
                entity = List[Length - 1];
                entity.floatingTextID.Index = index;
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