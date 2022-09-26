using AI.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Node
{
    // 
    public class SchedulerSystem
    {
        public NodeBase[] Nodes;


        public void Initialize()
        {
            int length = Enum.GetNames(typeof(Enums.NodeType)).Length;
            Nodes = new NodeBase[length];

            foreach (Type type in Assembly.GetAssembly(typeof(NodeBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(NodeBase))))
            {
                NodeBase node = (NodeBase)Activator.CreateInstance(type);
                Nodes[(int)node.Type] = node;
            }
        }

        public void Update(Contexts contexts, float deltaTime, ref Planet.PlanetState planet)
        {
            NodeEntity[] nodes = contexts.node.GetEntities();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].hasNodeExecution)
                {
                    int index = (int)nodes[i].nodeID.TypeID;
                    switch (nodes[i].nodeExecution.State)
                    {
                        case Enums.NodeState.Entry:
                            Nodes[index].OnEnter(ref planet, nodes[i]);
                            break;
                        case Enums.NodeState.Running:
                            Nodes[index].OnUpdate(ref planet, nodes[i]);
                            break;
                        case Enums.NodeState.Success:
                            Nodes[index].OnExit(ref planet, nodes[i]);
                            break;
                        case Enums.NodeState.Fail:
                            Nodes[index].OnExit(ref planet, nodes[i]);
                            break;
                        default:
                            Debug.Log("Not valid Action state.");
                            break;
                    }
                }
            }
        }
    }
}
