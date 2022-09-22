using System;
using KMath;
using Planet;
using UnityEngine;
using System.Collections.Generic;

namespace Action
{
    public class ToolActionPulseWeapon : ActionBase
    {
        private Item.FireWeaponPropreties WeaponProperty;
        private ItemInventoryEntity ItemEntity;

        public ToolActionPulseWeapon(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
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

            if(ItemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                if (ItemEntity.hasItemPulseWeaponPulse)
                {
                    int numGrenade = ItemEntity.itemPulseWeaponPulse.NumberOfGrenades;

                    if (numGrenade == 0)
                    {
                        Debug.Log("Grenade Clip is empty. Press R to reload.");
                        ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
                        return;
                    }
                }
            }
            else
            {
                if (ItemEntity.hasItemFireWeaponClip)
                {
                    int numBullet = ItemEntity.itemFireWeaponClip.NumOfBullets;

                    if (numBullet == 0)
                    {
                        Debug.Log("Clip is empty. Press R to reload.");
                        ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
                        return;
                    }
                }
            }

            if (ItemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                if (ItemEntity.hasItemPulseWeaponPulse)
                {
                    ItemEntity.itemPulseWeaponPulse.NumberOfGrenades--;
                }
            }
            else
            {
                if (ItemEntity.hasItemFireWeaponClip)
                {
                    ItemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;
                }
            }

            Vec2f startPos = AgentEntity.agentPhysicsState.Position;
            startPos.X += 0.3f;
            startPos.Y += 0.5f;

            if (!ItemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                var spread = WeaponProperty.SpreadAngle;

                for (int i = 0; i < bulletsPerShot; i++)
                {
                    var random = UnityEngine.Random.Range(-spread, spread);
                    planet.AddProjectile(startPos, new Vec2f((x - startPos.X) - random, y - startPos.Y).Normalized, Enums.ProjectileType.Bullet);
                }
            }
            else
                planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, Enums.ProjectileType.Grenade);

            ActionEntity.actionExecution.State = Enums.ActionState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(EntitasContext, ActionEntity.actionID.TypeID, AgentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnExit(ref PlanetState planet)
        {
            base.OnExit(ref planet);
        }
    }

    public class ToolActionPulseWeaponCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionPulseWeapon(entitasContext, actionID);
        }
    }
}
