//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Item.Attribute.SizeComponent itemAttributeSize { get { return (Item.Attribute.SizeComponent)GetComponent(GameComponentsLookup.ItemAttributeSize); } }
    public bool hasItemAttributeSize { get { return HasComponent(GameComponentsLookup.ItemAttributeSize); } }

    public void AddItemAttributeSize(UnityEngine.Vector2 newSize) {
        var index = GameComponentsLookup.ItemAttributeSize;
        var component = (Item.Attribute.SizeComponent)CreateComponent(index, typeof(Item.Attribute.SizeComponent));
        component.Size = newSize;
        AddComponent(index, component);
    }

    public void ReplaceItemAttributeSize(UnityEngine.Vector2 newSize) {
        var index = GameComponentsLookup.ItemAttributeSize;
        var component = (Item.Attribute.SizeComponent)CreateComponent(index, typeof(Item.Attribute.SizeComponent));
        component.Size = newSize;
        ReplaceComponent(index, component);
    }

    public void RemoveItemAttributeSize() {
        RemoveComponent(GameComponentsLookup.ItemAttributeSize);
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

    static Entitas.IMatcher<GameEntity> _matcherItemAttributeSize;

    public static Entitas.IMatcher<GameEntity> ItemAttributeSize {
        get {
            if (_matcherItemAttributeSize == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ItemAttributeSize);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherItemAttributeSize = matcher;
            }

            return _matcherItemAttributeSize;
        }
    }
}
