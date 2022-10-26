//imports UnityEngine

using Enums;
using Enums.PlanetTileMap;

namespace Node.Action
{
    public class ToolActionConstruction : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionConstruction;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            if(itemEntity.itemMech.InputsActive)
            {
                if (x >= 0 && x < GameState.Planet.TileMap.MapSize.X &&
                y >= 0 && y < GameState.Planet.TileMap.MapSize.Y)
                {
                    var mech = GameState.MechCreationApi.Get(itemEntity.itemMech.MechID);

                    var xRange = UnityEngine.Mathf.CeilToInt(mech.SpriteSize.X);
                    var yRange = UnityEngine.Mathf.CeilToInt(mech.SpriteSize.Y);

                    var allTilesAir = true;

                    for (int i = 0; i < xRange; i++)
                    {
                        for (int j = 0; j < yRange; j++)
                        {
                            if (GameState.Planet.TileMap.GetMidTileID(x + i, y + j) != TileID.Air)
                            {
                                allTilesAir = false;
                                break;
                            }
                        }
                    }

                    if (allTilesAir)
                    {
                        GameState.Planet.AddMech(new KMath.Vec2f(x, y), itemEntity.itemMech.MechID);

                        for (int i = 0; i < xRange; i++)
                        {
                            for (int j = 0; j < yRange; j++)
                            {
                                GameState.Planet.TileMap.SetMidTile(x + i, y + j, TileID.Mech);
                            }
                        }
                    }
                }
            }            
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
