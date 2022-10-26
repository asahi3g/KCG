using AI;
using Enums;
using KMath;
using System.Collections.Generic;
using System;

namespace Node.Action
{
    public class SelectClosestTarget : NodeBase
    {
        public override NodeType Type => NodeType.SelectClosestTarget;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;

        public override List<Tuple<string, Type>> RegisterEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("Target", typeof(Vec2f)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(NodeEntity nodeEntity)
        {
            AgentEntity agent = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            AgentEntity target = null;
            float dist = float.MaxValue;
            for (int i = 0; i < GameState.Planet.AgentList.Length; i++)
            {
                AgentEntity entity = GameState.Planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID || !entity.isAgentAlive)
                    continue;

                float newDist = (agent.agentPhysicsState.Position - entity.agentPhysicsState.Position).Magnitude;
                if (dist > newDist)
                {
                    target = entity;
                    dist = newDist;
                }
            }

            if (target == null)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
            }
            else
            {
                nodeEntity.nodeExecution.State = NodeState.Success;
                BlackBoard blackBoard = agent.agentController.Controller.BlackBoard;
                blackBoard.Set(nodeEntity.nodeBlackboardData.entriesIDs[0], target.agentPhysicsState.Position + target.physicsBox2DCollider.Size * 1f / 2f);
            }
        }
    }
}
