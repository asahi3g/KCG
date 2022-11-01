using NodeSystem;
using NodeSystem.BehaviorTree;
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
        static public NodeState Action(object ptr, int id)
        {
            ref BehaviorTreeState data = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref WaitActionData waitData = ref data.GetNodeData<WaitActionData>(id);

            if (data.NodesExecutiondata[id].ExecutionTime > waitData.WaitTime)
                return NodeState.Success;
            return NodeState.Running;
        }
    }
}
