using BehaviorTree;

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
        static public NodeState Action(object ptr, int index)
        {
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            ref SequenceState state = ref stateData.GetNodeData<SequenceState>(index);
            ref Node node = ref GameState.NodeManager.GetRef(stateData.NodesExecutiondata[index].Id);

            NodeState childState = new NodeState();
            switch (state)
            {
                case SequenceState.Entry:
                    ref Node entry = ref GameState.NodeManager.GetRef(node.Children[(int)SequenceState.Entry]);
                    ActionManager.Action entryFunction = GameState.ActionManager.Get(entry.ActionID);
                    childState = entryFunction(ptr, index);
                    break;
                case SequenceState.Running:
                    ref Node running = ref GameState.NodeManager.GetRef(node.Children[(int)SequenceState.Running]);
                    ActionManager.Action runningFunction = GameState.ActionManager.Get(running.ActionID);
                    childState = runningFunction(ptr, index);
                    break;
            }

            switch (childState)
            {
                case NodeState.Running:
                    state = SequenceState.Running;
                    break;
                case NodeState.Success:
                    ref Node sucess = ref GameState.NodeManager.GetRef(node.Children[(int)SequenceState.Running]);
                    ActionManager.Action sucessFunction = GameState.ActionManager.Get(sucess.ActionID);
                    childState = sucessFunction(ptr, index);
                    break;
                case NodeState.Failure:
                    ref Node failure = ref GameState.NodeManager.GetRef(node.Children[(int)SequenceState.Running]);
                    ActionManager.Action failureFunction = GameState.ActionManager.Get(failure.ActionID);
                    childState = failureFunction(ptr, index);
                    break;
            }
            return childState;
        }
    }
}
