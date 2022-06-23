//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Action.Attribute.Component actionAttribute { get { return (Action.Attribute.Component)GetComponent(GameComponentsLookup.ActionAttribute); } }
    public bool hasActionAttribute { get { return HasComponent(GameComponentsLookup.ActionAttribute); } }

    public void AddActionAttribute(int newTypeID) {
        var index = GameComponentsLookup.ActionAttribute;
        var component = (Action.Attribute.Component)CreateComponent(index, typeof(Action.Attribute.Component));
        component.TypeID = newTypeID;
        AddComponent(index, component);
    }

    public void ReplaceActionAttribute(int newTypeID) {
        var index = GameComponentsLookup.ActionAttribute;
        var component = (Action.Attribute.Component)CreateComponent(index, typeof(Action.Attribute.Component));
        component.TypeID = newTypeID;
        ReplaceComponent(index, component);
    }

    public void RemoveActionAttribute() {
        RemoveComponent(GameComponentsLookup.ActionAttribute);
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

    static Entitas.IMatcher<GameEntity> _matcherActionAttribute;

    public static Entitas.IMatcher<GameEntity> ActionAttribute {
        get {
            if (_matcherActionAttribute == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActionAttribute);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActionAttribute = matcher;
            }

            return _matcherActionAttribute;
        }
    }
}