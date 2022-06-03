//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Agent.PlayerComponent agentPlayerComponent = new Agent.PlayerComponent();

    public bool isAgentPlayer {
        get { return HasComponent(GameComponentsLookup.AgentPlayer); }
        set {
            if (value != isAgentPlayer) {
                var index = GameComponentsLookup.AgentPlayer;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : agentPlayerComponent;

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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAgentPlayer;

    public static Entitas.IMatcher<GameEntity> AgentPlayer {
        get {
            if (_matcherAgentPlayer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AgentPlayer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAgentPlayer = matcher;
            }

            return _matcherAgentPlayer;
        }
    }
}