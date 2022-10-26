//import UnityEngine

using Enums;
using Enums.PlanetTileMap;

namespace Node.Action
{
    public class ToolActionGeometryPlacement : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionGeometryPlacement;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity ItemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (ItemEntity.hasItemTile)
            {
                ItemEntity.itemTile.Layer = MapLayerType.Front;

                if (ItemEntity.itemTile.TileID == TileID.Error)
                    ItemEntity.itemTile.TileID = TileID.TB_R1_Metal;

                if (ItemEntity.itemTile.InputsActive)
                {
                    UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    if (x >= 0 && x < GameState.Planet.TileMap.MapSize.X && y >= 0 && y < GameState.Planet.TileMap.MapSize.Y)
                    {
                        switch (ItemEntity.itemTile.Layer)
                        {
                            case MapLayerType.Back:
                                GameState.Planet.TileMap.SetBackTile(x, y, ItemEntity.itemTile.TileID);
                                break;
                            case MapLayerType.Mid:
                                GameState.Planet.TileMap.SetMidTile(x, y, ItemEntity.itemTile.TileID);
                                break;
                            case MapLayerType.Front:
                                GameState.Planet.TileMap.SetFrontTile(x, y, ItemEntity.itemTile.TileID);
                                break;
                        }
                    }
                }
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
