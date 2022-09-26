//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class NodeEntity {

    public Node.TagetComponent nodeTaget { get { return (Node.TagetComponent)GetComponent(NodeComponentsLookup.NodeTaget); } }
    public bool hasNodeTaget { get { return HasComponent(NodeComponentsLookup.NodeTaget); } }

    public void AddNodeTaget(int newAgentTargetID, int newMechTargetID, KMath.Vec2f newTargetPos) {
        var index = NodeComponentsLookup.NodeTaget;
        var component = (Node.TagetComponent)CreateComponent(index, typeof(Node.TagetComponent));
        component.AgentTargetID = newAgentTargetID;
        component.MechTargetID = newMechTargetID;
        component.TargetPos = newTargetPos;
        AddComponent(index, component);
    }

    public void ReplaceNodeTaget(int newAgentTargetID, int newMechTargetID, KMath.Vec2f newTargetPos) {
        var index = NodeComponentsLookup.NodeTaget;
        var component = (Node.TagetComponent)CreateComponent(index, typeof(Node.TagetComponent));
        component.AgentTargetID = newAgentTargetID;
        component.MechTargetID = newMechTargetID;
        component.TargetPos = newTargetPos;
        ReplaceComponent(index, component);
    }

    public void RemoveNodeTaget() {
        RemoveComponent(NodeComponentsLookup.NodeTaget);
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
public sealed partial class NodeMatcher {

    static Entitas.IMatcher<NodeEntity> _matcherNodeTaget;

    public static Entitas.IMatcher<NodeEntity> NodeTaget {
        get {
            if (_matcherNodeTaget == null) {
                var matcher = (Entitas.Matcher<NodeEntity>)Entitas.Matcher<NodeEntity>.AllOf(NodeComponentsLookup.NodeTaget);
                matcher.componentNames = NodeComponentsLookup.componentNames;
                _matcherNodeTaget = matcher;
            }

            return _matcherNodeTaget;
        }
    }
}
