using Inventory;
using Item;
using Planet;
using UnityEngine;
using Enums;

namespace Node.Action
{
    public class ReloadAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ReloadAction; } }

        private ItemInventoryEntity GetItem(ref PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (!agentEntity.hasAgentInventory)
                return null;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            EntityComponent inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.InventoryModelID);

            if (inventoryModel.HasToolBar)
            {
                int selectedSlot = inventory.SelectedSlotID;
                return GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, agentEntity.agentInventory.InventoryID, selectedSlot);
            }

            return null;
        }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity item = GetItem(ref planet, nodeEntity);

            if (item != null)
            {
                if (item.hasItemFireWeaponClip)
                {
                    nodeEntity.nodeExecution.State = Enums.NodeState.Running;
                    return;
                }
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(ref planet, nodeEntity);
            FireWeaponPropreties WeaponPropreties = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            float runningTime = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            if (runningTime >= WeaponPropreties.ReloadTime)
            {
                if(itemEntity.hasItemFireWeaponClip)
                    itemEntity.itemFireWeaponClip.NumOfBullets = WeaponPropreties.ClipSize;

                nodeEntity.nodeExecution.State =  Enums.NodeState.Success;
            }
        }

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GetItem(ref planet, nodeEntity);

            if (nodeEntity.nodeExecution.State == Enums.NodeState.Fail)
            {
                Debug.Log("Reload Failed.");
            }
            else
            {
                if (itemEntity.hasItemFireWeaponClip)
                    Debug.Log("Weapon Reloaded." + itemEntity.itemFireWeaponClip.NumOfBullets.ToString() + " Ammo in the clip.");
            }

            base.OnExit(ref planet, nodeEntity);
        }
    }
}
