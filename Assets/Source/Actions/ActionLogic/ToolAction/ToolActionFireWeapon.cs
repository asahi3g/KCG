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

            int bulletsPerShot = ItemEntity.itemFireWeaponClip.BulletsPerShot;

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

            if (planet.Player != null)
            {
                if (worldPosition.x > planet.Player.agentPhysicsState.Position.X && planet.Player.agentPhysicsState.Direction == -1)
                {
                    planet.Player.agentPhysicsState.Direction = 1;
                }
                else if (worldPosition.x < planet.Player.agentPhysicsState.Position.X && planet.Player.agentPhysicsState.Direction == 1)
                {
                    planet.Player.agentPhysicsState.Direction = -1;
                }


                var player = planet.Player;
                player.FireGun(WeaponProperty.CoolDown);


                if (planet.Player.agentPhysicsState.Direction == 1)
                {
                    StartPos.X += 0.3f;
                    StartPos.Y += 1.75f;
            
                    if (x < StartPos.X)
                    {
                        x = StartPos.X + 0.5f;
                    }
                }
                else if (planet.Player.agentPhysicsState.Direction == -1)
                {
                    StartPos.X -= 0.3f;
                    StartPos.Y += 1.75f;

                    if (x > StartPos.X ) 
                    {
                        x = StartPos.X - 0.5f;
                    }
                }
            }


            if (ItemEntity.hasItemFireWeaponSpread)
            {
                var spread = ItemEntity.itemFireWeaponSpread;
                for(int i = 0; i < bulletsPerShot; i++)
                {
                    var random = UnityEngine.Random.Range(-spread.SpreadAngle, spread.SpreadAngle);
                    ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f((x - StartPos.X) - random, y - StartPos.Y).Normalized, Enums.ProjectileType.Bullet);
                    EndPointList.Add(ProjectileEntity);
                }
            }

            if(ItemEntity.itemType.Type == Enums.ItemType.Bow)
            {
                ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.Arrow);
            }
            else
            {
                if(ItemEntity.itemFireWeaponClip.BulletsPerShot > 0)
                {
                    for(int i = 0; i < bulletsPerShot; i++)
                    {
                        ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.Bullet);
                    }
                }
                else
                {
                    ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.Bullet);
                }
            }

            EndPointList.Add(ProjectileEntity);
            ActionEntity.actionExecution.State = Enums.ActionState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(EntitasContext, ActionEntity.actionID.TypeID, AgentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            float range = WeaponProperty.Range;
            float damage = WeaponProperty.BasicDemage;

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

            var entities = EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

            // Todo: Create a agent colision system?
            foreach (var entity in entities)
            {
                if (entity == AgentEntity)
                    continue;

                Vec2f entityPos = entity.agentPhysicsState.Position;
                Vec2f bulletPos = ProjectileEntity.projectilePhysicsState.Position;
                var physiscsState = entity.agentPhysicsState;

                Vec2f diff = bulletPos - entityPos;
                float Len = diff.Magnitude;
                diff.Y = 0;
                diff.Normalize();

                if (entity.hasAgentStats && Len <= 0.5f)
                {
                    Vector2 oppositeDirection = new Vector2(-diff.X, -diff.Y);
                    var stats = entity.agentStats;
                    stats.Health -= (int)damage;

                    planet.AddFloatingText("" + damage, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(entityPos.X, entityPos.Y + 0.35f));
                    ActionEntity.actionExecution.State = Enums.ActionState.Success;
                }
            }
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

