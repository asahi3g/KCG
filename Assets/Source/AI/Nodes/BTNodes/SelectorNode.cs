using Enums;
using UnityEngine;
using AI;

namespace Node
{
    // Todo(Urgent): Implement this.
    public class SelectorNode : NodeBase
    {
        public override NodeType Type => NodeType.SelectorNode;
        public override NodeGroup NodeGroup => NodeGroup.CompositeNode;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            nodeEntity.nodeComposite.CurrentID = 0;
            var children = nodeEntity.nodeComposite.Children;
            foreach (int childID in children)
            {
                NodeEntity child = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(childID);
                child.nodeExecution.State = NodeState.Entry;
            }
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        // Todo: Allow selection between more than two nodes.
        public override void OnUpdate(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var childern = nodeEntity.nodeComposite.Children;
            if (nodeEntity.nodeComposite.CurrentID >= childern.Count)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            int nodeID = childern[nodeEntity.nodeComposite.CurrentID];
            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);

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
                    nodeEntity.nodeExecution.State = NodeState.Success;
                    break;
                case NodeState.Fail:
                    nodes[index].OnFail(child);
                    nodeEntity.nodeComposite.CurrentID++;
                    break;
                default:
                    Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
