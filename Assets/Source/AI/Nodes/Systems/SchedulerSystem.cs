using AI;
using UnityEngine;

namespace Node
{
    // 
    public class SchedulerSystem
    {
        public void Update(Contexts contexts, float deltaTime, ref Planet.PlanetState planet)
        {
            NodeEntity[] nodes = contexts.node.GetEntities();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].hasNodeExecution)
                {
                    int index = (int)nodes[i].nodeID.TypeID;
                    switch (nodes[i].nodeExecution.State)
                    {
                        case Enums.NodeState.Entry:
                            SystemState.Nodes[index].OnEnter(ref planet, nodes[i]);
                            break;
                        case Enums.NodeState.Running:
                            SystemState.Nodes[index].OnUpdate(ref planet, nodes[i]);
                            break;
                        case Enums.NodeState.Success:
                            SystemState.Nodes[index].OnExit(ref planet, nodes[i]);
                            break;
                        case Enums.NodeState.Fail:
                            SystemState.Nodes[index].OnExit(ref planet, nodes[i]);
                            break;
                        default:
                            Debug.Log("Not valid Action state.");
                            break;
                    }
                }
            }
        }
    }
}
