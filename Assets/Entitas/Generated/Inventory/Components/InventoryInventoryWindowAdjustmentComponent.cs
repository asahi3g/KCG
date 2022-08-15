//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InventoryEntity {

    public Inventory.WindowAdjustmentComponent inventoryWindowAdjustment { get { return (Inventory.WindowAdjustmentComponent)GetComponent(InventoryComponentsLookup.InventoryWindowAdjustment); } }
    public bool hasInventoryWindowAdjustment { get { return HasComponent(InventoryComponentsLookup.InventoryWindowAdjustment); } }

    public void AddInventoryWindowAdjustment(Inventory.Window newWindow) {
        var index = InventoryComponentsLookup.InventoryWindowAdjustment;
        var component = (Inventory.WindowAdjustmentComponent)CreateComponent(index, typeof(Inventory.WindowAdjustmentComponent));
        component.window = newWindow;
        AddComponent(index, component);
    }

    public void ReplaceInventoryWindowAdjustment(Inventory.Window newWindow) {
        var index = InventoryComponentsLookup.InventoryWindowAdjustment;
        var component = (Inventory.WindowAdjustmentComponent)CreateComponent(index, typeof(Inventory.WindowAdjustmentComponent));
        component.window = newWindow;
        ReplaceComponent(index, component);
    }

    public void RemoveInventoryWindowAdjustment() {
        RemoveComponent(InventoryComponentsLookup.InventoryWindowAdjustment);
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

    static Entitas.IMatcher<InventoryEntity> _matcherInventoryWindowAdjustment;

    public static Entitas.IMatcher<InventoryEntity> InventoryWindowAdjustment {
        get {
            if (_matcherInventoryWindowAdjustment == null) {
                var matcher = (Entitas.Matcher<InventoryEntity>)Entitas.Matcher<InventoryEntity>.AllOf(InventoryComponentsLookup.InventoryWindowAdjustment);
                matcher.componentNames = InventoryComponentsLookup.componentNames;
                _matcherInventoryWindowAdjustment = matcher;
            }

            return _matcherInventoryWindowAdjustment;
        }
    }
}
