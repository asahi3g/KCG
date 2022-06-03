//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ECSInput.XYComponent eCSInputXY { get { return (ECSInput.XYComponent)GetComponent(GameComponentsLookup.ECSInputXY); } }
    public bool hasECSInputXY { get { return HasComponent(GameComponentsLookup.ECSInputXY); } }

    public void AddECSInputXY(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.ECSInputXY;
        var component = (ECSInput.XYComponent)CreateComponent(index, typeof(ECSInput.XYComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceECSInputXY(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.ECSInputXY;
        var component = (ECSInput.XYComponent)CreateComponent(index, typeof(ECSInput.XYComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveECSInputXY() {
        RemoveComponent(GameComponentsLookup.ECSInputXY);
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

    static Entitas.IMatcher<GameEntity> _matcherECSInputXY;

    public static Entitas.IMatcher<GameEntity> ECSInputXY {
        get {
            if (_matcherECSInputXY == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ECSInputXY);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherECSInputXY = matcher;
            }

            return _matcherECSInputXY;
        }
    }
}
