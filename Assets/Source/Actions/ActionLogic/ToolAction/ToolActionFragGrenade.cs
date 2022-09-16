using System;
using KMath;
using Planet;
using UnityEngine;
using System.Collections;

namespace Action
{
    public class ToolActionFragGrenade : ActionBase
    {
        private Item.FireWeaponPropreties WeaponProperty;
        private ProjectileEntity ProjectileEntity;
        private ItemInventoryEntity ItemEntity;
        private Vec2f StartPos;
        float radius = 0.0f;

        public ToolActionFragGrenade(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);
            WeaponProperty = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);


            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;


            // Start position
            StartPos = AgentEntity.agentPhysicsState.Position;
            StartPos.X += 1.0f * AgentEntity.agentPhysicsState.Direction;
            StartPos.Y += 2.0f;


            ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.FragGrenade);
            ProjectileEntity.AddProjectileExplosive(WeaponProperty.BlastRadius, WeaponProperty.MaxDamage, WeaponProperty.Elapse);
            planet.AddFloatingText(WeaponProperty.GrenadeFlags.ToString(), 2.0f, new Vec2f(0, 0), new Vec2f(AgentEntity.agentPhysicsState.Position.X + 0.5f, AgentEntity.agentPhysicsState.Position.Y));
            AgentEntity.UseTool(1.0f);

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

            // Check if projectile is inside in weapon range.
            if ((ProjectileEntity.projectilePhysicsState.Position - StartPos).Magnitude > range)
            {

                planet.AddParticleEmitter(ProjectileEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.ExplosionEmitter);

                // Check if projectile has hit a enemy.
                var entities = EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                // Todo: Create a agent colision system?
                foreach (var entity in entities)
                {
                    float dist = Vec2f.Distance(new Vec2f(AgentEntity.agentPhysicsState.Position.X, AgentEntity.agentPhysicsState.Position.Y), new Vec2f(ProjectileEntity.projectilePhysicsState.Position.X, ProjectileEntity.projectilePhysicsState.Position.Y));

                    if (dist < radius)
                    {
                        Vec2f entityPos = entity.agentPhysicsState.Position;
                        Vec2f bulletPos = ProjectileEntity.projectilePhysicsState.Position;
                        Vec2f diff = bulletPos - entityPos;
                        diff.Y = 0;
                        diff.Normalize();

                        Vector2 oppositeDirection = new Vector2(-diff.X, -diff.Y);

                        if (AgentEntity.hasAgentStats)
                        {
                            var stats = entity.agentStats;
                            stats.Health -= (int)damage;

                            // spawns a debug floating text for damage 
                            planet.AddFloatingText("" + damage, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(entityPos.X, entityPos.Y + 0.35f));
                            ActionEntity.actionExecution.State = Enums.ActionState.Success;
                        }
                    }
                }
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
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

    /// <summary>
    /// Factory Method
    /// </summary>
    public class ToolActionFragGrenadeCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionFragGrenade(entitasContext, actionID);
        }
    }
}

