using NodeSystem;
using BehaviorTree;
using Planet;
using UnityEngine;

namespace Action
{
    public class WaitAction
    {
        public struct WaitActionData
        {
            public WaitActionData(float waitTime)
            {
                WaitTime = waitTime;
            }

            public readonly float WaitTime;
        }
        // Action used only by AI.
        static public NodeState Action(object ptr, int id)
        {
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            ref WaitActionData waitData = ref data.GetNodeData<WaitActionData>(id);
            PlanetState planet = GameState.Planet;
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            var physicsState = agent.agentPhysicsState;

            if (data.NodesExecutiondata[id].ExecutionTime > waitData.WaitTime)
                return NodeState.Success;
            return NodeState.Running;
        }
    }
}
