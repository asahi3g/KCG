﻿using Entitas;
using UnityEngine;
using KMath;
using Enums.Tile;

namespace Action
{
    public class ToolActionRemoveTile : ActionBase
    {
        public ToolActionRemoveTile(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;
            
            if (x >= 0 && x < planet.TileMap.MapSize.X &&
            y >= 0 && y < planet.TileMap.MapSize.Y)
            {
                switch(planet.TileMap.GetFrontTileID(x, y))
                {
                    case TileID.Moon:
                        GameState.ItemSpawnSystem.SpawnItemParticle(EntitasContext, Enums.ItemType.Dirt, new Vec2f(x, y));
                        break;
                    case TileID.Bedrock:
                        GameState.ItemSpawnSystem.SpawnItemParticle(EntitasContext, Enums.ItemType.Bedrock, new Vec2f(x, y));
                        break;
                    case TileID.Wire:
                        GameState.ItemSpawnSystem.SpawnItemParticle(EntitasContext, Enums.ItemType.Wire, new Vec2f(x, y));
                        break;
                    case TileID.Pipe:
                        GameState.ItemSpawnSystem.SpawnItemParticle(EntitasContext, Enums.ItemType.Pipe, new Vec2f(x, y));
                        break;
                }

                planet.TileMap.RemoveFrontTile(x, y);
            }
            
            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);

        }
    }

    // Factory Method
    public class ToolActionRemoveTileCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionRemoveTile(entitasContext, actionID);
        }
    }
}
