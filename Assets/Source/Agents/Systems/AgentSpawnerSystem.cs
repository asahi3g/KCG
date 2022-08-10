using System.Collections.Generic;
using KMath;
using UnityEngine;
using Animancer;

namespace Agent
{
    public class AgentSpawnerSystem
    {

        AgentCreationApi AgentCreationApi;
        public AgentSpawnerSystem(AgentCreationApi agentCreationApi)
        {
            AgentCreationApi = agentCreationApi;
        }

        public AgentEntity SpawnPlayer(Contexts entitasContext, int spriteId, int width, int height, Vec2f position,
        int agentId, int startingAnimation, int playerHealth, int playerFood, int playerWater, int playerOxygen, 
        int playerFuel, float attackCoolDown, int inventoryID = -1, int equipmentInventoryID = -1)
        {
            var entity = entitasContext.agent.CreateEntity();

            var spriteSize = new Vec2f(width / 32f, height / 32f);

            entity.isAgentPlayer = true;
            entity.isECSInput = true;
            entity.AddECSInputXY(new Vec2f(0, 0), false, false);
            entity.AddAgentID(agentId);
            entity.AddAnimationState(1.0f, new Animation.Animation{Type=startingAnimation});
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
            entity.AddAgentPhysicsState(position, newPreviousPosition: default,
                newSpeed: 10f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero, 1,
                Enums.AgentMovementState.None, true, false, false, false, false, 0, 0, 0, 0);
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


        public AgentEntity SpawnCorpse(Contexts entitasContext, Vec2f position, int agentId, int spriteId, AgentType agentType, 
            int inventoryID)
        {
            var entity = entitasContext.agent.CreateEntity();
            ref Agent.AgentProperties properties = ref AgentCreationApi.GetRef((int)agentType);
            var spriteSize = properties.SpriteSize;

            entity.AddAgentID(agentId); // agent id 
            entity.isAgentCorpse = true;
            entity.AddPhysicsBox2DCollider(properties.CollisionDimensions, properties.CollisionOffset);
            entity.AddAgentPhysicsState(position, newPreviousPosition: default, 
                newSpeed: 1f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero, 1, 
                Enums.AgentMovementState.None,
                true, false, false, false, false, 0, 0, 0, 0); // used for physics simulation
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity

            entity.AddAgentInventory(inventoryID, -1, false);

            return entity;
        }

        public AgentEntity Spawn(Contexts entitasContext, Vec2f position, int agentId, AgentType agentType, 
            int inventoryID = -1, int equipmentInventoryID = -1)
        {
            var entity = entitasContext.agent.CreateEntity();

            ref Agent.AgentProperties properties = ref AgentCreationApi.GetRef((int)agentType);

            var spriteSize = properties.SpriteSize;
            var spriteId = 0;
            entity.AddAgentID(agentId); // agent id 
            entity.AddPhysicsBox2DCollider(properties.CollisionDimensions, properties.CollisionOffset);
            entity.AddAgentPhysicsState(position, newPreviousPosition: default,
                newSpeed: 2.5f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero, 1,
                 Enums.AgentMovementState.None, true, false, false, false, false, 0, 0, 0, 0); // used for physics simulation
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
                entity.AddAgentEnemy(properties.EnemyBehaviour, properties.DetectionRadius);

                Enums.ItemType[] Drops = new Enums.ItemType[3];
                int[] MaxDropCount = new int[3];
                float[] DropRate = new float[3];

                Drops[0] = Enums.ItemType.Slime;
                Drops[1] = Enums.ItemType.Food;
                Drops[2] = Enums.ItemType.Bone;

                MaxDropCount[0] = 1;
                MaxDropCount[1] = 3;
                MaxDropCount[2] = 6;

                DropRate[0] = 0.3f;
                DropRate[1] = 0.6f;
                DropRate[2] = 0.8f;

                entity.AddAgentItemDrop(Drops, MaxDropCount, DropRate);
            }
            
            return entity;
        }

    

    }
}
