using Enums;
using KMath;
using System.Collections.Generic;

namespace AI
{
    public class InsectBehavior : BehaviorBase
    {
        public override BehaviorType Type { get { return BehaviorType.Insect; } }
        
        NodeInfo Root = new NodeInfo
        {
            index = 0,
            pos = new Vec2f(583.16f, 184.112f),
            type = NodeType.DecoratorNode,
        };

        public override List<NodeInfo> Nodes
        {
            get
            {
                List<NodeInfo> nodes = new List<NodeInfo>();
                nodes.Add(Root);
                return nodes;
            }
        }
    };
}

