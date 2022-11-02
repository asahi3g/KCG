using NodeSystem;
using System.Runtime.CompilerServices;

namespace BehaviorTree
{
     // Function controller for sequence nodes.
    public class SequenceNode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int GetNextChildren(object ptr, int nodeIndex, NodeState lastResult)
        {
            // Get state data. 
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            ref int currentIndex = ref stateData.GetNodeData<int>(nodeIndex);
            ref NodeSystem.Node node = ref GameState.NodeManager.GetRef(stateData.NodesExecutiondata[nodeIndex].Id);

            // success = quit
            int nextChild = BTSpecialChild.ReturnToParent;

            if (currentIndex == BTSpecialChild.NotInitialized)
            {
                // newly activated: start from first
                currentIndex = 0;
                nextChild = currentIndex;
            }
            else if (lastResult == NodeState.Success && (currentIndex + 1) < node.Children.Length)
                nextChild = ++currentIndex;  // failed = choose next child

            return nextChild;
        }
    }

    // Function controller for Selector nodes.
    public class SelectorNode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int GetNextChildren(object ptr, int nodeIndex, NodeState lastResult)
        {
            // Get state data. 
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            ref int currentIndex = ref stateData.GetNodeData<int>(nodeIndex);
            ref NodeSystem.Node node = ref GameState.NodeManager.GetRef(stateData.NodesExecutiondata[nodeIndex].Id);

            // success = quit
            int nextChild = BTSpecialChild.ReturnToParent;

            if (currentIndex == BTSpecialChild.NotInitialized)
            {
                // newly activated: start from first
                currentIndex = 0;
                nextChild = currentIndex;
            }
            else if (lastResult == NodeState.Failure && (currentIndex + 1) < node.Children.Length)
                nextChild = ++currentIndex;  // failed = choose next child

            return nextChild;
        }
    }

    // Route functions for decorator node.
    public class DecoratorNode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int NextRoute(int childrenID)
        {
            if (childrenID == BTSpecialChild.NotInitialized)
                return 0;
            else
                return BTSpecialChild.ReturnToParent;
        }
    }

    // Route functions for a repeater node.
    public class RepeaterNode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NextRoute(object ptr, int nodeIndex, int childrenID)
        {
            if (childrenID != BTSpecialChild.NotInitialized)
            {
                // Get State reset data memory.
                ref NodesExecutionState btState = ref NodesExecutionState.GetRef((ulong)ptr);
                btState.ResetNodeMemoryData();
            }
            return 0;
        }
    }
}
