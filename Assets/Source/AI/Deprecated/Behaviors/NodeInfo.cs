using Enums;
using KMath;
using System.Collections.Generic;

namespace AI
{
    public class NodeInfo
    {
        public int index;
        public NodeType type;
        public Vec2f pos;
        public List<int> children;
        public List<int> entriesID;
    }
}
