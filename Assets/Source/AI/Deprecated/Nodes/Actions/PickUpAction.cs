//imports UnityEngine

using Enums;
using KMath;
using UnityEngine;

namespace Node.Action
{
    public class PickUpAction : NodeBase
    {
        public override ActionType  Type => ActionType .PickUpAction;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var itemEntity = planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

#if DEBUG
            if (itemEntity == null)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            if (!agentEntity.hasAgentInventory)
            {
                nodeEntity.nodeExecution.State = NodeState.Success;
                return;
            }
#endif

            Vec2f drawPos = itemEntity.itemPhysicsState.Position;
            if (!itemEntity.hasItemDrawPosition2D)
            {
                itemEntity.AddItemDrawPosition2D(Vec2f.Zero, drawPos);
            }
            itemEntity.AddItemItemParticleAttributeUnpickable(0);
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate( NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var itemEntity = planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (itemEntity == null)
            {
                Debug.Log("item entity is null");
            }
            
            if (itemEntity.hasItemType && itemEntity.hasItemDrawPosition2D)
            {
                Vec2f itemSize = GameState.ItemCreationApi.GetItemProperties(itemEntity.itemType.Type).Size;
                Vec2f itemCenterPos = itemEntity.itemDrawPosition2D.Position + itemSize / 2.0f;
                Vec2f agentCenterPos = agentEntity.agentPhysicsState.Position + new Vec2f(1.0f, 1.5f)/2f; // Todo: Add agentSizeCompenent

                if ((itemCenterPos - agentCenterPos).Magnitude < 0.2f)
                {
                    if (agentEntity.hasAgentInventory)
                    {
                        int inventoryID = agentEntity.agentInventory.InventoryID;
                        GameState.InventoryManager.PickUp(itemEntity, inventoryID);
                        nodeEntity.nodeExecution.State = NodeState.Success;
                        return;
                    }
                    nodeEntity.nodeExecution.State = NodeState.Fail;
                }

                var itemDrawPosition2D = itemEntity.itemDrawPosition2D;
                itemDrawPosition2D.Position += itemDrawPosition2D.Velocity * UnityEngine.Time.deltaTime;

                float acceleration =  15.0f * UnityEngine.Time.deltaTime;
                if (itemDrawPosition2D.Velocity.Magnitude >= 50.0f)
                    acceleration = 0.0f;

                itemDrawPosition2D.Velocity = (agentCenterPos - itemCenterPos).Normalized * (itemDrawPosition2D.Velocity.Magnitude + acceleration);
                itemDrawPosition2D.Position += itemDrawPosition2D.Velocity * UnityEngine.Time.deltaTime;
            }
        }
    }
}
