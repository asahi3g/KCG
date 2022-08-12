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

            var spriteSize = new Vec2f(width / 32f, height / 32f);

            entity.isAgentPlayer = true;
            entity.isECSInput = true;
            entity.AddECSInputXY(new Vec2f(0, 0), false, false);
            entity.AddAgentID(UniqueID++, AgentType.Player, -1);
            entity.AddAnimationState(1.0f, new Animation.Animation{Type=startingAnimation});
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
            entity.AddAgentPhysicsState(position, newPreviousPosition: default,
                newSpeed: 10f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero, 1,
                Enums.AgentMovementState.None, true, false, false, false, false, 0, 0, 0, 0, 0);
            var size = new Vec2f(spriteSize.X - 0.5f, spriteSize.Y);
            entity.AddPhysicsBox2DCollider(size, new Vec2f(0.25f, .0f));
            entity.AddAgentStats(playerHealth, playerFood, playerWater, playerOxygen, playerFuel, attackCoolDown);
            //entity.AddAgentInventory(0);
            // Add Inventory and toolbar.
           /* var attacher = Inventory.InventoryAttacher.Instance;
            attacher.AttachInventoryToAgent(entitasContext, 10, 6, entity);
            entity.agentInventory.AutoPick = true;*/

            if (inventoryID != -1)
                entity.AddAgentInventory(inventoryID, equipmentInventoryID, true);


            return entity;
        }


        public AgentEntity SpawnCorpse(Contexts entitasContext, Vec2f position, int spriteId, AgentType agentType, 
            int inventoryID)
        {
            var entity = entitasContext.agent.CreateEntity();
            ref Agent.AgentProperties properties = ref AgentCreationApi.GetRef((int)agentType);
            var spriteSize = properties.SpriteSize;

            entity.AddAgentID(UniqueID++, agentType, -1); // agent id 
            entity.isAgentCorpse = true;
            entity.AddPhysicsBox2DCollider(properties.CollisionDimensions, properties.CollisionOffset);
            entity.AddAgentPhysicsState(position, newPreviousPosition: default, 
                newSpeed: 1f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero, 1, 
                Enums.AgentMovementState.None,
                true, false, false, false, false, 0, 0, 0, 0, 0); // used for physics simulation
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity

            entity.AddAgentInventory(inventoryID, -1, false);

            return entity;
        }

        public AgentEntity Spawn(Contexts entitasContext, Vec2f position, AgentType agentType, 
            int inventoryID = -1, int equipmentInventoryID = -1)
        {
            var entity = entitasContext.agent.CreateEntity();

            ref Agent.AgentProperties properties = ref AgentCreationApi.GetRef((int)agentType);

            var spriteSize = properties.SpriteSize;
            var spriteId = 0;
            entity.AddAgentID(UniqueID++, agentType, -1); // agent id 
            entity.AddPhysicsBox2DCollider(properties.CollisionDimensions, properties.CollisionOffset);
            entity.AddAgentPhysicsState(position, newPreviousPosition: default,
                newSpeed: 2.5f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero, 1,
                 Enums.AgentMovementState.None, true, false, false, false, false, 0, 0, 0, 0, 0); // used for physics simulation
            entity.AddAgentStats((int)properties.Health, 100, 100, 100, 100, properties.AttackCooldown);

            if (agentType == Agent.AgentType.Player)
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

                GameObject Rig = new GameObject("Rig 1");
                Rig.transform.parent = model.transform;
                Rig.AddComponent<Rig>();

                RigLayer rigLayer = new RigLayer(Rig.GetComponent<Rig>());

                model.AddComponent<RigBuilder>();
                model.GetComponent<RigBuilder>().layers.Add(rigLayer);

                model.AddComponent<BoneRenderer>();

                model.GetComponent<BoneRenderer>().transforms = new Transform[52];

                model.GetComponent<BoneRenderer>().transforms[0] = model.transform.GetChild(0).GetChild(0); // Bip001 Pelvis

                model.GetComponent<BoneRenderer>().transforms[1] = model.transform.GetChild(0).GetChild(0).GetChild(0); // Bip001 L Thigh

                model.GetComponent<BoneRenderer>().transforms[2] = model.transform.GetChild(0).GetChild(0).GetChild(1); // Bip001 R Thigh

                model.GetComponent<BoneRenderer>().transforms[3] = model.transform.GetChild(0).GetChild(0).GetChild(2); // Bip001 Spine

                model.GetComponent<BoneRenderer>().transforms[4] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).
                    GetChild(1).GetChild(0); // Bip001 Head

                model.GetComponent<BoneRenderer>().transforms[5] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).
                    GetChild(1); // Bip001 Neck

                model.GetComponent<BoneRenderer>().transforms[6] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0); // Bip001 Spine2

                model.GetComponent<BoneRenderer>().transforms[7] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Clavicle

                model.GetComponent<BoneRenderer>().transforms[8] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2); // Bip001 R Clavicle

                model.GetComponent<BoneRenderer>().transforms[9] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0); // Bip001 Spine1

                model.GetComponent<BoneRenderer>().transforms[10] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0); // Bip001 R UpperArm

                model.GetComponent<BoneRenderer>().transforms[11] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0); // Bip001 R Forearm

                model.GetComponent<BoneRenderer>().transforms[12] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0); // Bip001 R Hand

                model.GetComponent<BoneRenderer>().transforms[13] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 R Finger0

                model.GetComponent<BoneRenderer>().transforms[14] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(1); // Bip001 R Finger1

                model.GetComponent<BoneRenderer>().transforms[15] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(2); // Bip001 R Finger2

                model.GetComponent<BoneRenderer>().transforms[16] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(3); // Bip001 R Finger3

                model.GetComponent<BoneRenderer>().transforms[17] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(4); // Bip001 R Finger4

                model.GetComponent<BoneRenderer>().transforms[18] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 R Finger01

                model.GetComponent<BoneRenderer>().transforms[19] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 R Finger02

                model.GetComponent<BoneRenderer>().transforms[20] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0); // Bip001 R Finger11

                model.GetComponent<BoneRenderer>().transforms[21] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0); // Bip001 R Finger12

                model.GetComponent<BoneRenderer>().transforms[22] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0); // Bip001 R Finger21

                model.GetComponent<BoneRenderer>().transforms[23] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0); // Bip001 R Finger22

                model.GetComponent<BoneRenderer>().transforms[24] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0); // Bip001 R Finger31

                model.GetComponent<BoneRenderer>().transforms[25] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0); // Bip001 R Finger32

                model.GetComponent<BoneRenderer>().transforms[26] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0); // Bip001 R Finger41

                model.GetComponent<BoneRenderer>().transforms[27] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0); // Bip001 R Finger42

                model.GetComponent<BoneRenderer>().transforms[28] = model.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0); // Bip001 R Foot

                model.GetComponent<BoneRenderer>().transforms[29] = model.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0); // Bip001 R Toe0

                model.GetComponent<BoneRenderer>().transforms[30] = model.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0); // Bip001 R Calf

                model.GetComponent<BoneRenderer>().transforms[31] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L UpperArm

                model.GetComponent<BoneRenderer>().transforms[32] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0); // Bip001 L Forearm

                model.GetComponent<BoneRenderer>().transforms[33] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0); // Bip001 L Hand

                model.GetComponent<BoneRenderer>().transforms[34] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Finger0

                model.GetComponent<BoneRenderer>().transforms[35] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(1); // Bip001 L Finger1

                model.GetComponent<BoneRenderer>().transforms[36] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                   GetChild(0).GetChild(0).GetChild(0).GetChild(2); // Bip001 L Finger2

                model.GetComponent<BoneRenderer>().transforms[37] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                   GetChild(0).GetChild(0).GetChild(0).GetChild(3); // Bip001 L Finger3

                model.GetComponent<BoneRenderer>().transforms[38] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                   GetChild(0).GetChild(0).GetChild(0).GetChild(4); // Bip001 L Finger4

                model.GetComponent<BoneRenderer>().transforms[39] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Finger01

                model.GetComponent<BoneRenderer>().transforms[40] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Finger02

                model.GetComponent<BoneRenderer>().transforms[41] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0); // Bip001 L Finger11

                model.GetComponent<BoneRenderer>().transforms[42] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0); // Bip001 L Finger12

                model.GetComponent<BoneRenderer>().transforms[43] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0); // Bip001 L Finger21

                model.GetComponent<BoneRenderer>().transforms[44] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0); // Bip001 L Finger22

                model.GetComponent<BoneRenderer>().transforms[45] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                   GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0); // Bip001 L Finger31

                model.GetComponent<BoneRenderer>().transforms[46] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                   GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0); // Bip001 L Finger32

                model.GetComponent<BoneRenderer>().transforms[47] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                   GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0); // Bip001 L Finger41

                model.GetComponent<BoneRenderer>().transforms[48] = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                   GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0); // Bip001 L Finger42

                model.GetComponent<BoneRenderer>().transforms[49] = model.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Foot

                model.GetComponent<BoneRenderer>().transforms[50] = model.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Toe0

                model.GetComponent<BoneRenderer>().transforms[51] = model.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Calf

                model.GetComponent<RigBuilder>().Build();

                GameObject BodyAim = new GameObject("BodyAim");
                BodyAim.transform.parent = Rig.transform;
                BodyAim.AddComponent<MultiAimConstraint>();
                BodyAim.GetComponent<MultiAimConstraint>().weight = 1.0f;
                BodyAim.GetComponent<MultiAimConstraint>().data.constrainedObject = model.transform.GetChild(0).GetChild(0).GetChild(2);
                BodyAim.GetComponent<MultiAimConstraint>().data.aimAxis = MultiAimConstraintData.Axis.Y;
                BodyAim.GetComponent<MultiAimConstraint>().data.upAxis = MultiAimConstraintData.Axis.X_NEG;
                GameObject emptyGO = new GameObject("Target");
                Transform Target = emptyGO.transform;
                Target.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                WeightedTransform TargetTransform = new WeightedTransform(Target, 1.0f);
                BodyAim.GetComponent<MultiAimConstraint>().data.sourceObjects = new WeightedTransformArray(1);
                BodyAim.GetComponent<MultiAimConstraint>().data.sourceObjects.Add(TargetTransform);

                GameObject Aim = new GameObject("Aim");
                Aim.transform.parent = Rig.transform;
                Aim.AddComponent<MultiAimConstraint>();
                Aim.GetComponent<MultiAimConstraint>().data.constrainedObject = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0);
                Aim.GetComponent<MultiAimConstraint>().data.aimAxis = MultiAimConstraintData.Axis.X_NEG;
                Aim.GetComponent<MultiAimConstraint>().data.upAxis = MultiAimConstraintData.Axis.Y_NEG;
                Aim.GetComponent<MultiAimConstraint>().data.sourceObjects = new WeightedTransformArray(1);
                Aim.GetComponent<MultiAimConstraint>().data.sourceObjects.Add(TargetTransform);

                GameObject SecondHandGrabWeapon = new GameObject("SecondHandGrabWeapon");
                SecondHandGrabWeapon.transform.parent = Rig.transform;
                SecondHandGrabWeapon.AddComponent<TwoBoneIKConstraint>();
                SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().weight = 1.0f;
                SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.root = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.mid = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0);
                SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.tip = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
                    GetChild(0).GetChild(0).GetChild(0);

                GameObject grabTarget = new GameObject("SecondHandGrabWeapon_target");
                grabTarget.transform.parent = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0);
                grabTarget.transform.position = new Vector3(-0.141000003f, 0.349000007f, 0.0270000007f);
                grabTarget.transform.eulerAngles = new Vector3(3.25567222f, 170.530273f, 158.903412f);
                grabTarget.transform.localScale = new Vector3(0.0999999791f, 0.0999999791f, 0.0999999866f);

                GameObject hintTarget = new GameObject("SecondHandGrabWeapon_hint");
                hintTarget.transform.parent = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0);
                hintTarget.transform.position = new Vector3(-9.09439087f, 12.3656187f, -7.37642956f);
                hintTarget.transform.eulerAngles = new Vector3(312.55481f, 65.0677643f, 187.059296f);
                hintTarget.transform.localScale = new Vector3(0.0999999791f, 0.0999999791f, 0.0999999866f);

                SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.target = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(5);
                SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.hint = model.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
                    GetChild(0).GetChild(0).GetChild(0).GetChild(6);

                //model.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = pixelMaterial;

                model.transform.position = new Vector3(position.X, position.Y, -1.0f);

                Vector3 eulers = model.transform.rotation.eulerAngles;
                model.transform.rotation = Quaternion.Euler(0, 90, 0);


                // create an animancer object and give it a reference to the Animator component
                GameObject animancerComponentGO = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
                // get the animator component from the game object
                // this component is used by animancer
                AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                animancerComponent.Animator = model.GetComponent<Animator>();
                entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent);
                

                entity.agentPhysicsState.Speed = 10.0f;
                entity.isAgentPlayer = true;
                entity.isECSInput = true;
                entity.AddECSInputXY(new Vec2f(0, 0), false, false);
                if (inventoryID != - 1)
                    entity.AddAgentInventory(inventoryID, equipmentInventoryID, true);

            }
            else if (agentType == Agent.AgentType.Agent)
            {
                entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
                entity.AddAnimationState(1.0f, new Animation.Animation{Type=properties.StartingAnimation});
            }
            else if (agentType == Agent.AgentType.Enemy)
            {
                entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
                entity.AddAnimationState(1.0f, new Animation.Animation{Type=properties.StartingAnimation});
                entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

            }
            else if (agentType == Agent.AgentType.EnemyGunner)
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
                // get the animator component from the game object
                // this component is used by animancer
                AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                animancerComponent.Animator = model.GetComponent<Animator>();
                entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent);
                entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                entity.agentPhysicsState.Speed = 3.0f;

                entity.SetAgentWeapon(Model3DWeapon.Gun);

            }
            else if (agentType == Agent.AgentType.EnemySwordman)
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
                // get the animator component from the game object
                // this component is used by animancer
                AnimancerComponent animancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
                animancerComponent.Animator = model.GetComponent<Animator>();
                entity.AddAgentModel3D(model, leftHand, rightHand, Model3DWeapon.None, null, animancerComponent);
                entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius, 0.0f);

                entity.agentPhysicsState.Speed = 3.0f;

                entity.SetAgentWeapon(Model3DWeapon.Sword);

            }
            
            return entity;
        }

    

    }
}
