using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace NodeSystem.BehaviorTree
{
     // Function controller for sequence nodes.
    public class SequenceNode
    {
        static public int GetNextChildren(object ptr, int nodeIndex, NodeState lastResult)
        {
            // Get state data. 
            ref BehaviorTreeState stateData = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref int currentIndex = ref stateData.GetNodeData<int>(nodeIndex);
            ref Node node = ref GameState.NodeManager.GetRef(stateData.NodesExecutiondata[nodeIndex].Id);

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

        static public int GetNextChildren(object ptr, int nodeIndex, NodeState lastResult)
        {
            // Get state data. 
            ref BehaviorTreeState stateData = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref int currentIndex = ref stateData.GetNodeData<int>(nodeIndex);
            ref Node node = ref GameState.NodeManager.GetRef(stateData.NodesExecutiondata[nodeIndex].Id);

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
        static public int NextRoute(int childrenID)
        {
            if (childrenID == BTSpecialChild.NotInitialized)
                return 0;
            else
                return BTSpecialChild.ReturnToParent;
        }
    }
    public class RepeaterNode
    {
        public static int NextRoute(object ptr, int nodeIndex, int childrenID)
        {
            if (childrenID != BTSpecialChild.NotInitialized)
            {
                // Get State reset data memory.
                ref BehaviorTreeState btState = ref BehaviorTreeState.GetRef((ulong)ptr);
                btState.ResetNodeMemoryData();
            }
            return 0;
        }
    }
}
