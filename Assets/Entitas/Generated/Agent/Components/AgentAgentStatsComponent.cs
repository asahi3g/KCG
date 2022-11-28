//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AgentEntity {

    public Agent.StatsComponent agentStats { get { return (Agent.StatsComponent)GetComponent(AgentComponentsLookup.AgentStats); } }
    public bool hasAgentStats { get { return HasComponent(AgentComponentsLookup.AgentStats); } }

    public void AddAgentStats(int newHealth, int newFood, int newWater, int newOxygen, int newFuel) {
        var index = AgentComponentsLookup.AgentStats;
        var component = (Agent.StatsComponent)CreateComponent(index, typeof(Agent.StatsComponent));
        component.Health = newHealth;
        component.Food = newFood;
        component.Water = newWater;
        component.Oxygen = newOxygen;
        component.Fuel = newFuel;
        AddComponent(index, component);
    }

    public void ReplaceAgentStats(int newHealth, int newFood, int newWater, int newOxygen, int newFuel) {
        var index = AgentComponentsLookup.AgentStats;
        var component = (Agent.StatsComponent)CreateComponent(index, typeof(Agent.StatsComponent));
        component.Health = newHealth;
        component.Food = newFood;
        component.Water = newWater;
        component.Oxygen = newOxygen;
        component.Fuel = newFuel;
        ReplaceComponent(index, component);
    }

    public void RemoveAgentStats() {
        RemoveComponent(AgentComponentsLookup.AgentStats);
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

    static Entitas.IMatcher<AgentEntity> _matcherAgentStats;

    public static Entitas.IMatcher<AgentEntity> AgentStats {
        get {
            if (_matcherAgentStats == null) {
                var matcher = (Entitas.Matcher<AgentEntity>)Entitas.Matcher<AgentEntity>.AllOf(AgentComponentsLookup.AgentStats);
                matcher.componentNames = AgentComponentsLookup.componentNames;
                _matcherAgentStats = matcher;
            }

            return _matcherAgentStats;
        }
    }
}
