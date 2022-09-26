using Enums;

namespace Node
{
    public class ToolActionUsePotion : NodeBase
    {
        public override NodeType Type { get { return NodeType.None; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            var player = planet.Player;
            player.UsePotion(2.0f);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}

