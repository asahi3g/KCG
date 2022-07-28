//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ParticleEntity {

    public Particle.Box2DColliderComponent particleBox2DCollider { get { return (Particle.Box2DColliderComponent)GetComponent(ParticleComponentsLookup.ParticleBox2DCollider); } }
    public bool hasParticleBox2DCollider { get { return HasComponent(ParticleComponentsLookup.ParticleBox2DCollider); } }

    public void AddParticleBox2DCollider(KMath.Vec2f newSize, KMath.Vec2f newOffset) {
        var index = ParticleComponentsLookup.ParticleBox2DCollider;
        var component = (Particle.Box2DColliderComponent)CreateComponent(index, typeof(Particle.Box2DColliderComponent));
        component.Size = newSize;
        component.Offset = newOffset;
        AddComponent(index, component);
    }

    public void ReplaceParticleBox2DCollider(KMath.Vec2f newSize, KMath.Vec2f newOffset) {
        var index = ParticleComponentsLookup.ParticleBox2DCollider;
        var component = (Particle.Box2DColliderComponent)CreateComponent(index, typeof(Particle.Box2DColliderComponent));
        component.Size = newSize;
        component.Offset = newOffset;
        ReplaceComponent(index, component);
    }

    public void RemoveParticleBox2DCollider() {
        RemoveComponent(ParticleComponentsLookup.ParticleBox2DCollider);
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
public sealed partial class ParticleMatcher {

    static Entitas.IMatcher<ParticleEntity> _matcherParticleBox2DCollider;

    public static Entitas.IMatcher<ParticleEntity> ParticleBox2DCollider {
        get {
            if (_matcherParticleBox2DCollider == null) {
                var matcher = (Entitas.Matcher<ParticleEntity>)Entitas.Matcher<ParticleEntity>.AllOf(ParticleComponentsLookup.ParticleBox2DCollider);
                matcher.componentNames = ParticleComponentsLookup.componentNames;
                _matcherParticleBox2DCollider = matcher;
            }

            return _matcherParticleBox2DCollider;
        }
    }
}
