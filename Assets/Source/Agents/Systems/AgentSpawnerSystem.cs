//imports UnityEngine

using KMath;
using Animancer;
using AI;
using System.Collections.Generic;
using UnityEngine;

namespace Agent
{
    public class AgentSpawnerSystem
    {
        private static int UniqueID = 0;

        private bool CreateAgentRenderer(AgentEntity agentEntity, out AgentRenderer agentRenderer)
        {
            agentRenderer = null;
            if (agentEntity != null)
            {
                ref AgentPropertiesTemplate agentPropertiesTemplate = ref GameState.AgentCreationApi.GetRef((int)agentEntity.agentID.Type);
                
                if (Engine3D.AssetManager.Singelton.GetPrefabAgent(agentPropertiesTemplate.ModelType, out agentRenderer))
                {
                    agentRenderer = UnityEngine.Object.Instantiate(agentRenderer);
                    agentRenderer.SetAgent(agentEntity);
                }
            }
            return agentRenderer != null;
        }

        public AgentEntity Spawn(Vec2f position, Enums.AgentType agentType, AgentFaction faction, int inventoryID = -1, int equipmentInventoryID = -1)
        {
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.CreateEntity();

            ref AgentPropertiesTemplate agentPropertiesTemplate = ref GameState.AgentCreationApi.GetRef((int)agentType);

            agentEntity.AddAgentID(UniqueID++, -1, agentType, faction, -1); // agent id 
            agentEntity.isAgentAlive = true;
            agentEntity.AddPhysicsBox2DCollider(agentPropertiesTemplate.CollisionDimensions, agentPropertiesTemplate.CollisionOffset);
            agentEntity.AddAgentAction(AgentAlertState.UnAlert);
            agentEntity.AddAgentStats(new ContainerInt(agentPropertiesTemplate.DefaultHealth, 0, agentPropertiesTemplate.DefaultHealth), new ContainerInt(100, 0, 100), 
                new ContainerInt(100, 0, 100), new ContainerInt(100, 0, 100), new ContainerInt(100, 0, 100));

            agentEntity.AddAgentStagger(false, agentPropertiesTemplate.StaggerAffectTime, 0.0f, false, agentPropertiesTemplate.ImpactAffectTime);

            agentEntity.AddAgentPhysicsState(
                newPosition: position,
                newPreviousPosition: position, 
                newSpeed: agentPropertiesTemplate.MovProperties.DefaultSpeed,
                newInitialJumpVelocity: Physics.PhysicsFormulas.GetSpeedToJump(agentPropertiesTemplate.MovProperties.JumpHeight),
                newVelocity: Vec2f.Zero, 
                newAcceleration: Vec2f.Zero, 
                newMovingDirection: 1,
                newFacingDirection: 1,
                newActionCooldown: 0,
                newJumpingTime: 0,
                newJumpedFromGround: false,
                newCurerentMoveList: 0,
                newMoveIndex: 0,
                newLastMovementState: Enums.AgentMovementState.None,
                newTimeBetweenMoves: 0,
                newGroundNormal: new Vec2f(0, 1.0f),
                newMovementState: Enums.AgentMovementState.None,
                newLastAgentAnimation: new AgentAnimation(),
                newLastMovingDirection: 1,
                newMovingDistance: 0,
                newSetMovementState: false,
                newAffectedByGravity: true,
                newAffectedByFriction: true,
                newInvulnerable: false,
                newOnGrounded: false,
                newDroping: false,
                newActionInProgress: false,
                newActionJustEnded: false,
                newIdleAfterShootingTime: 0,
                newJumpCounter: 0,
                newSlidingTime: 0,
                newDyingDuration: 0,
                newDashCooldown: 0,
                newDashDuration: 0,
                newActionDuration: 0,
                newStaggerDuration: 0,
                newRollCooldown: 0,
                newRollImpactDuration: 0);

            if (inventoryID != -1)
                agentEntity.AddAgentInventory(inventoryID, equipmentInventoryID, (agentType == Enums.AgentType.Player) ? true : false);

            if (agentType != Enums.AgentType.Player)
            {
                agentEntity.AddAgentsLineOfSight(new CircleSector()
                {
                    Radius = 50,
                    Fov = 60,
                    StartPos = position,
                    Dir = new Vec2f(agentEntity.agentPhysicsState.FacingDirection, 0.0f)
                });

                int behaviorTreeID = GameState.BehaviorTreeManager.Instantiate(agentPropertiesTemplate.BehaviorTreeRootID, agentEntity.agentID.ID);
                agentEntity.AddAgentController(
                    newBehaviorTreeId: behaviorTreeID,
                    newBlackboardID: GameState.BlackboardManager.CreateBlackboard(),
                    newSensorsID: new List<int>()
                    {
                                GameState.SensorManager.CreateSensor(Sensor.SensorType.Sight, agentEntity.agentID.ID, 0)
                                , GameState.SensorManager.CreateSensor(Sensor.SensorType.Hearing, agentEntity.agentID.ID, 0)
                    },
                    newSquadID: -1);
            }
            if (CreateAgentRenderer(agentEntity, out AgentRenderer agentRenderer))
            {
                agentEntity.AddAgentAgent3DModel(Model3DWeaponType.None, null,
                    agentPropertiesTemplate.AnimationType, Enums.ItemAnimationSet.Default, null, 0.0f, null, Vec2f.Zero, agentRenderer, 
                        false);
                    
                Agent3DModel agent3DModel = agentEntity.agentAgent3DModel;
                agent3DModel.SetLocalScale(agentPropertiesTemplate.ModelScale);
                agent3DModel.SetRenderer(agentRenderer);
                agent3DModel.Material = agentRenderer.GetModelMesh().GetComponent<UnityEngine.SkinnedMeshRenderer>().sharedMaterial;
                SetTransformHelper(agent3DModel, position.X, position.Y, 90f);
            }

            switch (agentType)
            {
                case Enums.AgentType.Player:
                    {
                        GameState.Planet.Player = agentEntity; // Todo: use id instead of pointer.
                        agentEntity.isAgentPlayer = true;

                        if (!agentEntity.hasAgentAction)
                            agentEntity.AddAgentAction(AgentAlertState.UnAlert);
                        break;
                    }
                case Enums.AgentType.Marine:
                    {
                        if (!agentEntity.hasAgentAction)
                            agentEntity.AddAgentAction(AgentAlertState.Alert);
                        else
                            agentEntity.agentAction.Action = AgentAlertState.Alert;

                        ItemInventoryEntity item = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.SMG);
                        GameState.InventoryManager.AddItem(item, inventoryID);
                        GameState.InventoryManager.ChangeSelectedSlot(0, inventoryID);
                        agentEntity.SetModel3DWeapon(item);

                        break;
                    }
            }

            void SetTransformHelper(Agent3DModel model3DComponent, float x, float y, float rotation)
            {
                if (model3DComponent == null) return;
                model3DComponent.SetPosition(x, y);
                model3DComponent.SetRotation(rotation);
            }

            return agentEntity;
        }
    }
}
