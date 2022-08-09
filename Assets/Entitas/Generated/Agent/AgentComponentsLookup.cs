//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class AgentComponentsLookup {

    public const int AgentAIController = 0;
    public const int AgentCorpse = 1;
    public const int AgentEnemy = 2;
    public const int AgentID = 3;
    public const int AgentInventory = 4;
    public const int AgentItemDrop = 5;
    public const int AgentPhysicsState = 6;
    public const int AgentPlayer = 7;
    public const int AgentSprite2D = 8;
    public const int AgentStats = 9;
    public const int AnimationState = 10;
    public const int ECSInput = 11;
    public const int ECSInputXY = 12;
    public const int PhysicsBox2DCollider = 13;
    public const int PhysicsSphere2DCollider = 14;

    public const int TotalComponents = 15;

    public static readonly string[] componentNames = {
        "AgentAIController",
        "AgentCorpse",
        "AgentEnemy",
        "AgentID",
        "AgentInventory",
        "AgentItemDrop",
        "AgentPhysicsState",
        "AgentPlayer",
        "AgentSprite2D",
        "AgentStats",
        "AnimationState",
        "ECSInput",
        "ECSInputXY",
        "PhysicsBox2DCollider",
        "PhysicsSphere2DCollider"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Agent.AIController),
        typeof(Agent.CorpseComponent),
        typeof(Agent.EnemyComponent),
        typeof(Agent.IDComponent),
        typeof(Agent.InventoryComponent),
        typeof(Agent.ItemDropComponent),
        typeof(Agent.PhysicsStateComponent),
        typeof(Agent.PlayerComponent),
        typeof(Agent.Sprite2DComponent),
        typeof(Agent.StatsComponent),
        typeof(Animation.StateComponent),
        typeof(ECSInput.Component),
        typeof(ECSInput.XYComponent),
        typeof(Physics.Box2DColliderComponent),
        typeof(Physics.Sphere2DColliderComponent)
    };
}
