﻿using Enums;
using Planet;
using UnityEngine;

namespace Node
{
    public class WaitAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.WaitAction; } }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeTime.StartTime = Time.realtimeSinceStartup;
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            float elapsed = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;

            if (elapsed >= nodeEntity.nodeDuration.Duration)
                nodeEntity.nodeExecution.State = NodeState.Success;
        }

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
            Debug.Log(nodeEntity.nodeDuration.Duration.ToString() + " seconds.");
        }
    }
}