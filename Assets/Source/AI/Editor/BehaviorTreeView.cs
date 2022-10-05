using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using System;
using UnityEngine.Networking.Types;

namespace AI
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory :
            UxmlFactory<BehaviorTreeView, UxmlTraits>
        { }

        public Action<NodeView> OnNodeSelected;

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
            evt.menu.AppendSeparator();
            evt.menu.AppendAction($"Delete", (a) => DeleteSelection());
        }

        public void PopulateView(int nodeID)
        {
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            NodeEntity nodeEntity = Contexts.sharedInstance.node.GetEntityWithNodeIDID(nodeID);
            NodeView nodeView = CreateNodeView(nodeEntity, SetRootPos());

            PopulateChildren(nodeView, nodeEntity);
            graphViewChanged += OnGraphViewChanged;
        }

        private void PopulateChildren(NodeView nodeView, NodeEntity nodeEntity)
        {
            var children = nodeEntity.GetChildren(Contexts.sharedInstance.node);
            Vector2 pos = nodeView.Postion + new Vector2(-NodeView.Width * (children.Count - 1) * 0.6f, NodeView.Height * 1.2f);
            foreach (var child in children)
            {
                NodeView childNodeView = CreateNodeView(child, pos);
                PopulateChildren(childNodeView, child);
                pos += new Vector2(NodeView.Width * 1.2f, 0f);
            }
        }

        public void DefaultPopulateView()
        {
            graphViewChanged -= OnGraphViewChanged;
            int rootID = GameState.BehaviorTreeCreationAPI.CreateTree();
            GameState.BehaviorTreeCreationAPI.EndTree();
            NodeEntity nodeEntity = Contexts.sharedInstance.node.GetEntityWithNodeIDID(rootID);
            CreateNodeView(nodeEntity, SetRootPos());
            graphViewChanged += OnGraphViewChanged;
        }

        private NodeView CreateNodeView(NodeEntity nodeEntity, Vector2 pos)
        {
            NodeView nodeView = new NodeView(nodeEntity, pos);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
            return nodeView;
        }

        private void CreateNodeView(NodeType nodeType, Vector2 pos)
        {
            NodeEntity nodeEntity = GameState.BehaviorTreeCreationAPI.CreateBehaviorTreeNode(nodeType);
            NodeView nodeView = new NodeView(nodeEntity, pos);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()!.Where(endPort =>
                          endPort.direction != startPort.direction &&
                          endPort.node != startPort.node).ToList();
        }

        private Vector2 SetRootPos() => new Vector2((resolvedStyle.width / 2.0f - NodeView.Width / 2.0f), resolvedStyle.height * 0.2f);
    }
}
