//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class GameComponentsLookup {

    public const int Agent2dInventory = 0;
    public const int InventoryItem = 1;
    public const int Item2DPosition = 2;
    public const int Item = 3;
    public const int ItemMove = 4;
    public const int ItemStack = 5;
    public const int VehicleComponentCollider = 6;
    public const int VehicleComponentDraw = 7;
    public const int GameObject = 8;
    public const int Particle = 9;
    public const int Position = 10;

    public const int TotalComponents = 11;

    public static readonly string[] componentNames = {
        "Agent2dInventory",
        "InventoryItem",
        "Item2DPosition",
        "Item",
        "ItemMove",
        "ItemStack",
        "VehicleComponentCollider",
        "VehicleComponentDraw",
        "GameObject",
        "Particle",
        "Position"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Components.Agent2dInventoryComponent),
        typeof(Components.InventoryItemComponent),
        typeof(Components.Item2DPosition),
        typeof(Components.ItemComponent),
        typeof(Components.ItemMoveComponent),
        typeof(Components.ItemStackComponent),
        typeof(Components.VehicleComponentCollider),
        typeof(Components.VehicleComponentDraw),
        typeof(src.ecs.Game.Particle.ParticleSpawn.GameObjectComponent),
        typeof(src.ecs.Game.Particle.ParticleSpawn.ParticleComponent),
        typeof(src.ecs.Game.Particle.ParticleSpawn.PositionComponent)
    };
}
