//imports UnityEngine

using Enums;
using Enums.PlanetTileMap;

namespace Node.Action
{
    public class ToolActionConstruction : NodeBase
    {
        public override ActionType  Type => ActionType .ToolActionConstruction;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
            int x = (int)worldPosition.X;
            int y = (int)worldPosition.Y;

            if(itemEntity.itemMechPlacement.InputsActive)
            {
                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    if (itemEntity.itemMechPlacement.MechID == MechType.Error)
                        itemEntity.itemMechPlacement.MechID = MechType.SmashableBox;

                    var mech = GameState.MechCreationApi.Get(itemEntity.itemMechPlacement.MechID);

                    var xRange = UnityEngine.Mathf.CeilToInt(mech.SpriteSize.X);
                    var yRange = UnityEngine.Mathf.CeilToInt(mech.SpriteSize.Y);

                    var allTilesAir = true;

                    for (int i = 0; i < xRange; i++)
                    {
                        for (int j = 0; j < yRange; j++)
                        {
                            if (planet.TileMap.GetMidTileID(x + i, y + j) != TileID.Air)
                            {
                                allTilesAir = false;
                                break;
                            }
                        }
                    }

                    if (allTilesAir)
                    {
                        planet.AddMech(new KMath.Vec2f(x, y), itemEntity.itemMechPlacement.MechID);

                        for (int i = 0; i < xRange; i++)
                        {
                            for (int j = 0; j < yRange; j++)
                            {
                                planet.TileMap.SetMidTile(x + i, y + j, TileID.Mech);
                            }
                        }
                    }
                }
            }            
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
