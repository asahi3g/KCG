//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Components.Box2DCollider componentsBox2DCollider { get { return (Components.Box2DCollider)GetComponent(GameComponentsLookup.ComponentsBox2DCollider); } }
    public bool hasComponentsBox2DCollider { get { return HasComponent(GameComponentsLookup.ComponentsBox2DCollider); } }

    public void AddComponentsBox2DCollider(UnityEngine.Vector2 newSize) {
        var index = GameComponentsLookup.ComponentsBox2DCollider;
        var component = (Components.Box2DCollider)CreateComponent(index, typeof(Components.Box2DCollider));
        component.Size = newSize;
        AddComponent(index, component);
    }

    public void ReplaceComponentsBox2DCollider(UnityEngine.Vector2 newSize) {
        var index = GameComponentsLookup.ComponentsBox2DCollider;
        var component = (Components.Box2DCollider)CreateComponent(index, typeof(Components.Box2DCollider));
        component.Size = newSize;
        ReplaceComponent(index, component);
    }

    public void RemoveComponentsBox2DCollider() {
        RemoveComponent(GameComponentsLookup.ComponentsBox2DCollider);
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

    static Entitas.IMatcher<GameEntity> _matcherComponentsBox2DCollider;

    public static Entitas.IMatcher<GameEntity> ComponentsBox2DCollider {
        get {
            if (_matcherComponentsBox2DCollider == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ComponentsBox2DCollider);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherComponentsBox2DCollider = matcher;
            }

            return _matcherComponentsBox2DCollider;
        }
    }
}
