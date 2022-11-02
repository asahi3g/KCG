using Enums;

namespace Node
{
    public class ToolActionPotion : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionPotion;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            var player = GameState.Planet.Player;
            player.UsePotion(2.0f);

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}

