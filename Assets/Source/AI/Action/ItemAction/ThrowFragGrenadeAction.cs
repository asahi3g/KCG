using KMath;
using Planet;
using UnityEngine;
using NodeSystem;
using Unity.Collections.LowLevel.Unsafe;

namespace Action
{
    public class ThrowFragGrenadeAction
    {
        // Todo: Make this usable by AI.
        static public NodeState Action(object objData, int id)
        {
            ref NodesExecutionState data = ref UnsafeUtility.As<object, NodesExecutionState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity itemEntity = agentEntity.GetItem();
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            // Start position
            Vec2f StartPos = agentEntity.agentPhysicsState.Position;
            StartPos.X += 1.0f * agentEntity.agentPhysicsState.FacingDirection;
            StartPos.Y += 2.0f;


            ProjectileEntity projectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.FragGrenade, agentEntity.agentID.ID);
            projectileEntity.AddProjectileExplosive(WeaponProperty.BlastRadius, WeaponProperty.MaxDamage, WeaponProperty.Elapse);
            planet.AddFloatingText(WeaponProperty.GrenadeFlags.ToString(), 2.0f, new Vec2f(0, 0), new Vec2f(agentEntity.agentPhysicsState.Position.X + 0.5f, agentEntity.agentPhysicsState.Position.Y));
            agentEntity.UseTool(1.0f);

            return NodeState.Success;

            // Todo: Urgent(Create new cool down system)
            //GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
        }
    }
}

