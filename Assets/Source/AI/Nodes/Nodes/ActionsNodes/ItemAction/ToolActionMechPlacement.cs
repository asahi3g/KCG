using System;
using UnityEngine;
using PlanetTileMap;
using Mech;
using Enums.Tile;
using KMath;

namespace Node.Action
{
    public class ToolActionMechPlacement : NodeBase
    {

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            planet.AddMech(new Vec2f(x, y), itemEntity.itemMechCastData.data.MechID);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }

        public override void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (itemEntity != null)
            {
                GameState.InventoryManager.RemoveItem(planet.EntitasContext, agentEntity.agentInventory.InventoryID, 
                    itemEntity.itemInventory.SlotID);
                itemEntity.Destroy();
            }
            base.OnExit(ref planet, nodeEntity);
        }
    }
}
