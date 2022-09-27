using System;
using KMath;
using Planet;
using UnityEngine;
using Enums;
using static UnityEngine.GraphicsBuffer;

namespace Node
{
    public class ShootFireWeaponAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ShootFireWeaponAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            // Todo: Move target selection to an agent system.
            Vec2f target = Vec2f.Zero;
            if(nodeEntity.hasNodeBlackboardData)
            {
                //target = // Get from blackboard.
            }
            else
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.X = worldPosition.x;
                target.Y = worldPosition.y;
                nodeEntity.AddNodeTarget(target);
            }

            int bulletsPerShot = WeaponProperty.BulletsPerShot;

            if (itemEntity.hasItemFireWeaponClip)
            {
                int numBullet = itemEntity.itemFireWeaponClip.NumOfBullets;
                if (numBullet <= 0)
                {
                    Debug.Log("Clip is empty. Press R to reload.");
                    nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                    return;
                }

                itemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;
            }


            if (target.X > agentEntity.agentPhysicsState.Position.X && agentEntity.agentPhysicsState.MovingDirection  == -1)
                agentEntity.agentPhysicsState.MovingDirection = 1;
            else if (target.X < agentEntity.agentPhysicsState.Position.X && agentEntity.agentPhysicsState.MovingDirection  == 1)
                agentEntity.agentPhysicsState.MovingDirection = -1;

            agentEntity.FireGun(WeaponProperty.CoolDown);
            Vec2f startPos = agentEntity.GetGunFiringPosition();

            if (Math.Sign(target.X - startPos.X) != Math.Sign(agentEntity.agentPhysicsState.MovingDirection ))
                agentEntity.agentPhysicsState.MovingDirection *= -1;

            GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
            nodeEntity.nodeExecution.State = Enums.NodeState.Running;
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            const float FIRE_DELAY = 0.25f;
            float elapsed = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;

            if (elapsed >= FIRE_DELAY)
            {
                AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
                ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
                Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

                Vec2f startPos = agentEntity.GetGunFiringPosition();
                Vec2f target = nodeEntity.nodeTarget.TargetPos;
                int bulletsPerShot = WeaponProperty.BulletsPerShot;
                var spread = WeaponProperty.SpreadAngle;
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    float randomSpread = UnityEngine.Random.Range(-spread, spread);
                    ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f((target.X - startPos.X) - randomSpread,
                        target.Y - startPos.Y).Normalized, WeaponProperty.ProjectileType, WeaponProperty.BasicDemage);

                    if (WeaponProperty.ProjectileType == Enums.ProjectileType.Arrow)
                        projectileEntity.isProjectileFirstHIt = false;

                    projectileEntity.AddProjectileRange(WeaponProperty.Range);
                }

                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
            }

        }
    }
}
