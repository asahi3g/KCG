//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ItemInventoryEntity {

    public Item.CastDataComponent itemCastData { get { return (Item.CastDataComponent)GetComponent(ItemInventoryComponentsLookup.ItemCastData); } }
    public bool hasItemCastData { get { return HasComponent(ItemInventoryComponentsLookup.ItemCastData); } }

    public void AddItemCastData(Enums.Tile.Data newData, bool newInputsActive) {
        var index = ItemInventoryComponentsLookup.ItemCastData;
        var component = (Item.CastDataComponent)CreateComponent(index, typeof(Item.CastDataComponent));
        component.data = newData;
        component.InputsActive = newInputsActive;
        AddComponent(index, component);
    }

    public void ReplaceItemCastData(Enums.Tile.Data newData, bool newInputsActive) {
        var index = ItemInventoryComponentsLookup.ItemCastData;
        var component = (Item.CastDataComponent)CreateComponent(index, typeof(Item.CastDataComponent));
        component.data = newData;
        component.InputsActive = newInputsActive;
        ReplaceComponent(index, component);
    }

    public void RemoveItemCastData() {
        RemoveComponent(ItemInventoryComponentsLookup.ItemCastData);
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
public sealed partial class ItemInventoryMatcher {

    static Entitas.IMatcher<ItemInventoryEntity> _matcherItemCastData;

    public static Entitas.IMatcher<ItemInventoryEntity> ItemCastData {
        get {
            if (_matcherItemCastData == null) {
                var matcher = (Entitas.Matcher<ItemInventoryEntity>)Entitas.Matcher<ItemInventoryEntity>.AllOf(ItemInventoryComponentsLookup.ItemCastData);
                matcher.componentNames = ItemInventoryComponentsLookup.componentNames;
                _matcherItemCastData = matcher;
            }

            return _matcherItemCastData;
        }
    }
}
