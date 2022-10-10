using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using System;
using KMath;
using static Unity.VisualScripting.Metadata;

namespace AI
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory :
            UxmlFactory<BehaviorTreeView, UxmlTraits>
        { }

        public Action<NodeView> OnNodeSelected;
        public List<NodeView> NodeViews;

        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            DefaultPopulateView();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem => {
                    Edge edge = elem as Edge;
                    if (edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        parentView.RemoveChild(childView);
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge => {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    parentView.AddChild(childView);
                });
            }

            return graphViewChange;
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
                        evt.menu.AppendAction($"Add Decorator Node/{node.Type.ToString()}", (a) => 
                            CreateNodeView(node.Type, new Vec2f(nodePosition.x, nodePosition.y)));
                        break;
                    case NodeGroup.CompositeNode:
                        evt.menu.AppendAction($"Add Composite Node/{node.Type.ToString()}", (a) => 
                            CreateNodeView(node.Type, new Vec2f(nodePosition.x, nodePosition.y)));
                        break;
                    case NodeGroup.ActionNode:
                        evt.menu.AppendAction($"Add Action Node/{node.Type.ToString()}", (a) => 
                            CreateNodeView(node.Type, new Vec2f(nodePosition.x, nodePosition.y)));
                        break;
                }
            }
            evt.menu.AppendSeparator();
            evt.menu.AppendAction($"Delete", (a) => DeleteSelected());
        }

        public void PopulateView(int i)
        {
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            for (int j = 0; j < AISystemState.Behaviors[i].Nodes.Count; j++)
            {
                CreateNodeView(AISystemState.Behaviors[i].Nodes[j]);
            }

            for (int j = 0; j < AISystemState.Behaviors[i].Nodes.Count; j++)
            {
                foreach (var index in NodeViews[j].Node.children)
                {
                    Edge edge = NodeViews[j].Output.ConnectTo(NodeViews[index].Input);
                    AddElement(edge);
                }
            }
        }

        public void DefaultPopulateView()
        {
            // Clear elements
            graphViewChanged -= OnGraphViewChanged;        
            CreateNodeView(NodeType.DecoratorNode, SetRootPos());
            graphViewChanged += OnGraphViewChanged;
        }

        private NodeView CreateNodeView(NodeInfo node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
            NodeViews.Add(nodeView);
            return nodeView;
        }

        private void CreateNodeView(NodeType nodeType, Vec2f position)
        {
            NodeInfo node = new NodeInfo
            {
                index = NodeViews.Count,
                type = nodeType,
                pos = position,
                children = (AISystemState.GetNodeGroup(nodeType) == NodeGroup.ActionNode) ? null : new List<int>()
            };
            CreateNodeView(node);
        }

        private void DeleteSelected()
        {
            // Delete selected.
            DeleteSelection();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()!.Where(endPort =>
                          endPort.direction != startPort.direction &&
                          endPort.node != startPort.node).ToList();
        }

        private Vec2f SetRootPos() => new Vec2f((resolvedStyle.width / 2.0f - NodeView.Width / 2.0f), resolvedStyle.height * 0.2f);
    }
}
