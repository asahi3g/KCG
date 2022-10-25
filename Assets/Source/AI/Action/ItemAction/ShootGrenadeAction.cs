using Enums;
using NodeSystem.BehaviorTree;
using Planet;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using KMath;

namespace Action
{
    public class ShootGrenadeAction
    {
        static public NodeState Action(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.CurrentPlanet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity itemEntity = agentEntity.GetItem(ref planet);
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

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
                    return NodeState.Fail;
                }
            }

            // Decrase number of bullets when shoot
            if (itemEntity.hasItemFireWeaponClip)
                itemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;

            Vec2f StartPos = agentEntity.agentPhysicsState.Position;
            StartPos.X += 0.5f;
            StartPos.Y += 0.5f;

            ProjectileEntity projectileEntity = null;
            if (itemEntity.itemType.Type == Enums.ItemType.GrenadeLauncher)
            {
                projectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.Grenade, agentEntity.agentID.ID);
                planet.AddFloatingText(WeaponProperty.GrenadeFlags.ToString(), 2.0f, new Vec2f(0, 0), new Vec2f(agentEntity.agentPhysicsState.Position.X + 0.5f, agentEntity.agentPhysicsState.Position.Y));
            }
            else if (itemEntity.itemType.Type == Enums.ItemType.RPG)
            {
                projectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.Rocket, agentEntity.agentID.ID);
            }
            else
            {
                return NodeState.Fail;

            }

            projectileEntity.AddProjectileExplosive(WeaponProperty.BlastRadius, WeaponProperty.MaxDamage, WeaponProperty.Elapse);
            // Todo: Urgent(Create new cool down system)
            //GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
            return NodeState.Success;
        }
    }
}

