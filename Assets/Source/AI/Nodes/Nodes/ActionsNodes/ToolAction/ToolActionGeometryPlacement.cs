using System;
using UnityEngine;
using Enums.Tile;

namespace Node.Action
{
    public class ToolActionGeometryPlacement : NodeBase
    {
        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity ItemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (ItemEntity.hasItemCastData)
            {
                ItemEntity.itemCastData.data.Layer = MapLayerType.Front;

                if (ItemEntity.itemCastData.data.TileID == TileID.Error)
                    ItemEntity.itemCastData.data.TileID = TileID.TI_1;

                if (ItemEntity.itemCastData.InputsActive)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    if (x >= 0 && x < planet.TileMap.MapSize.X &&
                            y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        switch (ItemEntity.itemCastData.data.Layer)
                        {
                            case MapLayerType.Back:
                                planet.TileMap.SetBackTile(x, y, ItemEntity.itemCastData.data.TileID);
                                break;
                            case MapLayerType.Mid:
                                planet.TileMap.SetMidTile(x, y, ItemEntity.itemCastData.data.TileID);
                                break;
                            case MapLayerType.Front:
                                planet.TileMap.SetFrontTile(x, y, ItemEntity.itemCastData.data.TileID);
                                break;
                        }
                    }
                }
            }
            else
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
            }

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
