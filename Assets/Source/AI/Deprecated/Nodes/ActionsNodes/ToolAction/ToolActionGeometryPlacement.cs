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
            ref var planet = ref GameState.Planet;
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (itemEntity.hasItemTile)
            {
                itemEntity.itemTile.Layer = MapLayerType.Front;

                if (itemEntity.itemTile.TileID == TileID.Error)
                    itemEntity.itemTile.TileID = TileID.TB_R1_Metal;

                if (itemEntity.itemTile.InputsActive)
                {
                    UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    if (x >= 0 && x < planet.TileMap.MapSize.X && y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        switch (itemEntity.itemTile.Layer)
                        {
                            case MapLayerType.Back:
                                planet.TileMap.SetBackTile(x, y, itemEntity.itemTile.TileID);
                                break;
                            case MapLayerType.Mid:
                                planet.TileMap.SetMidTile(x, y, itemEntity.itemTile.TileID);
                                break;
                            case MapLayerType.Front:
                                planet.TileMap.SetFrontTile(x, y, itemEntity.itemTile.TileID);
                                break;
                        }
                    }
                }
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
