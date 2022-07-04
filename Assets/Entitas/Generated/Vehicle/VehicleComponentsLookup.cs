//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class VehicleComponentsLookup {

    public const int ECSInput = 0;
    public const int PhysicsBox2DCollider = 1;
    public const int PhysicsSphere2DCollider = 2;
    public const int VehicleID = 3;
    public const int VehiclePhysicsState2D = 4;
    public const int VehicleSprite2D = 5;

    public const int TotalComponents = 6;

    public static readonly string[] componentNames = {
        "ECSInput",
        "PhysicsBox2DCollider",
        "PhysicsSphere2DCollider",
        "VehicleID",
        "VehiclePhysicsState2D",
        "VehicleSprite2D"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(ECSInput.Component),
        typeof(Physics.Box2DColliderComponent),
        typeof(Physics.Sphere2DColliderComponent),
        typeof(Vehicle.IDComponent),
        typeof(Vehicle.PhysicsState2DComponent),
        typeof(Vehicle.Sprite2DComponent)
    };
}
