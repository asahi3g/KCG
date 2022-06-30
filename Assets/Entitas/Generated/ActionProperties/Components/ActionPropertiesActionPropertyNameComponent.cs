//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ActionPropertiesEntity {

    public Action.Property.NameComponent actionPropertyName { get { return (Action.Property.NameComponent)GetComponent(ActionPropertiesComponentsLookup.ActionPropertyName); } }
    public bool hasActionPropertyName { get { return HasComponent(ActionPropertiesComponentsLookup.ActionPropertyName); } }

    public void AddActionPropertyName(string newTypeName) {
        var index = ActionPropertiesComponentsLookup.ActionPropertyName;
        var component = (Action.Property.NameComponent)CreateComponent(index, typeof(Action.Property.NameComponent));
        component.TypeName = newTypeName;
        AddComponent(index, component);
    }

    public void ReplaceActionPropertyName(string newTypeName) {
        var index = ActionPropertiesComponentsLookup.ActionPropertyName;
        var component = (Action.Property.NameComponent)CreateComponent(index, typeof(Action.Property.NameComponent));
        component.TypeName = newTypeName;
        ReplaceComponent(index, component);
    }

    public void RemoveActionPropertyName() {
        RemoveComponent(ActionPropertiesComponentsLookup.ActionPropertyName);
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

    static Entitas.IMatcher<ActionPropertiesEntity> _matcherActionPropertyName;

    public static Entitas.IMatcher<ActionPropertiesEntity> ActionPropertyName {
        get {
            if (_matcherActionPropertyName == null) {
                var matcher = (Entitas.Matcher<ActionPropertiesEntity>)Entitas.Matcher<ActionPropertiesEntity>.AllOf(ActionPropertiesComponentsLookup.ActionPropertyName);
                matcher.componentNames = ActionPropertiesComponentsLookup.componentNames;
                _matcherActionPropertyName = matcher;
            }

            return _matcherActionPropertyName;
        }
    }
}