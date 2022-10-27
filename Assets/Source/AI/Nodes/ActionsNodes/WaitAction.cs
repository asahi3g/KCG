using Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Node
{
    public class WaitAction : NodeBase
    {
        public override NodeType Type => NodeType.WaitAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;

        public override List<Tuple<string, Type>> RegisterEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("Wait", typeof(int)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(NodeEntity nodeEntity)
        {
            nodeEntity.nodeTime.StartTime = Time.realtimeSinceStartup;
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate(NodeEntity nodeEntity)
        {
            ref AI.BlackBoard blackboard = ref GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(
                nodeEntity.nodeOwner.AgentID).agentController.Controller.BlackBoard;

            float elapsed = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            int duration;
            blackboard.Get(nodeEntity.nodeBlackboardData.entriesIDs[0], out duration);
            if (elapsed >= duration)
                nodeEntity.nodeExecution.State = NodeState.Success;
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
        }
    }
}
