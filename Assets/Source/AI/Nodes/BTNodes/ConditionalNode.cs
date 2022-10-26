//imports UnityEngine

using Enums;
using Planet;
using AI;
using System.Collections.Generic;
using System;

namespace Node
{
    public class ConditionalNode : NodeBase
    {
        public override NodeType Type => NodeType.ConditionalNode;
        public override NodeGroup NodeGroup => NodeGroup.DecoratorNode;

        public override List<Tuple<string, Type>> RegisterEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("Conditional", typeof(bool)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(NodeEntity nodeEntity)
        {
            NodeEntity child = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodeDecorator.ChildID);
            child.nodeExecution.State = NodeState.Entry;
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public override void OnUpdate(NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            BlackBoard blackBoard = agentEntity.agentController.Controller.BlackBoard;
            NodeEntity child = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodeDecorator.ChildID);

            bool conditional;
            blackBoard.Get(nodeEntity.nodeBlackboardData.entriesIDs[0], out conditional);
            if (!conditional)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            ref var nodes = ref AISystemState.Nodes;
            int index = (int)child.nodeID.TypeID;
            switch (child.nodeExecution.State)
            {
                case NodeState.Entry:
                    nodes[index].OnEnter(child);
                    break;
                case NodeState.Running:
                    nodes[index].OnUpdate(child);
                    break;
                case NodeState.Success:
                    nodes[index].OnExit(child);
                    nodeEntity.nodeExecution.State = NodeState.Success;
                    break;
                case NodeState.Fail:
                    nodes[index].OnFail(child);
                    nodeEntity.nodeExecution.State = NodeState.Fail;
                    break;
                default:
                    UnityEngine.Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
