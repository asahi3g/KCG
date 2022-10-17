using Enums;
using Planet;
using UnityEngine;
using AI;
using KMath;
using System.Collections.Generic;
using System;

namespace Node
{
    // Todo(Urgent): Implement this.
    public class SelectorNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.SelectorNode; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.CompositeNode; } }
        public override List<Tuple<string, Type>> RegisterStates()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("Selector", typeof(bool)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeComposite.CurrentID = 0;
            var children = nodeEntity.nodeComposite.Children;
            foreach (int childID in children)
            {
                NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(childID);
                child.nodeExecution.State = NodeState.Entry;
            }
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        // Todo: Allow selection between more than two nodes.
        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            BlackBoard blackBoard = agentEntity.agentController.Controller.BlackBoard;
            int index = 1;
            bool first = false;
            blackBoard.Get(nodeEntity.nodeBlackboardData.entriesIDs[0], out first);
            if (first)
                index = 0;

            NodeEntity child = planet.EntitasContext.node.GetEntityWithNodeIDID(nodeEntity.nodeComposite.Children[index]);
            ref var nodes = ref AISystemState.Nodes;
            index = (int)child.nodeID.TypeID;
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
                    nodeEntity.nodeExecution.State = NodeState.Success;
                    break;
                case NodeState.Fail:
                    nodes[index].OnExit(ref planet, child);
                    nodeEntity.nodeExecution.State = NodeState.Fail;
                    break;
                default:
                    Debug.Log("Not valid Action state.");
                    break;
            }
        }
    }
}
