//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ProjectileEntity {

    static readonly Projectile.DeleteComponent projectileDeleteComponent = new Projectile.DeleteComponent();

    public bool isProjectileDelete {
        get { return HasComponent(ProjectileComponentsLookup.ProjectileDelete); }
        set {
            if (value != isProjectileDelete) {
                var index = ProjectileComponentsLookup.ProjectileDelete;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : projectileDeleteComponent;

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

    static Entitas.IMatcher<ProjectileEntity> _matcherProjectileDelete;

    public static Entitas.IMatcher<ProjectileEntity> ProjectileDelete {
        get {
            if (_matcherProjectileDelete == null) {
                var matcher = (Entitas.Matcher<ProjectileEntity>)Entitas.Matcher<ProjectileEntity>.AllOf(ProjectileComponentsLookup.ProjectileDelete);
                matcher.componentNames = ProjectileComponentsLookup.componentNames;
                _matcherProjectileDelete = matcher;
            }

            return _matcherProjectileDelete;
        }
    }
}