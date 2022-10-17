using Enums;
using UnityEngine;
using Planet;
using AI;

namespace Node
{
    public class SequenceNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.SequenceNode; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.CompositeNode; } }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeComposite.CurrentID = 0;
            var children = nodeEntity.nodeComposite.Children;
            foreach (int childID in children)
            {
                NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(childID);
                child.nodeExecution.State = NodeState.Entry;
            }
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            var childern = nodeEntity.nodeComposite.Children;
            if (nodeEntity.nodeComposite.CurrentID >= childern.Count)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            int nodeID = childern[nodeEntity.nodeComposite.CurrentID];
            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);

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
                    nodeEntity.nodeComposite.CurrentID++;
                    if (nodeEntity.nodeComposite.CurrentID >= childern.Count)
                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
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

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
        }
    }
}
