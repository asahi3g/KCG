using Enums;
using UnityEngine;
using Planet;

namespace Node
{
    public class SequenceNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.SequenceNode; } }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeComposite.CurrentID = 0;
            nodeEntity.nodeExecution.State = Enums.NodeState.Running;
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            var childern = nodeEntity.nodeComposite.Children;          
            if (nodeEntity.nodeComposite.CurrentID >= childern.Count)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail; // children is empty.
                return;
            }

            int nodeID = childern[nodeEntity.nodeComposite.CurrentID];
            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);

            switch (child.nodeExecution.State)
            {
                case Enums.NodeState.Entry:
                    //child.nodeExecution.Logic.OnEnter(ref planet, child);
                    break;
                case Enums.NodeState.Running:
                    //child.nodeExecution.Logic.OnUpdate(ref planet, child);
                    break;
                case Enums.NodeState.Success:
                    //child.nodeExecution.Logic.OnExit(ref planet, child);
                    nodeEntity.nodeComposite.CurrentID++;
                    if (nodeEntity.nodeComposite.CurrentID >= childern.Count)
                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                    break;
                case Enums.NodeState.Fail:
                    //child.nodeExecution.Logic.OnExit(ref planet);
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
