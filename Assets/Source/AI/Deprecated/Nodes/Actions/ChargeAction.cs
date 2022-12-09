//imports UnityEngine

using Enums;
using Inventory;

namespace Node.Action
{
    public class ChargeAction : NodeBase
    {
        public override ActionType  Type => ActionType .ChargeAction;

        ItemInventoryEntity GetItem(NodeEntity nodeEntity)
        {
            var planet = GameState.Planet;
            var entitasContext = planet.EntitasContext;
            AgentEntity agentEntity = entitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            if (!agentEntity.hasAgentInventory)
                return null;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            InventoryEntityComponent inventory = entitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
            ref InventoryTemplateData InventoryEntityTemplate = ref GameState.InventoryCreationApi.Get(inventory.InventoryEntityTemplateID);

            if (InventoryEntityTemplate.HasToolBar)
            {
                int selectedSlot = inventory.SelectedSlotIndex;
                return GameState.InventoryManager.GetItemInSlot(inventoryID, selectedSlot);
            }

            return null;
        }

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(nodeEntity);
            if (itemEntity == null)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }
            
            bool isChargable = itemEntity.hasItemFireWeaponChargedWeapon;
            if (isChargable)
            {
                nodeEntity.nodeExecution.State = NodeState.Running;
                return;
            }
            nodeEntity.nodeExecution.State = NodeState.Fail;
        }

        public override void OnUpdate(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(nodeEntity);

            if (itemEntity.itemFireWeaponChargedWeapon.ChargeRate < itemEntity.itemFireWeaponChargedWeapon.ChargeMax)
            {
                itemEntity.itemFireWeaponChargedWeapon.ChargeRate += itemEntity.itemFireWeaponChargedWeapon.ChargeRatio;
                nodeEntity.nodeExecution.State = NodeState.Success;
            }
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(nodeEntity);
            Item.FireWeaponProperties fireWeaponProperties = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            float tempCharge = 0.0f;

            if(itemEntity.hasItemFireWeaponChargedWeapon)
                tempCharge = itemEntity.itemFireWeaponChargedWeapon.ChargeRate;

            float difference = itemEntity.itemFireWeaponChargedWeapon.ChargeRate - tempCharge;
            if (nodeEntity.nodeExecution.State == NodeState.Fail)
            {
                UnityEngine.Debug.Log("Reload Failed.");
            }
            else
            {
                UnityEngine.Debug.Log("Weapon Charged: " + difference.ToString());
            }

            base.OnExit(nodeEntity);
        }
    }
}
