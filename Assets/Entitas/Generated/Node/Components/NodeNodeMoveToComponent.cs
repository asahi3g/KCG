//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class NodeEntity {

    public Node.MoveToComponent nodeMoveTo { get { return (Node.MoveToComponent)GetComponent(NodeComponentsLookup.NodeMoveTo); } }
    public bool hasNodeMoveTo { get { return HasComponent(NodeComponentsLookup.NodeMoveTo); } }

    public void AddNodeMoveTo(KMath.Vec2f newGoalPosition) {
        var index = NodeComponentsLookup.NodeMoveTo;
        var component = (Node.MoveToComponent)CreateComponent(index, typeof(Node.MoveToComponent));
        component.GoalPosition = newGoalPosition;
        AddComponent(index, component);
    }

    public void ReplaceNodeMoveTo(KMath.Vec2f newGoalPosition) {
        var index = NodeComponentsLookup.NodeMoveTo;
        var component = (Node.MoveToComponent)CreateComponent(index, typeof(Node.MoveToComponent));
        component.GoalPosition = newGoalPosition;
        ReplaceComponent(index, component);
    }

    public void RemoveNodeMoveTo() {
        RemoveComponent(NodeComponentsLookup.NodeMoveTo);
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

    static Entitas.IMatcher<NodeEntity> _matcherNodeMoveTo;

    public static Entitas.IMatcher<NodeEntity> NodeMoveTo {
        get {
            if (_matcherNodeMoveTo == null) {
                var matcher = (Entitas.Matcher<NodeEntity>)Entitas.Matcher<NodeEntity>.AllOf(NodeComponentsLookup.NodeMoveTo);
                matcher.componentNames = NodeComponentsLookup.componentNames;
                _matcherNodeMoveTo = matcher;
            }

            return _matcherNodeMoveTo;
        }
    }
}
