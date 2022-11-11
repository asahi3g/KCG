//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PodEntity {

    public Vehicle.Pod.StatusComponent vehiclePodStatus { get { return (Vehicle.Pod.StatusComponent)GetComponent(PodComponentsLookup.VehiclePodStatus); } }
    public bool hasVehiclePodStatus { get { return HasComponent(PodComponentsLookup.VehiclePodStatus); } }

    public void AddVehiclePodStatus(System.Collections.Generic.List<AgentEntity> newAgentsInside, KMath.Vec2f newRightPanel, KMath.Vec2f newLeftPanel, KMath.Vec2f newTopPanel, KMath.Vec2f newBottomPanel, KMath.Vec2f newRightPanelWidth, KMath.Vec2f newLeftPanelWidth, KMath.Vec2f newTopPanelWidth, KMath.Vec2f newBottomPanelWidth, KMath.Vec2f newRightPanelHeight, KMath.Vec2f newLeftPanelHeight, KMath.Vec2f newTopPanelHeight, KMath.Vec2f newBottomPanelHeight) {
        var index = PodComponentsLookup.VehiclePodStatus;
        var component = (Vehicle.Pod.StatusComponent)CreateComponent(index, typeof(Vehicle.Pod.StatusComponent));
        component.AgentsInside = newAgentsInside;
        component.RightPanel = newRightPanel;
        component.LeftPanel = newLeftPanel;
        component.TopPanel = newTopPanel;
        component.BottomPanel = newBottomPanel;
        component.RightPanelWidth = newRightPanelWidth;
        component.LeftPanelWidth = newLeftPanelWidth;
        component.TopPanelWidth = newTopPanelWidth;
        component.BottomPanelWidth = newBottomPanelWidth;
        component.RightPanelHeight = newRightPanelHeight;
        component.LeftPanelHeight = newLeftPanelHeight;
        component.TopPanelHeight = newTopPanelHeight;
        component.BottomPanelHeight = newBottomPanelHeight;
        AddComponent(index, component);
    }

    public void ReplaceVehiclePodStatus(System.Collections.Generic.List<AgentEntity> newAgentsInside, KMath.Vec2f newRightPanel, KMath.Vec2f newLeftPanel, KMath.Vec2f newTopPanel, KMath.Vec2f newBottomPanel, KMath.Vec2f newRightPanelWidth, KMath.Vec2f newLeftPanelWidth, KMath.Vec2f newTopPanelWidth, KMath.Vec2f newBottomPanelWidth, KMath.Vec2f newRightPanelHeight, KMath.Vec2f newLeftPanelHeight, KMath.Vec2f newTopPanelHeight, KMath.Vec2f newBottomPanelHeight) {
        var index = PodComponentsLookup.VehiclePodStatus;
        var component = (Vehicle.Pod.StatusComponent)CreateComponent(index, typeof(Vehicle.Pod.StatusComponent));
        component.AgentsInside = newAgentsInside;
        component.RightPanel = newRightPanel;
        component.LeftPanel = newLeftPanel;
        component.TopPanel = newTopPanel;
        component.BottomPanel = newBottomPanel;
        component.RightPanelWidth = newRightPanelWidth;
        component.LeftPanelWidth = newLeftPanelWidth;
        component.TopPanelWidth = newTopPanelWidth;
        component.BottomPanelWidth = newBottomPanelWidth;
        component.RightPanelHeight = newRightPanelHeight;
        component.LeftPanelHeight = newLeftPanelHeight;
        component.TopPanelHeight = newTopPanelHeight;
        component.BottomPanelHeight = newBottomPanelHeight;
        ReplaceComponent(index, component);
    }

    public void RemoveVehiclePodStatus() {
        RemoveComponent(PodComponentsLookup.VehiclePodStatus);
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
public sealed partial class PodMatcher {

    static Entitas.IMatcher<PodEntity> _matcherVehiclePodStatus;

    public static Entitas.IMatcher<PodEntity> VehiclePodStatus {
        get {
            if (_matcherVehiclePodStatus == null) {
                var matcher = (Entitas.Matcher<PodEntity>)Entitas.Matcher<PodEntity>.AllOf(PodComponentsLookup.VehiclePodStatus);
                matcher.componentNames = PodComponentsLookup.componentNames;
                _matcherVehiclePodStatus = matcher;
            }

            return _matcherVehiclePodStatus;
        }
    }
}
