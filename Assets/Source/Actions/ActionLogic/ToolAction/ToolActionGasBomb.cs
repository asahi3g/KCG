using System;
using KMath;
using Planet;
using UnityEngine;
using System.Collections;

namespace Action
{
    public class ToolActionGasBomb : ActionBase
    {
        private Item.FireWeaponPropreties WeaponProperty;
        private ProjectileEntity ProjectileEntity;
        private ItemInventoryEntity ItemEntity;
        private Vec2f StartPos;

        public ToolActionGasBomb(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);
            WeaponProperty = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            int bulletsPerShot = ItemEntity.itemFireWeaponClip.BulletsPerShot;

            // Check if gun got any ammo
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

            // Decrase number of bullets when shoot
            if (ItemEntity.hasItemFireWeaponClip)
                ItemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;

            // Start position
            StartPos = AgentEntity.agentPhysicsState.Position;
            StartPos.X += 0.5f;
            StartPos.Y += 0.5f;

            ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.GasGrenade);

            ActionEntity.actionExecution.State = Enums.ActionState.Running;

            GameState.ActionCoolDownSystem.SetCoolDown(EntitasContext, ActionEntity.actionID.TypeID, AgentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            // Check if projectile has hit something and was destroyed.
            if (!ProjectileEntity.isEnabled)
            {
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
                return;
            }
            
            ActionEntity.actionExecution.State = Enums.ActionState.Success;
            
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

    /// <summary>
    /// Factory Method
    /// </summary>
    public class ToolActionGasBombCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionGasBomb(entitasContext, actionID);
        }
    }
}

