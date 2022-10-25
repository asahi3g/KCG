using KMath;
using System;
using System.Collections.Generic;

namespace AI
{
    public struct BlackBoard
    {
        public List<bool> BoolEntries;
        public List<float> FloatEnties;
        public List<int> IntEntries;
        public List<Vec2f> VecEntries;
        public Dictionary<int, int> IDToIndex;

        public void Get(int ID, out bool value)
        {
            int index = IDToIndex[ID];
            value = BoolEntries[index];
        }
        public void Get(int ID, out float value)
        {
            int index = IDToIndex[ID];
            value = FloatEnties[index];
        }
        public void Get(int ID, out int value)
        {
            int index = IDToIndex[ID];
            value = IntEntries[index];
        }

        public void Get(int ID, out Vec2f value)
        {
            int index = IDToIndex[ID];
            value = VecEntries[index];
        }

        public void Set(int ID, bool value)
        {
            int index = IDToIndex[ID];
            BoolEntries[index] = value;
        }
        public void Set(int ID, float value)
        {
            int index = IDToIndex[ID];
            FloatEnties[index] = value;
        }
        public void Set(int ID, int value)
        {
            int index = IDToIndex[ID];
            IntEntries[index] = value;
        }

        public void Set(int ID, Vec2f value)
        {
            int index = IDToIndex[ID];
            VecEntries[index] = value;
        }

        public static BlackBoard CreateBlackboard()
        {
            BlackBoard blackBoard = new BlackBoard();
            blackBoard.BoolEntries = new List<bool>();
            blackBoard.FloatEnties = new List<float>();
            blackBoard.IntEntries = new List<int>();
            blackBoard.VecEntries = new List<Vec2f>();
            blackBoard.IDToIndex = new Dictionary<int, int>();
            return blackBoard;
        }
    }
}

