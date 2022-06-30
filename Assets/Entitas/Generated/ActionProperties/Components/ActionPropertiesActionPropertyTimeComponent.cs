//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ActionPropertiesEntity {

    public Action.Property.TimeComponent actionPropertyTime { get { return (Action.Property.TimeComponent)GetComponent(ActionPropertiesComponentsLookup.ActionPropertyTime); } }
    public bool hasActionPropertyTime { get { return HasComponent(ActionPropertiesComponentsLookup.ActionPropertyTime); } }

    public void AddActionPropertyTime(float newDuration) {
        var index = ActionPropertiesComponentsLookup.ActionPropertyTime;
        var component = (Action.Property.TimeComponent)CreateComponent(index, typeof(Action.Property.TimeComponent));
        component.Duration = newDuration;
        AddComponent(index, component);
    }

    public void ReplaceActionPropertyTime(float newDuration) {
        var index = ActionPropertiesComponentsLookup.ActionPropertyTime;
        var component = (Action.Property.TimeComponent)CreateComponent(index, typeof(Action.Property.TimeComponent));
        component.Duration = newDuration;
        ReplaceComponent(index, component);
    }

    public void RemoveActionPropertyTime() {
        RemoveComponent(ActionPropertiesComponentsLookup.ActionPropertyTime);
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

    static Entitas.IMatcher<ActionPropertiesEntity> _matcherActionPropertyTime;

    public static Entitas.IMatcher<ActionPropertiesEntity> ActionPropertyTime {
        get {
            if (_matcherActionPropertyTime == null) {
                var matcher = (Entitas.Matcher<ActionPropertiesEntity>)Entitas.Matcher<ActionPropertiesEntity>.AllOf(ActionPropertiesComponentsLookup.ActionPropertyTime);
                matcher.componentNames = ActionPropertiesComponentsLookup.componentNames;
                _matcherActionPropertyTime = matcher;
            }

            return _matcherActionPropertyTime;
        }
    }
}