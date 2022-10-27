//imports UnityEngine

using KMath;
using Animancer;
using AI;

namespace Agent
{
    public class AgentSpawnerSystem
    {
        private static int UniqueID = 0;

        public AgentEntity SpawnPlayer(Contexts entitasContext, int spriteId, int width, int height, Vec2f position,
            int startingAnimation, int playerHealth, int playerFood, int playerWater, int playerOxygen, 
            int playerFuel, float attackCoolDown, int inventoryID = -1, int equipmentInventoryID = -1)
        {
            var entity = entitasContext.agent.CreateEntity();
            ref Agent.AgentProperties properties = ref GameState.AgentCreationApi.GetRef((int)Enums.AgentType.Player);

            var spriteSize = new Vec2f(width / 32f, height / 32f);

            entity.isAgentPlayer = true;
            entity.isECSInput = true;
            entity.AddECSInputXY(new Vec2f(0, 0), false, false);
            entity.AddAgentID(UniqueID++, -1, Enums.AgentType.Player, 0);
            entity.isAgentAlive = true;
            entity.AddAnimationState(1.0f, new Animation.Animation{Type=startingAnimation});
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
            Vec2f size = new Vec2f(spriteSize.X - 0.5f, spriteSize.Y);
            entity.AddPhysicsBox2DCollider(size, new Vec2f(0.25f, .0f));
            entity.AddAgentAction(AgentAlertState.UnAlert);
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
                 newMovingDirection: 1,
                 newFacingDirection: 1,
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


        public AgentEntity SpawnCorpse(Contexts entitasContext, Vec2f position, int spriteId, Enums.AgentType agentType, int inventoryID)
        {
            var entity = entitasContext.agent.CreateEntity();
            ref Agent.AgentProperties properties = ref GameState.AgentCreationApi.GetRef((int)agentType);
            var spriteSize = properties.SpriteSize;

            entity.AddAgentID(UniqueID++, -1, agentType, 0); // agent id 
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
                newMovingDirection: 1,
                newFacingDirection: 1,
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


        public AgentEntity Spawn(Contexts entitasContext, Vec2f position, Enums.AgentType agentType, int faction,
            int inventoryID = -1, int equipmentInventoryID = -1)
        {
            var entity = entitasContext.agent.CreateEntity();

            ref Agent.AgentProperties properties = ref GameState.AgentCreationApi.GetRef((int)agentType);

            var spriteSize = properties.SpriteSize;
            var spriteId = 0;
            entity.AddAgentID(UniqueID++, -1, agentType, faction); // agent id 
            entity.isAgentAlive = true;
            entity.AddPhysicsBox2DCollider(properties.CollisionDimensions, properties.CollisionOffset);
            entity.AddAgentAction(AgentAlertState.UnAlert);
            entity.AddAgentStats((int)properties.Health, 100, 100, 100, 100, properties.AttackCooldown, false);

            entity.AddAgentPhysicsState(
                newPosition: position, 
                newPreviousPosition: default, 
                newSpeed: properties.MovProperties.DefaultSpeed,
                newInitialJumpVelocity: Physics.PhysicsFormulas.GetSpeedToJump(properties.MovProperties.JumpHeight),
                newVelocity: Vec2f.Zero, 
                newAcceleration: Vec2f.Zero, 
                newMovingDirection: 1,
                newFacingDirection: 1,
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
                newSlashCooldown: 0,
                newActionDuration: 0,
                newActionCooldown: 0,
                newStaggerDuration: 0,
                newRollCooldown: 0,
                newRollImpactDuration: 0);

            if (inventoryID != -1)
                entity.AddAgentInventory(inventoryID, equipmentInventoryID, (agentType == Enums.AgentType.Player) ? true : false);

            switch (agentType)
            {
                case Enums.AgentType.Player:
                    {

                        UnityEngine.Material pixelMaterial = Engine3D.AssetManager.Singelton.GetMaterial(Engine3D.MaterialType.PixelMaterial);

                        UnityEngine.GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.SpaceMarine);
                        UnityEngine.GameObject model = UnityEngine.GameObject.Instantiate(prefab);

                        //GameObject leftHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                        //GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        // GameObject rightHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(3).
                        // GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;

                        UnityEngine.GameObject leftHand = model.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetChild(0).
                        GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        UnityEngine.GameObject rightHand = model.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetChild(1).
                        GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;


                        // create an animancer object and give it a reference to the Animator component
                        UnityEngine.GameObject animancerComponentGO = new UnityEngine.GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<UnityEngine.Animator>();
                        entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent,
                            Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, new Vec3f(3.0f, 3.0f, 3.0f));


                        entity.agentPhysicsState.Speed = 10.0f;
                        entity.isAgentPlayer = true;
                        entity.isECSInput = true;
                        entity.AddECSInputXY(new Vec2f(0, 0), false, false);

                        if(!entity.hasAgentAction)
                            entity.AddAgentAction(AgentAlertState.UnAlert);
                        break;
                    }
                case Enums.AgentType.Agent:
                    {
                        entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite component to the entity
                        entity.AddAnimationState(1.0f, new Animation.Animation{Type=properties.StartingAnimation});
                        break;
                    }
                case Enums.AgentType.Slime:
                    {
                        entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite component to the entity
                        entity.AddAnimationState(1.0f, new Animation.Animation{Type=properties.StartingAnimation});
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);
                        break;
                    }
                case Enums.AgentType.FlyingSlime:
                    {
                        entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite component to the entity
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);
                        break;
                    }
                case Enums.AgentType.EnemyGunner:
                    {
                        UnityEngine.GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);
                        UnityEngine.GameObject model = UnityEngine.GameObject.Instantiate(prefab);

                        UnityEngine.GameObject leftHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        UnityEngine.GameObject rightHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

                        model.transform.position = new UnityEngine.Vector3(position.X, position.Y, -1.0f);

                        UnityEngine.Vector3 eulers = model.transform.rotation.eulerAngles;
                        model.transform.rotation = UnityEngine.Quaternion.Euler(0, 90, 0);


                        // create an animancer object and give it a reference to the Animator component
                        UnityEngine.GameObject animancerComponentGO = new UnityEngine.GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<UnityEngine.Animator>();
                        entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent,
                          Enums.AgentAnimationType.HumanoidAnimation,
                        Enums.ItemAnimationSet.Default, new Vec3f(3.0f, 3.0f, 3.0f));
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                        entity.agentPhysicsState.Speed = 6.0f;

                        entity.SetAgentWeapon(Model3DWeapon.Pistol);
                        Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol, entitasContext);
                        break;
                    }
                case Enums.AgentType.EnemySwordman:
                    {
                        UnityEngine.GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);
                        UnityEngine.GameObject model = UnityEngine.GameObject.Instantiate(prefab);

                        UnityEngine.GameObject leftHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        UnityEngine.GameObject rightHand = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

                        model.transform.position = new UnityEngine.Vector3(position.X, position.Y, -1.0f);

                        UnityEngine.Vector3 eulers = model.transform.rotation.eulerAngles;
                        model.transform.rotation = UnityEngine.Quaternion.Euler(0, 90, 0);


                        // create an animancer object and give it a reference to the Animator component
                        UnityEngine.GameObject animancerComponentGO = new UnityEngine.GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<UnityEngine.Animator>();
                        entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent,  
                        Enums.AgentAnimationType.HumanoidAnimation,
                        Enums.ItemAnimationSet.Default, new Vec3f(3.0f, 3.0f, 3.0f));
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                        entity.SetAgentWeapon(Model3DWeapon.Sword);
                        break;
                    }
                case Enums.AgentType.EnemyInsect:
                    {
                        UnityEngine.GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.SmallInsect);
                        UnityEngine.GameObject model = UnityEngine.GameObject.Instantiate(prefab);

                        model.transform.position = new UnityEngine.Vector3(position.X, position.Y, -1.0f);

                        UnityEngine.Vector3 eulers = model.transform.rotation.eulerAngles;
                        model.transform.rotation = UnityEngine.Quaternion.Euler(0, 90, 0);

                        // create an animancer object and give it a reference to the Animator component
                        UnityEngine.GameObject animancerComponentGO = new UnityEngine.GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<UnityEngine.Animator>();
                        entity.AddAgentModel3D(model, null, null, Model3DWeapon.None, null, animancerComponent, 
                            Enums.AgentAnimationType.GroundInsectAnimation, Enums.ItemAnimationSet.Default,
                            new Vec3f(0.6f, 0.6f, 0.6f));
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                        entity.agentPhysicsState.Speed = 6.0f;

                        break;
                    }
                case Enums.AgentType.EnemyHeavy:
                    {
                        UnityEngine.GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.HeavyInsect);
                        UnityEngine.GameObject model = UnityEngine.GameObject.Instantiate(prefab);

                        model.transform.position = new UnityEngine.Vector3(position.X, position.Y, -1.0f);

                        UnityEngine.Vector3 eulers = model.transform.rotation.eulerAngles;
                        model.transform.rotation = UnityEngine.Quaternion.Euler(0, 90, 0);

                        // create an animancer object and give it a reference to the Animator component
                        UnityEngine.GameObject animancerComponentGO = new UnityEngine.GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;
                        // get the animator component from the game object
                        // this component is used by animancer
                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<UnityEngine.Animator>();
                        entity.AddAgentModel3D(model, null, null, Model3DWeapon.None, null, animancerComponent,
                            Enums.AgentAnimationType.GroundInsectHeavyAnimation,
                            Enums.ItemAnimationSet.Default, new Vec3f(0.8f, 0.8f, 0.8f));
                        entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                        entity.agentPhysicsState.Speed = 4.0f;

                        break;
                    }
                case Enums.AgentType.EnemyMarine:
                    {
                        properties = ref GameState.AgentCreationApi.GetRef((int)agentType);

                        UnityEngine.Material pixelMaterial = Engine3D.AssetManager.Singelton.GetMaterial(Engine3D.MaterialType.PixelMaterial);

                        UnityEngine.GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.SpaceMarine);
                        UnityEngine.GameObject model = UnityEngine.GameObject.Instantiate(prefab);

                        UnityEngine.GameObject leftHand = model.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetChild(0).
                        GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                        UnityEngine.GameObject rightHand = model.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetChild(1).
                        GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;

                        UnityEngine.GameObject animancerComponentGO = new UnityEngine.GameObject("AnimancerComponent", typeof(AnimancerComponent));
                        animancerComponentGO.transform.parent = model.transform;

                        AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                        animancerComponent.Animator = model.GetComponent<UnityEngine.Animator>();
                        entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent,
                            Enums.AgentAnimationType.SpaceMarineAnimations, Enums.ItemAnimationSet.Default, new Vec3f(3.0f, 3.0f, 3.0f));

                        entity.agentPhysicsState.Speed = 10.0f;

                        // This is for testin purposes,
                        // will be deleted soon.

                        UnityEngine.SpriteRenderer renderer = properties.TrackStub.AddComponent<UnityEngine.SpriteRenderer>();
                        renderer.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<UnityEngine.Sprite>("UI/Skin/UISprite.psd");
                        renderer.color = UnityEngine.Color.red;
                        properties.TrackStub.transform.position = model.transform.position;
                        properties.TrackStub.transform.localScale = new UnityEngine.Vector3(2, 2, 2);

                        //---

                        if (!entity.hasAgentAction)
                            entity.AddAgentAction(AgentAlertState.Alert);
                        else
                            entity.agentAction.Action = AgentAlertState.Alert;

                        ItemInventoryEntity item = GameState.ItemSpawnSystem.SpawnInventoryItem(entitasContext, Enums.ItemType.SMG);
                        GameState.InventoryManager.AddItem(entitasContext, item, inventoryID);
                        entity.AddAgentController(AISystemState.Behaviors.Get((int)Enums.BehaviorType.Marine).InstatiateBehavior(entitasContext, entity.agentID.ID));
                        entity.HandleItemSelected(item);
                        break;
                    }
            }

            return entity;
        }
    }
}
