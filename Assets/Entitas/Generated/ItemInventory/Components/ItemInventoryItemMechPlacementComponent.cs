//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ItemInventoryEntity {

    public Item.MechPlacementComponent itemMechPlacement { get { return (Item.MechPlacementComponent)GetComponent(ItemInventoryComponentsLookup.ItemMechPlacement); } }
    public bool hasItemMechPlacement { get { return HasComponent(ItemInventoryComponentsLookup.ItemMechPlacement); } }

    public void AddItemMechPlacement(Enums.MechType newMechID, bool newInputsActive) {
        var index = ItemInventoryComponentsLookup.ItemMechPlacement;
        var component = (Item.MechPlacementComponent)CreateComponent(index, typeof(Item.MechPlacementComponent));
        component.MechID = newMechID;
        component.InputsActive = newInputsActive;
        AddComponent(index, component);
    }

    public void ReplaceItemMechPlacement(Enums.MechType newMechID, bool newInputsActive) {
        var index = ItemInventoryComponentsLookup.ItemMechPlacement;
        var component = (Item.MechPlacementComponent)CreateComponent(index, typeof(Item.MechPlacementComponent));
        component.MechID = newMechID;
        component.InputsActive = newInputsActive;
        ReplaceComponent(index, component);
    }

    public void RemoveItemMechPlacement() {
        RemoveComponent(ItemInventoryComponentsLookup.ItemMechPlacement);
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

    static Entitas.IMatcher<ItemInventoryEntity> _matcherItemMechPlacement;

    public static Entitas.IMatcher<ItemInventoryEntity> ItemMechPlacement {
        get {
            if (_matcherItemMechPlacement == null) {
                var matcher = (Entitas.Matcher<ItemInventoryEntity>)Entitas.Matcher<ItemInventoryEntity>.AllOf(ItemInventoryComponentsLookup.ItemMechPlacement);
                matcher.componentNames = ItemInventoryComponentsLookup.componentNames;
                _matcherItemMechPlacement = matcher;
            }

            return _matcherItemMechPlacement;
        }
    }
}