//imports UnityEngine

using System;
using System.Linq;
using System.Reflection;
using Node;
using Enums;

namespace AI
{
    public static class AISystemState
    {
        static AISystemState()
        {
            CreateNodes();
        }
        public static NodeGroup GetNodeGroup(ItemUsageActionType  type)
        {
            return Nodes[(int)type].NodeGroup;
        }

        static void CreateNodes()
        {
            int length = Enum.GetNames(typeof(ItemUsageActionType )).Length - 1;
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
