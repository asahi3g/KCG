using Enums;
using System.Collections.Generic;
using KMath;

namespace AI
{
    public class BehaviorBase
    {
        public virtual BehaviorType Type { get { return BehaviorType.Error; } }

        NodeInfo Root = new NodeInfo
        {
            index = 0,
            pos = new Vec2f(0, 0),
            type = NodeType.DecoratorNode,
            children = new List<int>()
        };
        public virtual string Name { get { return Type.ToString(); } }
        public virtual List<NodeInfo> Nodes
        {
            get
            {
                List<NodeInfo> nodes = new List<NodeInfo>();
                nodes.Add(Root);
                return nodes;
            }
        }
    }
}
