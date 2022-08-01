using System;
using UnityEngine;
using Enums.Tile;
using PlanetTileMap;

namespace Action
{
    public class ToolActionConstruction : ActionBase
    {

        public ToolActionConstruction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
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
                //planet.AddMech(new KMath.Vec2f(x, y), Mech.MechType.Storage);
            }

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }
    }

    // Factory Method
    public class ToolActionConstructionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionConstruction(entitasContext, actionID);
        }
    }
}
