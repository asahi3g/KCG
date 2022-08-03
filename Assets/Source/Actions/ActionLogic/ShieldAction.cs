using Inventory;
using Planet;

namespace Action
{
    public class ShieldAction : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        // Constructor
        public ShieldAction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref PlanetState planet)
        {
            if (!AgentEntity.hasAgentInventory)
                return;

            int inventoryID = AgentEntity.agentInventory.InventoryID;
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(AgentEntity.agentInventory.InventoryID);

            if (inventory.HasTooBar())
            {
                // Set Selected Slot
                int selectedSlot = inventory.SelectedSlotID;

                // Set Item Entity
                ItemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);

                // If Item In Slot Is A Melee Attack Weapon
                if(ItemEntity.itemType.Type== Enums.ItemType.Sword || ItemEntity.itemType.Type == Enums.ItemType.StunBaton)
                {
                    // If Shield Active Is False
                    if (!ActionPropertyEntity.actionPropertyShield.ShieldActive)
                    {
                        // Set Shield Active True
                        ActionPropertyEntity.actionPropertyShield.ShieldActive = true;

                        // Set Invulnerable True
                        AgentEntity.agentMovable.Invulnerable = true;
                    }
                    else
                    {
                        // Set Shield Active False
                        ActionPropertyEntity.actionPropertyShield.ShieldActive = false;

                        // Set Invulnerable False
                        AgentEntity.agentMovable.Invulnerable = false;
                    }
                }

                // Execute Update
                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Running);
            }

            // Return Fail
            ActionEntity.actionExecution.State = Enums.ActionState.Fail;
        }

        public override void OnUpdate(float deltaTime, ref PlanetState planet)
        {
            // Execute Exit
            ActionEntity.actionExecution.State = Enums.ActionState.Success;
        }

        public override void OnExit(ref PlanetState planet)
        {
            // Exit()
            base.OnExit(ref planet);
        }
    }

    /// <summary>
    /// Factory Method
    /// </summary>
    public class ShieldActionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ShieldAction(entitasContext, actionID);
        }
    }
}
