using NodeSystem.BehaviorTree;
using Unity.Collections.LowLevel.Unsafe;

namespace NodeSystem
{
    public class ActionSequenceNode
    {
        public enum SequenceState : byte
        {
            Entry,
            Running,
            Sucess,
            Failure
        }

        public struct ActionSequenceData
        {
            public ActionSequenceData(SequenceState state = SequenceState.Entry)
            {
                NodeState = state;
            }

            public SequenceState NodeState;
        }
        static public NodeState Execute(object ptr, int id)
        {
            ref BehaviorTreeState data = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref ActionSequenceData actionSequenceData = ref data.GetNodeData<ActionSequenceData>(id);
            ref Node node = ref GameState.NodeManager.GetRef(id);
            
            int childIndex = id + 1; // Get child information.
            NodeState childState = new NodeState();
            switch (actionSequenceData.NodeState)
            {
                case SequenceState.Entry:
                    ref Node entry = ref GameState.NodeManager.GetRef(node.Children[(int)SequenceState.Entry]);
                    ActionManager.Action entryFunction = GameState.ActionManager.Get(entry.ActionID);
                    childState = entryFunction(ptr, childIndex);
                    break;
                case SequenceState.Running:
                    ref Node running = ref GameState.NodeManager.GetRef(node.Children[(int)SequenceState.Running]);
                    ActionManager.Action runningFunction = GameState.ActionManager.Get(running.ActionID);
                    childState = runningFunction(ptr, childIndex);
                    break;
            }

            switch (childState)
            {
                case NodeState.Running:
                    actionSequenceData.NodeState = SequenceState.Running;
                    break;
                case NodeState.Success:
                    ref Node sucess = ref GameState.NodeManager.GetRef(node.Children[(int)SequenceState.Running]);
                    ActionManager.Action sucessFunction = GameState.ActionManager.Get(sucess.ActionID);
                    childState = sucessFunction(ptr, childIndex);
                    break;
                case NodeState.Failure:
                    ref Node failure = ref GameState.NodeManager.GetRef(node.Children[(int)SequenceState.Running]);
                    ActionManager.Action failureFunction = GameState.ActionManager.Get(failure.ActionID);
                    childState = failureFunction(ptr, childIndex);
                    break;
            }
            return childState;
        }
    }
}
