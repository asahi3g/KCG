using Enums.PlanetTileMap;
using UnityEngine;

namespace Action
{
    public class BackgroundPlacementTool
    {
        // Action used by either player and AI
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity ItemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            var player = planet.Player;
            var itemInventory = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            var inv = planet.EntitasContext.inventory.GetEntityWithInventoryID(player.agentInventory.InventoryID);

            //if (inv.)

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
