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

            PopulateView();

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/AI/Editor/Resources/BehaviorTreeEditorStyle.uss");
            styleSheets.Add(styleSheet);
        }

        public void Init(int Id)
        {
            ID = Id;
        }

        public void PopulateView()
        {
            ref BehaviorTree.BehaviorTreeExecute bt = ref GameState.BehaviorTreeManager.Get(ID);
            NodeSystem.Node currentNode = GameState.NodeManager.Get(bt.RootNodeId);

            // Get number of leaf nodes.
            

            //for (int j = 0; j < bt.; j++)
            //{
            //    CreateNodeView(AISystemState.Behaviors.Get((int)Type).Nodes[j]);
            //}
            //
            //for (int j = 0; j < AISystemState.Behaviors.Get((int)Type).Nodes.Count; j++)
            //{
            //    if (NodeViews[j].Node.children == null)
            //        continue;
            //
            //    foreach (var index in NodeViews[j].Node.children)
            //    {
            //        Edge edge = NodeViews[j].Output.ConnectTo(NodeViews[index].Input);
            //        AddElement(edge);
            //    }
            //}
        }

        public void ClearTree()
        {
            NodeViews.Clear();
            DeleteElements(graphElements);
        }

        private NodeView CreateNodeView(int nodeID)
        {
            NodeView nodeView = new NodeView(nodeID);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
            NodeViews.Add(nodeView);
            return nodeView;
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
