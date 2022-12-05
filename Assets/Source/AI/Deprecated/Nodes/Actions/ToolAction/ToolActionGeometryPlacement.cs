//import UnityEngine

using Enums;
using Enums.PlanetTileMap;

namespace Node.Action
{
    public class ToolActionGeometryPlacement : NodeBase
    {
        public override ActionType  Type => ActionType .ToolActionGeometryPlacement;

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
                    var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
                    int x = (int)worldPosition.X;
                    int y = (int)worldPosition.Y;

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
