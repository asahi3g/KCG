// TODO(Brandon): 
// AgentSystem should not be importing GUI (for call back)

using System;
using System.Collections.Generic;
using Action;
using KMath;
using static GameState;
using static Action.ActionBasic;
using static Condition.ConditionBasic;
using Enums;

namespace Agent
{
    public class AgentCreationApi
    {
        // Start is called before the first frame update
        private int CurrentIndex;
        private AgentPropertiesTemplate[] PropertiesArray;

        private Dictionary<string, int> NameToID;

        public AgentCreationApi()
        {
            NameToID = new Dictionary<string, int>();
            PropertiesArray = new AgentPropertiesTemplate[1024];
            for(int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new AgentPropertiesTemplate();
            }
            CurrentIndex = -1;
        }

        public AgentPropertiesTemplate Get(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id];
            }

            return new AgentPropertiesTemplate();
        }

        public MovementProperties GetMovementProperties(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id].MovProperties;
            }

            return new MovementProperties();
        }

        public ref AgentPropertiesTemplate GetRef(int Id)
        {      
            return ref PropertiesArray[Id];
        }
        public AgentPropertiesTemplate Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get(value);
            }

            return new AgentPropertiesTemplate();
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

        public void SetDropTableID(int dropTableID, int inventoryDropTableID)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DropTableID = dropTableID;
                PropertiesArray[CurrentIndex].InventoryDropTableID = inventoryDropTableID;
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

        public void SetAgentModelType(Engine3D.AgentModelType modelType)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].ModelType = modelType;
            }
        }

        public void SetAgentModelScale(Vec3f modelScale)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].ModelScale = modelScale;
            }
        }

    /*    public void SetDetectionRadius(float detectionRadius)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DetectionRadius = detectionRadius;
            }
        }*/

        public void SetDefaultHealth(int defaultHealth)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DefaultHealth = defaultHealth;
            }
        }

        public void SetBasicAttack(in BasicAttack attack)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Attack = attack;
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

        public void SetStaggerAffectTime(float time)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StaggerAffectTime = time;
            }
        }

        public void End()
        {
            CurrentIndex = -1;
        }

        public void InitializeResources()
        {
            RegisterConditions();
            RegisterBasicActions();
            int marineBehavior = CreateMarineBehavior();
            int insectBehavior = CreateInsectBehavior();

            int dropID = LootTableCreationAPI.Create();
            LootTableCreationAPI.AddItem(ItemType.Slime, 1);
            LootTableCreationAPI.AddItem(ItemType.Slime, 1);
            LootTableCreationAPI.SetEntry(1, 30);
            LootTableCreationAPI.AddItem(ItemType.Food, 4);
            LootTableCreationAPI.SetEntry(1, 25);
            LootTableCreationAPI.SetEntry(2, 40);
            LootTableCreationAPI.SetEntry(3, 25);
            LootTableCreationAPI.SetEntry(4, 5);
            LootTableCreationAPI.AddItem(ItemType.Bone, 4);
            LootTableCreationAPI.SetEntry(3, 50);
            LootTableCreationAPI.SetEntry(4, 25);
            LootTableCreationAPI.SetEntry(5, 15);
            LootTableCreationAPI.SetEntry(6, 10);
            LootTableCreationAPI.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.Player);
            GameState.AgentCreationApi.SetName("player");
            GameState.AgentCreationApi.SetMovement(10f, 3.5f, 2);
            GameState.AgentCreationApi.SetDefaultHealth(30000);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.35f, 0.0f), new Vec2f(0.75f, 2.6f));
            GameState.AgentCreationApi.SetAgentModelType(Engine3D.AgentModelType.Humanoid);
            GameState.AgentCreationApi.SetAgentAnimationType(Enums.AgentAnimationType.SpaceMarineAnimations);
            GameState.AgentCreationApi.SetAgentModelScale(new Vec3f(3.0f, 3.0f, 3.0f));        
            GameState.AgentCreationApi.SetStaggerAffectTime(0.5f);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.Agent);
            GameState.AgentCreationApi.SetName("agent");
            GameState.AgentCreationApi.SetMovement(5f, 3.5f, 1);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.25f, 0.0f), new Vec2f(0.5f, 1.5f));
            GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.CharacterMoveLeft);
            GameState.AgentCreationApi.SetAgentModelType(Engine3D.AgentModelType.Humanoid);
            GameState.AgentCreationApi.SetAgentAnimationType(Enums.AgentAnimationType.SpaceMarineAnimations);
            GameState.AgentCreationApi.SetAgentModelScale(new Vec3f(3.0f, 3.0f, 3.0f));
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.Slime);
            GameState.AgentCreationApi.SetName("Slime");
            GameState.AgentCreationApi.SetMovement(5f, 3.5f, 1);
            GameState.AgentCreationApi.SetDropTableID(dropID, dropID);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.125f, 0.0f), new Vec2f(0.75f, 0.5f));
            GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.SlimeMoveLeft);
            GameState.AgentCreationApi.SetDefaultHealth(100);
            // Windup is zero because attack is not a melee.
            GameState.AgentCreationApi.SetBasicAttack( new BasicAttack() { CoolDown = 0.8f, Demage = 20, Range = 1.0f, Windup = 0.0f });
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.FlyingSlime);
            GameState.AgentCreationApi.SetName("Flying Slime");
            GameState.AgentCreationApi.SetFlyingMovement(3.0f);
            GameState.AgentCreationApi.SetDropTableID(dropID, dropID);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.125f, 0.0f), new Vec2f(0.75f, 0.5f));
            GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.SlimeMoveLeft);
            GameState.AgentCreationApi.SetDefaultHealth(100);
            GameState.AgentCreationApi.SetBasicAttack(new BasicAttack() { CoolDown = 0.8f, Demage = 20, Range = 1.0f, Windup = 0.0f });
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemySwordman);
            GameState.AgentCreationApi.SetName("enemy-swordman");
            GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(dropID, dropID);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
            GameState.AgentCreationApi.SetBasicAttack(new BasicAttack() { CoolDown = 0.8f, Demage = 20, Range = 1.0f, Windup = 0.0f });
            GameState.AgentCreationApi.SetDefaultHealth(100);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyGunner);
            GameState.AgentCreationApi.SetName("enemy-gunner");
            GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(dropID, dropID);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.5f, 2.5f));
            GameState.AgentCreationApi.SetBehaviorTree(marineBehavior);
            GameState.AgentCreationApi.SetDefaultHealth(100);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.InsectSmall);
            GameState.AgentCreationApi.SetName("enemy-insect");
            GameState.AgentCreationApi.SetMovement(6f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(dropID, dropID);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(1.25f, 1.0f));
            GameState.AgentCreationApi.SetBasicAttack(new BasicAttack() { CoolDown = 0.8f, Demage = 20, Range = 1.0f, Windup = 1.0f });
            GameState.AgentCreationApi.SetBehaviorTree(insectBehavior);
            GameState.AgentCreationApi.SetDefaultHealth(100);
            GameState.AgentCreationApi.SetAgentModelType(Engine3D.AgentModelType.InsectSmall);
            GameState.AgentCreationApi.SetAgentAnimationType(Enums.AgentAnimationType.GroundInsectAnimation);
            GameState.AgentCreationApi.SetAgentModelScale(new Vec3f(1.0f, 1.0f, 1.0f));
            GameState.AgentCreationApi.End();


            GameState.AgentCreationApi.Create((int)Enums.AgentType.InsectLarge);
            GameState.AgentCreationApi.SetName("enemy-insect-heavy");
            GameState.AgentCreationApi.SetMovement(4.0f, 3.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(dropID, dropID);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.5f, 0.0f), new Vec2f(1.25f, 2.5f));
            GameState.AgentCreationApi.SetBasicAttack(new BasicAttack() { CoolDown = 0.8f, Demage = 20, Range = 1.5f, Windup = 1.0f });
            GameState.AgentCreationApi.SetDefaultHealth(100);
            GameState.AgentCreationApi.SetAgentModelType(Engine3D.AgentModelType.InsectHeavy);
            GameState.AgentCreationApi.SetAgentAnimationType(Enums.AgentAnimationType.GroundInsectHeavyAnimation);
            GameState.AgentCreationApi.SetAgentModelScale(new Vec3f(1.6f, 1.6f, 1.6f));
            GameState.AgentCreationApi.SetBehaviorTree(insectBehavior);
            GameState.AgentCreationApi.End();

            GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyMarine);
            GameState.AgentCreationApi.SetName("Marine");
            GameState.AgentCreationApi.SetMovement(3f, 4.5f, 2);
            GameState.AgentCreationApi.SetDropTableID(dropID, dropID);
            GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.35f, 0.0f), new Vec2f(0.75f, 2.6f));
            GameState.AgentCreationApi.SetBehaviorTree(marineBehavior);
            GameState.AgentCreationApi.SetBasicAttack(new BasicAttack() { CoolDown = 0.8f, Demage = 20, Range = 1.5f, Windup = 2.0f });
            GameState.AgentCreationApi.SetDefaultHealth(100);
            GameState.AgentCreationApi.SetStaggerAffectTime(0.5f);
            GameState.AgentCreationApi.End();
        }

        int CreateMarineBehavior()
        {
            // Node always returns success.
            int successNodeID = NodeManager.CreateNode("SuccessNode", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(NodeSystem.ActionManager.SuccessActionID);
            NodeManager.EndNode();

            int wait0_1sId = NodeManager.CreateNode("Wait", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("Wait"));
            NodeManager.SetData(new WaitAction.WaitActionData(0.5f));
            NodeManager.EndNode();

            int wait1sId = NodeManager.CreateNode("Wait", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("Wait"));
            NodeManager.SetData(new WaitAction.WaitActionData(0f));
            NodeManager.EndNode();

            int selectTargetId = NodeManager.CreateNode("SelectClosestTarget", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("SelectClosestTarget"));
            NodeManager.EndNode();

            int moveToId = NodeManager.CreateNode("MoveToDir", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("MoveDirectlyToward"));
            NodeManager.SetData(ConditionManager.GetID("CanSeeAndInRange"));
            NodeManager.EndNode();

            int moveToBestScoreId = NodeManager.CreateNode("MoveToDir", NodeSystem.ItemUsageActionType.Action);
            NodeManager.SetAction(ActionManager.GetID("MoveToBestScorePos"));
            NodeManager.EndNode();

            int aimAtId = NodeManager.CreateNode("AimAt", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("AimAt"));
            NodeManager.EndNode();

            int reloadWeaponId = NodeManager.CreateNode("ReloadWeapon", NodeSystem.ItemUsageActionType .ActionSequence);
            NodeManager.SetAction(ActionManager.GetID("ReloadWeapon"));
            NodeManager.EndNode();

            int fireWeaponId = NodeManager.CreateNode("FireWeapon", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("FireWeapon"));
            NodeManager.SetData(new ShootFireWeaponAction.ShootFireWeaponData());
            NodeManager.EndNode();

            int sequenceId = NodeManager.CreateNode("Sequence", NodeSystem.ItemUsageActionType .Sequence);
            NodeManager.SetCondition(ConditionManager.GetID("HasBulletInClip"));
            NodeManager.AddChild(selectTargetId);
            NodeManager.AddChild(moveToId);
            NodeManager.AddChild(moveToBestScoreId);
            NodeManager.AddChild(aimAtId);
            NodeManager.AddChild(fireWeaponId);
            NodeManager.AddChild(wait0_1sId);
            NodeManager.EndNode();

            int selectorShootId = NodeManager.CreateNode("SelectorShoot", NodeSystem.ItemUsageActionType .Selector);
            NodeManager.SetCondition(ConditionManager.GetID("HasEnemyAlive"));
            NodeManager.AddChild(sequenceId);
            NodeManager.AddChild(reloadWeaponId);
            NodeManager.EndNode();

            int selectorStateId = NodeManager.CreateNode("SelectorState", NodeSystem.ItemUsageActionType .Selector);
            NodeManager.AddChild(selectorShootId);
            NodeManager.AddChild(wait1sId);
            NodeManager.EndNode();

            int repeaterId = NodeManager.CreateNode("Repeater", NodeSystem.ItemUsageActionType .Repeater);
            NodeManager.AddChild(selectorStateId);
            NodeManager.EndNode();

            int rootId = NodeManager.CreateNode("MarineBehavior", NodeSystem.ItemUsageActionType .Decorator);
            NodeManager.AddChild(repeaterId);
            NodeManager.EndNode();

            return rootId;
        }

        int CreateInsectBehavior()
        {
            int waitId = NodeManager.CreateNode("Wait", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("Wait"));
            NodeManager.SetData(new WaitAction.WaitActionData(0.5f));
            NodeManager.EndNode();

            int selectTargetId = NodeManager.CreateNode("SelectClosestTarget", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("SelectClosestTarget"));
            NodeManager.EndNode();

            int moveToId = NodeManager.CreateNode("MoveToDir", NodeSystem.ItemUsageActionType .Action);
            NodeManager.SetAction(ActionManager.GetID("MoveDirectlyToward"));
            NodeManager.SetData(ConditionManager.GetID("ItIsOnTheNextTile"));
            NodeManager.EndNode();

            int meleeId = NodeManager.CreateNode("MeleeAtack", NodeSystem.ItemUsageActionType .ActionSequence);
            NodeManager.SetAction(ActionManager.GetID("MeleeAtack"));
            NodeManager.EndNode();

            int sequenceId = NodeManager.CreateNode("Sequence", NodeSystem.ItemUsageActionType .Sequence);
            NodeManager.SetCondition(ConditionManager.GetID("HasEnemyAlive"));
            NodeManager.AddChild(selectTargetId);
            NodeManager.AddChild(moveToId);
            NodeManager.AddChild(meleeId);
            NodeManager.AddChild(waitId);
            NodeManager.EndNode();

            int selectorStateId = NodeManager.CreateNode("SelectorState", NodeSystem.ItemUsageActionType .Selector);
            NodeManager.AddChild(sequenceId);
            NodeManager.AddChild(waitId);
            NodeManager.EndNode();

            int repeaterId = NodeManager.CreateNode("Repeater", NodeSystem.ItemUsageActionType .Repeater);
            NodeManager.AddChild(selectorStateId);
            NodeManager.EndNode();

            int rootId = NodeManager.CreateNode("InsectBehavior", NodeSystem.ItemUsageActionType .Decorator);
            NodeManager.AddChild(repeaterId);
            NodeManager.EndNode();

            return rootId;
        }
    } 
}
