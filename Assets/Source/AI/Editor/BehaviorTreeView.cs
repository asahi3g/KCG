using AI.Editor;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AI
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory :
            UxmlFactory<BehaviorTreeView, UxmlTraits>
        { }

        NodeView RootNode;
        List<NodeView> Nodes = new List<NodeView>();

        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            PopulateView();
        }

        public void PopulateView()
        {
            CreateNodeView();
        }

        static int i = 0;
        private void CreateNodeView()
        {
            NodeEntity nodeEntity = Contexts.sharedInstance.node.CreateEntity();
            nodeEntity.AddNodeID(i++, Enums.NodeType.SelectorNode);
            nodeEntity.AddNodeComposite(new List<int>(), -1);
            nodeEntity.isNodeRoot = false;
            NodeView nodeView = new NodeView(nodeEntity);
            AddElement(nodeView);
        }
    }
}
