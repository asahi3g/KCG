using System;
using UnityEngine;
using Enums.Tile;
using PlanetTileMap;

namespace Action
{
    public class ToolActionConstruction : ActionBase
    {
        public struct Data
        {
            public Mech.MechType MechID;
        }

        Data data;

        public ToolActionConstruction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
            data = (Data)ActionPropertyEntity.actionPropertyData.Data;
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            if (x >= 0 && x < planet.TileMap.MapSize.X &&
            y >= 0 && y < planet.TileMap.MapSize.Y)
            {
                var mech = GameState.MechCreationApi.Get((int)data.MechID);

                var xRange = Mathf.CeilToInt(mech.SpriteSize.X);
                var yRange = Mathf.CeilToInt(mech.SpriteSize.Y);

                var allTilesAir = true;

                for (int i = 0; i < xRange; i++)
                {
                    for (int j = 0; j < yRange; j++)
                    {
                        if (planet.TileMap.GetMidTileID(x + i, y + j) != TileID.Air)
                        {
                            allTilesAir = false;
                            break;
                        }
                    }
                }

                if (allTilesAir)
                {
                    planet.AddMech(new KMath.Vec2f(x, y), data.MechID);

                    for (int i = 0; i < xRange; i++)
                    {
                        for (int j = 0; j < yRange; j++)
                        {
                            planet.TileMap.SetMidTile(x + i, y + j, TileID.Mech);
                        }
                    }
                }

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
