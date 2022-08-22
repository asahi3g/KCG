using Entitas;
using UnityEngine;
using KMath;

namespace Action
{
    public class PickUpAction : ActionBase
    {
        private ItemParticleEntity ItemEntity;
        private float Speed = 3.0f;
        private float aceleration = 0.5f;

        public PickUpAction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            ItemEntity = EntitasContext.itemParticle.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

#if DEBUG
            // Item Doesnt Exist
            if (ItemEntity == null)
            {
                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
                return;
            }

            // Check if Agent has an inventory.
            if (!AgentEntity.hasAgentInventory)
            {
                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
                return;
            }
#endif

            Vec2f drawPos = ItemEntity.itemPhysicsState.Position;
            if (!ItemEntity.hasItemDrawPosition2D)
            {
                ItemEntity.AddItemDrawPosition2D(drawPos, Vec2f.Zero);
            }
            ItemEntity.isItemUnpickable = true;

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Running);
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            // Update item pos.

            // Center position Item.
            if (ItemEntity.hasItemType && ItemEntity.hasItemDrawPosition2D)
            {
                // Get Item Size
                Vec2f itemSize = GameState.ItemCreationApi.Get(ItemEntity.itemType.Type).SpriteSize;

                // Get Item Center Position
                Vec2f itemCenterPos = ItemEntity.itemDrawPosition2D.Value + itemSize / 2.0f;

                // Get Agent Center Position
                Vec2f agentCenterPos = AgentEntity.agentPhysicsState.Position + new Vec2f(1.0f, 1.5f)/2f; // Todo: Add agentSizeCompenent

                // Get Distance
                if ((itemCenterPos - agentCenterPos).Magnitude < 0.1f)
                {
                    // Check Component Is Available
                    if (AgentEntity.hasAgentInventory)
                    {
                        // Get Inventory ID
                        int inventoryID = AgentEntity.agentInventory.InventoryID;

                        // Try ading item to Inventory.
                        if (!GameState.InventoryManager.IsFull(EntitasContext, inventoryID))
                        {
                            // If Item Equals To Tile/Mech (Material)
                            if(ItemEntity.itemType.Type == Enums.ItemType.Dirt || ItemEntity.itemType.Type == Enums.ItemType.Bedrock ||
                                ItemEntity.itemType.Type == Enums.ItemType.Pipe || ItemEntity.itemType.Type == Enums.ItemType.Wire)
                            {
                                // Get All Inventories
                                var entities = EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));

                                // Iterate All Entities
                                foreach (var entity in entities)
                                {
                                    // Check Component Is Available
                                    if(entity.hasInventoryName)
                                    {
                                        // Check Name Equals To Material Bag
                                        if(entity.inventoryName.Name == "MaterialBag")
                                        {
                                            // Check Inventory Available Space
                                            if(GameState.InventoryManager.IsFull(EntitasContext, entity.inventoryID.ID))
                                            {
                                                // Pickup Item To Inventory
                                                GameState.InventoryManager.PickUp(EntitasContext, ItemEntity, inventoryID);

                                                // Return True
                                                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                return;
                                            }
                                            else
                                            {
                                                // Pickup Item To Inventory
                                                GameState.InventoryManager.PickUp(EntitasContext, ItemEntity, entity.inventoryID.ID);

                                                // Return True
                                                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                return;
                                            }
                                        }
                                    }
                                }
                            }

                            // Pickup Item To Inventory
                            GameState.InventoryManager.PickUp(EntitasContext, ItemEntity, inventoryID);

                            // Return True
                            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                            return;
                        }
                    }

                    // Inventory and toolbar are full.
                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
                }

                // Increase Speed Adding Acceleration
                Speed += aceleration * deltaTime;

                // Set New Speed
                float speed = Speed * deltaTime;

                // Update Draw Position.
                Vec2f mov = (agentCenterPos - itemCenterPos).Normalized * speed;

                // Draw Item
                ItemEntity.ReplaceItemDrawPosition2D(ItemEntity.itemDrawPosition2D.Value + mov, ItemEntity.itemDrawPosition2D.Value);
            }
        }

        public override void OnExit(ref Planet.PlanetState planet)
        {
            if(ItemEntity.isEnabled)
                ItemEntity.RemoveItemDrawPosition2D();
            base.OnExit(ref planet);
        }
    }

    public class PickUpActionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int action)
        {
            return new PickUpAction(entitasContext, action);
        }
    }
}
