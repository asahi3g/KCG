using Enums;
using UnityEngine;
using Enums.Tile;

namespace Action
{
    public class ToolActionGeometryPlacement
    {
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity ItemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (ItemEntity.hasItemTile)
            {
                ItemEntity.itemTile.Layer = MapLayerType.Front;

                if (ItemEntity.itemTile.TileID == TileID.Error)
                    ItemEntity.itemTile.TileID = TileID.TB_R1_Metal;

                if (ItemEntity.itemTile.InputsActive)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    if (x >= 0 && x < planet.TileMap.MapSize.X && y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        switch (ItemEntity.itemTile.Layer)
                        {
                            case MapLayerType.Back:
                                planet.TileMap.SetBackTile(x, y, ItemEntity.itemTile.TileID);
                                break;
                            case MapLayerType.Mid:
                                planet.TileMap.SetMidTile(x, y, ItemEntity.itemTile.TileID);
                                break;
                            case MapLayerType.Front:
                                planet.TileMap.SetFrontTile(x, y, ItemEntity.itemTile.TileID);
                                break;
                        }
                    }
                }
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
