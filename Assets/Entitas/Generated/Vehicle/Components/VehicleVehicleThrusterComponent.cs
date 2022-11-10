//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class VehicleEntity {

    public Vehicle.ThrusterComponent vehicleThruster { get { return (Vehicle.ThrusterComponent)GetComponent(VehicleComponentsLookup.VehicleThruster); } }
    public bool hasVehicleThruster { get { return HasComponent(VehicleComponentsLookup.VehicleThruster); } }

    public void AddVehicleThruster(bool newJet, KMath.Vec2f newAngle, Enums.JetSize newJetSize, bool newIsLaunched) {
        var index = VehicleComponentsLookup.VehicleThruster;
        var component = (Vehicle.ThrusterComponent)CreateComponent(index, typeof(Vehicle.ThrusterComponent));
        component.Jet = newJet;
        component.Angle = newAngle;
        component.JetSize = newJetSize;
        component.isLaunched = newIsLaunched;
        AddComponent(index, component);
    }

    public void ReplaceVehicleThruster(bool newJet, KMath.Vec2f newAngle, Enums.JetSize newJetSize, bool newIsLaunched) {
        var index = VehicleComponentsLookup.VehicleThruster;
        var component = (Vehicle.ThrusterComponent)CreateComponent(index, typeof(Vehicle.ThrusterComponent));
        component.Jet = newJet;
        component.Angle = newAngle;
        component.JetSize = newJetSize;
        component.isLaunched = newIsLaunched;
        ReplaceComponent(index, component);
    }

    public void RemoveVehicleThruster() {
        RemoveComponent(VehicleComponentsLookup.VehicleThruster);
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
public sealed partial class VehicleMatcher {

    static Entitas.IMatcher<VehicleEntity> _matcherVehicleThruster;

    public static Entitas.IMatcher<VehicleEntity> VehicleThruster {
        get {
            if (_matcherVehicleThruster == null) {
                var matcher = (Entitas.Matcher<VehicleEntity>)Entitas.Matcher<VehicleEntity>.AllOf(VehicleComponentsLookup.VehicleThruster);
                matcher.componentNames = VehicleComponentsLookup.componentNames;
                _matcherVehicleThruster = matcher;
            }

            return _matcherVehicleThruster;
        }
    }
}
