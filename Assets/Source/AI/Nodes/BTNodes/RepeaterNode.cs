using Enums;
using Planet;
using UnityEngine;
using AI;

namespace Node
{
    public class RepeatedNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.RepeatedNode; } }
        public override bool IsActionNode { get { return false; } }


        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodesDecorator.ChildID);

            ref var nodes = ref SystemState.Nodes;
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
                    nodes[index].OnEnter(ref planet, child);
                    child.nodeExecution.State = Enums.NodeState.Running;
                    //nodes[index].OnExit(ref planet, child);
                    break;
                case NodeState.Fail:
                    nodes[index].OnEnter(ref planet, child);
                    child.nodeExecution.State = Enums.NodeState.Running;
                    //nodes[index].OnExit(ref planet, child);
                    //nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                    break;
                default:
                    Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
