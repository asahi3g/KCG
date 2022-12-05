using UnityEngine;
using KMath;
using Enums.PlanetTileMap;

namespace Action
{
    public class PickaxeAction
    {
        // Action used by either player and AI.
        // Todo: Implement this.
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            //AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            //
            //
            //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //float x = worldPosition.x;
            //float y = worldPosition.y;
            //
            //
            //var tile = planet.TileMap.GetTile((int)x, (int)y).FrontTileID;
            //if(tile == TileID.Bedrock)
            //{
            //    nodeEntity.nodeExecution.State = Enums.NodeState.Success;
            //    return;
            //}
            //
            //ref PlanetTileMap.TileProperty tileProprieties = ref GameState.TileCreationApi.GetTileProperty(tile);
            //
            //GameState.LootDropSystem.Add(tileProprieties.DropTableID, new Vec2f(x, y));
            //
            //planet.TileMap.RemoveFrontTile((int)x, (int)y);
            //agentEntity.agentPhysicsState.MovementState = AgentMovementState.PickaxeHit;
            //
            //nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
