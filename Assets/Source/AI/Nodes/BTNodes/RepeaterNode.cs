using Enums;
using Planet;
using UnityEngine;
using AI;

namespace Node
{
    public class RepeaterNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.RepeaterNode; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.DecoratorNode; } }

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
                    child.nodeExecution.State = Enums.NodeState.Entry;
                    break;
                case NodeState.Fail:
                    nodes[index].OnExit(ref planet, child);
                    child.nodeExecution.State = Enums.NodeState.Entry;
                    break;
                default:
                    Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
