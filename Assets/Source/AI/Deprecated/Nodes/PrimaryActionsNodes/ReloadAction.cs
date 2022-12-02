﻿using Item;
using UnityEngine;
using Enums;
using Planet;

namespace Node.Action
{
    public class ReloadAction : NodeBase
    {
        public override ItemUsageActionType  Type => ItemUsageActionType .ReloadAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;

        PlanetState planet = GameState.Planet;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity item = nodeEntity.GetItem();
            agentEntity.agentPhysicsState.ActionInProgress = true;

            if (item != null)
            {
                if (item.hasItemFireWeaponClip)
                {
                    nodeEntity.nodeExecution.State = NodeState.Running;
                    return;
                }
            }
            nodeEntity.nodeExecution.State = NodeState.Fail;
        }

        public override void OnUpdate(NodeEntity nodeEntity)
        {
            ItemInventoryEntity item = nodeEntity.GetItem();
            FireWeaponProperties fireWeaponProperties = GameState.ItemCreationApi.GetWeapon(item.itemType.Type);

            float runningTime = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            if (runningTime >= fireWeaponProperties.ReloadTime)
            {
                if(item.hasItemFireWeaponClip)
                    item.itemFireWeaponClip.NumOfBullets = fireWeaponProperties.ClipSize;

                nodeEntity.nodeExecution.State =  NodeState.Success;
            }
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity item = nodeEntity.GetItem();

            if (item.hasItemFireWeaponClip)
                Debug.Log("Weapon Reloaded." + item.itemFireWeaponClip.NumOfBullets.ToString() + " Ammo in the clip.");
            agentEntity.agentPhysicsState.ActionInProgress = false;
        }

        public override void OnFail(NodeEntity nodeEntity)
        {
            Debug.Log("Reload Failed.");
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            agentEntity.agentPhysicsState.ActionInProgress = false;
        }
    }
}