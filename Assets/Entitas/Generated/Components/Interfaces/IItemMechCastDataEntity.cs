//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial interface IItemMechCastDataEntity {

    Item.MechCastDataComponent itemMechCastData { get; }
    bool hasItemMechCastData { get; }

    void AddItemMechCastData(Mech.Data newData, bool newInputsActive);
    void ReplaceItemMechCastData(Mech.Data newData, bool newInputsActive);
    void RemoveItemMechCastData();
}