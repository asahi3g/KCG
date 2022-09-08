//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ProjectileEntity {

    public Projectile.OnHitComponent projectileOnHit { get { return (Projectile.OnHitComponent)GetComponent(ProjectileComponentsLookup.ProjectileOnHit); } }
    public bool hasProjectileOnHit { get { return HasComponent(ProjectileComponentsLookup.ProjectileOnHit); } }

    public void AddProjectileOnHit(int newAgentID, float newHitTime, KMath.Vec2f newHitPos) {
        var index = ProjectileComponentsLookup.ProjectileOnHit;
        var component = (Projectile.OnHitComponent)CreateComponent(index, typeof(Projectile.OnHitComponent));
        component.AgentID = newAgentID;
        component.HitTime = newHitTime;
        component.HitPos = newHitPos;
        AddComponent(index, component);
    }

    public void ReplaceProjectileOnHit(int newAgentID, float newHitTime, KMath.Vec2f newHitPos) {
        var index = ProjectileComponentsLookup.ProjectileOnHit;
        var component = (Projectile.OnHitComponent)CreateComponent(index, typeof(Projectile.OnHitComponent));
        component.AgentID = newAgentID;
        component.HitTime = newHitTime;
        component.HitPos = newHitPos;
        ReplaceComponent(index, component);
    }

    public void RemoveProjectileOnHit() {
        RemoveComponent(ProjectileComponentsLookup.ProjectileOnHit);
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

    static Entitas.IMatcher<ProjectileEntity> _matcherProjectileOnHit;

    public static Entitas.IMatcher<ProjectileEntity> ProjectileOnHit {
        get {
            if (_matcherProjectileOnHit == null) {
                var matcher = (Entitas.Matcher<ProjectileEntity>)Entitas.Matcher<ProjectileEntity>.AllOf(ProjectileComponentsLookup.ProjectileOnHit);
                matcher.componentNames = ProjectileComponentsLookup.componentNames;
                _matcherProjectileOnHit = matcher;
            }

            return _matcherProjectileOnHit;
        }
    }
}
