//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InventoryEntity {

    public Inventory.EntityComponent inventoryEntity { get { return (Inventory.EntityComponent)GetComponent(InventoryComponentsLookup.InventoryEntity); } }
    public bool hasInventoryEntity { get { return HasComponent(InventoryComponentsLookup.InventoryEntity); } }

    public void AddInventoryEntity(int newIndex, int newInventoryModelID, Inventory.Slot[] newSlots, int newSelectedSlotID, int newSize, Utility.BitSet newSlotsMask) {
        var index = InventoryComponentsLookup.InventoryEntity;
        var component = (Inventory.EntityComponent)CreateComponent(index, typeof(Inventory.EntityComponent));
        component.Index = newIndex;
        component.InventoryModelID = newInventoryModelID;
        component.Slots = newSlots;
        component.SelectedSlotID = newSelectedSlotID;
        component.Size = newSize;
        component.SlotsMask = newSlotsMask;
        AddComponent(index, component);
    }

    public void ReplaceInventoryEntity(int newIndex, int newInventoryModelID, Inventory.Slot[] newSlots, int newSelectedSlotID, int newSize, Utility.BitSet newSlotsMask) {
        var index = InventoryComponentsLookup.InventoryEntity;
        var component = (Inventory.EntityComponent)CreateComponent(index, typeof(Inventory.EntityComponent));
        component.Index = newIndex;
        component.InventoryModelID = newInventoryModelID;
        component.Slots = newSlots;
        component.SelectedSlotID = newSelectedSlotID;
        component.Size = newSize;
        component.SlotsMask = newSlotsMask;
        ReplaceComponent(index, component);
    }

    public void RemoveInventoryEntity() {
        RemoveComponent(InventoryComponentsLookup.InventoryEntity);
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

    static Entitas.IMatcher<InventoryEntity> _matcherInventoryEntity;

    public static Entitas.IMatcher<InventoryEntity> InventoryEntity {
        get {
            if (_matcherInventoryEntity == null) {
                var matcher = (Entitas.Matcher<InventoryEntity>)Entitas.Matcher<InventoryEntity>.AllOf(InventoryComponentsLookup.InventoryEntity);
                matcher.componentNames = InventoryComponentsLookup.componentNames;
                _matcherInventoryEntity = matcher;
            }

            return _matcherInventoryEntity;
        }
    }
}