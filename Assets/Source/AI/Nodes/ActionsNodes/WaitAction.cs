using Entitas;
using Enums;
using Planet;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Node
{
    public class WaitAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.WaitAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }
        public override List<Tuple<string, Type>> RegisterStates()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("Wait", typeof(int)),
            };
            return blackboardEntries;
    }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeTime.StartTime = Time.realtimeSinceStartup;
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ref AI.BlackBoard blackboard = ref planet.EntitasContext.agent.GetEntityWithAgentID(
                nodeEntity.nodeOwner.AgentID).agentController.Controller.BlackBoard;

            float elapsed = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            int duration;
            blackboard.Get(nodeEntity.nodeBlackboardData.entriesIDs[0], out duration);
            if (elapsed >= duration)
                nodeEntity.nodeExecution.State = NodeState.Success;
        }

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
        }
    }
}
