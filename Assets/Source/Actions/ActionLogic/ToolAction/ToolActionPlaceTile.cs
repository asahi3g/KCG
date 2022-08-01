using System;
using UnityEngine;
using Enums.Tile;
using PlanetTileMap;

namespace Action
{
    public class ToolActionPlaceTile : ActionBase
    {
        public struct Data
        {
            public TileID TileID;
            public MapLayerType Layer;
        }

        Data data;

        public ToolActionPlaceTile(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
            data = (Data)ActionPropertyEntity.actionPropertyData.Data;
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            if(ActionPropertyEntity.actionPropertyTilePlacement.TilesPos.Contains(new KMath.Vec2i(x, y)))
            {
                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    for(int i = 0; i < ActionPropertyEntity.actionPropertyTilePlacement.Tiles.Count; i++)
                    {
                        switch (data.Layer)
                        {
                            case MapLayerType.Back:
                                planet.TileMap.SetBackTile(ActionPropertyEntity.actionPropertyTilePlacement.TilesPos[i].X,
                            ActionPropertyEntity.actionPropertyTilePlacement.TilesPos[i].Y, data.TileID);
                                break;
                            case MapLayerType.Mid:
                                planet.TileMap.SetMidTile(ActionPropertyEntity.actionPropertyTilePlacement.TilesPos[i].X,
                            ActionPropertyEntity.actionPropertyTilePlacement.TilesPos[i].Y, data.TileID);
                                break;
                            case MapLayerType.Front:
                                planet.TileMap.SetFrontTile(ActionPropertyEntity.actionPropertyTilePlacement.TilesPos[i].X,
                            ActionPropertyEntity.actionPropertyTilePlacement.TilesPos[i].Y, data.TileID);
                                break;
                        }
                    }
                }

                ActionPropertyEntity.actionPropertyTilePlacement.Tiles.Clear();
                ActionPropertyEntity.actionPropertyTilePlacement.TilesPos.Clear();
                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
            }
            else
            {
                ActionPropertyEntity.actionPropertyTilePlacement.Tiles.Add(planet.TileMap.GetTile(x, y));
                ActionPropertyEntity.actionPropertyTilePlacement.TilesPos.Add(new KMath.Vec2i(x, y));
                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
            }
        }
    }

    // Factory Method
    public class ToolActionPlaceTileCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionPlaceTile(entitasContext, actionID);
        }
    }
}
