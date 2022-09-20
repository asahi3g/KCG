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
                int selectedSlot = inventory.SelectedSlotID;
                ItemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                if(ItemEntity.itemType.Type is Enums.ItemType.Sword or Enums.ItemType.StunBaton)
                {
                    AgentEntity.agentPhysicsState.Invulnerable = !AgentEntity.agentPhysicsState.Invulnerable;
                }

                // Execute Update
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
            }

            // Return Fail
            ActionEntity.actionExecution.State = Enums.ActionState.Fail;
        }

        public override void OnExit(ref PlanetState planet)
        {
            base.OnExit(ref planet);
        }
    }

    public class ShieldActionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ShieldAction(entitasContext, actionID);
        }
    }
}
