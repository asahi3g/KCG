//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MechEntity {

    public Mech.Planter.PlanterComponent mechPlanterPlanter { get { return (Mech.Planter.PlanterComponent)GetComponent(MechComponentsLookup.MechPlanterPlanter); } }
    public bool hasMechPlanterPlanter { get { return HasComponent(MechComponentsLookup.MechPlanterPlanter); } }

    public void AddMechPlanterPlanter(bool newGotSeed, MechEntity newPlant, float newPlantGrowth, float newGrowthTarget, float newWaterLevel, float newMaxWaterLevel, int newLightLevel) {
        var index = MechComponentsLookup.MechPlanterPlanter;
        var component = (Mech.Planter.PlanterComponent)CreateComponent(index, typeof(Mech.Planter.PlanterComponent));
        component.GotSeed = newGotSeed;
        component.Plant = newPlant;
        component.PlantGrowth = newPlantGrowth;
        component.GrowthTarget = newGrowthTarget;
        component.WaterLevel = newWaterLevel;
        component.MaxWaterLevel = newMaxWaterLevel;
        component.LightLevel = newLightLevel;
        AddComponent(index, component);
    }

    public void ReplaceMechPlanterPlanter(bool newGotSeed, MechEntity newPlant, float newPlantGrowth, float newGrowthTarget, float newWaterLevel, float newMaxWaterLevel, int newLightLevel) {
        var index = MechComponentsLookup.MechPlanterPlanter;
        var component = (Mech.Planter.PlanterComponent)CreateComponent(index, typeof(Mech.Planter.PlanterComponent));
        component.GotSeed = newGotSeed;
        component.Plant = newPlant;
        component.PlantGrowth = newPlantGrowth;
        component.GrowthTarget = newGrowthTarget;
        component.WaterLevel = newWaterLevel;
        component.MaxWaterLevel = newMaxWaterLevel;
        component.LightLevel = newLightLevel;
        ReplaceComponent(index, component);
    }

    public void RemoveMechPlanterPlanter() {
        RemoveComponent(MechComponentsLookup.MechPlanterPlanter);
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
public sealed partial class MechMatcher {

    static Entitas.IMatcher<MechEntity> _matcherMechPlanterPlanter;

    public static Entitas.IMatcher<MechEntity> MechPlanterPlanter {
        get {
            if (_matcherMechPlanterPlanter == null) {
                var matcher = (Entitas.Matcher<MechEntity>)Entitas.Matcher<MechEntity>.AllOf(MechComponentsLookup.MechPlanterPlanter);
                matcher.componentNames = MechComponentsLookup.componentNames;
                _matcherMechPlanterPlanter = matcher;
            }

            return _matcherMechPlanterPlanter;
        }
    }
}