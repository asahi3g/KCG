using NodeSystem.BehaviorTree;

namespace NodeSystem
{
    public class RepeaterNode
    {
        static public NodeState Execute(object ptr, int id)
        {
            ref BehaviorTreeState data = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref Node node = ref GameState.NodeManager.GetRef(id);
            ref Node child = ref GameState.NodeManager.GetRef(node.Children[0]);

            // Run child contion.
            ConditionManager.Condition condition = GameState.ConditionManager.Get(child.ConditionalID);
            if (!condition(ptr))
            {
                data.ResetNodeMemoryData();
                return NodeState.Running;
            }

            // Run child action function
            ActionManager.Action action = GameState.ActionManager.Get(child.ActionID);
            NodeState childState = action(ptr, child.ID);
            if (childState != NodeState.Running)
                data.ResetNodeMemoryData();
            
            return NodeState.Running;
        }
    }
}
