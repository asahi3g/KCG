//import UnityEngine

using Enums;
using Enums.PlanetTileMap;
using UnityEngine;

namespace Node.Action
{
    public class ToolActionGeometryPlacement : NodeBase
    {
        public override ActionType  Type => ActionType .ToolActionGeometryPlacement;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Debug.Log($"{nameof(ToolActionGeometryPlacement)}.{nameof(OnEnter)}({nodeEntity})");
            ref var planet = ref GameState.Planet;
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (itemEntity.hasItemTile)
            {
                TileID tileID = itemEntity.itemTile.TileID;

                if (tileID == TileID.Error)
                {
                    Debug.Log($"Cant place {nameof(TileID)}.{tileID}");
                }
                else
                {
                    MapLayerType mapLayerType = itemEntity.itemTile.Layer;
                    
                    if (mapLayerType == MapLayerType.Error)
                    {
                        Debug.Log($"Cant place {nameof(MapLayerType)}.{mapLayerType}");
                    }
                    else
                    {
                        if (itemEntity.itemTile.InputsActive)
                        {
                            var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
                            int x = (int)worldPosition.X;
                            int y = (int)worldPosition.Y;

                            if (x >= 0 && x < planet.TileMap.MapSize.X && y >= 0 && y < planet.TileMap.MapSize.Y)
                            {
                                Debug.Log($"Setting tile [{x},{y}] layer '{mapLayerType}' as '{tileID}'");

                                switch (mapLayerType)
                                {
                                    case MapLayerType.Back:
                                        planet.TileMap.SetBackTile(x, y, tileID);
                                        break;
                                    case MapLayerType.Mid:
                                        planet.TileMap.SetMidTile(x, y, tileID);
                                        break;
                                    case MapLayerType.Front:
                                        planet.TileMap.SetFrontTile(x, y, tileID);
                                        break;
                                }
                            }
                            else
                            {
                                Debug.Log($"Off the bounds");
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log($"Item '{itemEntity}' has not tile");
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
