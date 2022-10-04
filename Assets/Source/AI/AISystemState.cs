using System.Linq;
using System.Reflection;
using Node;
using Enums;
using System;

namespace AI
{
    public static class AISystemState
    {
        static AISystemState()
        {
            int length = Enum.GetNames(typeof(Enums.NodeType)).Length - 1;
            Nodes = new NodeBase[length];

            foreach (Type type in Assembly.GetAssembly(typeof(NodeBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(NodeBase))))
            {
                NodeBase node = (NodeBase)Activator.CreateInstance(type);
                Nodes[(int)node.Type] = node;
            }
        }

        public static NodeBase[] Nodes;
    }
}
