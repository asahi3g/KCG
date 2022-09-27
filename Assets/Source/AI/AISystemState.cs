using System.Linq;
using System.Reflection;
using Node;
using Enums;
using System;

namespace AI
{
    public static class SystemState
    {
        static SystemState()
        {
            int length = Enum.GetNames(typeof(Enums.NodeType)).Length - 1;
            Nodes = new NodeBase[length];

            int actionNodesLength = 0;
            foreach (Type type in Assembly.GetAssembly(typeof(NodeBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(NodeBase))))
            {
                NodeBase node = (NodeBase)Activator.CreateInstance(type);
                Nodes[(int)node.Type] = node;
                if (node.IsActionNode)
                    actionNodesLength++;
            }
            
            ActionNodes =   new NodeType[actionNodesLength];
            BTNodes =       new NodeType[length - actionNodesLength];

            for (int i = 0, actionIndex = 0, btIndex = 0; i < actionNodesLength; i++)
            {
                if (Nodes[i].IsActionNode)
                    ActionNodes[actionIndex++] = Nodes[i].Type;
                else
                    BTNodes[btIndex++] = Nodes[i].Type;
            }
        }

        // Todo: Make it ReadOnly.
        public static NodeBase[] Nodes;
        public static NodeType[] ActionNodes;
        public static NodeType[] BTNodes;
    }
}
