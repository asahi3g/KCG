using System;
using KMath;
using Planet;
using UnityEngine;
using Enums;
using System.Collections.Generic;

namespace Node
{
    public class ShootFireWeaponAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ShootFireWeaponAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }
        public override List<Tuple<string, Type>> RegisterStates()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("Target", typeof(Vec2f)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            if (!agentEntity.hasAgentInventory)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }

            int inventoryID = agentEntity.agentInventory.InventoryID;
            InventoryEntity inventoryEntity = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            int selected = inventoryEntity.inventoryEntity.SelectedSlotID;
            ItemInventoryEntity itemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selected);
            if (itemEntity == null)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            var physicsState = agentEntity.agentPhysicsState;

            if (physicsState.MovementState != AgentMovementState.Falling &&
            physicsState.MovementState != AgentMovementState.Jump &&
            physicsState.MovementState != AgentMovementState.Flip &&
            physicsState.MovementState != AgentMovementState.JetPackFlying &&
            physicsState.MovementState != AgentMovementState.SlidingLeft &&
            physicsState.MovementState != AgentMovementState.SlidingRight)
            {
                Vec2f target = Vec2f.Zero;

                if (agentEntity.hasAgentController)
                {
                    ref AI.BlackBoard blackboard = ref planet.EntitasContext.agent.GetEntityWithAgentID(
                        nodeEntity.nodeOwner.AgentID).agentController.Controller.BlackBoard;
                    blackboard.Get(nodeEntity.nodeBlackboardData.entriesIDs[0], out target);
                }
                else if (agentEntity.isAgentPlayer)
                {
                    // Todo: Move target selection to an agent system.
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    target.X = worldPosition.x;
                    target.Y = worldPosition.y;
                }
                nodeEntity.ReplaceNodeTarget(target);

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

                agentEntity.FireGun(WeaponProperty.CoolDown);
                Vec2f startPos = agentEntity.GetGunFiringPosition();

                if (Math.Sign(target.X - startPos.X) != Math.Sign(agentEntity.agentPhysicsState.FacingDirection))
                    agentEntity.agentPhysicsState.FacingDirection *= -1;

                GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
                nodeEntity.nodeExecution.State = Enums.NodeState.Running;
            }
            else
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
            }
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            const float FIRE_DELAY = 0.25f;
            float elapsed = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            Vec2f target = nodeEntity.nodeTarget.TargetPos;

            if (elapsed >= FIRE_DELAY)
            {
                AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
                int inventoryID = agentEntity.agentInventory.InventoryID;
                InventoryEntity inventoryEntity = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                int selected = inventoryEntity.inventoryEntity.SelectedSlotID;
                ItemInventoryEntity itemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selected);
                Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

                Vec2f startPos = agentEntity.GetGunFiringPosition();
                int bulletsPerShot = WeaponProperty.BulletsPerShot;
                var spread = WeaponProperty.SpreadAngle;
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    float randomSpread = UnityEngine.Random.Range(-spread, spread);
                    ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f((target.X - startPos.X) - randomSpread,
                        target.Y - startPos.Y).Normalized, WeaponProperty.ProjectileType, WeaponProperty.BasicDemage, agentEntity.agentID.ID);

                    if (WeaponProperty.ProjectileType == Enums.ProjectileType.Arrow)
                    {
                        projectileEntity.isProjectileFirstHIt = false;
                    }

                    projectileEntity.AddProjectileRange(WeaponProperty.Range);
                }

                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
            }

        }
    }
}
