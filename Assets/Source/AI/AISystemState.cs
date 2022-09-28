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
        }

        // Todo: Make it ReadOnly.
        public static NodeBase[] Nodes;
    }
}
