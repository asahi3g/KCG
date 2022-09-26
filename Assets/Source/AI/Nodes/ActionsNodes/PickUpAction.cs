using Enums;
using UnityEngine;
using KMath;

namespace Node.Action
{
    public class PickUpAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.PickUpAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemParticleEntity itemEntity = planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

#if DEBUG
            if (itemEntity == null)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }

            if (!agentEntity.hasAgentInventory)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                return;
            }
#endif

            Vec2f drawPos = itemEntity.itemPhysicsState.Position;
            if (!itemEntity.hasItemDrawPosition2D)
            {
                itemEntity.AddItemDrawPosition2D(Vec2f.Zero, Vec2f.Zero, drawPos);
            }
            itemEntity.isItemUnpickable = true;

            nodeEntity.nodeExecution.State = Enums.NodeState.Running;
        }

        public override void OnUpdate( ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemParticleEntity itemEntity = planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            
            if (itemEntity.hasItemType && itemEntity.hasItemDrawPosition2D)
            {
                Vec2f itemSize = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).SpriteSize;
                Vec2f itemCenterPos = itemEntity.itemDrawPosition2D.Position + itemSize / 2.0f;
                Vec2f agentCenterPos = agentEntity.agentPhysicsState.Position + new Vec2f(1.0f, 1.5f)/2f; // Todo: Add agentSizeCompenent

                if ((itemCenterPos - agentCenterPos).Magnitude < 0.4f)
                {
                    if (agentEntity.hasAgentInventory)
                    {
                        int inventoryID = agentEntity.agentInventory.InventoryID;
                        GameState.InventoryManager.PickUp(planet.EntitasContext, itemEntity, inventoryID);
                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                        return;
                    }
                    nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                }

                var itemDrawPosition2D = itemEntity.itemDrawPosition2D;
                itemDrawPosition2D.Acceleration = (agentCenterPos - itemCenterPos).Normalized * 5.0f * Time.deltaTime;
                itemDrawPosition2D.Velocity += itemDrawPosition2D.Acceleration;
                itemDrawPosition2D.Position += itemDrawPosition2D.Velocity * Time.deltaTime;
            }
        }
    }
}
