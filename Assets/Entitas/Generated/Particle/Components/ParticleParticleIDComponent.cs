//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ParticleEntity {

    public Particle.IDComponent particleID { get { return (Particle.IDComponent)GetComponent(ParticleComponentsLookup.ParticleID); } }
    public bool hasParticleID { get { return HasComponent(ParticleComponentsLookup.ParticleID); } }

    public void AddParticleID(long newID, int newIndex) {
        var index = ParticleComponentsLookup.ParticleID;
        var component = (Particle.IDComponent)CreateComponent(index, typeof(Particle.IDComponent));
        component.ID = newID;
        component.Index = newIndex;
        AddComponent(index, component);
    }

    public void ReplaceParticleID(long newID, int newIndex) {
        var index = ParticleComponentsLookup.ParticleID;
        var component = (Particle.IDComponent)CreateComponent(index, typeof(Particle.IDComponent));
        component.ID = newID;
        component.Index = newIndex;
        ReplaceComponent(index, component);
    }

    public void RemoveParticleID() {
        RemoveComponent(ParticleComponentsLookup.ParticleID);
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

    static Entitas.IMatcher<ParticleEntity> _matcherParticleID;

    public static Entitas.IMatcher<ParticleEntity> ParticleID {
        get {
            if (_matcherParticleID == null) {
                var matcher = (Entitas.Matcher<ParticleEntity>)Entitas.Matcher<ParticleEntity>.AllOf(ParticleComponentsLookup.ParticleID);
                matcher.componentNames = ParticleComponentsLookup.componentNames;
                _matcherParticleID = matcher;
            }

            return _matcherParticleID;
        }
    }
}
