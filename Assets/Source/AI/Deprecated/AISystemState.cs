//imports UnityEngine

using System;
using System.Linq;
using System.Reflection;
using Node;
using Enums;
using AI.Sensor;

namespace AI
{
    public static class AISystemState
    {
        static AISystemState()
        {
            CreateNodes();
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

        public static NodeBase[] Nodes;
        public static SensorBase[] Sensors;
    }
}
