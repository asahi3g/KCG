using System;
using KMath;
using Planet;
using UnityEngine;

namespace Action
{
    public class ToolActionFireWeapon : ActionBase
    {
        public ToolActionFireWeapon(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            ItemInventoryEntity ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);

            // Todo: Move target selection to an agent system.
            Vec2f target = Vec2f.Zero;
            if(ActionEntity.hasActionTaget)
            {
                int agentTargetID = ActionEntity.actionTaget.AgentTargetID;
                if (agentTargetID != -1)
                {
                    AgentEntity agentEntity = EntitasContext.agent.GetEntityWithAgentID(agentTargetID);
                    target = agentEntity.agentPhysicsState.Position + agentEntity.physicsBox2DCollider.Size.Y * 0.7f;
                }
                else
                    target = ActionEntity.actionTaget.TargetPos;
            }
            else
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.X = worldPosition.x;
                target.Y = worldPosition.y;
            }

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

            if (target.X > AgentEntity.agentPhysicsState.Position.X && AgentEntity.agentPhysicsState.MovingDirection  == -1)
                AgentEntity.agentPhysicsState.MovingDirection = 1;
            else if (target.X < AgentEntity.agentPhysicsState.Position.X && AgentEntity.agentPhysicsState.MovingDirection  == 1)
                AgentEntity.agentPhysicsState.MovingDirection = -1;

            AgentEntity.FireGun(WeaponProperty.CoolDown);

            startPos.X += 0.3f * AgentEntity.agentPhysicsState.MovingDirection ;
            startPos.Y += 1.75f;

            // Todo: Rotate agent instead.
            if (Math.Sign(target.X - startPos.X) != Math.Sign(AgentEntity.agentPhysicsState.MovingDirection ))
                target.X = startPos.X + 0.5f * AgentEntity.agentPhysicsState.MovingDirection;

            var spread = WeaponProperty.SpreadAngle;
            for(int i = 0; i < bulletsPerShot; i++)
            {
                float randomSpread = UnityEngine.Random.Range(-spread, spread);
                ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f((target.X - startPos.X) - randomSpread,
                    target.Y - startPos.Y).Normalized, WeaponProperty.ProjectileType, WeaponProperty.BasicDemage);

                if (WeaponProperty.ProjectileType == Enums.ProjectileType.Arrow)
                    projectileEntity.isProjectileFirstHIt = false;

                projectileEntity.AddProjectileRange(WeaponProperty.Range);
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
