//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class FloatingTextEntity {

    public FloatingText.MovementComponent floatingTextMovement { get { return (FloatingText.MovementComponent)GetComponent(FloatingTextComponentsLookup.FloatingTextMovement); } }
    public bool hasFloatingTextMovement { get { return HasComponent(FloatingTextComponentsLookup.FloatingTextMovement); } }

    public void AddFloatingTextMovement(KMath.Vec2f newVelocity, KMath.Vec2f newPosition) {
        var index = FloatingTextComponentsLookup.FloatingTextMovement;
        var component = (FloatingText.MovementComponent)CreateComponent(index, typeof(FloatingText.MovementComponent));
        component.Velocity = newVelocity;
        component.Position = newPosition;
        AddComponent(index, component);
    }

    public void ReplaceFloatingTextMovement(KMath.Vec2f newVelocity, KMath.Vec2f newPosition) {
        var index = FloatingTextComponentsLookup.FloatingTextMovement;
        var component = (FloatingText.MovementComponent)CreateComponent(index, typeof(FloatingText.MovementComponent));
        component.Velocity = newVelocity;
        component.Position = newPosition;
        ReplaceComponent(index, component);
    }

    public void RemoveFloatingTextMovement() {
        RemoveComponent(FloatingTextComponentsLookup.FloatingTextMovement);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class FloatingTextMatcher {

    static Entitas.IMatcher<FloatingTextEntity> _matcherFloatingTextMovement;

    public static Entitas.IMatcher<FloatingTextEntity> FloatingTextMovement {
        get {
            if (_matcherFloatingTextMovement == null) {
                var matcher = (Entitas.Matcher<FloatingTextEntity>)Entitas.Matcher<FloatingTextEntity>.AllOf(FloatingTextComponentsLookup.FloatingTextMovement);
                matcher.componentNames = FloatingTextComponentsLookup.componentNames;
                _matcherFloatingTextMovement = matcher;
            }

            return _matcherFloatingTextMovement;
        }
    }
}