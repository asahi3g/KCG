//imports UnityEngine

using Enums;
using Enums.PlanetTileMap;
using UnityEngine;

namespace Node.Action
{
    public class ToolActionMechPlacement : NodeBase
    {
        public override ItemUsageActionType Type => ItemUsageActionType.ToolActionMechPlacement;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            bool Mech = false;
            ref var planet = ref GameState.Planet;
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
            int x = (int)worldPosition.X;
            int y = (int)worldPosition.Y;

            if (itemEntity.itemMechPlacement.InputsActive)
            {
                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    var mechEntities = GameState.Planet.EntitasContext.mech.GetGroup(MechMatcher.MechID);

                    foreach(var entity in mechEntities)
                    {
                        if(KMath.Vec2f.Distance(new KMath.Vec2f(x, y), entity.mechPosition2D.Value) < 1.5f)
                        {
                            Mech = true;
                        }
                    }

                    if(!Mech)
                    {
                        planet.AddMech(new KMath.Vec2f(x, y), MechType.SmashableBox);
                    }
                }
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
