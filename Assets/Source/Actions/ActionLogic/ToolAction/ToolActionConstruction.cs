using System;
using UnityEngine;
using PlanetTileMap;
using Mech;
using Enums.Tile;

namespace Action
{
    public class ToolActionConstruction : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        public ToolActionConstruction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {

        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            if(ItemEntity.itemMechCastData.data.MechID == null)
                ItemEntity.itemMechCastData.data = (Mech.Data)ActionProperty.ObjectData;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            if(ItemEntity.itemMechCastData.InputsActive)
            {
                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    var mech = GameState.MechCreationApi.Get((int)ItemEntity.itemMechCastData.data.MechID);

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
                        planet.AddMech(new KMath.Vec2f(x, y), ItemEntity.itemMechCastData.data.MechID);

                        for (int i = 0; i < xRange; i++)
                        {
                            for (int j = 0; j < yRange; j++)
                            {
                                planet.TileMap.SetMidTile(x + i, y + j, TileID.Mech);
                            }
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
