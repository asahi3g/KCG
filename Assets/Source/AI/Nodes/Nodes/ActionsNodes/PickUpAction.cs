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
                itemEntity.AddItemDrawPosition2D(drawPos, Vec2f.Zero);
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
                Vec2f itemCenterPos = itemEntity.itemDrawPosition2D.Value + itemSize / 2.0f;
                Vec2f agentCenterPos = agentEntity.agentPhysicsState.Position + new Vec2f(1.0f, 1.5f)/2f; // Todo: Add agentSizeCompenent

                if ((itemCenterPos - agentCenterPos).Magnitude < 0.1f)
                {
                    if (agentEntity.hasAgentInventory)
                    {
                        int inventoryID = agentEntity.agentInventory.InventoryID;

                        if (!GameState.InventoryManager.IsFull(planet.EntitasContext, inventoryID))
                        {
                            if(itemEntity.itemType.Type == Enums.ItemType.Dirt || itemEntity.itemType.Type == Enums.ItemType.Bedrock ||
                                itemEntity.itemType.Type == Enums.ItemType.Pipe || itemEntity.itemType.Type == Enums.ItemType.Wire)
                            {
                                var entities = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));

                                foreach (var entity in entities)
                                {
                                    if(entity.hasInventoryName)
                                    {
                                        if(entity.inventoryName.Name == "MaterialBag")
                                        {
                                            if(GameState.InventoryManager.IsFull(planet.EntitasContext, entity.inventoryID.ID))
                                            {
                                                GameState.InventoryManager.PickUp(planet.EntitasContext, itemEntity, inventoryID);
                                                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                return;
                                            }
                                            else
                                            {
                                                GameState.InventoryManager.PickUp(planet.EntitasContext, itemEntity, entity.inventoryID.ID);
                                                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            else if(itemEntity.itemType.Type == Enums.ItemType.HealthPositon)
                            {
                                var entities = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));
                                foreach (var entity in entities)
                                {
                                    if (entity.hasInventoryName)
                                    {
                                        if (entity.inventoryName.Name == "MaterialBag")
                                        {
                                            if (GameState.InventoryManager.IsFull(planet.EntitasContext, entity.inventoryID.ID))
                                            {
                                                GameState.InventoryManager.PickUp(planet.EntitasContext, itemEntity, inventoryID);
                                                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                return;
                                            }
                                            else
                                            {
                                                GameState.InventoryManager.PickUp(planet.EntitasContext, itemEntity, entity.inventoryID.ID);
                                                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            GameState.InventoryManager.PickUp(planet.EntitasContext, itemEntity, inventoryID);
                            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                            return;
                        }
                    }
                    nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                }

               // Todo: Move this update draw position code out of here.
               // Speed += aceleration * deltaTime;
                float speed = 25.0f * Time.deltaTime;
                Vec2f mov = (agentCenterPos - itemCenterPos).Normalized * speed;
                itemEntity.ReplaceItemDrawPosition2D(itemEntity.itemDrawPosition2D.Value + mov, itemEntity.itemDrawPosition2D.Value);
            }
        }
    }
}
