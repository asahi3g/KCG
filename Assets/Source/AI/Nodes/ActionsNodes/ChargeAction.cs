//imports UnityEngine

using Enums;
using Inventory;

namespace Node.Action
{
    public class ChargeAction : NodeBase
    {
        public override NodeType Type => NodeType.ChargeAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        ItemInventoryEntity GetItem(NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            if (!agentEntity.hasAgentInventory)
                return null;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            EntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.InventoryModelID);

            if (inventoryModel.HasToolBar)
            {
                int selectedSlot = inventory.SelectedSlotID;
                return GameState.InventoryManager.GetItemInSlot(GameState.Planet.EntitasContext, inventoryID, selectedSlot);
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
            
            bool isChargable = itemEntity.hasItemFireWeaponCharge;
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

            if (itemEntity.itemFireWeaponCharge.ChargeRate < itemEntity.itemFireWeaponCharge.ChargeMax)
            {
                itemEntity.itemFireWeaponCharge.ChargeRate += itemEntity.itemFireWeaponCharge.ChargeRatio;
                nodeEntity.nodeExecution.State = NodeState.Success;
            }
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(nodeEntity);
            Item.FireWeaponPropreties WeaponPropreties = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            float tempCharge = itemEntity.itemFireWeaponCharge.ChargeRate;

            float difference = itemEntity.itemFireWeaponCharge.ChargeRate - tempCharge;
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
