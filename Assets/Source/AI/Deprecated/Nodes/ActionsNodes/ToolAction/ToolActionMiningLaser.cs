using KMath;
using UnityEngine;
using Enums;

namespace Node
{
    public class ToolActionMiningLaser : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionMiningLaser;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeID.ID);
            Vec2f   agentPosition = agentEntity.agentPhysicsState.Position;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int fromX = (int)agentPosition.X;
            int fromY = (int)agentPosition.Y;

            int toX = (int)worldPosition.x;
            int toY = (int)worldPosition.y;


            Cell start = new Cell
            {
                x = (int)fromX,
                y = (int)fromY
            };

            Cell end = new Cell
            {
                x = (int)toX,
                y = (int)toY
            };

            foreach (var cell in start.LineTo(end))
            {
                ref PlanetTileMap.TileProperty tileProprieties = ref GameState.TileCreationApi.GetTileProperty(planet.TileMap.GetFrontTileID(cell.x, cell.y));
                GameState.LootDropSystem.Add(tileProprieties.DropTableID, new Vec2f(cell.x, cell.y));

                planet.TileMap.RemoveFrontTile(cell.x, cell.y);
                Debug.DrawLine(new Vector3(agentPosition.X, agentPosition.Y, 0.0f), new Vector3(worldPosition.x, worldPosition.y, 0.0f), Color.red);
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
