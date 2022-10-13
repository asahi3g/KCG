using Enums;
using KMath;
using System.Collections.Generic;

namespace AI
{
    public class PatrolBehavior : BehaviorBase
    {
        public override BehaviorType Type { get { return BehaviorType.Patrol; } }
        
        NodeInfo Root = new NodeInfo
        {
            index = 0,
            pos = new Vec2f(572.12f, 173.032f),
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

