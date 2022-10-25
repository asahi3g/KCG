using KMath;
using Enums;
using UnityEngine;
using Item;

namespace Node
{
    public class ShootPulseWeaponAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ShootPulseWeaponAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }


        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

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
                        nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                        return;
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
                        nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                        return;
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

            nodeEntity.nodeExecution.State = Enums.NodeState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
        }
    }
}
