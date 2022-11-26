using System;
using KMath;
using Planet;
using UnityEngine;
using Enums;
using NodeSystem;
using AI;
using Particle;

namespace Action
{
    public class ShootFireWeaponAction
    {
        public struct ShootFireWeaponData
        {
            public Vec2f Target;
        }

        // Action used by either player and AI.
        static public NodeSystem.NodeState Action(object ptr, int id)
        {
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity itemEntity = agentEntity.GetItem();

            if (itemEntity == null)
                return NodeSystem.NodeState.Failure;
            Item.FireWeaponProperties fireWeaponProperties = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            if (/*physicsState.MovementState != AgentMovementState.Falling &&
            physicsState.MovementState != AgentMovementState.Jump &&
            physicsState.MovementState != AgentMovementState.Flip &&
            physicsState.MovementState != AgentMovementState.JetPackFlying &&
            physicsState.MovementState != AgentMovementState.SlidingLeft &&
            physicsState.MovementState != AgentMovementState.SlidingRight*/true)
            {
                Vec2f target = agentEntity.agentModel3D.AimTarget;

                int bulletsPerShot = fireWeaponProperties.BulletsPerShot;

                if (itemEntity.hasItemFireWeaponClip)
                {
                    int numBullet = itemEntity.itemFireWeaponClip.NumOfBullets;
                    if (numBullet <= 0)
                    {
                        Debug.Log("Clip is empty. Press R to reload.");
                        return NodeSystem.NodeState.Failure;
                    }

                    itemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;
                }

                agentEntity.FireGun(fireWeaponProperties.CoolDown);
                Vec2f startPos = agentEntity.GetGunFiringPosition();

                if (Math.Sign(target.X - startPos.X) != Math.Sign(agentEntity.agentPhysicsState.FacingDirection))
                    agentEntity.agentPhysicsState.FacingDirection *= -1;

                // Todo: Urgent(Create new cool down system)
                //GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
                ref ShootFireWeaponData shootingData = ref data.GetActionSequenceData<ShootFireWeaponData>(id);
                shootingData.Target = target;

                GameState.Planet.AddParticleEmitter(agentEntity.GetGunFiringPosition(), ParticleEmitterType.MuzzleFlash);
                var spread = fireWeaponProperties.SpreadAngle;
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    float randomSpread = UnityEngine.Random.Range(-spread, spread);
                    ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f((target.X - startPos.X) - randomSpread,
                        target.Y - startPos.Y).Normalized, fireWeaponProperties.ProjectileType, fireWeaponProperties.BasicDemage, agentEntity.agentID.ID);


                    if (fireWeaponProperties.ProjectileType == Enums.ProjectileType.Arrow)
                    {
                        projectileEntity.isProjectileFirstHIt = false;
                    }

                    //projectileEntity.AddProjectileRange(WeaponProperty.Range);
                }

                return NodeSystem.NodeState.Success;
            }

            return NodeSystem.NodeState.Failure;
        }
    }
}
