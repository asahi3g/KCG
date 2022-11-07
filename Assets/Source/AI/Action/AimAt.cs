using NodeSystem;
using AI;
using Planet;
using KMath;
using System;
using UnityEngine;

namespace Action
{
    public class AimAt
    {
        static public NodeState Action(object ptr, int id)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);

            // Vary angle in 15 degrees.
            Vec2f gunPos = agent.GetGunFiringPosition();
            float angle = gunPos.GetAngle(blackboard.AttackTarget);
            const float SpreadAngle = 5.0f * Mathf.Deg2Rad;
            float randomAngle = UnityEngine.Random.Range(-SpreadAngle, SpreadAngle);
            Vec2f dir = blackboard.AttackTarget - gunPos;
            Vec2f aimTarget = dir.Rotate(randomAngle) + gunPos;

            agent.SetAimTarget(aimTarget);
            return NodeState.Success;
        }
    }
}
