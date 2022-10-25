using Enums;

namespace Action
{
    public class ToolActionPotion
    {
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            var player = planet.Player;
            player.UsePotion(2.0f);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}

