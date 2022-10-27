using Enums;
using UnityEngine;
using AI;

namespace Node
{
    public class SequenceNode : NodeBase
    {
        public override NodeType Type => NodeType.SequenceNode;
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

        public override void OnUpdate(NodeEntity nodeEntity)
        {
            var childern = nodeEntity.nodeComposite.Children;
            if (nodeEntity.nodeComposite.CurrentID >= childern.Count)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            int nodeID = childern[nodeEntity.nodeComposite.CurrentID];
            NodeEntity child = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);

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
                    nodeEntity.nodeComposite.CurrentID++;
                    if (nodeEntity.nodeComposite.CurrentID >= childern.Count)
                        nodeEntity.nodeExecution.State = NodeState.Success;
                    break;
                case NodeState.Fail:
                    nodes[index].OnFail(child);
                    nodeEntity.nodeExecution.State = NodeState.Fail;
                    break;
                default:
                    Debug.Log("Not valid Action state.");
                    break;
            }
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
        }
    }
}
