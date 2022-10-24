using Enums;
using Utility;

namespace AI
{
    public class BehaviorList
    {

        public BehaviorProperties[] List;
        public int Length;
        public int Capacity;

        public BehaviorList()
        {
            List = new BehaviorProperties[16];
            Capacity = List.Length;
        }

        public void Add(BehaviorProperties behavior)
        {
            if (Length >= Capacity)
                ExpandArray();

            List[Length] = behavior;
            Length++;
        }

        public ref BehaviorProperties Get(int index)
        {
            Utils.Assert(index < Length);
            return ref List[index];
        }

        public void Remove(int index)
        {
            Utils.Assert(index >= 0 && index < Length);

            ref BehaviorProperties behavior = ref List[index];
            Length--;
            if (index != Length)
            {
                behavior = List[Length];
                behavior.TypeID = (BehaviorType)index;
            }
        }

        private void ExpandArray()
        {
            int NewCapacity = Capacity + 8;
            System.Array.Resize(ref List, Capacity);
        }
    }
}
