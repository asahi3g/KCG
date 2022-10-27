using Enums;
using UnityEngine;
using AI;

namespace Node
{
    public class RepeaterNode : NodeBase
    {
        public override NodeType Type => NodeType.RepeaterNode;
        public override NodeGroup NodeGroup => NodeGroup.DecoratorNode;

        public override void OnUpdate(NodeEntity nodeEntity)
        {
            NodeEntity child = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodeDecorator.ChildID);

            ref var nodes = ref AISystemState.Nodes;
            int index = (int)child.nodeID.TypeID;
            switch (child.nodeExecution.State)
            {
                case NodeState.Entry:
                    nodes[index].OnEnter(child);
                    break;
                case NodeState.Running:
                    nodes[index].OnUpdate(child);
                    break;
                case NodeState.Success:
                    nodes[index].OnExit(child);
                    child.nodeExecution.State = NodeState.Entry;
                    break;
                case NodeState.Fail:
                    nodes[index].OnFail(child);
                    child.nodeExecution.State = NodeState.Entry;
                    break;
                default:
                    Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
