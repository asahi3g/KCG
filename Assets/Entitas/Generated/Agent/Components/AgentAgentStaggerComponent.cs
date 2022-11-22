//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AgentEntity {

    public Agent.StaggerComponent agentStagger { get { return (Agent.StaggerComponent)GetComponent(AgentComponentsLookup.AgentStagger); } }
    public bool hasAgentStagger { get { return HasComponent(AgentComponentsLookup.AgentStagger); } }

    public void AddAgentStagger(bool newStagger, float newStaggerAffectTime, float newElapsed) {
        var index = AgentComponentsLookup.AgentStagger;
        var component = (Agent.StaggerComponent)CreateComponent(index, typeof(Agent.StaggerComponent));
        component.Stagger = newStagger;
        component.StaggerAffectTime = newStaggerAffectTime;
        component.elapsed = newElapsed;
        AddComponent(index, component);
    }

    public void ReplaceAgentStagger(bool newStagger, float newStaggerAffectTime, float newElapsed) {
        var index = AgentComponentsLookup.AgentStagger;
        var component = (Agent.StaggerComponent)CreateComponent(index, typeof(Agent.StaggerComponent));
        component.Stagger = newStagger;
        component.StaggerAffectTime = newStaggerAffectTime;
        component.elapsed = newElapsed;
        ReplaceComponent(index, component);
    }

    public void RemoveAgentStagger() {
        RemoveComponent(AgentComponentsLookup.AgentStagger);
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
public sealed partial class AgentMatcher {

    static Entitas.IMatcher<AgentEntity> _matcherAgentStagger;

    public static Entitas.IMatcher<AgentEntity> AgentStagger {
        get {
            if (_matcherAgentStagger == null) {
                var matcher = (Entitas.Matcher<AgentEntity>)Entitas.Matcher<AgentEntity>.AllOf(AgentComponentsLookup.AgentStagger);
                matcher.componentNames = AgentComponentsLookup.componentNames;
                _matcherAgentStagger = matcher;
            }

            return _matcherAgentStagger;
        }
    }
}