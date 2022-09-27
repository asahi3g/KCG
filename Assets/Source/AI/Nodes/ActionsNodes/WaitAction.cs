using Entitas;
using Enums;
using UnityEngine;

namespace Node
{
    public class WaitAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.WaitAction; } }

        public override void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            float elapsed = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;

            if (elapsed >= nodeEntity.nodeDuration.Duration)
                nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
