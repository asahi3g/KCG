//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Contexts : Entitas.IContexts {

    public static Contexts sharedInstance {
        get {
            if (_sharedInstance == null) {
                _sharedInstance = new Contexts();
            }

            return _sharedInstance;
        }
        set { _sharedInstance = value; }
    }

    static Contexts _sharedInstance;

    public ActionContext action { get; set; }
    public ActionCoolDownContext actionCoolDown { get; set; }
    public ActionPropertiesContext actionProperties { get; set; }
    public AgentContext agent { get; set; }
    public AIContext aI { get; set; }
    public FloatingTextContext floatingText { get; set; }
    public GameContext game { get; set; }
    public InputContext input { get; set; }
    public InventoryContext inventory { get; set; }
    public ItemContext item { get; set; }
    public ItemPropertiesContext itemProperties { get; set; }
    public ParticleContext particle { get; set; }
    public ProjectileContext projectile { get; set; }
    public VehicleContext vehicle { get; set; }

    public Entitas.IContext[] allContexts { get { return new Entitas.IContext [] { action, actionCoolDown, actionProperties, agent, aI, floatingText, game, input, inventory, item, itemProperties, particle, projectile, vehicle }; } }

    public Contexts() {
        action = new ActionContext();
        actionCoolDown = new ActionCoolDownContext();
        actionProperties = new ActionPropertiesContext();
        agent = new AgentContext();
        aI = new AIContext();
        floatingText = new FloatingTextContext();
        game = new GameContext();
        input = new InputContext();
        inventory = new InventoryContext();
        item = new ItemContext();
        itemProperties = new ItemPropertiesContext();
        particle = new ParticleContext();
        projectile = new ProjectileContext();
        vehicle = new VehicleContext();

        var postConstructors = System.Linq.Enumerable.Where(
            GetType().GetMethods(),
            method => System.Attribute.IsDefined(method, typeof(Entitas.CodeGeneration.Attributes.PostConstructorAttribute))
        );

        foreach (var postConstructor in postConstructors) {
            postConstructor.Invoke(this, null);
        }
    }

    public void Reset() {
        var contexts = allContexts;
        for (int i = 0; i < contexts.Length; i++) {
            contexts[i].Reset();
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EntityIndexGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Contexts {

    public const string ActionID = "ActionID";
    public const string ActionTypeID = "ActionTypeID";
    public const string ActionCoolDownTypeID = "ActionCoolDownTypeID";
    public const string ActionCoolDownAgentID = "ActionCoolDownAgentID";
    public const string ActionItem = "ActionItem";
    public const string ActionOwner = "ActionOwner";
    public const string ActionProperty = "ActionProperty";
    public const string ActionPropertyNameTypeName = "ActionPropertyNameTypeName";
    public const string ActionPropertyNameTypeName = "ActionPropertyNameTypeName";
    public const string AgentAIController = "AgentAIController";
    public const string AgentID = "AgentID";
    public const string AIGoal = "AIGoal";
    public const string FloatingTextID = "FloatingTextID";
    public const string InventoryID = "InventoryID";
    public const string ItemAttachedInventory = "ItemAttachedInventory";
    public const string ItemID = "ItemID";
    public const string ItemProperty = "ItemProperty";
    public const string ItemPropertyAction = "ItemPropertyAction";
    public const string ItemType = "ItemType";
    public const string ParticleEmitterID = "ParticleEmitterID";
    public const string ProjectileID = "ProjectileID";
    public const string VehicleID = "VehicleID";

    [Entitas.CodeGeneration.Attributes.PostConstructor]
    public void InitializeEntityIndices() {
        action.AddEntityIndex(new Entitas.PrimaryEntityIndex<ActionEntity, int>(
            ActionID,
            action.GetGroup(ActionMatcher.Action),
            (e, c) => ((Action.Component)c).ID));

        action.AddEntityIndex(new Entitas.EntityIndex<ActionEntity, Enums.ActionType>(
            ActionTypeID,
            action.GetGroup(ActionMatcher.Action),
            (e, c) => ((Action.Component)c).TypeID));

        actionCoolDown.AddEntityIndex(new Entitas.EntityIndex<ActionCoolDownEntity, Enums.ActionType>(
            ActionCoolDownTypeID,
            actionCoolDown.GetGroup(ActionCoolDownMatcher.ActionCoolDown),
            (e, c) => ((Action.CoolDown.Component)c).TypeID));

        actionCoolDown.AddEntityIndex(new Entitas.EntityIndex<ActionCoolDownEntity, int>(
            ActionCoolDownAgentID,
            actionCoolDown.GetGroup(ActionCoolDownMatcher.ActionCoolDown),
            (e, c) => ((Action.CoolDown.Component)c).AgentID));

        action.AddEntityIndex(new Entitas.PrimaryEntityIndex<ActionEntity, int>(
            ActionItem,
            action.GetGroup(ActionMatcher.ActionItem),
            (e, c) => ((Action.ItemComponent)c).ItemID));

        action.AddEntityIndex(new Entitas.EntityIndex<ActionEntity, int>(
            ActionOwner,
            action.GetGroup(ActionMatcher.ActionOwner),
            (e, c) => ((Action.OwnerComponent)c).AgentID));

        actionProperties.AddEntityIndex(new Entitas.PrimaryEntityIndex<ActionPropertiesEntity, Enums.ActionType>(
            ActionProperty,
            actionProperties.GetGroup(ActionPropertiesMatcher.ActionProperty),
            (e, c) => ((Action.Property.Component)c).TypeID));

        actionProperties.AddEntityIndex(new Entitas.PrimaryEntityIndex<ActionPropertiesEntity, string>(
            ActionPropertyNameTypeName,
            actionProperties.GetGroup(ActionPropertiesMatcher.ActionPropertyName),
            (e, c) => ((Action.Property.NameComponent)c).TypeName));
        actionProperties.AddEntityIndex(new Entitas.PrimaryEntityIndex<ActionPropertiesEntity, string>(
            ActionPropertyNameTypeName,
            actionProperties.GetGroup(ActionPropertiesMatcher.ActionPropertyName),
            (e, c) => ((Action.Property.NameComponent)c).TypeName));

        actionProperties.AddEntityIndex(new Entitas.PrimaryEntityIndex<ActionPropertiesEntity, string>(
            ActionPropertyNameTypeName,
            actionProperties.GetGroup(ActionPropertiesMatcher.ActionPropertyName),
            (e, c) => ((Action.Property.NameComponent)c).TypeName));
        actionProperties.AddEntityIndex(new Entitas.PrimaryEntityIndex<ActionPropertiesEntity, string>(
            ActionPropertyNameTypeName,
            actionProperties.GetGroup(ActionPropertiesMatcher.ActionPropertyName),
            (e, c) => ((Action.Property.NameComponent)c).TypeName));

        agent.AddEntityIndex(new Entitas.PrimaryEntityIndex<AgentEntity, int>(
            AgentAIController,
            agent.GetGroup(AgentMatcher.AgentAIController),
            (e, c) => ((Agent.AIController)c).AgentPlannerID));

        agent.AddEntityIndex(new Entitas.PrimaryEntityIndex<AgentEntity, int>(
            AgentID,
            agent.GetGroup(AgentMatcher.AgentID),
            (e, c) => ((Agent.IDComponent)c).ID));

        aI.AddEntityIndex(new Entitas.PrimaryEntityIndex<AIEntity, int>(
            AIGoal,
            aI.GetGroup(AIMatcher.AIGoal),
            (e, c) => ((AI.GoalComponent)c).GoalID));

        floatingText.AddEntityIndex(new Entitas.PrimaryEntityIndex<FloatingTextEntity, int>(
            FloatingTextID,
            floatingText.GetGroup(FloatingTextMatcher.FloatingTextID),
            (e, c) => ((FloatingText.IDComponent)c).Index));

        inventory.AddEntityIndex(new Entitas.PrimaryEntityIndex<InventoryEntity, int>(
            InventoryID,
            inventory.GetGroup(InventoryMatcher.InventoryID),
            (e, c) => ((Inventory.IDComponent)c).ID));

        item.AddEntityIndex(new Entitas.EntityIndex<ItemEntity, int>(
            ItemAttachedInventory,
            item.GetGroup(ItemMatcher.ItemAttachedInventory),
            (e, c) => ((Item.AttachedInventoryComponent)c).InventoryID));

        item.AddEntityIndex(new Entitas.PrimaryEntityIndex<ItemEntity, int>(
            ItemID,
            item.GetGroup(ItemMatcher.ItemID),
            (e, c) => ((Item.IDComponent)c).ID));

        itemProperties.AddEntityIndex(new Entitas.PrimaryEntityIndex<ItemPropertiesEntity, Enums.ItemType>(
            ItemProperty,
            itemProperties.GetGroup(ItemPropertiesMatcher.ItemProperty),
            (e, c) => ((Item.Property.Component)c).ItemType));

        itemProperties.AddEntityIndex(new Entitas.EntityIndex<ItemPropertiesEntity, Enums.ActionType>(
            ItemPropertyAction,
            itemProperties.GetGroup(ItemPropertiesMatcher.ItemPropertyAction),
            (e, c) => ((Item.Property.ActionComponent)c).ActionTypeID));

        item.AddEntityIndex(new Entitas.EntityIndex<ItemEntity, Enums.ItemType>(
            ItemType,
            item.GetGroup(ItemMatcher.ItemType),
            (e, c) => ((Item.TypeComponent)c).Type));

        particle.AddEntityIndex(new Entitas.PrimaryEntityIndex<ParticleEntity, int>(
            ParticleEmitterID,
            particle.GetGroup(ParticleMatcher.ParticleEmitterID),
            (e, c) => ((Particle.EmitterIDComponent)c).ParticleEmitterId));

        projectile.AddEntityIndex(new Entitas.PrimaryEntityIndex<ProjectileEntity, int>(
            ProjectileID,
            projectile.GetGroup(ProjectileMatcher.ProjectileID),
            (e, c) => ((Projectile.IDComponent)c).ID));

        vehicle.AddEntityIndex(new Entitas.PrimaryEntityIndex<VehicleEntity, int>(
            VehicleID,
            vehicle.GetGroup(VehicleMatcher.VehicleID),
            (e, c) => ((Vehicle.IDComponent)c).ID));
    }
}

public static class ContextsExtensions {

    public static ActionEntity GetEntityWithActionID(this ActionContext context, int ID) {
        return ((Entitas.PrimaryEntityIndex<ActionEntity, int>)context.GetEntityIndex(Contexts.ActionID)).GetEntity(ID);
    }

    public static System.Collections.Generic.HashSet<ActionEntity> GetEntitiesWithActionTypeID(this ActionContext context, Enums.ActionType TypeID) {
        return ((Entitas.EntityIndex<ActionEntity, Enums.ActionType>)context.GetEntityIndex(Contexts.ActionTypeID)).GetEntities(TypeID);
    }

    public static System.Collections.Generic.HashSet<ActionCoolDownEntity> GetEntitiesWithActionCoolDownTypeID(this ActionCoolDownContext context, Enums.ActionType TypeID) {
        return ((Entitas.EntityIndex<ActionCoolDownEntity, Enums.ActionType>)context.GetEntityIndex(Contexts.ActionCoolDownTypeID)).GetEntities(TypeID);
    }

    public static System.Collections.Generic.HashSet<ActionCoolDownEntity> GetEntitiesWithActionCoolDownAgentID(this ActionCoolDownContext context, int AgentID) {
        return ((Entitas.EntityIndex<ActionCoolDownEntity, int>)context.GetEntityIndex(Contexts.ActionCoolDownAgentID)).GetEntities(AgentID);
    }

    public static ActionEntity GetEntityWithActionItem(this ActionContext context, int ItemID) {
        return ((Entitas.PrimaryEntityIndex<ActionEntity, int>)context.GetEntityIndex(Contexts.ActionItem)).GetEntity(ItemID);
    }

    public static System.Collections.Generic.HashSet<ActionEntity> GetEntitiesWithActionOwner(this ActionContext context, int AgentID) {
        return ((Entitas.EntityIndex<ActionEntity, int>)context.GetEntityIndex(Contexts.ActionOwner)).GetEntities(AgentID);
    }

    public static ActionPropertiesEntity GetEntityWithActionProperty(this ActionPropertiesContext context, Enums.ActionType TypeID) {
        return ((Entitas.PrimaryEntityIndex<ActionPropertiesEntity, Enums.ActionType>)context.GetEntityIndex(Contexts.ActionProperty)).GetEntity(TypeID);
    }

    public static ActionPropertiesEntity GetEntityWithActionPropertyNameTypeName(this ActionPropertiesContext context, string TypeName) {
        return ((Entitas.PrimaryEntityIndex<ActionPropertiesEntity, string>)context.GetEntityIndex(Contexts.ActionPropertyNameTypeName)).GetEntity(TypeName);
    }

    public static ActionPropertiesEntity GetEntityWithActionPropertyNameTypeName(this ActionPropertiesContext context, string TypeName) {
        return ((Entitas.PrimaryEntityIndex<ActionPropertiesEntity, string>)context.GetEntityIndex(Contexts.ActionPropertyNameTypeName)).GetEntity(TypeName);
    }

    public static ActionPropertiesEntity GetEntityWithActionPropertyNameTypeName(this ActionPropertiesContext context, string TypeName) {
        return ((Entitas.PrimaryEntityIndex<ActionPropertiesEntity, string>)context.GetEntityIndex(Contexts.ActionPropertyNameTypeName)).GetEntity(TypeName);
    }

    public static ActionPropertiesEntity GetEntityWithActionPropertyNameTypeName(this ActionPropertiesContext context, string TypeName) {
        return ((Entitas.PrimaryEntityIndex<ActionPropertiesEntity, string>)context.GetEntityIndex(Contexts.ActionPropertyNameTypeName)).GetEntity(TypeName);
    }

    public static AgentEntity GetEntityWithAgentAIController(this AgentContext context, int AgentPlannerID) {
        return ((Entitas.PrimaryEntityIndex<AgentEntity, int>)context.GetEntityIndex(Contexts.AgentAIController)).GetEntity(AgentPlannerID);
    }

    public static AgentEntity GetEntityWithAgentID(this AgentContext context, int ID) {
        return ((Entitas.PrimaryEntityIndex<AgentEntity, int>)context.GetEntityIndex(Contexts.AgentID)).GetEntity(ID);
    }

    public static AIEntity GetEntityWithAIGoal(this AIContext context, int GoalID) {
        return ((Entitas.PrimaryEntityIndex<AIEntity, int>)context.GetEntityIndex(Contexts.AIGoal)).GetEntity(GoalID);
    }

    public static FloatingTextEntity GetEntityWithFloatingTextID(this FloatingTextContext context, int Index) {
        return ((Entitas.PrimaryEntityIndex<FloatingTextEntity, int>)context.GetEntityIndex(Contexts.FloatingTextID)).GetEntity(Index);
    }

    public static InventoryEntity GetEntityWithInventoryID(this InventoryContext context, int ID) {
        return ((Entitas.PrimaryEntityIndex<InventoryEntity, int>)context.GetEntityIndex(Contexts.InventoryID)).GetEntity(ID);
    }

    public static System.Collections.Generic.HashSet<ItemEntity> GetEntitiesWithItemAttachedInventory(this ItemContext context, int InventoryID) {
        return ((Entitas.EntityIndex<ItemEntity, int>)context.GetEntityIndex(Contexts.ItemAttachedInventory)).GetEntities(InventoryID);
    }

    public static ItemEntity GetEntityWithItemID(this ItemContext context, int ID) {
        return ((Entitas.PrimaryEntityIndex<ItemEntity, int>)context.GetEntityIndex(Contexts.ItemID)).GetEntity(ID);
    }

    public static ItemPropertiesEntity GetEntityWithItemProperty(this ItemPropertiesContext context, Enums.ItemType ItemType) {
        return ((Entitas.PrimaryEntityIndex<ItemPropertiesEntity, Enums.ItemType>)context.GetEntityIndex(Contexts.ItemProperty)).GetEntity(ItemType);
    }

    public static System.Collections.Generic.HashSet<ItemPropertiesEntity> GetEntitiesWithItemPropertyAction(this ItemPropertiesContext context, Enums.ActionType ActionTypeID) {
        return ((Entitas.EntityIndex<ItemPropertiesEntity, Enums.ActionType>)context.GetEntityIndex(Contexts.ItemPropertyAction)).GetEntities(ActionTypeID);
    }

    public static System.Collections.Generic.HashSet<ItemEntity> GetEntitiesWithItemType(this ItemContext context, Enums.ItemType Type) {
        return ((Entitas.EntityIndex<ItemEntity, Enums.ItemType>)context.GetEntityIndex(Contexts.ItemType)).GetEntities(Type);
    }

    public static ParticleEntity GetEntityWithParticleEmitterID(this ParticleContext context, int ParticleEmitterId) {
        return ((Entitas.PrimaryEntityIndex<ParticleEntity, int>)context.GetEntityIndex(Contexts.ParticleEmitterID)).GetEntity(ParticleEmitterId);
    }

    public static ProjectileEntity GetEntityWithProjectileID(this ProjectileContext context, int ID) {
        return ((Entitas.PrimaryEntityIndex<ProjectileEntity, int>)context.GetEntityIndex(Contexts.ProjectileID)).GetEntity(ID);
    }

    public static VehicleEntity GetEntityWithVehicleID(this VehicleContext context, int ID) {
        return ((Entitas.PrimaryEntityIndex<VehicleEntity, int>)context.GetEntityIndex(Contexts.VehicleID)).GetEntity(ID);
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.VisualDebugging.CodeGeneration.Plugins.ContextObserverGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Contexts {

#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)

    [Entitas.CodeGeneration.Attributes.PostConstructor]
    public void InitializeContextObservers() {
        try {
            CreateContextObserver(action);
            CreateContextObserver(actionCoolDown);
            CreateContextObserver(actionProperties);
            CreateContextObserver(agent);
            CreateContextObserver(aI);
            CreateContextObserver(floatingText);
            CreateContextObserver(game);
            CreateContextObserver(input);
            CreateContextObserver(inventory);
            CreateContextObserver(item);
            CreateContextObserver(itemProperties);
            CreateContextObserver(particle);
            CreateContextObserver(projectile);
            CreateContextObserver(vehicle);
        } catch(System.Exception) {
        }
    }

    public void CreateContextObserver(Entitas.IContext context) {
        if (UnityEngine.Application.isPlaying) {
            var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);
            UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
        }
    }

#endif
}
