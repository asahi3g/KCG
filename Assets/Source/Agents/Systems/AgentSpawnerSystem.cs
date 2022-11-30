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

        public AgentEntity SpawnPlayer(int spriteId, int width, int height, Vec2f position,
            int startingAnimation, int playerHealth, int playerFood, int playerWater, int playerOxygen, 
            int playerFuel, float attackCoolDown, int inventoryID = -1, int equipmentInventoryID = -1)
        {
            var entity = GameState.Planet.EntitasContext.agent.CreateEntity();
            ref AgentPropertiesTemplate properties = ref GameState.AgentCreationApi.GetRef((int)Enums.AgentType.Player);

            var spriteSize = new Vec2f(width / 32f, height / 32f);

            entity.isAgentPlayer = true;
            entity.AddAgentID(UniqueID++, -1, Enums.AgentType.Player, 0);
            entity.isAgentAlive = true;
            entity.AddAnimationState(1.0f, new Animation.Animation{Type=startingAnimation});
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
            Vec2f size = new Vec2f(spriteSize.X - 0.5f, spriteSize.Y);
            entity.AddPhysicsBox2DCollider(size, new Vec2f(0.25f, .0f));
            entity.AddAgentAction(AgentAlertState.UnAlert);
            entity.AddAgentStats(new ContainerInt(9999, 0, 9999), new ContainerInt(100, 0, 100), new ContainerInt(100, 0, 
                100), new ContainerInt(100, 0, 100), new ContainerInt(100, 0, 100));

            if (inventoryID != -1)
                entity.AddAgentInventory(inventoryID, equipmentInventoryID, true);

            entity.AddAgentStagger(false, properties.StaggerAffectTime, 0.0f);

            entity.AddAgentPhysicsState(
                 newPosition: position,
                 newPreviousPosition: default,
                 newSpeed: properties.MovProperties.DefaultSpeed,
                 newInitialJumpVelocity: Physics.PhysicsFormulas.GetSpeedToJump(properties.MovProperties.JumpHeight),
                 newVelocity: Vec2f.Zero,
                 newAcceleration: Vec2f.Zero,
                 newMovingDirection: 1,
                 newFacingDirection: 1,
                 newGroundNormal: new Vec2f(0, 1.0f),
                 newMovementState: Enums.AgentMovementState.None,
                 newLastAgentAnimation: new AgentAnimation(),
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
                 newActionDuration: 0,
                 newSlidingTime: 0,
                 newDyingDuration: 0,
                 newDashCooldown: 0,
                 newDashDuration: 0,
                 newStaggerDuration: 0,
                 newRollCooldown: 0,
                 newRollImpactDuration: 0);


            return entity;
        }


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

        public AgentEntity Spawn(Vec2f position, Enums.AgentType agentType, int faction,
            int inventoryID = -1, int equipmentInventoryID = -1)
        {
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.CreateEntity();

            ref AgentPropertiesTemplate agentPropertiesTemplate = ref GameState.AgentCreationApi.GetRef((int)agentType);

            var spriteId = 0;
            agentEntity.AddAgentID(UniqueID++, -1, agentType, faction); // agent id 
            agentEntity.isAgentAlive = true;
            agentEntity.AddPhysicsBox2DCollider(agentPropertiesTemplate.CollisionDimensions, agentPropertiesTemplate.CollisionOffset);
            agentEntity.AddAgentAction(AgentAlertState.UnAlert);
            agentEntity.AddAgentStats(new ContainerInt(100, 0, 100), new ContainerInt(100, 0, 100), new ContainerInt(100, 0,100), new ContainerInt(100, 0, 100), new ContainerInt(100, 0, 100));

            agentEntity.AddAgentStagger(false, agentPropertiesTemplate.StaggerAffectTime, 0.0f);

            agentEntity.AddAgentPhysicsState(
                newPosition: position,
                newPreviousPosition: position, 
                newSpeed: agentPropertiesTemplate.MovProperties.DefaultSpeed,
                newInitialJumpVelocity: Physics.PhysicsFormulas.GetSpeedToJump(agentPropertiesTemplate.MovProperties.JumpHeight),
                newVelocity: Vec2f.Zero, 
                newAcceleration: Vec2f.Zero, 
                newMovingDirection: 1,
                newFacingDirection: 1,
                newGroundNormal: new Vec2f(0, 1.0f),
                newMovementState: Enums.AgentMovementState.None,
                newLastAgentAnimation: new AgentAnimation(),
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

            switch (agentType)
            {
                case Enums.AgentType.Player:
                    {
                        if (CreateAgentRenderer(agentEntity, out AgentRenderer agentRenderer))
                        {
                            Agent3DModel agent3DModel = agentEntity.AddAgentModel3D(agentRenderer, Model3DWeaponType.None, null,
                                agentPropertiesTemplate.AnimationType, Enums.ItemAnimationSet.Default, agentPropertiesTemplate.ModelScale,
                                Vec2f.Zero);

                            SetTransformHelper(agent3DModel, position.X, position.Y, 90f);

                            // entity.agentPhysicsState.Speed = 10.0f;
                            agentEntity.isAgentPlayer = true;

                            if(!agentEntity.hasAgentAction)
                                agentEntity.AddAgentAction(AgentAlertState.UnAlert);
                        }
                        
                        break;
                    }
                case Enums.AgentType.Agent:
                    {
                        //entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite component to the entity
                        agentEntity.AddAnimationState(1.0f, new Animation.Animation{Type=agentPropertiesTemplate.StartingAnimation});
                        break;
                    }
                case Enums.AgentType.Slime:
                    {
                       // entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite component to the entity
                        agentEntity.AddAnimationState(1.0f, new Animation.Animation{Type=agentPropertiesTemplate.StartingAnimation});
                        break;
                    }
                case Enums.AgentType.FlyingSlime:
                    {
                        //entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite component to the entity
                        break;
                    }
                case Enums.AgentType.EnemyGunner:
                    {
                        if (CreateAgentRenderer(agentEntity, out AgentRenderer agentRenderer))
                        {
                            Agent3DModel agent3DModel = agentEntity.AddAgentModel3D(agentRenderer, Model3DWeaponType.None, null,
                                Enums.AgentAnimationType.HumanoidAnimation,
                                Enums.ItemAnimationSet.Default, new Vec3f(3.0f, 3.0f, 3.0f), Vec2f.Zero);
                            SetTransformHelper(agent3DModel, position.X, position.Y, 90f);

                            agentEntity.agentPhysicsState.Speed = 6.0f;

                            agentEntity.SetModel3DWeapon(Model3DWeaponType.Pistol);
                            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol);
                        }
                        
                        break;
                    }
                case Enums.AgentType.EnemySwordman:
                    {
                        if (CreateAgentRenderer(agentEntity, out AgentRenderer agentRenderer))
                        {
                            Agent3DModel agent3DModel = agentEntity.AddAgentModel3D(agentRenderer, Model3DWeaponType.None, null,  
                                Enums.AgentAnimationType.HumanoidAnimation,
                                Enums.ItemAnimationSet.Default, new Vec3f(3.0f, 3.0f, 3.0f), Vec2f.Zero);

                            SetTransformHelper(agent3DModel, position.X, position.Y, 90f);
                            agentEntity.SetModel3DWeapon(Model3DWeaponType.Sword);
                        }
                        
                        break;
                    }
                case Enums.AgentType.InsectSmall:
                    {
                        if (CreateAgentRenderer(agentEntity, out AgentRenderer agentRenderer))
                        {
                            Agent3DModel agent3DModel = agentEntity.AddAgentModel3D(agentRenderer, Model3DWeaponType.None, null, 
                                agentPropertiesTemplate.AnimationType, Enums.ItemAnimationSet.Default,
                                agentPropertiesTemplate.ModelScale, Vec2f.Zero);
                            SetTransformHelper(agent3DModel, position.X, position.Y, 90f);
                        }

                        break;
                    }
                case Enums.AgentType.InsectLarge:
                    {
                        if (CreateAgentRenderer(agentEntity, out AgentRenderer agentRenderer))
                        {
                            Agent3DModel agent3DModel = agentEntity.AddAgentModel3D(agentRenderer, Model3DWeaponType.None, null,
                                agentPropertiesTemplate.AnimationType,
                                Enums.ItemAnimationSet.Default, agentPropertiesTemplate.ModelScale, Vec2f.Zero);
                            SetTransformHelper(agent3DModel, position.X, position.Y, 90f);
                        }

                        break;
                    }
                case Enums.AgentType.EnemyMarine:
                    {
                        if (CreateAgentRenderer(agentEntity, out AgentRenderer agentRenderer))
                        {
                            Agent3DModel agent3DModel = agentEntity.AddAgentModel3D(agentRenderer, Model3DWeaponType.None, null,
                                Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, new Vec3f(3.0f, 3.0f, 3.0f), Vec2f.Zero);
                            SetTransformHelper(agent3DModel, position.X, position.Y, 90f);
                            
                            agentEntity.agentPhysicsState.Speed = 10.0f;

                            if (!agentEntity.hasAgentAction)
                                agentEntity.AddAgentAction(AgentAlertState.Alert);
                            else
                                agentEntity.agentAction.Action = AgentAlertState.Alert;

                            ItemInventoryEntity item = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.SMG);
                            GameState.InventoryManager.AddItem(item, inventoryID);
                            agentEntity.SetModel3DWeapon(item);
                        }
                        
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
