using KMath;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using static UnityEditor.PlayerSettings;

namespace AI
{
    public class BlackBoard
    {
        const int MASK = 0b0000000_11111111_11111111_11111111;

        public int OwnerAgentID;

        List<bool>   BoolEntries;
        List<float>  FloatEnties;
        List<int>    IntEntries;
        List<Vec2f>  VecEntries;

        Dictionary<int, int> IDsToIndex;
        List<string> Names;
        List<string> Descriptions;
        int Length;

        public BlackBoard(int ownerAgentID)
        {
            OwnerAgentID = ownerAgentID;
            BoolEntries = new List<bool>();
            FloatEnties = new List<float>();
            IntEntries = new List<int>();
            VecEntries = new List<Vec2f>();

            IDsToIndex = new Dictionary<int, int>();
            Names = new List<string>();
            Descriptions = new List<string>();
            Length = 0;
        }

        /// <summary>
        /// Register new vairable.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns> Returns ID of variable registered.</returns>
        public int Register(Type type, string name = "", string description = "") 
        {
            Names.Add(name);
            Descriptions.Add(description);

            int id = 0;
            if (type == typeof(bool))
            {
                id = 1 << 31;
                id += BoolEntries.Count;
                BoolEntries.Add(new bool());
            }
            else if (type == typeof(float))
            {
                id = 1 << 30;
                id += FloatEnties.Count;
                FloatEnties.Add(new float());
            }
            else if (type == typeof(int))
            {
                id = 1 << 29;
                id += IntEntries.Count;
                IntEntries.Add(new int());
            }
            else if (type == typeof(Vec2f))
            {
                id = 1 << 28;
                id += VecEntries.Count;
                VecEntries.Add(new Vec2f());
            }

            IDsToIndex.Add(id, Length++);
            return id;
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
