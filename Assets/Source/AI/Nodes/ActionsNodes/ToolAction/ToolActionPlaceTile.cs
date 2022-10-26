using Enums;
using UnityEngine;
using Enums.PlanetTileMap;

namespace Node
{
    public class ToolActionPlaceTile : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionPlaceTile;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemInventory = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (itemInventory.hasItemTile)
            {
                if (itemInventory.itemTile.InputsActive)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    if (x >= 0 && x < GameState.Planet.TileMap.MapSize.X && y >= 0 && y < GameState.Planet.TileMap.MapSize.Y)
                    {
                        switch (itemInventory.itemTile.Layer)
                        {
                            case MapLayerType.Back:
                                GameState.Planet.TileMap.SetBackTile(x, y, itemInventory.itemTile.TileID);
                                break;
                            case MapLayerType.Mid:
                                GameState.Planet.TileMap.SetMidTile(x, y, itemInventory.itemTile.TileID);
                                break;
                            case MapLayerType.Front:
                                GameState.Planet.TileMap.SetFrontTile(x, y, itemInventory.itemTile.TileID);
                                break;
                        }
                    }
                }
            }
            else
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;

                if (x >= 0 && x < GameState.Planet.TileMap.MapSize.X && y >= 0 && y < GameState.Planet.TileMap.MapSize.Y)
                {
                    switch (itemInventory.itemTile.Layer)
                    {
                        case MapLayerType.Back:
                            GameState.Planet.TileMap.SetBackTile(x, y, itemInventory.itemTile.TileID);
                            break;
                        case MapLayerType.Mid:
                            GameState.Planet.TileMap.SetMidTile(x, y, itemInventory.itemTile.TileID);
                            break;
                        case MapLayerType.Front:
                            GameState.Planet.TileMap.SetFrontTile(x, y, itemInventory.itemTile.TileID);
                            break;
                    }
                }   
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
