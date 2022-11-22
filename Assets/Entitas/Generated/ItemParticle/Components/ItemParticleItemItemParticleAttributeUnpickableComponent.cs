//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ItemParticleEntity {

    public Item.ItemParticleAttributeUnpickableComponent itemItemParticleAttributeUnpickable { get { return (Item.ItemParticleAttributeUnpickableComponent)GetComponent(ItemParticleComponentsLookup.ItemItemParticleAttributeUnpickable); } }
    public bool hasItemItemParticleAttributeUnpickable { get { return HasComponent(ItemParticleComponentsLookup.ItemItemParticleAttributeUnpickable); } }

    public void AddItemItemParticleAttributeUnpickable(float newDuration) {
        var index = ItemParticleComponentsLookup.ItemItemParticleAttributeUnpickable;
        var component = (Item.ItemParticleAttributeUnpickableComponent)CreateComponent(index, typeof(Item.ItemParticleAttributeUnpickableComponent));
        component.Duration = newDuration;
        AddComponent(index, component);
    }

    public void ReplaceItemItemParticleAttributeUnpickable(float newDuration) {
        var index = ItemParticleComponentsLookup.ItemItemParticleAttributeUnpickable;
        var component = (Item.ItemParticleAttributeUnpickableComponent)CreateComponent(index, typeof(Item.ItemParticleAttributeUnpickableComponent));
        component.Duration = newDuration;
        ReplaceComponent(index, component);
    }

    public void RemoveItemItemParticleAttributeUnpickable() {
        RemoveComponent(ItemParticleComponentsLookup.ItemItemParticleAttributeUnpickable);
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
public sealed partial class ItemParticleMatcher {

    static Entitas.IMatcher<ItemParticleEntity> _matcherItemItemParticleAttributeUnpickable;

    public static Entitas.IMatcher<ItemParticleEntity> ItemItemParticleAttributeUnpickable {
        get {
            if (_matcherItemItemParticleAttributeUnpickable == null) {
                var matcher = (Entitas.Matcher<ItemParticleEntity>)Entitas.Matcher<ItemParticleEntity>.AllOf(ItemParticleComponentsLookup.ItemItemParticleAttributeUnpickable);
                matcher.componentNames = ItemParticleComponentsLookup.componentNames;
                _matcherItemItemParticleAttributeUnpickable = matcher;
            }

            return _matcherItemItemParticleAttributeUnpickable;
        }
    }
}