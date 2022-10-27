using AI;
using UnityEngine;

namespace Node
{
    public class SchedulerSystem
    {
<<<<<<< HEAD
        public void Update(Planet.PlanetState planet)
=======
        public void Update()
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
        {
            NodeEntity[] nodes = GameState.Planet.EntitasContext.node.GetEntities();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].isNodeBT)
                    continue;

                int index = (int)nodes[i].nodeID.TypeID;
                switch (nodes[i].nodeExecution.State)
                {
                    case Enums.NodeState.Entry:
                        AISystemState.Nodes[index].OnEnter(nodes[i]);
                        break;
                    case Enums.NodeState.Running:
                        AISystemState.Nodes[index].OnUpdate(nodes[i]);
                        break;
                    case Enums.NodeState.Success:
                        AISystemState.Nodes[index].OnExit(nodes[i]);
                        nodes[i].Destroy();
                        break;
                    case Enums.NodeState.Fail:
                        AISystemState.Nodes[index].OnExit(nodes[i]);
                        nodes[i].Destroy();
                        break;
                    default:
                        Debug.Log("Not valid Action state.");
                        break;
                }
            }

            RunBehaviorTrees();
        }

        private void RunBehaviorTrees()
        {
            ref var planet = ref GameState.Planet;
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity agent = planet.AgentList.Get(i);
                if (!agent.hasAgentController || !agent.isAgentAlive)
                    continue;

                AgentController controller = agent.agentController.Controller;
                controller.Update(agent);
                NodeEntity nodeEntity = planet.EntitasContext.node.GetEntityWithNodeIDID(controller.BehaviorTreeRoot);
                
                int index = (int)nodeEntity.nodeID.TypeID;
                switch (nodeEntity.nodeExecution.State)
                {
                    case Enums.NodeState.Entry:
                        AISystemState.Nodes[index].OnEnter(nodeEntity);
                        break;
                    case Enums.NodeState.Running:
                        AISystemState.Nodes[index].OnUpdate(nodeEntity);
                        break;
                    case Enums.NodeState.Success:
                        AISystemState.Nodes[index].OnExit(nodeEntity);
                        break;
                    case Enums.NodeState.Fail:
                        AISystemState.Nodes[index].OnExit(nodeEntity);
                        break;
                    default:
                        Debug.Log("Not valid Action state.");
                        break;
                }
            }
        }
    }
}
