using AI;
using UnityEngine;

namespace Node
{
    public class SchedulerSystem
    {
        public void Update()
        {
            NodeEntity[] nodes = GameState.Planet.EntitasContext.node.GetEntities();
            int length = nodes.Length;

            for (int i = 0; i < length; i++)
            {
                NodeEntity node = nodes[i];
                
                if (node.isNodeBT)
                    continue;

                int index = (int)node.nodeID.TypeID;
                switch (node.nodeExecution.State)
                {
                    case Enums.NodeState.Entry:
                        AISystemState.Nodes[index].OnEnter(node);
                        break;
                    case Enums.NodeState.Running:
                        AISystemState.Nodes[index].OnUpdate(node);
                        break;
                    case Enums.NodeState.Success:
                        AISystemState.Nodes[index].OnExit(node);
                        node.Destroy();
                        break;
                    case Enums.NodeState.Fail:
                        AISystemState.Nodes[index].OnExit(node);
                        node.Destroy();
                        break;
                    default:
                        Debug.Log("Not valid Action state.");
                        break;
                }
            }
        }
    }
}
