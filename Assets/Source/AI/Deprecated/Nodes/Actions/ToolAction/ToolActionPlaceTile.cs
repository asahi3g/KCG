using Enums;
using UnityEngine;
using Enums.PlanetTileMap;

namespace Node
{
    public class ToolActionPlaceTile : NodeBase
    {
        public override ActionType Type => ActionType.ToolActionPlaceTile;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            if (nodeEntity == null)
            {
                Debug.Log($"{nameof(NodeEntity)} is null");
                return;
            }

            if (nodeEntity.hasNodeTool)
            {
                Debug.Log($"{nameof(NodeEntity)}.{nameof(nodeEntity.hasNodeTool)} is false");
                return;
            }
            
            ref var planet = ref GameState.Planet;
            ItemInventoryEntity itemInventory = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (itemInventory.hasItemTile)
            {   
                if (itemInventory.itemTile.InputsActive)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    if (itemInventory.itemTile.TileID == TileID.Error)
                        itemInventory.itemTile.TileID = TileID.Moon;

                    if (x >= 0 && x < planet.TileMap.MapSize.X && y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        switch (itemInventory.itemTile.Layer)
                        {
                            case MapLayerType.Back:
                                planet.TileMap.SetBackTile(x, y, itemInventory.itemTile.TileID);
                                break;
                            case MapLayerType.Mid:
                                planet.TileMap.SetMidTile(x, y, itemInventory.itemTile.TileID);
                                break;
                            case MapLayerType.Front:
                                planet.TileMap.SetFrontTile(x, y, itemInventory.itemTile.TileID);
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

                if (itemInventory.itemTile.TileID == TileID.Error)
                    itemInventory.itemTile.TileID = TileID.Moon;

                if (x >= 0 && x < planet.TileMap.MapSize.X && y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    switch (itemInventory.itemTile.Layer)
                    {
                        case MapLayerType.Back:
                            planet.TileMap.SetBackTile(x, y, itemInventory.itemTile.TileID);
                            break;
                        case MapLayerType.Mid:
                            planet.TileMap.SetMidTile(x, y, itemInventory.itemTile.TileID);
                            break;
                        case MapLayerType.Front:
                            planet.TileMap.SetFrontTile(x, y, itemInventory.itemTile.TileID);
                            break;
                    }
                }   
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
