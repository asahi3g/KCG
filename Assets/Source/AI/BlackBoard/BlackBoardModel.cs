using Enums;
using KMath;
using System;
using System.Collections.Generic;

namespace AI
{
    // Only one of each type.
    public class BlackBoardModel
    {
        public BehaviorType BehaviorType;

        BlackBoard BlackBoard;
        Dictionary<int, int> IDsToIndex;
        List<string> Names;
        List<string> Descriptions;
        int Length;

        public BlackBoardModel(BehaviorType type)
        {
            BehaviorType = type;
            BlackBoard = new BlackBoard();
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
                id += BlackBoard.BoolEntries.Count;
                BlackBoard.BoolEntries.Add(new bool());
            }
            else if (type == typeof(float))
            {
                id = 1 << 30;
                id += BlackBoard.FloatEnties.Count;
                BlackBoard.FloatEnties.Add(new float());
            }
            else if (type == typeof(int))
            {
                id = 1 << 29;
                id += BlackBoard.IntEntries.Count;
                BlackBoard.IntEntries.Add(new int());
            }
            else if (type == typeof(Vec2f))
            {
                id = 1 << 28;
                id += BlackBoard.VecEntries.Count;
                BlackBoard.VecEntries.Add(new Vec2f());
            }

            IDsToIndex.Add(id, Length++);
            return id;
        }

        public BlackBoard Instantiate()
        {
            return BlackBoard;
        }

    }
}
