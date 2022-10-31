//imports UnityEngine

using Enums;
using KMath;

namespace Node
{
    public class PickaxeAction : NodeBase
    {
        public override NodeType Type => NodeType.PickaxeAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);


            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;


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
