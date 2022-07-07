﻿using Entitas;
using Planet;
using UnityEngine;

namespace Action
{
    public class ReloadAction : ActionBase
    {
        private ItemPropertiesEntity ItemPropretiesEntity;
        private InventoryEntity InventoryEntity;
        private ItemEntity ItemEntity;
        private float runningTime = 0f;

        public ReloadAction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref PlanetState planet)
        {
            // Todo: start playing some animation
            if (AgentEntity.hasAgentToolBar)
            {
                int toolBarID = AgentEntity.agentToolBar.ToolBarID;
                InventoryEntity = planet.EntitasContext.inventory.GetEntityWithInventoryID(toolBarID);
                int selectedSlot = InventoryEntity.inventorySlots.Selected;
                ItemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.item, toolBarID, selectedSlot);
                ItemPropretiesEntity = 
                    planet.EntitasContext.itemProperties.GetEntityWithItemProperty(ItemEntity.itemType.Type);

                bool isReloadable = ItemEntity.hasItemFireWeaponClip;

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
            if (runningTime >= ItemPropretiesEntity.itemPropertyFireWeaponClip.ReloadTime)
            {
                ItemEntity.itemFireWeaponClip.NumOfBullets = ItemPropretiesEntity.itemPropertyFireWeaponClip.ClipSize;
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
                Debug.Log("Weapon Reloaded." + ItemEntity.itemFireWeaponClip.NumOfBullets.ToString() + " Bullets in the clip.");
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
