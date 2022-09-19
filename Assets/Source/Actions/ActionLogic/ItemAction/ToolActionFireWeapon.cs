using System;
using KMath;
using Planet;
using UnityEngine;
using System.Collections.Generic;

namespace Action
{
    public class ToolActionFireWeapon : ActionBase
    {
        private Item.FireWeaponPropreties WeaponProperty;
        private ProjectileEntity ProjectileEntity;
        private ItemInventoryEntity ItemEntity;
        private Vec2f StartPos;
        private List<ProjectileEntity> EndPointList = new List<ProjectileEntity>();

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

            StartPos = AgentEntity.agentPhysicsState.Position;

            if (worldPosition.x > AgentEntity.agentPhysicsState.Position.X && AgentEntity.agentPhysicsState.Direction == -1)
                AgentEntity.agentPhysicsState.Direction = 1;
            else if (worldPosition.x < AgentEntity.agentPhysicsState.Position.X && AgentEntity.agentPhysicsState.Direction == 1)
                AgentEntity.agentPhysicsState.Direction = -1;

            AgentEntity.FireGun(WeaponProperty.CoolDown);

            StartPos.X += 0.3f * AgentEntity.agentPhysicsState.Direction;
            StartPos.Y += 1.75f;
            
            // Todo: Rotate agent instead.
            if (Math.Sign(x - StartPos.X) != Math.Sign(AgentEntity.agentPhysicsState.Direction))
                x = StartPos.X + 0.5f * AgentEntity.agentPhysicsState.Direction;

            var spread = WeaponProperty.SpreadAngle;
            for(int i = 0; i < bulletsPerShot; i++)
            {
                float randomSpread = UnityEngine.Random.Range(-spread, spread);
                ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f((x - StartPos.X) - randomSpread, y - StartPos.Y).Normalized, WeaponProperty.ProjectileType, WeaponProperty.BasicDemage);
                EndPointList.Add(ProjectileEntity);
            }

            if (WeaponProperty.ProjectileType == Enums.ProjectileType.Arrow)
                ProjectileEntity.isProjectileFirstHIt = false;

            ActionEntity.actionExecution.State = Enums.ActionState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(EntitasContext, ActionEntity.actionID.TypeID, AgentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            float range = WeaponProperty.Range;

            if (!ProjectileEntity.isEnabled)
            {
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
                return;
            }

            if ((ProjectileEntity.projectilePhysicsState.Position - StartPos).Magnitude > range)
            {
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
            }

            // Draw Gizmos Start (Spread, Fire, Angle, Recoil Cone)
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

    public class ToolActionFireWeaponCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionFireWeapon(entitasContext, actionID);
        }
    }
}
