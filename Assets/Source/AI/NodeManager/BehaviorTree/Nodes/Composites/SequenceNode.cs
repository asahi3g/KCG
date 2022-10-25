using NodeSystem.BehaviorTree;
using Unity.Collections.LowLevel.Unsafe;

namespace NodeSystem
{
    public class SequenceNode
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
            if (lastResult == NodeState.Success && (currentIndex + 1) < node.Children.Length)
                nextChild = ++currentIndex;

            return nextChild;
        }

        static public NodeState Execute(object ptr, int id)
        {
            ref BehaviorTreeState stateData = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref CompositeNodeData compositeData = ref stateData.GetNodeData<CompositeNodeData>(id);
            ref Node node = ref GameState.NodeManager.GetRef(id);
            
            // Check child condtion
            ref Node child = ref GameState.NodeManager.GetRef(node.Children[compositeData.CurrentChild]);
            ConditionManager.Condition condition = GameState.ConditionManager.Get(child.ConditionalID);
            if (!condition(ptr))
                return NodeState.Failure;

            // Run child action
            ActionManager.Action action = GameState.ActionManager.Get(child.ActionID);
            NodeState childState = action(ptr, child.ID);
            if (childState == NodeState.Failure)
                return NodeState.Failure;
            
            if (childState == NodeState.Success)
            {
                compositeData.CurrentChild++;
                if (compositeData.CurrentChild == node.Children.Length)
                    return NodeState.Success;
                else
                    return NodeState.Running;
            }
            return childState;
        }
    }
}
