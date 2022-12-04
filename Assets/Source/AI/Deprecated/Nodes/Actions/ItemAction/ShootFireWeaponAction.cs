using System;
using KMath;
using UnityEngine;
using Enums;
using System.Collections.Generic;
using Particle;

namespace Node
{
    public class ShootFireWeaponAction : NodeBase
    {
        public override ActionType  Type => ActionType .ShootFireWeaponAction;

        public override List<Tuple<string, Type>> RegisterEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("Target", typeof(Vec2f)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (agentEntity.agentPhysicsState.ActionInProgress)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            if (!agentEntity.hasAgentInventory)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            int inventoryID = agentEntity.agentInventory.InventoryID;
            InventoryEntity inventoryEntity = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            int selected = inventoryEntity.inventoryInventoryEntity.SelectedSlotIndex;
            ItemInventoryEntity itemEntity = GameState.InventoryManager.GetItemInSlot(inventoryID, selected);
            if (itemEntity == null)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }
            Item.FireWeaponProperties fireWeaponProperties = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            var physicsState = agentEntity.agentPhysicsState;

            if (true)
            {
                Vec2f target = Vec2f.Zero;
                if (agentEntity.isAgentPlayer)
                {
                    // Todo: Move target selection to an agent system.
                    target = agentEntity.GetGunFiringTarget();
                }
                nodeEntity.ReplaceNodeTarget(target);

                int bulletsPerShot = fireWeaponProperties.BulletsPerShot;

                if (itemEntity.hasItemFireWeaponClip)
                {
                    int numBullet = itemEntity.itemFireWeaponClip.NumOfBullets;
                    if (numBullet <= 0)
                    {
                        //Debug.Log("Clip is empty. Press R to reload.");
                        nodeEntity.nodeExecution.State = NodeState.Fail;
                        return;
                    }

                    itemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;
                }

                agentEntity.FireGun(fireWeaponProperties.CoolDown);
                Vec2f startPos = agentEntity.GetGunFiringPosition();

                if (Math.Sign(target.X - startPos.X) != Math.Sign(agentEntity.agentPhysicsState.FacingDirection))
                    agentEntity.agentPhysicsState.FacingDirection *= -1;

                GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, fireWeaponProperties.CoolDown);
                GameState.Planet.AddParticleEmitter(agentEntity.GetGunFiringPosition() + new Vec2f(-0.33f, -0.33f), ParticleEmitterType.MuzzleFlash);
                var spread = fireWeaponProperties.SpreadAngle;
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    float randomSpread = UnityEngine.Random.Range(-spread, spread);
                    ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f((target.X - agentEntity.GetGunOrigin().X) - randomSpread,
                        target.Y - agentEntity.GetGunOrigin().Y).Normalized, fireWeaponProperties.ProjectileType, fireWeaponProperties.BasicDemage, agentEntity.agentID.ID);

                    
                    if (fireWeaponProperties.ProjectileType == ProjectileType.Arrow)
                    {
                        projectileEntity.isProjectileFirstHIt = false;
                    }

                    //projectileEntity.AddProjectileRange(WeaponProperty.Range);
                }

                nodeEntity.nodeExecution.State = NodeState.Success;
            }
            else
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
            }
        }
    }
}
