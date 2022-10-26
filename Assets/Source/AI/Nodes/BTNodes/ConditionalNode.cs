//imports UnityEngine

using Enums;
using Planet;
using AI;
using System.Collections.Generic;
using System;
using UnityEditor.Experimental.GraphView;

namespace Node
{
    public class ConditionalNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.ConditionalNode; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.DecoratorNode; } }

        public override List<Tuple<string, Type>> RegisterEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("Conditional", typeof(bool)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodeDecorator.ChildID);
            child.nodeExecution.State = NodeState.Entry;
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            BlackBoard blackBoard = agentEntity.agentController.Controller.BlackBoard;
            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodeDecorator.ChildID);

            bool conditional = false;
            blackBoard.Get(nodeEntity.nodeBlackboardData.entriesIDs[0], out conditional);
            if (!conditional)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }

            ref var nodes = ref AISystemState.Nodes;
            int index = (int)child.nodeID.TypeID;
            switch (child.nodeExecution.State)
            {
                case NodeState.Entry:
                    nodes[index].OnEnter(ref planet, child);
                    break;
                case NodeState.Running:
                    nodes[index].OnUpdate(ref planet, child);
                    break;
                case NodeState.Success:
                    nodes[index].OnExit(ref planet, child);
                    nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                    break;
                case NodeState.Fail:
                    nodes[index].OnFail(ref planet, child);
                    nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                    break;
                default:
                    UnityEngine.Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
