//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ActionPropertiesEntity {

    public Action.Property.FactoryComponent actionPropertyFactory { get { return (Action.Property.FactoryComponent)GetComponent(ActionPropertiesComponentsLookup.ActionPropertyFactory); } }
    public bool hasActionPropertyFactory { get { return HasComponent(ActionPropertiesComponentsLookup.ActionPropertyFactory); } }

    public void AddActionPropertyFactory(Action.ActionCreator newActionFactory) {
        var index = ActionPropertiesComponentsLookup.ActionPropertyFactory;
        var component = (Action.Property.FactoryComponent)CreateComponent(index, typeof(Action.Property.FactoryComponent));
        component.ActionFactory = newActionFactory;
        AddComponent(index, component);
    }

    public void ReplaceActionPropertyFactory(Action.ActionCreator newActionFactory) {
        var index = ActionPropertiesComponentsLookup.ActionPropertyFactory;
        var component = (Action.Property.FactoryComponent)CreateComponent(index, typeof(Action.Property.FactoryComponent));
        component.ActionFactory = newActionFactory;
        ReplaceComponent(index, component);
    }

    public void RemoveActionPropertyFactory() {
        RemoveComponent(ActionPropertiesComponentsLookup.ActionPropertyFactory);
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
public sealed partial class ActionPropertiesMatcher {

    static Entitas.IMatcher<ActionPropertiesEntity> _matcherActionPropertyFactory;

    public static Entitas.IMatcher<ActionPropertiesEntity> ActionPropertyFactory {
        get {
            if (_matcherActionPropertyFactory == null) {
                var matcher = (Entitas.Matcher<ActionPropertiesEntity>)Entitas.Matcher<ActionPropertiesEntity>.AllOf(ActionPropertiesComponentsLookup.ActionPropertyFactory);
                matcher.componentNames = ActionPropertiesComponentsLookup.componentNames;
                _matcherActionPropertyFactory = matcher;
            }

            return _matcherActionPropertyFactory;
        }
    }
}