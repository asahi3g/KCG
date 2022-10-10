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
            pos = new Vec2f(0, 0),
            type = NodeType.DecoratorNode,
            children = new List<int>() { 1 }
        };
        NodeInfo node1 = new NodeInfo
        {
            index = 1,
            pos = new Vec2f(0, 0),
            type = NodeType.RepeaterNode,
            children = new List<int>() { 2 }
        };
        NodeInfo node2 = new NodeInfo
        {
            index = 2,
            pos = new Vec2f(0, 0),
            type = NodeType.SelectorNode,
            children = new List<int>() { 3, 8 }
        };
        NodeInfo node3 = new NodeInfo
        {
            index = 3,
            pos = new Vec2f(0, 0),
            type = NodeType.SequenceNode,
            children = new List<int>() { 4, 5, 6, 7 }
        };
        NodeInfo node4 = new NodeInfo
        {
            index = 4,
            pos = new Vec2f(0, 0),
            type = NodeType.SelectClosestTarget,
        };
        NodeInfo node5 = new NodeInfo
        {
            index = 5,
            pos = new Vec2f(0, 0),
            type = NodeType.ShootFireWeaponAction,
        };
        NodeInfo node6 = new NodeInfo
        {
            index = 6,
            pos = new Vec2f(0, 0),
            type = NodeType.WaitAction,
        };
        NodeInfo node7 = new NodeInfo
        {
            index = 7,
            pos = new Vec2f(0, 0),
            type = NodeType.WaitAction,
            children = new List<int>()
        };
        NodeInfo node8 = new NodeInfo
        {
            index = 8,
            pos = new Vec2f(0, 0),
            type = NodeType.SequenceNode,
            children = new List<int>() { 9 }
        };
        NodeInfo node9 = new NodeInfo
        {
            index = 9,
            pos = new Vec2f(0, 0),
            type = NodeType.WaitAction,
        };


        public override List<NodeInfo> Nodes
        {
            get
            {
                List<NodeInfo> nodes = new List<NodeInfo>();
                nodes.Add(Root);
                nodes.Add(node1);
                nodes.Add(node2);
                nodes.Add(node3);
                nodes.Add(node4);
                nodes.Add(node5);
                nodes.Add(node6);
                nodes.Add(node7);
                nodes.Add(node8);
                nodes.Add(node9);
                return nodes;
            }
        }
    }
}
