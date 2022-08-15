//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ItemParticleEntity {

    public Item.MechCastDataComponent itemMechCastData { get { return (Item.MechCastDataComponent)GetComponent(ItemParticleComponentsLookup.ItemMechCastData); } }
    public bool hasItemMechCastData { get { return HasComponent(ItemParticleComponentsLookup.ItemMechCastData); } }

    public void AddItemMechCastData(Mech.Data newData, bool newInputsActive) {
        var index = ItemParticleComponentsLookup.ItemMechCastData;
        var component = (Item.MechCastDataComponent)CreateComponent(index, typeof(Item.MechCastDataComponent));
        component.data = newData;
        component.InputsActive = newInputsActive;
        AddComponent(index, component);
    }

    public void ReplaceItemMechCastData(Mech.Data newData, bool newInputsActive) {
        var index = ItemParticleComponentsLookup.ItemMechCastData;
        var component = (Item.MechCastDataComponent)CreateComponent(index, typeof(Item.MechCastDataComponent));
        component.data = newData;
        component.InputsActive = newInputsActive;
        ReplaceComponent(index, component);
    }

    public void RemoveItemMechCastData() {
        RemoveComponent(ItemParticleComponentsLookup.ItemMechCastData);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ItemParticleEntity : IItemMechCastDataEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class ItemParticleMatcher {

    static Entitas.IMatcher<ItemParticleEntity> _matcherItemMechCastData;

    public static Entitas.IMatcher<ItemParticleEntity> ItemMechCastData {
        get {
            if (_matcherItemMechCastData == null) {
                var matcher = (Entitas.Matcher<ItemParticleEntity>)Entitas.Matcher<ItemParticleEntity>.AllOf(ItemParticleComponentsLookup.ItemMechCastData);
                matcher.componentNames = ItemParticleComponentsLookup.componentNames;
                _matcherItemMechCastData = matcher;
            }

            return _matcherItemMechCastData;
        }
    }
}