//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial interface IItemIDEntity {

    Item.IDComponent itemID { get; }
    bool hasItemID { get; }

    void AddItemID(int newID, int newIndex, string newItemName);
    void ReplaceItemID(int newID, int newIndex, string newItemName);
    void RemoveItemID();
}
