using System;
using KMath;
using Planet;
using UnityEngine;
using Enums;

namespace Node
{
    public class ShootFireWeaponAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ShootFireWeaponAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity ItemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);

            // Todo: Move target selection to an agent system.
            Vec2f target = Vec2f.Zero;
            if(nodeEntity.hasNodeTaget)
            {
                int agentTargetID = nodeEntity.nodeTaget.AgentTargetID;
                if (agentTargetID != -1)
                {
                    AgentEntity targetAgentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(agentTargetID);
                    target = targetAgentEntity.agentPhysicsState.Position + targetAgentEntity.physicsBox2DCollider.Size.Y * 0.7f;
                }
                else
                    target = nodeEntity.nodeTaget.TargetPos;
            }
            else
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.X = worldPosition.x;
                target.Y = worldPosition.y;
            }

            int bulletsPerShot = WeaponProperty.BulletsPerShot;

            if (ItemEntity.hasItemFireWeaponClip)
            {
                int numBullet = ItemEntity.itemFireWeaponClip.NumOfBullets;
                if (numBullet <= 0)
                {
                    Debug.Log("Clip is empty. Press R to reload.");
                    nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                    return;
                }
            }

            if (ItemEntity.hasItemFireWeaponClip)
                ItemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;

            Vec2f startPos = agentEntity.agentPhysicsState.Position;

            if (target.X > agentEntity.agentPhysicsState.Position.X && agentEntity.agentPhysicsState.MovingDirection  == -1)
                agentEntity.agentPhysicsState.MovingDirection = 1;
            else if (target.X < agentEntity.agentPhysicsState.Position.X && agentEntity.agentPhysicsState.MovingDirection  == 1)
                agentEntity.agentPhysicsState.MovingDirection = -1;

            agentEntity.FireGun(WeaponProperty.CoolDown);

            startPos.X += 0.3f * agentEntity.agentPhysicsState.MovingDirection ;
            startPos.Y += 1.75f;

            // Todo: Rotate agent instead.
            if (Math.Sign(target.X - startPos.X) != Math.Sign(agentEntity.agentPhysicsState.MovingDirection ))
                target.X = startPos.X + 0.5f * agentEntity.agentPhysicsState.MovingDirection;

            var spread = WeaponProperty.SpreadAngle;
            for(int i = 0; i < bulletsPerShot; i++)
            {
                float randomSpread = UnityEngine.Random.Range(-spread, spread);
                ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f((target.X - startPos.X) - randomSpread,
                    target.Y - startPos.Y).Normalized, WeaponProperty.ProjectileType, WeaponProperty.BasicDemage);

                if (WeaponProperty.ProjectileType == Enums.ProjectileType.Arrow)
                    projectileEntity.isProjectileFirstHIt = false;

                projectileEntity.AddProjectileRange(WeaponProperty.Range);
            }

            nodeEntity.nodeExecution.State = Enums.NodeState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
            base.OnExit(ref planet, nodeEntity);
        }
    }
}
