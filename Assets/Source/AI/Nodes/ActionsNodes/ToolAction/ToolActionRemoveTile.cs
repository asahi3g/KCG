using UnityEngine;
using KMath;
using Enums;

namespace Node
{
    public class ToolActionRemoveTile : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionRemoveTile;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;
            
            if (x >= 0 && x < GameState.Planet.TileMap.MapSize.X &&
            y >= 0 && y < GameState.Planet.TileMap.MapSize.Y)
            {
                ref PlanetTileMap.TileProperty tileProprieties = ref GameState.TileCreationApi.GetTileProperty(GameState.Planet.TileMap.GetFrontTileID(x, y));
                GameState.LootDropSystem.Add(tileProprieties.DropTableID, new Vec2f(x, y));

                GameState.Planet.TileMap.RemoveFrontTile(x, y);
            }
            
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
