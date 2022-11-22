//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InventoryEntity {

    public Inventory.InventoryEntityComponent inventoryInventoryEntity { get { return (Inventory.InventoryEntityComponent)GetComponent(InventoryComponentsLookup.InventoryInventoryEntity); } }
    public bool hasInventoryInventoryEntity { get { return HasComponent(InventoryComponentsLookup.InventoryInventoryEntity); } }

    public void AddInventoryInventoryEntity(int newIndex, int newInventoryEntityTemplateID, Inventory.Slot[] newSlots, int newSelectedSlotID, int newSize, Utility.BitSet newSlotsMask) {
        var index = InventoryComponentsLookup.InventoryInventoryEntity;
        var component = (Inventory.InventoryEntityComponent)CreateComponent(index, typeof(Inventory.InventoryEntityComponent));
        component.Index = newIndex;
        component.InventoryEntityTemplateID = newInventoryEntityTemplateID;
        component.Slots = newSlots;
        component.SelectedSlotID = newSelectedSlotID;
        component.Size = newSize;
        component.SlotsMask = newSlotsMask;
        AddComponent(index, component);
    }

    public void ReplaceInventoryInventoryEntity(int newIndex, int newInventoryEntityTemplateID, Inventory.Slot[] newSlots, int newSelectedSlotID, int newSize, Utility.BitSet newSlotsMask) {
        var index = InventoryComponentsLookup.InventoryInventoryEntity;
        var component = (Inventory.InventoryEntityComponent)CreateComponent(index, typeof(Inventory.InventoryEntityComponent));
        component.Index = newIndex;
        component.InventoryEntityTemplateID = newInventoryEntityTemplateID;
        component.Slots = newSlots;
        component.SelectedSlotID = newSelectedSlotID;
        component.Size = newSize;
        component.SlotsMask = newSlotsMask;
        ReplaceComponent(index, component);
    }

    public void RemoveInventoryInventoryEntity() {
        RemoveComponent(InventoryComponentsLookup.InventoryInventoryEntity);
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

    static Entitas.IMatcher<InventoryEntity> _matcherInventoryInventoryEntity;

    public static Entitas.IMatcher<InventoryEntity> InventoryInventoryEntity {
        get {
            if (_matcherInventoryInventoryEntity == null) {
                var matcher = (Entitas.Matcher<InventoryEntity>)Entitas.Matcher<InventoryEntity>.AllOf(InventoryComponentsLookup.InventoryInventoryEntity);
                matcher.componentNames = InventoryComponentsLookup.componentNames;
                _matcherInventoryInventoryEntity = matcher;
            }

            return _matcherInventoryInventoryEntity;
        }
    }
}