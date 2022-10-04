using Enums;
using Planet;
using UnityEngine;
using AI;

namespace Node
{
    public class DecoratorNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.DecoratorNode; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.DecoratorNode; } }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodesDecorator.ChildID);
            child.nodeExecution.State = NodeState.Entry;
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodesDecorator.ChildID);

            ref var nodes = ref AISystemState.Nodes;
            int index = (int)child.nodeID.TypeID;
            switch (child.nodeExecution.State)
            {
                case NodeState.Entry:
                    nodes[index].OnEnter(ref planet, child);
                    break;
                case NodeState.Running:
                    nodes[index].OnUpdate(ref planet, child);
                    break;
                case NodeState.Success:
                    nodes[index].OnExit(ref planet, child);
                    break;
                case NodeState.Fail:
                    nodes[index].OnExit(ref planet, child);
                    nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                    break;
                default:
                    Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
