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
        private ProjectileEntity ProjectileEntity;
        private ItemInventoryEntity ItemEntity;
        private Vec2f StartPos;
        private List<ProjectileEntity> EndPointList = new List<ProjectileEntity>();

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

            StartPos = AgentEntity.agentPhysicsState.Position;
            StartPos.X += 0.3f;
            StartPos.Y += 0.5f;

            if (!ItemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                var spread = WeaponProperty.SpreadAngle;

                for (int i = 0; i < bulletsPerShot; i++)
                {
                    var random = UnityEngine.Random.Range(-spread, spread);
                    ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f((x - StartPos.X) - random, y - StartPos.Y).Normalized, Enums.ProjectileType.Bullet);
                    EndPointList.Add(ProjectileEntity);
                }
            }
            else
                ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.Grenade);

            EndPointList.Add(ProjectileEntity);
            ActionEntity.actionExecution.State = Enums.ActionState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(EntitasContext, ActionEntity.actionID.TypeID, AgentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            float range = WeaponProperty.Range;
            float damage = WeaponProperty.BasicDemage;

            // Check if projectile has hit something and was destroyed.
            if (!ProjectileEntity.isEnabled)
            {
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
                return;
            }

            if ((ProjectileEntity.projectilePhysicsState.Position - StartPos).Magnitude > range)
                 ActionEntity.actionExecution.State = Enums.ActionState.Success;

#if UNITY_EDITOR
            for (int i = 0; i < EndPointList.Count; i++)
            {
                if (EndPointList[i].hasProjectilePhysicsState)
                    Debug.DrawLine(new Vector3(StartPos.X, StartPos.Y, 0), new Vector3(EndPointList[i].projectilePhysicsState.Position.X, EndPointList[i].projectilePhysicsState.Position.Y, 0), Color.red, 2.0f, false);
            }
#endif
        }

        public override void OnExit(ref PlanetState planet)
        {
            if (ProjectileEntity != null)
            {
                if (ProjectileEntity.isEnabled)
                {
                    planet.RemoveProjectile(ProjectileEntity.projectileID.Index);
                }
            }
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
