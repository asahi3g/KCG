using NodeSystem.BehaviorTree;
using System;

namespace NodeSystem
{
    public class SelectorNode
    {
        const int ReturnToParent = -2;	// special value for child indices: return to parent node

        static public int GetNextChildren(object ptr, int nodeIndex, NodeState lastResult)
        {
            // Get state data. 
            ref BehaviorTreeState stateData = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref int currentIndex = ref stateData.GetNodeData<int>(nodeIndex);
            ref Node node = ref GameState.NodeManager.GetRef(stateData.NodesExecutiondata[nodeIndex].Id);

            // success = quit
            int nextChild = ReturnToParent;

            // failed = choose next child
            if (lastResult == NodeState.Failure && (currentIndex + 1) < node.Children.Length)
                nextChild = ++currentIndex;

            return nextChild;
        }

        static public NodeState Execute(object ptr, int id)
        {
            // Get state data. 
            ref BehaviorTreeState stateData = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref int currentIndex = ref stateData.GetNodeData<int>(id);
            ref Node node = ref GameState.NodeManager.GetRef(id);
            while (currentIndex < node.Children.Length)
            {
                // Run child action.
                ref Node child = ref GameState.NodeManager.GetRef(node.Children[currentIndex]);

                ConditionManager.Condition condition = GameState.ConditionManager.Get(child.ConditionalID);
                if (condition(ptr))
                {
                    // If failure run next child action.
                    currentIndex++;
                    continue;
                }
                ActionManager.Action action = GameState.ActionManager.Get(child.ActionID);
                NodeState childState = action(ptr, child.ID);
                if (childState == NodeState.Failure)
                {
                    // If failure run next child action.
                    currentIndex++;
                    continue;
                }
                return childState;
            }
            return NodeState.Failure;
        }
    }
}
