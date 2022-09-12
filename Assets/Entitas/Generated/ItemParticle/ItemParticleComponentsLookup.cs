//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class ItemParticleComponentsLookup {

    public const int ItemDrawPosition2D = 0;
    public const int ItemFireWeaponCharge = 1;
    public const int ItemFireWeaponClip = 2;
    public const int ItemID = 3;
    public const int ItemLabel = 4;
    public const int ItemPhysicsState = 5;
    public const int ItemPulseWeaponPulse = 6;
    public const int ItemStack = 7;
    public const int ItemType = 8;
    public const int ItemUnpickable = 9;
    public const int PhysicsBox2DCollider = 10;
    public const int PhysicsSphere2DCollider = 11;

    public const int TotalComponents = 12;

    public static readonly string[] componentNames = {
        "ItemDrawPosition2D",
        "ItemFireWeaponCharge",
        "ItemFireWeaponClip",
        "ItemID",
        "ItemLabel",
        "ItemPhysicsState",
        "ItemPulseWeaponPulse",
        "ItemStack",
        "ItemType",
        "ItemUnpickable",
        "PhysicsBox2DCollider",
        "PhysicsSphere2DCollider"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Item.DrawPosition2DComponent),
        typeof(Item.FireWeapon.ChargeComponent),
        typeof(Item.FireWeapon.ClipComponent),
        typeof(Item.IDComponent),
        typeof(Item.LabelComponent),
        typeof(Item.PhysicsStateComponent),
        typeof(Item.PulseWeapon.PulseComponent),
        typeof(Item.StackComponent),
        typeof(Item.TypeComponent),
        typeof(Item.Unpickable),
        typeof(Physics.Box2DColliderComponent),
        typeof(Physics.Sphere2DColliderComponent)
    };
}
