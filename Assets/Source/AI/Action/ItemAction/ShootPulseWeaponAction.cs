using NodeSystem;
using Planet;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using KMath;

namespace Action
{
    public class ShootPulseWeaponAction
    {
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

            int bulletsPerShot = WeaponProperty.BulletsPerShot;

            if(itemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                if (itemEntity.hasItemPulseWeaponPulse)
                {
                    int numGrenade = itemEntity.itemPulseWeaponPulse.NumberOfGrenades;

                    if (numGrenade == 0)
                    {
                        Debug.Log("Grenade Clip is empty. Press R to reload.");
                        return NodeState.Failure;
                    }
                }
            }
            else
            {
                if (itemEntity.hasItemFireWeaponClip)
                {
                    int numBullet = itemEntity.itemFireWeaponClip.NumOfBullets;

                    if (numBullet == 0)
                    {
                        Debug.Log("Clip is empty. Press R to reload.");
                        return NodeState.Failure;
                    }
                }
            }

            if (itemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                if (itemEntity.hasItemPulseWeaponPulse)
                {
                    itemEntity.itemPulseWeaponPulse.NumberOfGrenades--;
                }
            }
            else
            {
                if (itemEntity.hasItemFireWeaponClip)
                {
                    itemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;
                }
            }

            Vec2f startPos = agentEntity.agentPhysicsState.Position;
            startPos.X += 0.3f;
            startPos.Y += 0.5f;

            if (!itemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                var spread = WeaponProperty.SpreadAngle;

                for (int i = 0; i < bulletsPerShot; i++)
                {
                    var random = UnityEngine.Random.Range(-spread, spread);
                    planet.AddProjectile(startPos, new Vec2f((x - startPos.X) - random, y - startPos.Y).Normalized, Enums.ProjectileType.Bullet, agentEntity.agentID.ID);
                }
            }
            else
                planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, Enums.ProjectileType.Grenade, agentEntity.agentID.ID);

            // Todo: Urgent(Create new cool down system)
            //GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
            return NodeState.Success;
        }
    }
}
