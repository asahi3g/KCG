//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AgentEntity {

    public Agent.ControllerComponent agentController { get { return (Agent.ControllerComponent)GetComponent(AgentComponentsLookup.AgentController); } }
    public bool hasAgentController { get { return HasComponent(AgentComponentsLookup.AgentController); } }

    public void AddAgentController(int newBehaviorTreeId, int newBlackboardID) {
        var index = AgentComponentsLookup.AgentController;
        var component = (Agent.ControllerComponent)CreateComponent(index, typeof(Agent.ControllerComponent));
        component.BehaviorTreeId = newBehaviorTreeId;
        component.BlackboardID = newBlackboardID;
        AddComponent(index, component);
    }

    public void ReplaceAgentController(int newBehaviorTreeId, int newBlackboardID) {
        var index = AgentComponentsLookup.AgentController;
        var component = (Agent.ControllerComponent)CreateComponent(index, typeof(Agent.ControllerComponent));
        component.BehaviorTreeId = newBehaviorTreeId;
        component.BlackboardID = newBlackboardID;
        ReplaceComponent(index, component);
    }

    public void RemoveAgentController() {
        RemoveComponent(AgentComponentsLookup.AgentController);
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

    static Entitas.IMatcher<AgentEntity> _matcherAgentController;

    public static Entitas.IMatcher<AgentEntity> AgentController {
        get {
            if (_matcherAgentController == null) {
                var matcher = (Entitas.Matcher<AgentEntity>)Entitas.Matcher<AgentEntity>.AllOf(AgentComponentsLookup.AgentController);
                matcher.componentNames = AgentComponentsLookup.componentNames;
                _matcherAgentController = matcher;
            }

            return _matcherAgentController;
        }
    }
}
