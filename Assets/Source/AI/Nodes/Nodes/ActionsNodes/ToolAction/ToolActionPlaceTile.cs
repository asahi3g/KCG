using System;
using UnityEngine;
using Enums.Tile;
using PlanetTileMap;

namespace Node
{
    public class ToolActionPlaceTile : NodeBase
    {
        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemInventory = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            NodeProperties nodeProperties = GameState.ActionCreationApi.Get(nodeEntity.nodeID.TypeID);

            if (itemInventory.hasItemCastData)
            {
                if(itemInventory.itemCastData.data.TileID == TileID.Error)
                    itemInventory.itemCastData.data = (Data)nodeProperties.ObjectData;

                if (itemInventory.itemCastData.InputsActive)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    if (x >= 0 && x < planet.TileMap.MapSize.X &&
                            y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        switch (itemInventory.itemCastData.data.Layer)
                        {
                            case MapLayerType.Back:
                                planet.TileMap.SetBackTile(x, y, itemInventory.itemCastData.data.TileID);
                                break;
                            case MapLayerType.Mid:
                                planet.TileMap.SetMidTile(x, y, itemInventory.itemCastData.data.TileID);
                                break;
                            case MapLayerType.Front:
                                planet.TileMap.SetFrontTile(x, y, itemInventory.itemCastData.data.TileID);
                                break;
                        }
                    }
                }
            }
            else
            {

                Data data = (Data)nodeProperties.ObjectData;

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;

                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                        y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    switch (data.Layer)
                    {
                        case MapLayerType.Back:
                            planet.TileMap.SetBackTile(x, y, data.TileID);
                            break;
                        case MapLayerType.Mid:
                            planet.TileMap.SetMidTile(x, y, data.TileID);
                            break;
                        case MapLayerType.Front:
                            planet.TileMap.SetFrontTile(x, y, data.TileID);
                            break;
                    }
                }
                
            }

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
