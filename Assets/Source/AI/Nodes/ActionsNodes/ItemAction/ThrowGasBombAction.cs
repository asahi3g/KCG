using KMath;
using UnityEngine;
using Item;
using Enums;

namespace Node
{
    public class ThrowGasBombAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ThrowGasBombAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            Vec2f startPos = agentEntity.agentPhysicsState.Position;
            startPos.X += 0.5f;
            startPos.Y += 0.5f;

            GameState.InventoryManager.RemoveItem(planet.EntitasContext, agentEntity.agentInventory.InventoryID, itemEntity.itemInventory.SlotID);
            itemEntity.Destroy();

            ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, Enums.ProjectileType.GasGrenade, false);
            GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}

