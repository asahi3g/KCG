//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Item.AttachedInventoryComponent itemAttachedInventory { get { return (Item.AttachedInventoryComponent)GetComponent(GameComponentsLookup.ItemAttachedInventory); } }
    public bool hasItemAttachedInventory { get { return HasComponent(GameComponentsLookup.ItemAttachedInventory); } }

    public void AddItemAttachedInventory(int newInventoryID, int newSlotNumber) {
        var index = GameComponentsLookup.ItemAttachedInventory;
        var component = (Item.AttachedInventoryComponent)CreateComponent(index, typeof(Item.AttachedInventoryComponent));
        component.InventoryID = newInventoryID;
        component.SlotNumber = newSlotNumber;
        AddComponent(index, component);
    }

    public void ReplaceItemAttachedInventory(int newInventoryID, int newSlotNumber) {
        var index = GameComponentsLookup.ItemAttachedInventory;
        var component = (Item.AttachedInventoryComponent)CreateComponent(index, typeof(Item.AttachedInventoryComponent));
        component.InventoryID = newInventoryID;
        component.SlotNumber = newSlotNumber;
        ReplaceComponent(index, component);
    }

    public void RemoveItemAttachedInventory() {
        RemoveComponent(GameComponentsLookup.ItemAttachedInventory);
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

    static Entitas.IMatcher<GameEntity> _matcherItemAttachedInventory;

    public static Entitas.IMatcher<GameEntity> ItemAttachedInventory {
        get {
            if (_matcherItemAttachedInventory == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ItemAttachedInventory);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherItemAttachedInventory = matcher;
            }

            return _matcherItemAttachedInventory;
        }
    }
}