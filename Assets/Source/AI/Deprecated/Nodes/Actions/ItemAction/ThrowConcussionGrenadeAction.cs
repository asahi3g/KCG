using Enums;
using KMath;
using UnityEngine;
using Item;

namespace Node
{
    public class ThrowConcussionGrenadeAction : NodeBase
    {
        public override ActionType  Type => ActionType .ThrowConcussionGrenadeAction;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            var planet = GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            var WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            // Start position
            Vec2f StartPos = agentEntity.agentPhysicsState.Position;
            StartPos.X += 1.0f * agentEntity.agentPhysicsState.FacingDirection;
            StartPos.Y += 2.0f;


            ProjectileEntity projectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, ProjectileType.ConcussionGrenade, agentEntity.agentID.ID);
            projectileEntity.AddProjectileExplosive(WeaponProperty.BlastRadius, WeaponProperty.MaxDamage, WeaponProperty.Elapse);
            //planet.AddFloatingText(WeaponProperty.GrenadeFlags.ToString(), 2.0f, new Vec2f(0, 0), new Vec2f(agentEntity.agentPhysicsState.Position.X + 0.5f, agentEntity.agentPhysicsState.Position.Y));
            //agentEntity.UseTool(1.0f);

            nodeEntity.nodeExecution.State = NodeState.Running;

            GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
        }
    }
}

