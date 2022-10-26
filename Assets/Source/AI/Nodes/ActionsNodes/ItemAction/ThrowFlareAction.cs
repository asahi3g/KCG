using KMath;
using UnityEngine;
using Item;
using Enums;

namespace Node
{
    public class ThrowFlareAction : NodeBase
    {
        public override NodeType Type => NodeType.ThrowFlareAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            Vec2f startPos = agentEntity.agentPhysicsState.Position;
            startPos.X += 0.5f;
            startPos.Y += 0.5f;

            GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, agentEntity.agentInventory.InventoryID, itemEntity.itemInventory.SlotID);
            itemEntity.Destroy();

            GameState.Planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, ProjectileType.Flare, agentEntity.agentID.ID, false);
            GameState.ActionCoolDownSystem.SetCoolDown(GameState.Planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}

