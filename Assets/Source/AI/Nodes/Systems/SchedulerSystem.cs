using AI;
using System;
using UnityEngine;

namespace Node
{
    public class SchedulerSystem
    {
        public void Update(ref Planet.PlanetState planet)
        {
            NodeEntity[] nodes = planet.EntitasContext.node.GetEntities();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].isNodeBT)
                    continue;

                int index = (int)nodes[i].nodeID.TypeID;
                switch (nodes[i].nodeExecution.State)
                {
                    case Enums.NodeState.Entry:
                        AISystemState.Nodes[index].OnEnter(ref planet, nodes[i]);
                        break;
                    case Enums.NodeState.Running:
                        AISystemState.Nodes[index].OnUpdate(ref planet, nodes[i]);
                        break;
                    case Enums.NodeState.Success:
                        AISystemState.Nodes[index].OnExit(ref planet, nodes[i]);
                        nodes[i].Destroy();
                        break;
                    case Enums.NodeState.Fail:
                        AISystemState.Nodes[index].OnExit(ref planet, nodes[i]);
                        nodes[i].Destroy();
                        break;
                    default:
                        Debug.Log("Not valid Action state.");
                        break;
                }
            }

            RunBehaviorTrees(ref planet);
        }

        private void RunBehaviorTrees(ref Planet.PlanetState planet)
        {
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity agent = planet.AgentList.Get(i);
                if (!agent.hasAgentController || !agent.isAgentAlive)
                    continue;

                AgentController controller = agent.agentController.Controller;
                controller.Update(agent, ref planet);
                NodeEntity nodeEntity = planet.EntitasContext.node.GetEntityWithNodeIDID(controller.BehaviorTreeRoot);
                
                int index = (int)nodeEntity.nodeID.TypeID;
                switch (nodeEntity.nodeExecution.State)
                {
                    case Enums.NodeState.Entry:
                        AISystemState.Nodes[index].OnEnter(ref planet, nodeEntity);
                        break;
                    case Enums.NodeState.Running:
                        AISystemState.Nodes[index].OnUpdate(ref planet, nodeEntity);
                        break;
                    case Enums.NodeState.Success:
                        AISystemState.Nodes[index].OnExit(ref planet, nodeEntity);
                        break;
                    case Enums.NodeState.Fail:
                        AISystemState.Nodes[index].OnExit(ref planet, nodeEntity);
                        break;
                    default:
                        Debug.Log("Not valid Action state.");
                        break;
                }
            }
        }
    }
}
