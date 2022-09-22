﻿using System;
using KMath;
using Planet;
using UnityEngine;
using System.Collections.Generic;

namespace Action
{
    public class ToolActionFireWeapon : ActionBase
    {
        private Item.FireWeaponPropreties WeaponProperty;
        private ItemInventoryEntity ItemEntity;

        public ToolActionFireWeapon(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);
            WeaponProperty = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            int bulletsPerShot = WeaponProperty.BulletsPerShot;

            if (ItemEntity.hasItemFireWeaponClip)
            {
                int numBullet = ItemEntity.itemFireWeaponClip.NumOfBullets;
                if (numBullet <= 0)
                {
                    Debug.Log("Clip is empty. Press R to reload.");
                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
                    return;
                }
            }

            if (ItemEntity.hasItemFireWeaponClip)
                ItemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;

            Vec2f startPos = AgentEntity.agentPhysicsState.Position;

            if (worldPosition.x > AgentEntity.agentPhysicsState.Position.X && AgentEntity.agentPhysicsState.MovingDirection  == -1)
                AgentEntity.agentPhysicsState.MovingDirection = 1;
            else if (worldPosition.x < AgentEntity.agentPhysicsState.Position.X && AgentEntity.agentPhysicsState.MovingDirection  == 1)
                AgentEntity.agentPhysicsState.MovingDirection = -1;

            AgentEntity.FireGun(WeaponProperty.CoolDown);

            startPos.X += 0.3f * AgentEntity.agentPhysicsState.MovingDirection ;
            startPos.Y += 1.75f;
            
            // Todo: Rotate agent instead.
            if (Math.Sign(x - startPos.X) != Math.Sign(AgentEntity.agentPhysicsState.MovingDirection ))
                x = startPos.X + 0.5f * AgentEntity.agentPhysicsState.MovingDirection;

            var spread = WeaponProperty.SpreadAngle;
            for(int i = 0; i < bulletsPerShot; i++)
            {
                float randomSpread = UnityEngine.Random.Range(-spread, spread);
                ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f((x - startPos.X) - randomSpread, 
                    y - startPos.Y).Normalized, WeaponProperty.ProjectileType, WeaponProperty.BasicDemage);

                if (WeaponProperty.ProjectileType == Enums.ProjectileType.Arrow)
                    projectileEntity.isProjectileFirstHIt = false;
            }

            ActionEntity.actionExecution.State = Enums.ActionState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(EntitasContext, ActionEntity.actionID.TypeID, AgentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnExit(ref PlanetState planet)
        {
            base.OnExit(ref planet);
        }
    }

    public class ToolActionFireWeaponCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionFireWeapon(entitasContext, actionID);
        }
    }
}
