﻿using UnityEngine;
using KMath;
using Enums;

namespace Node
{
    public class ToolActionRemoveTile : NodeBase
    {
        public override ItemUsageActionType  Type => ItemUsageActionType .ToolActionRemoveTile;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;
            
            if (x >= 0 && x < planet.TileMap.MapSize.X &&
            y >= 0 && y < planet.TileMap.MapSize.Y)
            {
                ref PlanetTileMap.TileProperty tileProprieties = ref GameState.TileCreationApi.GetTileProperty(planet.TileMap.GetFrontTileID(x, y));
                GameState.LootDropSystem.Add(tileProprieties.DropTableID, new Vec2f(x, y));

                planet.TileMap.RemoveFrontTile(x, y);
            }
            
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
