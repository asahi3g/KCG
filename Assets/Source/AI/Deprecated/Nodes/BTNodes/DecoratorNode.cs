//imports UnityEngine

using Enums;
using AI;

namespace Node
{
    public class DecoratorNode : NodeBase
    {
        public override NodeType Type => NodeType.DecoratorNode;
        public override NodeGroup NodeGroup => NodeGroup.DecoratorNode;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            NodeEntity child = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodeDecorator.ChildID);
            child.nodeExecution.State = NodeState.Entry;
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

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
                    break;
                case NodeState.Fail:
                    nodes[index].OnFail(child);
                    nodeEntity.nodeExecution.State = NodeState.Fail;
                    break;
                default:
                    UnityEngine.Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
