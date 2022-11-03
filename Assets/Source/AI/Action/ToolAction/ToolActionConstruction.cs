using UnityEngine;
using Enums.PlanetTileMap;

namespace Action
{
    public class ToolActionConstruction
    {
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            if(itemEntity.itemMech.InputsActive)
            {
                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    var mech = GameState.MechCreationApi.Get(itemEntity.itemMech.MechID);

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
                        planet.AddMech(new KMath.Vec2f(x, y), itemEntity.itemMech.MechID);

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
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}