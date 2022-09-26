using Enums;
using KMath;
using Planet;
using UnityEngine;
using Item;

namespace Node
{
    public class ThrowFragGrenadeAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ThrowFragGrenadeAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            // Start position
            Vec2f StartPos = agentEntity.agentPhysicsState.Position;
            StartPos.X += 1.0f * agentEntity.agentPhysicsState.FacingDirection;
            StartPos.Y += 2.0f;


            ProjectileEntity projectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.FragGrenade);
            projectileEntity.AddProjectileExplosive(WeaponProperty.BlastRadius, WeaponProperty.MaxDamage, WeaponProperty.Elapse);
            planet.AddFloatingText(WeaponProperty.GrenadeFlags.ToString(), 2.0f, new Vec2f(0, 0), new Vec2f(agentEntity.agentPhysicsState.Position.X + 0.5f, agentEntity.agentPhysicsState.Position.Y));
            agentEntity.UseTool(1.0f);

            nodeEntity.nodeExecution.State = Enums.NodeState.Running;

            GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
            base.OnExit(ref planet, nodeEntity);
        }
    }
}

