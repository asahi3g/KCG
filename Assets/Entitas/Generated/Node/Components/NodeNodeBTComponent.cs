//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class NodeEntity {

    static readonly Node.BTComponent nodeBTComponent = new Node.BTComponent();

    public bool isNodeBT {
        get { return HasComponent(NodeComponentsLookup.NodeBT); }
        set {
            if (value != isNodeBT) {
                var index = NodeComponentsLookup.NodeBT;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : nodeBTComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<NodeEntity> _matcherNodeBT;

    public static Entitas.IMatcher<NodeEntity> NodeBT {
        get {
            if (_matcherNodeBT == null) {
                var matcher = (Entitas.Matcher<NodeEntity>)Entitas.Matcher<NodeEntity>.AllOf(NodeComponentsLookup.NodeBT);
                matcher.componentNames = NodeComponentsLookup.componentNames;
                _matcherNodeBT = matcher;
            }

            return _matcherNodeBT;
        }
    }
}