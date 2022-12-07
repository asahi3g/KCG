//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ProjectileEntity {

    public Projectile.PhysicsStateComponent projectilePhysicsState { get { return (Projectile.PhysicsStateComponent)GetComponent(ProjectileComponentsLookup.ProjectilePhysicsState); } }
    public bool hasProjectilePhysicsState { get { return HasComponent(ProjectileComponentsLookup.ProjectilePhysicsState); } }

    public void AddProjectilePhysicsState(KMath.Vec2f newPosition, KMath.Vec2f newPreviousPosition, float newRotation, KMath.Vec2f newVelocity, KMath.Vec2f newAcceleration, bool newOnGrounded, float newTimeToLive, int newFramesToLive) {
        var index = ProjectileComponentsLookup.ProjectilePhysicsState;
        var component = (Projectile.PhysicsStateComponent)CreateComponent(index, typeof(Projectile.PhysicsStateComponent));
        component.Position = newPosition;
        component.PreviousPosition = newPreviousPosition;
        component.Rotation = newRotation;
        component.Velocity = newVelocity;
        component.Acceleration = newAcceleration;
        component.OnGrounded = newOnGrounded;
        component.TimeToLive = newTimeToLive;
        component.FramesToLive = newFramesToLive;
        AddComponent(index, component);
    }

    public void ReplaceProjectilePhysicsState(KMath.Vec2f newPosition, KMath.Vec2f newPreviousPosition, float newRotation, KMath.Vec2f newVelocity, KMath.Vec2f newAcceleration, bool newOnGrounded, float newTimeToLive, int newFramesToLive) {
        var index = ProjectileComponentsLookup.ProjectilePhysicsState;
        var component = (Projectile.PhysicsStateComponent)CreateComponent(index, typeof(Projectile.PhysicsStateComponent));
        component.Position = newPosition;
        component.PreviousPosition = newPreviousPosition;
        component.Rotation = newRotation;
        component.Velocity = newVelocity;
        component.Acceleration = newAcceleration;
        component.OnGrounded = newOnGrounded;
        component.TimeToLive = newTimeToLive;
        component.FramesToLive = newFramesToLive;
        ReplaceComponent(index, component);
    }

    public void RemoveProjectilePhysicsState() {
        RemoveComponent(ProjectileComponentsLookup.ProjectilePhysicsState);
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
public sealed partial class ProjectileMatcher {

    static Entitas.IMatcher<ProjectileEntity> _matcherProjectilePhysicsState;

    public static Entitas.IMatcher<ProjectileEntity> ProjectilePhysicsState {
        get {
            if (_matcherProjectilePhysicsState == null) {
                var matcher = (Entitas.Matcher<ProjectileEntity>)Entitas.Matcher<ProjectileEntity>.AllOf(ProjectileComponentsLookup.ProjectilePhysicsState);
                matcher.componentNames = ProjectileComponentsLookup.componentNames;
                _matcherProjectilePhysicsState = matcher;
            }

            return _matcherProjectilePhysicsState;
        }
    }
}
