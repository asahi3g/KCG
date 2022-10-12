using UnityEngine;
using Enums;
using KMath;

namespace Node
{
    public class PickaxeAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.PickaxeAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            var tile = planet.TileMap.GetTile((int)x, (int)y).FrontTileID;
            if(tile == Enums.Tile.TileID.Bedrock)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                return;
            }

            planet.TileMap.RemoveFrontTile((int)x, (int)y);
            switch (tile)
            {
                case Enums.Tile.TileID.Moon:
                    GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.Moon, new Vec2f(x, y));
                    break;
            }

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
