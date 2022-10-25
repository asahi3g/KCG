//imports UnityEngine

using Enums;
using Planet;
using Inventory;

namespace Node.Action
{
    public class ChargeAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ChargeAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }


        ItemInventoryEntity GetItem(ref PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            if (!agentEntity.hasAgentInventory)
                return null;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            Inventory.EntityComponent inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.InventoryModelID);

            if (inventoryModel.HasToolBar)
            {
                int selectedSlot = inventory.SelectedSlotID;
                return GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
            }

            return null;
        }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(ref planet, nodeEntity);
            if (itemEntity == null)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }
            
            bool isChargable = itemEntity.hasItemFireWeaponCharge;
            if (isChargable)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Running;
                return;
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(ref planet, nodeEntity);

            if (itemEntity.itemFireWeaponCharge.ChargeRate < itemEntity.itemFireWeaponCharge.ChargeMax)
            {
                itemEntity.itemFireWeaponCharge.ChargeRate += itemEntity.itemFireWeaponCharge.ChargeRatio;
                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
            }
        }

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(ref planet, nodeEntity);
            Item.FireWeaponPropreties WeaponPropreties = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            float tempCharge = itemEntity.itemFireWeaponCharge.ChargeRate;

            float difference = itemEntity.itemFireWeaponCharge.ChargeRate - tempCharge;
            if (nodeEntity.nodeExecution.State == Enums.NodeState.Fail)
            {
                UnityEngine.Debug.Log("Reload Failed.");
            }
            else
            {
                UnityEngine.Debug.Log("Weapon Charged: " + difference.ToString());
            }

            base.OnExit(ref planet, nodeEntity);
        }
    }
}
