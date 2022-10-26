//imports UnityEngine

using Enums;
using KMath;

namespace Node.Action
{
    public class PickUpAction : NodeBase
    {
        public override NodeType Type => NodeType.PickUpAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemParticleEntity itemEntity = GameState.Planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

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
            itemEntity.isItemUnpickable = true;

            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate( NodeEntity nodeEntity)
        {
            ItemParticleEntity itemEntity = GameState.Planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            
            if (itemEntity.hasItemType && itemEntity.hasItemDrawPosition2D)
            {
                Vec2f itemSize = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).SpriteSize;
                Vec2f itemCenterPos = itemEntity.itemDrawPosition2D.Position + itemSize / 2.0f;
                Vec2f agentCenterPos = agentEntity.agentPhysicsState.Position + new Vec2f(1.0f, 1.5f)/2f; // Todo: Add agentSizeCompenent

                if ((itemCenterPos - agentCenterPos).Magnitude < 0.2f)
                {
                    if (agentEntity.hasAgentInventory)
                    {
                        int inventoryID = agentEntity.agentInventory.InventoryID;
                        GameState.InventoryManager.PickUp(GameState.Planet.EntitasContext, itemEntity, inventoryID);
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
