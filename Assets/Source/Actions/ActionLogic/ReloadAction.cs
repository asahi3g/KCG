using Inventory;
using Planet;
using UnityEngine;

namespace Action
{
    public class ReloadAction : ActionBase
    {
        private Item.FireWeaponPropreties WeaponPropreties;
        private ItemInventoryEntity ItemEntity;
        private float runningTime = 0f;

        public ReloadAction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref PlanetState planet)
        {
            if (!AgentEntity.hasAgentInventory)
                return;

            int inventoryID = AgentEntity.agentInventory.InventoryID;
            Inventory.EntityComponent inventory = EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.InventoryModelID);

            // Todo: start playing some animation
            if (inventoryModel.HasToolBar)
            {
                int selectedSlot = inventory.SelectedSlotID;
                ItemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext,
                    AgentEntity.agentInventory.InventoryID, selectedSlot);
                WeaponPropreties = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);

                bool isReloadable = ItemEntity.hasItemFireWeaponClip | ItemEntity.hasItemPulseWeaponPulse;

                if (isReloadable)
                {
                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Running);
                    return;
                }
            }
            ActionEntity.actionExecution.State = Enums.ActionState.Fail;
        }

        public override void OnUpdate(float deltaTime, ref PlanetState planet)
        {
            runningTime += deltaTime;
            if (runningTime >= WeaponPropreties.ReloadTime)
            {
                if(ItemEntity.hasItemFireWeaponClip)
                    ItemEntity.itemFireWeaponClip.NumOfBullets = WeaponPropreties.ClipSize;
                
                if(ItemEntity.hasItemPulseWeaponPulse)
                    ItemEntity.itemPulseWeaponPulse.NumberOfGrenades = WeaponPropreties.GrenadeClipSize;

                ActionEntity.actionExecution.State =  Enums.ActionState.Success;
            }
        }

        public override void OnExit(ref PlanetState planet)
        {
            if (ActionEntity.actionExecution.State == Enums.ActionState.Fail)
            {
                Debug.Log("Reload Failed.");
            }
            else
            {
                if (ItemEntity.hasItemFireWeaponClip)
                    Debug.Log("Weapon Reloaded." + ItemEntity.itemFireWeaponClip.NumOfBullets.ToString() + " Ammo in the clip.");
                else if (ItemEntity.hasItemPulseWeaponPulse)
                    Debug.Log("Weapon Reloaded." + ItemEntity.itemPulseWeaponPulse.NumberOfGrenades.ToString() + " Grenades in the clip");
            }

            base.OnExit(ref planet);
        }
    }


    public class ReloadActionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ReloadAction(entitasContext, actionID);
        }
    }
}
