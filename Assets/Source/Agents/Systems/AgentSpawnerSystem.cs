using System.Collections.Generic;
using KMath;
using UnityEngine;
using Animancer;
using UnityEngine.Animations.Rigging;

namespace Agent
{
    public class AgentSpawnerSystem
    {
        private static int UniqueID = 0;

        AgentCreationApi AgentCreationApi;
        public AgentSpawnerSystem(AgentCreationApi agentCreationApi)
        {
            AgentCreationApi = agentCreationApi;
        }

        public AgentEntity SpawnPlayer(Contexts entitasContext, int spriteId, int width, int height, Vec2f position,
            int startingAnimation, int playerHealth, int playerFood, int playerWater, int playerOxygen, 
            int playerFuel, float attackCoolDown, int inventoryID = -1, int equipmentInventoryID = -1)
        {
            var entity = entitasContext.agent.CreateEntity();
            ref Agent.AgentProperties properties = ref AgentCreationApi.GetRef((int)Enums.AgentType.Player);

            var spriteSize = new Vec2f(width / 32f, height / 32f);

            entity.isAgentPlayer = true;
            entity.isECSInput = true;
            entity.AddECSInputXY(new Vec2f(0, 0), false, false);
            entity.AddAgentID(UniqueID++, -1, Enums.AgentType.Player);
            entity.AddAgentState(AgentState.Alive);
            entity.AddAnimationState(1.0f, new Animation.Animation{Type=startingAnimation});
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
            Vec2f size = new Vec2f(spriteSize.X - 0.5f, spriteSize.Y);
            entity.AddPhysicsBox2DCollider(size, new Vec2f(0.25f, .0f));
            entity.AddAgentAgentAction(AgentAction.UnAlert);
            entity.AddAgentStats(playerHealth, playerFood, playerWater, playerOxygen, playerFuel, attackCoolDown, false);

            if (inventoryID != -1)
                entity.AddAgentInventory(inventoryID, equipmentInventoryID, true);

            entity.AddAgentPhysicsState(
                 newPosition: position,
                 newPreviousPosition: default,
                 newSpeed: properties.MovProperties.DefaultSpeed,
                 newInitialJumpVelocity: Physics.PhysicsFormulas.GetSpeedToJump(properties.MovProperties.JumpHeight),
                 newVelocity: Vec2f.Zero,
                 newAcceleration: Vec2f.Zero,
                 newDirection: 1,
                 newMovementState: Enums.AgentMovementState.None,
                 newAffectedByGravity: true,
                 newAffectedByFriction: true,
                 newInvulnerable: false,
                 newOnGrounded: false,
                 newDroping: false,
                 newActionInProgress: false,
                 newActionJustEnded: false,
                 newJumpCounter: 0,
                 newActionDuration: 0,
                 newActionCooldown: 0,
                 newSlidingTime: 0,
                 newDyingDuration: 0,
                 newDashCooldown: 0,
                 newSlashCooldown: 0,
                 newStaggerDuration: 0,
                 newRollCooldown: 0,
                 newRollImpactDuration: 0);

            return entity;
        }


        public AgentEntity SpawnCorpse(Contexts entitasContext, Vec2f position, int spriteId, Enums.AgentType agentType, 
            int inventoryID)
        {
            var entity = entitasContext.agent.CreateEntity();
            ref Agent.AgentProperties properties = ref AgentCreationApi.GetRef((int)agentType);
            var spriteSize = properties.SpriteSize;

            entity.AddAgentID(UniqueID++, -1, agentType); // agent id 
            entity.isAgentCorpse = true;
            entity.AddPhysicsBox2DCollider(properties.CollisionDimensions, properties.CollisionOffset);
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity

            if (inventoryID != -1)
                entity.AddAgentInventory(inventoryID, -1, false);

            // used for physics simulation
            entity.AddAgentPhysicsState(
                newPosition: position,
                newPreviousPosition: default,
                newSpeed: properties.MovProperties.DefaultSpeed,
                 newInitialJumpVelocity: Physics.PhysicsFormulas.GetSpeedToJump(properties.MovProperties.JumpHeight),
                newVelocity: Vec2f.Zero,
                newAcceleration: Vec2f.Zero,
                newDirection: 1,
                newMovementState: Enums.AgentMovementState.None,
                newAffectedByGravity: true,
                newAffectedByFriction: true,
                newInvulnerable: false,
                newOnGrounded: false,
                newDroping: false,
                newActionInProgress: false,
                newActionJustEnded: false,
                newJumpCounter: 0,
                newSlidingTime: 0,
                newActionDuration: 0,
                newActionCooldown: 0,
                newDyingDuration: 0,
                newDashCooldown: 0,
                newSlashCooldown: 0,
                newStaggerDuration: 0,
                newRollCooldown: 0,
                newRollImpactDuration: 0);

            return entity;
        }


        public AgentEntity Spawn(Contexts entitasContext, Vec2f position, Enums.AgentType agentType, 
            int inventoryID = -1, int equipmentInventoryID = -1)
        {
            var entity = entitasContext.agent.CreateEntity();

            ref Agent.AgentProperties properties = ref AgentCreationApi.GetRef((int)agentType);

            var spriteSize = properties.SpriteSize;
            var spriteId = 0;
            entity.AddAgentID(UniqueID++, -1, agentType); // agent id 
            entity.AddAgentState(AgentState.Alive);
            entity.AddPhysicsBox2DCollider(properties.CollisionDimensions, properties.CollisionOffset);
            entity.AddAgentStats((int)properties.Health, 100, 100, 100, 100, properties.AttackCooldown, false);
            // used for physics simulation
            entity.AddAgentPhysicsState(
                position, 
                newPreviousPosition: default, 
                newSpeed: properties.MovProperties.DefaultSpeed,
                newInitialJumpVelocity: Physics.PhysicsFormulas.GetSpeedToJump(properties.MovProperties.JumpHeight),
                newVelocity: Vec2f.Zero, 
                newAcceleration: Vec2f.Zero, 
                newDirection: 1,
                newMovementState: Enums.AgentMovementState.None,
                newAffectedByGravity: true,
                newAffectedByFriction: true,
                newInvulnerable: false,
                newOnGrounded: false,
                newDroping: false,
                newActionInProgress: false,
                newActionJustEnded: false,
                newJumpCounter: 0,
                newSlidingTime: 0,
                newDyingDuration: 0,
                newDashCooldown: 0,
                newSlashCooldown: 0,
                newActionDuration: 0,
                newActionCooldown: 0,
                newStaggerDuration: 0,
                newRollCooldown: 0,
                newRollImpactDuration: 0);

            switch (agentType)
            {
                case Enums.AgentType.Player:
                    {
                        /*entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
                        entity.AddAnimationState(1.0f, new Animation.Animation{Type=properties.StartingAnimation});*/

                        Material pixelMaterial = Engine3D.AssetManager.Singelton.GetMaterial(Engine3D.MaterialType.PixelMaterial);

                        GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);
                        GameObject model = GameObject.Instantiate(prefab);

                        //var hand = model.transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
                        GameObject leftHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        GameObject rightHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        //GameObject leftHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        //GameObject rightHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        // GameObject dummy = model.transform.GetChild(0).gameObject;      
                        // GameObject.Destroy(dummy);    

                        //model.transform.position = new Vector3(position.X, position.Y, -1.0f);

                        //model.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = pixelMaterial;


                        // create an animancer object and give it a reference to the Animator component
                        GameObject animancerComponentGO = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<Animator>();
                        entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent, Enums.AgentAnimationType.HumanoidAnimation);


                        entity.agentPhysicsState.Speed = 10.0f;
                        entity.isAgentPlayer = true;
                        entity.isECSInput = true;
                        entity.AddECSInputXY(new Vec2f(0, 0), false, false);
                        entity.AddAgentAgentAction(AgentAction.UnAlert);

                        break;
                    }
            case Enums.AgentType.Agent:
                    {
                        entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
                        entity.AddAnimationState(1.0f, new Animation.Animation{Type=properties.StartingAnimation});
                        break;
                    }
            case Enums.AgentType.Slime:
                    {
                        entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
                        entity.AddAnimationState(1.0f, new Animation.Animation{Type=properties.StartingAnimation});
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);
                        break;
                    }
                case Enums.AgentType.FlyingSlime:
                    {
                        entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);
                        break;
                    }
                case Enums.AgentType.EnemyGunner:
                    {
                        GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);
                        GameObject model = GameObject.Instantiate(prefab);

                        GameObject leftHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        GameObject rightHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

                        model.transform.position = new Vector3(position.X, position.Y, -1.0f);

                        Vector3 eulers = model.transform.rotation.eulerAngles;
                        model.transform.rotation = Quaternion.Euler(0, 90, 0);


                        // create an animancer object and give it a reference to the Animator component
                        GameObject animancerComponentGO = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<Animator>();
                        entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent,  Enums.AgentAnimationType.HumanoidAnimation);
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                        entity.agentPhysicsState.Speed = 6.0f;

                        entity.SetAgentWeapon(Model3DWeapon.Gun);
                        break;
                    }
                case Enums.AgentType.EnemySwordman:
                    {
                        GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);
                        GameObject model = GameObject.Instantiate(prefab);

                        GameObject leftHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        GameObject rightHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

                        model.transform.position = new Vector3(position.X, position.Y, -1.0f);

                        Vector3 eulers = model.transform.rotation.eulerAngles;
                        model.transform.rotation = Quaternion.Euler(0, 90, 0);


                        // create an animancer object and give it a reference to the Animator component
                        GameObject animancerComponentGO = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<Animator>();
                        entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent,  Enums.AgentAnimationType.HumanoidAnimation);
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                        entity.SetAgentWeapon(Model3DWeapon.Sword);
                        break;
                    }
                    case Enums.AgentType.EnemyInsect:
                    {
                        GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.SmallInsect);
                        GameObject model = GameObject.Instantiate(prefab);

                        model.transform.position = new Vector3(position.X, position.Y, -1.0f);

                        Vector3 eulers = model.transform.rotation.eulerAngles;
                        model.transform.rotation = Quaternion.Euler(0, 90, 0);

                        // create an animancer object and give it a reference to the Animator component
                        GameObject animancerComponentGO = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<Animator>();
                        entity.AddAgentModel3D(model, null, null, Model3DWeapon.None, null, animancerComponent,  Enums.AgentAnimationType.GroundInsectAnimation);
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                        entity.agentPhysicsState.Speed = 6.0f;

                        break;
                    }
                    case Enums.AgentType.EnemyHeavy:
                    {
                        GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.HeavyInsect);
                        GameObject model = GameObject.Instantiate(prefab);

                        model.transform.position = new Vector3(position.X, position.Y, -1.0f);

                        Vector3 eulers = model.transform.rotation.eulerAngles;
                        model.transform.rotation = Quaternion.Euler(0, 90, 0);

                        // create an animancer object and give it a reference to the Animator component
                        GameObject animancerComponentGO = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<Animator>();
                        entity.AddAgentModel3D(model, null, null, Model3DWeapon.None, null, animancerComponent,  Enums.AgentAnimationType.GroundInsectHeavyAnimation);
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                        entity.agentPhysicsState.Speed = 4.0f;

                        break;
                    }
            }

            if (inventoryID != -1)
                entity.AddAgentInventory(inventoryID, equipmentInventoryID, (agentType == Enums.AgentType.Player) ? true : false);

            return entity;
        }
    }
}
