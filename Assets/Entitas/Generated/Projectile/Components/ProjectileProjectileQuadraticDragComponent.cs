//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ProjectileEntity {

    public Projectile.QuadraticDragComponent projectileQuadraticDrag { get { return (Projectile.QuadraticDragComponent)GetComponent(ProjectileComponentsLookup.ProjectileQuadraticDrag); } }
    public bool hasProjectileQuadraticDrag { get { return HasComponent(ProjectileComponentsLookup.ProjectileQuadraticDrag); } }

    public void AddProjectileQuadraticDrag(bool newCanDrag, float newDrag) {
        var index = ProjectileComponentsLookup.ProjectileQuadraticDrag;
        var component = (Projectile.QuadraticDragComponent)CreateComponent(index, typeof(Projectile.QuadraticDragComponent));
        component.canDrag = newCanDrag;
        component.Drag = newDrag;
        AddComponent(index, component);
    }

    public void ReplaceProjectileQuadraticDrag(bool newCanDrag, float newDrag) {
        var index = ProjectileComponentsLookup.ProjectileQuadraticDrag;
        var component = (Projectile.QuadraticDragComponent)CreateComponent(index, typeof(Projectile.QuadraticDragComponent));
        component.canDrag = newCanDrag;
        component.Drag = newDrag;
        ReplaceComponent(index, component);
    }

    public void RemoveProjectileQuadraticDrag() {
        RemoveComponent(ProjectileComponentsLookup.ProjectileQuadraticDrag);
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

    static Entitas.IMatcher<ProjectileEntity> _matcherProjectileQuadraticDrag;

    public static Entitas.IMatcher<ProjectileEntity> ProjectileQuadraticDrag {
        get {
            if (_matcherProjectileQuadraticDrag == null) {
                var matcher = (Entitas.Matcher<ProjectileEntity>)Entitas.Matcher<ProjectileEntity>.AllOf(ProjectileComponentsLookup.ProjectileQuadraticDrag);
                matcher.componentNames = ProjectileComponentsLookup.componentNames;
                _matcherProjectileQuadraticDrag = matcher;
            }

            return _matcherProjectileQuadraticDrag;
        }
    }
}
