using Enums;
using KMath;
using System.Collections.Generic;

namespace AI
{
    public class MarineBehavior : BehaviorBase
    {
        public override BehaviorType Type { get { return BehaviorType.Marine; } }
        
        NodeInfo Root = new NodeInfo
        {
            index = 0,
            pos = new Vec2f(24.79999f, -227.4763f),
            type = NodeType.DecoratorNode,
         
            children = new List<int>(){1}
        };

        NodeInfo Child1 = new NodeInfo
        {
            index = 1,
            pos = new Vec2f(24.79998f, -77.41328f),
            type = NodeType.RepeaterNode,
            children = new List<int>(){ 8}
        };

        NodeInfo Child2 = new NodeInfo
        {
            index = 2,
            pos = new Vec2f(125.6f, 185.6f),
            type = NodeType.SelectorNode,
            children = new List<int>(){ 9,  7}
        };

        NodeInfo Child3 = new NodeInfo
        {
            index = 3,
            pos = new Vec2f(-99.59993f, 186.1451f),
            type = NodeType.SequenceNode,
            children = new List<int>(){ 4,  5,  6}
        };

        NodeInfo Child4 = new NodeInfo
        {
            index = 4,
            pos = new Vec2f(-324.097f, 331.2001f),
            type = NodeType.SelectClosestTarget,
        };

        NodeInfo Child5 = new NodeInfo
        {
            index = 5,
            pos = new Vec2f(-174.497f, 332.8f),
            type = NodeType.ShootFireWeaponAction,
        };

        NodeInfo Child6 = new NodeInfo
        {
            index = 6,
            pos = new Vec2f(-24.097f, 332.8f),
            type = NodeType.WaitAction,
        };

        NodeInfo Child7 = new NodeInfo
        {
            index = 7,
            pos = new Vec2f(252.7913f, 331.2f),
            type = NodeType.WaitAction,
            children = new List<int>(){}
        };

        NodeInfo Child8 = new NodeInfo
        {
            index = 8,
            pos = new Vec2f(24.79998f, 70.76591f),
            type = NodeType.SequenceNode,
            children = new List<int>(){ 3,  2}
        };

        NodeInfo Child9 = new NodeInfo
        {
            index = 9,
            pos = new Vec2f(117.767f, 332.8f),
            type = NodeType.WaitAction,
        };

        public override List<NodeInfo> Nodes
        {
            get
            {
                List<NodeInfo> nodes = new List<NodeInfo>();
                nodes.Add(Root);
                nodes.Add(Child1);
                nodes.Add(Child2);
                nodes.Add(Child3);
                nodes.Add(Child4);
                nodes.Add(Child5);
                nodes.Add(Child6);
                nodes.Add(Child7);
                nodes.Add(Child8);
                nodes.Add(Child9);
                return nodes;
            }
        }
    };
}

