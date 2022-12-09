using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using System;
using KMath;
using UnityEditor;

namespace AI
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory :
            UxmlFactory<BehaviorTreeView, UxmlTraits>
        { }

        public Action<NodeView> OnNodeSelected;
        public int ID;
        public List<NodeView> NodeViews;

        public BehaviorTreeView()
        {
            NodeViews = new List<NodeView>();
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/AI/Editor/Resources/BehaviorTreeEditorStyle.uss");
            styleSheets.Add(styleSheet);
        }

        public void Init(int Id)
        {
            ID = Id;
            PopulateView();
        }

        public void PopulateView()
        {
            const int NODES_GAPX = 30;

            // Create nodeViews.
            ref BehaviorTree.BehaviorTreeExecute bt = ref GameState.BehaviorTreeManager.Get(ID);
            NodeView rootView = new NodeView(bt.RootNodeId, isEntryNode: true);
            NodeViews.Add(rootView);
            AddElement(rootView);
            CreateChildsNodeView(rootView);

            // Set X position of leaf nodes.
            int NumLeafNodes = 0;
            foreach (NodeView nodeView in NodeViews)
            {
                if (nodeView.IsLeafNode())
                {
                    NumLeafNodes++;
                    nodeView.SetPos(new Vec2f(NumLeafNodes * (NodeView.Width + NODES_GAPX), nodeView.Position.Y));
                }
            }
            // Set X position of other nodes.
            for (int i = (NodeViews.Count - 1); i >= 0; i--)
            {
                NodeView nodeView = NodeViews[i];
                if (!nodeView.IsLeafNode())
                {
                    NodeSystem.Node node = GameState.NodeManager.Get(nodeView.nodeID);
                    int ChildrenCount = node.Children.Length;
                    if (ChildrenCount == 1)
                    {
                        nodeView.SetPos(new Vec2f(NodeViews[i + 1].Position.X, nodeView.Position.Y));
                    }
                    else
                    {
                        NodeView firstChild = NodeViews[i + 1];
                        NodeView lastChild = GetNodeViewByID(node.Children[ChildrenCount - 1]);
                        nodeView.SetPos(new Vec2f((firstChild.Position.X + lastChild.Position.X)/2, nodeView.Position.Y));
                    }
                }
            }
            
            foreach (NodeView nodeView in NodeViews)
            {
                if (nodeView.IsLeafNode())
                    continue;
                NodeSystem.Node node = GameState.NodeManager.Get(nodeView.nodeID);
                
                foreach (int childId in node.Children)
                {
                    NodeView inputNode = GetNodeViewByID(childId);
                    Edge edge = nodeView.Output.ConnectTo(inputNode.Input);
                    AddElement(edge);
                }
            }
        }

        // Recursive function: Add childs of node to NodeViews list.
        void CreateChildsNodeView(NodeView parentNodeView)
        {
            const int NODES_GAPY = 50;

            NodeSystem.Node node = GameState.NodeManager.Get(parentNodeView.nodeID);
            if (node.Children != null)
            {
                foreach (int childId in node.Children)
                {
                    NodeView childView = CreateNodeView(childId);
                    childView.SetPos(new Vec2f(0f, parentNodeView.Position.Y + NodeView.Height + NODES_GAPY)); // Set only y here.
                    if (!childView.IsLeafNode())
                        CreateChildsNodeView(childView);
                }
            }
        }

        NodeView GetNodeViewByID(int nodeID)
        {
            Debug.Log("testing");
            foreach (var node in NodeViews)
            { 
                if (node.nodeID == nodeID)
                    return node;
            }
            return null;
        }

        public void ClearTree()
        {
            NodeViews.Clear();
            DeleteElements(graphElements);
        }

        private NodeView CreateNodeView(int nodeID)
        {
            NodeView nodeView = new NodeView(nodeID);
            NodeViews.Add(nodeView);
            AddElement(nodeView);
            return nodeView;
        }
    }
}

