using Item;
using KMath;
using Enums;
using UnityEngine;

namespace Node
{
    public class ShootGrenadeAction : NodeBase
    {
        public override NodeType Type => NodeType.ShootGrenadeAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            var WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            int bulletsPerShot = WeaponProperty.BulletsPerShot;

            // Check if gun got any ammo
            if (itemEntity.hasItemFireWeaponClip)
            {
                int numBullet = itemEntity.itemFireWeaponClip.NumOfBullets;
                if (numBullet == 0)
                {
                    Debug.Log("Clip is empty. Press R to reload.");
                    nodeEntity.nodeExecution.State = NodeState.Fail;
                    return;
                }
            }

            // Decrase number of bullets when shoot
            if (itemEntity.hasItemFireWeaponClip)
                itemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;

            Vec2f StartPos = agentEntity.agentPhysicsState.Position;
            StartPos.X += 0.5f;
            StartPos.Y += 0.5f;

            ProjectileEntity projectileEntity = null;
            if (itemEntity.itemType.Type == ItemType.GrenadeLauncher)
            {
                projectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, ProjectileType.Grenade, agentEntity.agentID.ID);
                planet.AddFloatingText(WeaponProperty.GrenadeFlags.ToString(), 2.0f, new Vec2f(0, 0), new Vec2f(agentEntity.agentPhysicsState.Position.X + 0.5f, agentEntity.agentPhysicsState.Position.Y));
            }
            else if (itemEntity.itemType.Type == ItemType.RPG)
            {
                projectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, ProjectileType.Rocket, agentEntity.agentID.ID);
            }
            else
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            projectileEntity.AddProjectileExplosive(WeaponProperty.BlastRadius, WeaponProperty.MaxDamage, WeaponProperty.Elapse);
            GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
            nodeEntity.nodeExecution.State = NodeState.Running;
        }
    }
}

