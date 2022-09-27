using Enums;
using Planet;
using UnityEngine;
using AI;

namespace Node
{
    // Todo(Urgent): Implement this.
    public class SelectorNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.SequenceNode; } }

        // Todo: Allow selection between more than two nodes.
        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            BlackBoard blackBoard = agentEntity.agentController.Controller.BlackBoard;
            int index = 1;
            bool first = false;
            blackBoard.Get(nodeEntity.nodeBlackboardData.DataID, ref first);
            if (first)
                index = 0;

            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodeComposite.Children[index]);
            ref var nodes = ref SystemState.Nodes;
            index = (int)child.nodeID.TypeID;
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
