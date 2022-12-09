//imports UnityEngine

using Enums;
using KMath;

namespace Node
{
    public class PickaxeAction : NodeBase
    {
        public override ActionType  Type => ActionType .PickaxeAction;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            var planet = GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);


            var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
            float x = worldPosition.X;
            float y = worldPosition.Y;


            var tile = planet.TileMap.GetTile((int)x, (int)y).FrontTileID;
            if(tile == Enums.PlanetTileMap.TileID.Bedrock)
            {
                nodeEntity.nodeExecution.State = NodeState.Success;
                return;
            }

            ref PlanetTileMap.TileProperty tileProprieties = ref GameState.TileCreationApi.GetTileProperty(tile);

            GameState.LootDropSystem.Add(tileProprieties.DropTableID, new Vec2f(x, y));

            planet.TileMap.RemoveFrontTile((int)x, (int)y);
            agentEntity.agentPhysicsState.MovementState = AgentMovementState.PickaxeHit;

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
