//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ProjectileEntity {

    static readonly Projectile.FirstFrameComponent projectileFirstFrameComponent = new Projectile.FirstFrameComponent();

    public bool isProjectileFirstFrame {
        get { return HasComponent(ProjectileComponentsLookup.ProjectileFirstFrame); }
        set {
            if (value != isProjectileFirstFrame) {
                var index = ProjectileComponentsLookup.ProjectileFirstFrame;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : projectileFirstFrameComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<ProjectileEntity> _matcherProjectileFirstFrame;

    public static Entitas.IMatcher<ProjectileEntity> ProjectileFirstFrame {
        get {
            if (_matcherProjectileFirstFrame == null) {
                var matcher = (Entitas.Matcher<ProjectileEntity>)Entitas.Matcher<ProjectileEntity>.AllOf(ProjectileComponentsLookup.ProjectileFirstFrame);
                matcher.componentNames = ProjectileComponentsLookup.componentNames;
                _matcherProjectileFirstFrame = matcher;
            }

            return _matcherProjectileFirstFrame;
        }
    }
}