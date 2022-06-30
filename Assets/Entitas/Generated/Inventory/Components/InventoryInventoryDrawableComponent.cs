//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InventoryEntity {

    static readonly Inventory.DrawableComponent inventoryDrawableComponent = new Inventory.DrawableComponent();

    public bool isInventoryDrawable {
        get { return HasComponent(InventoryComponentsLookup.InventoryDrawable); }
        set {
            if (value != isInventoryDrawable) {
                var index = InventoryComponentsLookup.InventoryDrawable;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : inventoryDrawableComponent;

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
public sealed partial class InventoryMatcher {

    static Entitas.IMatcher<InventoryEntity> _matcherInventoryDrawable;

    public static Entitas.IMatcher<InventoryEntity> InventoryDrawable {
        get {
            if (_matcherInventoryDrawable == null) {
                var matcher = (Entitas.Matcher<InventoryEntity>)Entitas.Matcher<InventoryEntity>.AllOf(InventoryComponentsLookup.InventoryDrawable);
                matcher.componentNames = InventoryComponentsLookup.componentNames;
                _matcherInventoryDrawable = matcher;
            }

            return _matcherInventoryDrawable;
        }
    }
}