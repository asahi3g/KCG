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
            CreateNodes();
            CreateBehaviors();
        }

        static void CreateNodes()
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

        static void CreateBehaviors()
        {
            int length = Enum.GetNames(typeof(BehaviorType)).Length - 1;
            Behaviors = new BehaviorProperties[length];

            for (int i = 0; i < length; i++)
            {
                Behaviors[i].TypeID = (BehaviorType)i;
                Behaviors[i].RootID = -1;
            }

            foreach (Type type in Assembly.GetAssembly(typeof(BehaviorBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BehaviorBase))))
            {
                BehaviorBase behavior = (BehaviorBase)Activator.CreateInstance(type);
                BehaviorProperties behaviorProperties = new BehaviorProperties();
                Behaviors[(int)behaviorProperties.TypeID].RootID = behavior.BehaviorTreeGenerator();
            }

            {
                BehaviorBase behavior = new BehaviorBase();
                for (int i = 0; i < length; i++)
                {
                    if (Behaviors[i].RootID == -1)
                        Behaviors[i].RootID = behavior.BehaviorTreeGenerator();
                }
            }
        }

        public static NodeBase[] Nodes;
        public static BehaviorProperties[] Behaviors;
    }
}
