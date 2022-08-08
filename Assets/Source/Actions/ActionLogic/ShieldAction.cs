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
            Inventory.EntityComponent inventory = EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.InventoryModelID);

            if (inventoryModel.HasToolBar)
            {
                // Set Selected Slot
                int selectedSlot = inventory.SelectedSlotID;

                // Set Item Entity
                ItemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);

                // If Item In Slot Is A Melee Attack Weapon
                if(ItemEntity.itemType.Type is Enums.ItemType.Sword or Enums.ItemType.StunBaton)
                {
                    // Toggle Shield
                    ActionPropertyEntity.actionPropertyShield.ShieldActive = !ActionPropertyEntity.actionPropertyShield.ShieldActive;
                    // Toggle Invulnerable
                    AgentEntity.agentMovable.Invulnerable = !AgentEntity.agentMovable.Invulnerable;
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
