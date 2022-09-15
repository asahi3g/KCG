using System;
using System.Collections.Generic;
using KMath;


namespace Agent
{
    public class AgentCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private AgentProperties[] PropertiesArray;

        private Dictionary<string, int> NameToID;

        public AgentCreationApi()
        {
            NameToID = new Dictionary<string, int>();
            PropertiesArray = new AgentProperties[1024];
            for(int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new AgentProperties();
            }
            CurrentIndex = -1;
        }

        public AgentProperties Get(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id];
            }

            return new AgentProperties();
        }

        public MovementProperties GetMovementProperties(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id].MovProperties;
            }

            return new MovementProperties();
        }

        public ref AgentProperties GetRef(int Id)
        {      
            return ref PropertiesArray[Id];
        }
        public AgentProperties Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get(value);
            }

            return new AgentProperties();
        }
        public void Create(int Id)
        {
            while (Id >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length * 2);
            }

            CurrentIndex = Id;
            if (CurrentIndex != -1)
            {
                PropertiesArray[CurrentIndex].PropertiesId = CurrentIndex;
            }
        }

        public void SetName(string name)
        {
            if (CurrentIndex == -1) return;
            
            if (!NameToID.ContainsKey(name))
            {
                NameToID.Add(name, CurrentIndex);
            }

            PropertiesArray[CurrentIndex].Name = name;
        }

        public void SetMovement(float defaultSpeed, float jumHegiht, int numOfJumps)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MovProperties.DefaultSpeed = defaultSpeed;
                PropertiesArray[CurrentIndex].MovProperties.JumpHeight = jumHegiht;
                PropertiesArray[CurrentIndex].MovProperties.MaxNumOfJumps = jumHegiht;
                PropertiesArray[CurrentIndex].MovProperties.MovType = Enums.AgentMovementType.DefaultMovement;
            }
        }

        public void SetFlyingMovement(float defaultSpeed)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MovProperties.DefaultSpeed = defaultSpeed;
                PropertiesArray[CurrentIndex].MovProperties.MovType = Enums.AgentMovementType.FlyingMovemnt;
            }
        }

        public void SetDropTableID(Enums.LootTableType dropTableID, Enums.LootTableType inventoryDropTableID)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DropTableID = dropTableID;
                PropertiesArray[CurrentIndex].InventoryDropTableID = inventoryDropTableID;
            }
        }

        public void SetSpriteSize(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].SpriteSize = size;
            }
        }

        public void SetStartingAnimation(int startingAnimation)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingAnimation = startingAnimation;
            }
        }

        public void SetEnemyBehaviour(EnemyBehaviour enemyBehaviour)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].EnemyBehaviour = enemyBehaviour;
            }
        }

        public void SetAgentAnimationType(Enums.AgentAnimationType agentAnimationType)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].AnimationType = agentAnimationType;
            }
        }

        public void SetDetectionRadius(float detectionRadius)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DetectionRadius = detectionRadius;
            }
        }

        public void SetHealth(float health)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Health = health;
            }
        }

        public void SetAttackCooldown(float attackCooldown)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].AttackCooldown = attackCooldown;
            }
        }

        public void SetCollisionBox(Vec2f offset, Vec2f dimensions)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].CollisionOffset = offset;
                PropertiesArray[CurrentIndex].CollisionDimensions = dimensions;
            }
        }

        public void End()
        {
            CurrentIndex = -1;
        }



        public void InitializeResources()
        {


            GameState.AgentCreationApi.Create((int)Enums.AgentType.Player);
            GameState.AgentCreationApi.SetName("player");
            GameState.AgentCreationApi.SetMovement(10f, 3.5f, 2);
            GameState.AgentCreationApi.SetHealth(300.0f);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.35f, 0.0f), new Vec2f(0.75f, 2.8f));
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.Agent);
            GameState.AgentCreationApi.SetName("agent");
            GameState.AgentCreationApi.SetMovement(5f, 3.5f, 1);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.25f, 0.0f), new Vec2f(0.5f, 1.5f));
            GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.CharacterMoveLeft);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.Slime);
            GameState.AgentCreationApi.SetName("Slime");
            GameState.AgentCreationApi.SetMovement(5f, 3.5f, 1);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.125f, 0.0f), new Vec2f(0.75f, 0.5f));
            GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.SlimeMoveLeft);
            GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Slime);
            GameState.AgentCreationApi.SetDetectionRadius(4.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.SetAttackCooldown(0.8f);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.FlyingSlime);
            GameState.AgentCreationApi.SetName("Flying Slime");
            GameState.AgentCreationApi.SetFlyingMovement(3.0f);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.125f, 0.0f), new Vec2f(0.75f, 0.5f));
            GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.SlimeMoveLeft);
            GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Slime);
            GameState.AgentCreationApi.SetDetectionRadius(4.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.SetAttackCooldown(0.8f);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemySwordman);
            GameState.AgentCreationApi.SetName("enemy-swordman");
            GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
            GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Swordman);
            GameState.AgentCreationApi.SetDetectionRadius(16.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyGunner);
            GameState.AgentCreationApi.SetName("enemy-gunner");
            GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
            GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Gunner);
            GameState.AgentCreationApi.SetDetectionRadius(24.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyInsect);
            GameState.AgentCreationApi.SetName("enemy-insect");
            GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
            GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Insect);
            GameState.AgentCreationApi.SetDetectionRadius(16.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();


            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyHeavy);
            GameState.AgentCreationApi.SetName("enemy-insect-heavy");
            GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
            GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Insect);
            GameState.AgentCreationApi.SetDetectionRadius(16.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();
        }
    }

}
