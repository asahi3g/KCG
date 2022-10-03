using System.Windows.Forms.DataVisualization.Charting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AI.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public new class UxmlFactory : UxmlFactory<NodeView, GraphView.UxmlTraits> { }

        public static NodeView CreateNewNode(NodeEntity nodeEntity)
        { 
            NodeView nodeView = new NodeView();
            nodeView.title = nodeEntity.nodeID.TypeID.ToString();
            return nodeView;
        }
    }
}
