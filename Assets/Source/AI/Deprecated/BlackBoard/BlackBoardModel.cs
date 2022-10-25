using Enums;
using KMath;
using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine.UIElements;
using static AI.BlackBoardModel;

namespace AI
{
    // Only one of each type.
    public class BlackBoardModel
    {
        const int BOOL_MASK = 1 << 24;
        const int FLOAT_MASK = 2 << 24;
        const int INT_MASK = 3 << 24;
        const int VEC2F_MASK = 4 << 24;
        const int TYPE_MASK = 255 << 24;

        public struct EntryInfo
        {
            public string Name;
            public string Description;
            public int ID;

            public Type GetEntryType()
            {
                int typeCode = ID & TYPE_MASK;
                switch (typeCode)
                {
                    case BOOL_MASK:
                        return typeof(bool);
                    case FLOAT_MASK:
                        return typeof(float);
                    case INT_MASK:
                        return typeof(int);
                    case VEC2F_MASK:
                        return typeof(Vec2f);
                    default:
                        return null;
                }
            }
        }

        public BehaviorType BehaviorType;

        public BlackBoard Data;
        public Dictionary<int, int> IDToIndex;
        public Dictionary<string, int> NameToIndex;
        public EntryInfo[] Entries;
        public int Length;
        /// <summary>
        /// Guarantee that Ids doesn't repeat
        /// </summary>
        private int LastIDValue;

        public BlackBoardModel(BehaviorType type)
        {
            BehaviorType = type;
            Data = BlackBoard.CreateBlackboard();
            IDToIndex = new Dictionary<int, int>();
            NameToIndex = new Dictionary<string, int>();
            Entries = new EntryInfo[64];
            Length = 0;
            LastIDValue = 0;
        }
        
        // Todo:
        //      Make sure every name is unique.
        //      Use a singe value for indexing and id. 64 bits.
        //      Create function to set value.
        
        /// <summary>
        /// Register new vairable.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns> Returns ID of variable registered.</returns>
        public int Register(Type type, string name = "", string description = "")
        {
            ref EntryInfo entryInfo = ref Entries[Length];
            entryInfo.Name = name;
            entryInfo.Description = description;

            int id = 0;
            if (type == typeof(bool))
            {
                id = 1 << 24;
                id += LastIDValue++;
                Data.IDToIndex.Add(id, Data.BoolEntries.Count);
                Data.BoolEntries.Add(new bool());
            }
            else if (type == typeof(float))
            {
                id = 2 << 24;
                id += LastIDValue++;
                Data.IDToIndex.Add(id, Data.FloatEnties.Count);
                Data.FloatEnties.Add(new float());
            }
            else if (type == typeof(int))
            {
                id = 3 << 24;
                id += LastIDValue++;
                Data.IDToIndex.Add(id, Data.IntEntries.Count);
                Data.IntEntries.Add(new int());
            }
            else if (type == typeof(Vec2f))
            {
                id = 4 << 24;
                id += LastIDValue++;
                Data.IDToIndex.Add(id, Data.VecEntries.Count);
                Data.VecEntries.Add(new Vec2f());
            }

            entryInfo.ID = id;
            IDToIndex.Add(id, Length++);
            NameToIndex.Add(name, id);
            return id;
        }

        public int Register(ref EntryInfo entryInfo, bool value)
        {
            Entries[Length] = entryInfo;
            Utility.Utils.Assert(IDToIndex.TryAdd(entryInfo.ID, Length++));
            Utility.Utils.Assert(NameToIndex.TryAdd(entryInfo.Name, entryInfo.ID));
            Data.IDToIndex.Add(entryInfo.ID, Data.BoolEntries.Count);
            Data.BoolEntries.Add(value);
            return entryInfo.ID;
        }

        public int Register(ref EntryInfo entryInfo, int value)
        {
            Entries[Length] = entryInfo;
            Utility.Utils.Assert(IDToIndex.TryAdd(entryInfo.ID, Length++));
            Utility.Utils.Assert(NameToIndex.TryAdd(entryInfo.Name, entryInfo.ID));
            Data.IDToIndex.Add(entryInfo.ID, Data.IntEntries.Count);
            Data.IntEntries.Add(value);
            return entryInfo.ID;
        }

        public int Register(ref EntryInfo entryInfo, float value)
        {
            Entries[Length] = entryInfo;
            Utility.Utils.Assert(IDToIndex.TryAdd(entryInfo.ID, Length++));
            Utility.Utils.Assert(NameToIndex.TryAdd(entryInfo.Name, entryInfo.ID));
            Data.IDToIndex.Add(entryInfo.ID, Data.FloatEnties.Count);
            Data.FloatEnties.Add(value);
            return entryInfo.ID;
        }

        public int Register(ref EntryInfo entryInfo, Vec2f value)
        {
            Entries[Length] = entryInfo;
            Utility.Utils.Assert(IDToIndex.TryAdd(entryInfo.ID, Length++));
            Utility.Utils.Assert(NameToIndex.TryAdd(entryInfo.Name, entryInfo.ID));
            Data.IDToIndex.Add(entryInfo.ID, Data.VecEntries.Count);
            Data.VecEntries.Add(value);
            return entryInfo.ID;
        }

        /// <summary>
        /// Get list with index of all entries of a specific type.
        /// Used by visual tool.
        /// </summary>
        public List<int> GetEntriesOfType(Type type)
        {
            List<int> entriesType = new List<int>();
            for (int i = 0; i < Length; i++)
            {
                if (Entries[i].GetEntryType() == type)
                    entriesType.Add(i);
            }
            return entriesType;
        }

        public object GetValue(int id)
        {
            int typeCode = id & TYPE_MASK;
            int index;
            if (Data.IDToIndex.TryGetValue(id, out index))
            {
                switch (typeCode)
                {
                    case BOOL_MASK:
                        return Data.BoolEntries[index];
                    case FLOAT_MASK:
                        return Data.FloatEnties[index];
                    case INT_MASK:
                        return Data.IntEntries[index];
                    case VEC2F_MASK:
                        return Data.VecEntries[index];
                    default:
                        return null;
                }
            }
            else 
            {
                switch (typeCode)
                {
                    case BOOL_MASK:
                        return false;
                    case FLOAT_MASK:
                        return 0f;
                    case INT_MASK:
                        return 0;
                    case VEC2F_MASK:
                        return Vec2f.Zero;
                    default:
                        return null;
                }
            }
        }

        public ref EntryInfo GetEntryInfo(int index) => ref Entries[index];

        public ref EntryInfo GetEntryInfoFromID(int index) => ref Entries[IDToIndex[index]];

        public int GetID(string name)
        {
            int id = -1;
            NameToIndex.TryGetValue(name, out id);
            return id;
        }
    }
}
