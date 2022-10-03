using AI.Editor;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AI
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory :
            UxmlFactory<BehaviorTreeView,
                        GraphView.UxmlTraits>
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

        private void CreateNodeView()
        {
            //NodeView nodeView = NodeView.CreateNewNode();
            //AddElement(nodeView);
        }
    }
}
