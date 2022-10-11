using Enums;
using System.Collections.Generic;
using KMath;
using System.Windows.Forms.DataVisualization.Charting;
using static Unity.VisualScripting.Metadata;

namespace AI
{
    public class BehaviorBase
    {
        public virtual BehaviorType Type { get { return BehaviorType.Error; } }
        public virtual string Name { get { return Type.ToString(); } }
        public virtual List<NodeInfo> Nodes
        {
            get
            {
                List<NodeInfo> nodes = new List<NodeInfo>();
                NodeInfo node = new NodeInfo
                {
                    index = 0,
                    pos = new Vec2f(0, 0),
                    type = NodeType.DecoratorNode,
                    children = new List<int>()
                };
                nodes.Add(node);
                return nodes;
            }
        }
    }
}
