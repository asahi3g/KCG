using KMath;
using UnityEngine;
using Item;
using Enums;

namespace Node
{
    public class ThrowGasBombAction : NodeBase
    {
        public override ActionType  Type => ActionType .ThrowGasBombAction;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            var planet = GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            var WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            Vec2f startPos = agentEntity.agentPhysicsState.Position;
            startPos.X += 0.5f;
            startPos.Y += 0.5f;

            GameState.InventoryManager.RemoveItem(agentEntity.agentInventory.InventoryID, itemEntity.itemInventory.SlotID);
            itemEntity.Destroy();

            ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, ProjectileType.GasGrenade, agentEntity.agentID.ID, false);
            GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}

