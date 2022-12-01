//imports UnityEngine

using Enums;

namespace Node
{
    public class TestAction : NodeBase
    {
        public override ItemUsageActionType Type => ItemUsageActionType.SecondActionTest;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;

            UnityEngine.Debug.Log("TestActionHeld");

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
