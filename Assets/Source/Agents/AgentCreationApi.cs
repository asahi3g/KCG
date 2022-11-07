//imports UnityEngine

using System;
using System.Collections.Generic;
using Action;
using KMath;
using static GameState;
using static Action.ActionBasic;
using static Condition.ConditionBasic;
using UnityEditor.Tilemaps;


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

        public void SetBehaviorTree(int rootId)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].BehaviorTreeRootID = rootId;
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
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.35f, 0.0f), new Vec2f(0.75f, 2.6f));
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
            GameState.AgentCreationApi.SetDetectionRadius(16.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyGunner);
            GameState.AgentCreationApi.SetName("enemy-gunner");
            GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.5f, 2.5f));
            GameState.AgentCreationApi.SetDetectionRadius(24.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyInsect);
            GameState.AgentCreationApi.SetName("enemy-insect");
            GameState.AgentCreationApi.SetMovement(6f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(1.25f, 1.0f));
            GameState.AgentCreationApi.SetDetectionRadius(16.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();


            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyHeavy);
            GameState.AgentCreationApi.SetName("enemy-insect-heavy");
            GameState.AgentCreationApi.SetMovement(4.0f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.5f, 0.0f), new Vec2f(1.25f, 2.5f));
            GameState.AgentCreationApi.SetDetectionRadius(16.0f);
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyMarine);
            GameState.AgentCreationApi.SetName("Marine");
            GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
            GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.35f, 0.0f), new Vec2f(0.75f, 2.1f));
            GameState.AgentCreationApi.SetBehaviorTree(CreateMarineBehavior());
            GameState.AgentCreationApi.SetHealth(100.0f);
            GameState.AgentCreationApi.End();
        }

        int CreateMarineBehavior()
        {
            RegisterConditions();
            RegisterBasicActions();

            // Node always returns success.
            int successNodeID = NodeManager.CreateNode("SuccessNode", NodeSystem.NodeType.Action);
            NodeManager.SetAction(NodeSystem.ActionManager.SuccessActionID);
            NodeManager.EndNode();

            int wait0_1sId = NodeManager.CreateNode("Wait", NodeSystem.NodeType.Action);
            NodeManager.SetAction(ActionManager.GetID("Wait"));
            NodeManager.SetData(new WaitAction.WaitActionData(0.1f));
            NodeManager.EndNode();

            int wait1sId = NodeManager.CreateNode("Wait", NodeSystem.NodeType.Action);
            NodeManager.SetAction(ActionManager.GetID("Wait"));
            NodeManager.SetData(new WaitAction.WaitActionData(1f));
            NodeManager.EndNode();

            int selectTargetId = NodeManager.CreateNode("SelectClosestTarget", NodeSystem.NodeType.Action);
            NodeManager.SetAction(ActionManager.GetID("SelectClosestTarget"));
            NodeManager.EndNode();

            int moveTo = NodeManager.CreateNode("MoveToDir", NodeSystem.NodeType.Action);
            NodeManager.SetAction(ActionManager.GetID("MoveDirectlyToward"));
            NodeManager.SetData(-1);
            NodeManager.EndNode();

            int aimAtId = NodeManager.CreateNode("AimAt", NodeSystem.NodeType.Action);
            NodeManager.SetAction(ActionManager.GetID("AimAt"));
            NodeManager.EndNode();

            int reloadWeaponId = NodeManager.CreateNode("ReloadWeapon", NodeSystem.NodeType.ActionSequence);
            NodeManager.SetAction(ActionManager.GetID("ReloadWeapon"));
            NodeManager.EndNode();

            int fireWeaponId = NodeManager.CreateNode("FireWeapon", NodeSystem.NodeType.ActionSequence);
            NodeManager.SetAction(ActionManager.GetID("FireWeapon"));
            NodeManager.AddData(new ShootFireWeaponAction.ShootFireWeaponData());
            NodeManager.EndNode();

            int sequenceId = NodeManager.CreateNode("Sequence", NodeSystem.NodeType.Sequence);
            NodeManager.SetCondition(ConditionManager.GetID("HasBulletInClip"));
            NodeManager.AddChild(selectTargetId);
            NodeManager.AddChild(moveTo);
            NodeManager.AddChild(aimAtId);
            NodeManager.AddChild(fireWeaponId);
            NodeManager.AddChild(wait0_1sId);
            NodeManager.EndNode();

            int selectorShootId = NodeManager.CreateNode("SelectorShoot", NodeSystem.NodeType.Selector);
            NodeManager.SetCondition(ConditionManager.GetID("HasEnemyAlive"));
            NodeManager.AddChild(sequenceId);
            NodeManager.AddChild(reloadWeaponId);
            NodeManager.EndNode();

            int selectorStateId = NodeManager.CreateNode("SelectorState", NodeSystem.NodeType.Selector);
            NodeManager.AddChild(selectorShootId);
            NodeManager.AddChild(wait1sId);
            NodeManager.EndNode();

            int repeaterId = NodeManager.CreateNode("Repeater", NodeSystem.NodeType.Repeater);
            NodeManager.AddChild(selectorStateId);
            NodeManager.EndNode();

            int rootId = NodeManager.CreateNode("MarineBehavior", NodeSystem.NodeType.Decorator);
            NodeManager.AddChild(repeaterId);
            NodeManager.EndNode();

            return rootId;
        }

        int CreateInsectBehavior()
        {
            int waitId = NodeManager.CreateNode("Wait", NodeSystem.NodeType.Action);
            NodeManager.SetAction(ActionManager.GetID("Wait"));
            NodeManager.SetData(new WaitAction.WaitActionData(0.5f));
            NodeManager.EndNode();

            int moveTo = NodeManager.CreateNode("MoveToDir", NodeSystem.NodeType.Action);
            NodeManager.SetAction(ActionManager.GetID("MoveDirectlyToward"));
            NodeManager.SetData(-1);
            NodeManager.EndNode();

            int melee = NodeManager.CreateNode("Melee", NodeSystem.NodeType.Action);
            NodeManager.SetAction(ActionManager.GetID("Melee"));
            NodeManager.SetData(-1);
            NodeManager.EndNode();

            int sequenceId = NodeManager.CreateNode("Sequence", NodeSystem.NodeType.Sequence);
            NodeManager.AddChild(waitId);
            NodeManager.AddChild(moveTo);
            NodeManager.AddChild(melee);
            NodeManager.EndNode();

            int repeaterId = NodeManager.CreateNode("Repeater", NodeSystem.NodeType.Repeater);
            NodeManager.AddChild(sequenceId);
            NodeManager.EndNode();

            int rootId = NodeManager.CreateNode("MarineBehavior", NodeSystem.NodeType.Decorator);
            NodeManager.AddChild(repeaterId);
            NodeManager.EndNode();

            return rootId;
        }
    }

}
