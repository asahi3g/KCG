using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;

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

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            foreach (var node in AISystemState.Nodes)
            {
                if (node == null)
                    continue;

                Vector2 nodePosition = this.ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);

                switch (node.NodeGroup)
                {
                    case NodeGroup.DecoratorNode:
                        evt.menu.AppendAction($"Add Decorator Node/{node.Type.ToString()}", (a) => CreateNodeView(node.Type, nodePosition));
                        break;
                    case NodeGroup.CompositeNode:
                        evt.menu.AppendAction($"Add Composite Node/{node.Type.ToString()}", (a) => CreateNodeView(node.Type, nodePosition));
                        break;
                    case NodeGroup.ActionNode:
                        evt.menu.AppendAction($"Add Action Node/{node.Type.ToString()}", (a) => CreateNodeView(node.Type, nodePosition));
                        break;
                }
            }
        }

        public void PopulateView()
        {
            CreateNodeView(NodeType.DecoratorNode, Vector2.zero, true);
        }

        static int i = 0;
        private void CreateNodeView(NodeType type, Vector2 pos, bool isRoot = false)
        {
            NodeEntity nodeEntity = Contexts.sharedInstance.node.CreateEntity();
            nodeEntity.AddNodeID(i++, type);
            switch (AISystemState.Nodes[(int)type].NodeGroup)
            {
                case NodeGroup.DecoratorNode:
                    nodeEntity.AddNodesDecorator(-1);
                    break;
                case NodeGroup.CompositeNode:
                    nodeEntity.AddNodeComposite(new List<int>(), -1);
                    break;
            }
            nodeEntity.isNodeRoot = isRoot;
            NodeView nodeView = new NodeView(nodeEntity, pos);
            AddElement(nodeView);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()!.Where(endPort =>
                          endPort.direction != startPort.direction &&
                          endPort.node != startPort.node).ToList();
        }
    }
}
