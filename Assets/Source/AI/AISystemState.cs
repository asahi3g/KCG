using System;
using System.Linq;
using System.Reflection;
using Node;
using Enums;
using AI.Sensor;
using UnityEngine;

namespace AI
{
    public static class AISystemState
    {
        static AISystemState()
        {
            CreateNodes();
            CreateBehaviors();
            CreateSensors();
        }
        public static NodeGroup GetNodeGroup(NodeType type)
        {
            return Nodes[(int)type].NodeGroup;
        }

        static void CreateNodes()
        {
            int length = Enum.GetNames(typeof(NodeType)).Length - 1;
            Nodes = new NodeBase[length];

            foreach (Type type in Assembly.GetAssembly(typeof(NodeBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(NodeBase))))
            {
                NodeBase node = (NodeBase)Activator.CreateInstance(type);
                Nodes[(int)node.Type] = node;
            }
        }

        static void CreateSensors()
        {
            int length = Enum.GetNames(typeof(SensorType)).Length - 1;
            Sensors = new SensorBase[length];

            foreach (Type type in Assembly.GetAssembly(typeof(SensorBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(SensorBase))))
            {
                SensorBase sensor = (SensorBase)Activator.CreateInstance(type);
                Sensors[(int)sensor.Type] = sensor;
            }
        }

        static void CreateBehaviors()
        {
            int length = Enum.GetNames(typeof(BehaviorType)).Length;
            Behaviors = new BehaviorList();

            for (int i = 0; i < length; i++)
            {
                BehaviorProperties behavior = new BehaviorProperties();
                behavior.TypeID = (BehaviorType)i;
                behavior.Nodes = null;
                Behaviors.Add(behavior);
            }

            foreach (Type type in Assembly.GetAssembly(typeof(BehaviorBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BehaviorBase))))
            {
                BehaviorBase behavior = (BehaviorBase)Activator.CreateInstance(type);
                Behaviors.Get((int)behavior.Type).Nodes = behavior.Nodes;
                Behaviors.Get((int)behavior.Type).Name = behavior.Name;
                Behaviors.Get((int)behavior.Type).BlackBoard = behavior.AgentBlackboard;
                Behaviors.Get((int)behavior.Type).Sensors = behavior.Sensors;
                Behaviors.Get((int)behavior.Type).SensorCount = behavior.SensorCount;
            }

            {
                BehaviorBase behavior = new BehaviorBase();
                for (int i = 0; i < length; i++)
                {
                    if (Behaviors.Get(i).Nodes == null)
                    {
                        Behaviors.Get(i).Nodes = behavior.Nodes;
                        Behaviors.Get(i).Name = ((BehaviorType)i).ToString();
                        Behaviors.Get(i).BlackBoard = new BlackBoardModel((BehaviorType)i);
                        Behaviors.Get(i).Sensors = behavior.Sensors;
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
            BehaviorProperties behaviorProprerties = new BehaviorProperties();
            BehaviorBase behavior = new BehaviorBase();
            behaviorProprerties.Nodes = behavior.Nodes;
            behaviorProprerties.TypeID = (BehaviorType)oldLength;
            behaviorProprerties.Name = newName;
            Behaviors.Add(behaviorProprerties);

            return oldLength;
        }

        static int Contains(string name)
        {
            for (int i = 0; i < Behaviors.Length; i++)
            {
                if (Behaviors.Get(i).Name == name)
                    return i;
            }
            return -1;
        }

        public static NodeBase[] Nodes;
        public static SensorBase[] Sensors;
        public static BehaviorList Behaviors;
    }
}
