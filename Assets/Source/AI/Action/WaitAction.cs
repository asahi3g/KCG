using NodeSystem;
using BehaviorTree;

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
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            ref WaitActionData waitData = ref data.GetNodeData<WaitActionData>(id);

            if (data.NodesExecutiondata[id].ExecutionTime > waitData.WaitTime)
                return NodeState.Success;
            return NodeState.Running;
        }
    }
}
