using System;
using System.Linq;
using System.Reflection;
using Node;
using Enums;
using UnityEngine;

namespace AI
{
    public static class AISystemState
    {
        static AISystemState()
        {
            CreateNodes();
            CreateBehaviors();
        }

        public static NodeGroup GetNodeGroup(NodeType type)
        {
            return Nodes[(int)type].NodeGroup;
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
            int length = Enum.GetNames(typeof(BehaviorType)).Length;
            Behaviors = new BehaviorProperties[length];

            for (int i = 0; i < length; i++)
            {
                Behaviors[i].TypeID = (BehaviorType)i;
                Behaviors[i].Nodes = null;
            }

            foreach (Type type in Assembly.GetAssembly(typeof(BehaviorBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BehaviorBase))))
            {
                BehaviorBase behavior = (BehaviorBase)Activator.CreateInstance(type);
                Behaviors[(int)behavior.Type].Nodes = behavior.Nodes;
                Behaviors[(int)behavior.Type].Name = behavior.Name;
            }

            {
                BehaviorBase behavior = new BehaviorBase();

                for (int i = 0; i < length; i++)
                {
                    if (Behaviors[i].Nodes == null)
                    {
                        Behaviors[i].Nodes = null;
                        Behaviors[i].Name = ((BehaviorType)i).ToString();
                    }
                }
            }
        }

        public static int CreateNewBehavior(string name)
        {
            int index = Contains(name);
            int j = 1;
            string newName = name;
            while (index != -1)
            {
                newName = name + j.ToString();
                index = Contains(newName);
                j++;
            }

            int oldLength = Behaviors.Length;
            Array.Resize<BehaviorProperties>(ref Behaviors, oldLength + 1);
            Debug.Log(oldLength);
            BehaviorBase behavior = new BehaviorBase();
            Behaviors[Behaviors.Length - 1].Nodes = behavior.Nodes;
            Behaviors[Behaviors.Length - 1].TypeID = (BehaviorType)oldLength;
            Behaviors[Behaviors.Length - 1].Name = newName;

            return oldLength;
        }

        static int Contains(string name)
        {
            for (int i = 0; i < Behaviors.Length; i++)
            {
                if (Behaviors[i].Name == name)
                    return i;
            }
            return -1;
        }

        public static NodeBase[] Nodes;
        public static BehaviorProperties[] Behaviors;
    }
}
