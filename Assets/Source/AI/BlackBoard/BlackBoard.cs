using KMath;
using System;
using System.Collections.Generic;

namespace AI
{
    public struct BlackBoard
    {
        const int MASK = 0b0000000_11111111_11111111_11111111;

        public int OwnerAgentID;

        public List<bool>   BoolEntries;
        public List<float>  FloatEnties;
        public List<int>    IntEntries;
        public List<Vec2f>  VecEntries;

        public BlackBoard(int ownerAgentID)
        {
            OwnerAgentID = ownerAgentID;
            BoolEntries = new List<bool>();
            FloatEnties = new List<float>();
            IntEntries = new List<int>();
            VecEntries = new List<Vec2f>();
        }

        public void Get(int ID, ref bool value)
        {
            int index = ID & MASK;
            value = BoolEntries[index];
        }
        public void Get(int ID, ref float value)
        {
            int index = ID & MASK;
            value = FloatEnties[index];
        }
        public void Get(int ID, ref int value)
        {
            int index = ID & MASK;
            value = IntEntries[index];
        }

        public void Get(int ID, ref Vec2f value)
        {
            int index = ID & MASK;
            value = VecEntries[index];
        }

        public void Set(int ID, bool value)
        {
            int index = ID & MASK;
            BoolEntries[index] = value;
        }
        public void Set(int ID, float value)
        {
            int index = ID & MASK;
            FloatEnties[index] = value;
        }
        public void Set(int ID, int value)
        {
            int index = ID & MASK;
            IntEntries[index] = value;
        }

        public void Set(int ID, Vec2f value)
        {
            int index = ID & MASK;
            VecEntries[index] = value;
        }
    }
}
