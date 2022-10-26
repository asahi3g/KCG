using System.Collections.Generic;
using AI.Sensor;
using Enums;

namespace AI
{
    public struct BehaviorProperties
    {
        public BehaviorType TypeID;
        public string Name;
        public List<NodeInfo> Nodes;
        public BlackBoardModel BlackBoard;
        public BlackBoardModel SquadBlackBoard;
        public SensorEntity[] Sensors;
        public int SensorCount;


        int CreateTree(int agentID, int index)
        {
            NodeInfo node = Nodes[index];
            int nodeID;
            int entriesCount = 0;
            if (node.entriesID != null)
                entriesCount = node.entriesID.Count;

            if (entriesCount != 0)
                nodeID = GameState.ActionCreationSystem.CreateBehaviorTreeNode(node.type, agentID, node.entriesID.ToArray());
            else
                nodeID = GameState.ActionCreationSystem.CreateBehaviorTreeNode(node.type, agentID);

            if (node.children == null)
                return nodeID;

            NodeEntity nodeEntity = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);
            for (int i = 0; i < node.children.Count; i++)
            {
                int childId = CreateTree(agentID, node.children[i]);
                switch (AISystemState.Nodes[(int)node.type].NodeGroup)
                {
                    case NodeGroup.CompositeNode:
                        nodeEntity.nodeComposite.Children.Add(childId);
                        break;
                    case NodeGroup.DecoratorNode:
                        nodeEntity.nodeDecorator.ChildID = childId;
                        break;
                }
            }
            return nodeID;
        }
        int CreateTree(int agentID) => CreateTree(agentID, 0);

        public AgentController InstatiateBehavior(int agentID)
        {
            AgentController controller = new AgentController();
            controller.Sensors = new SensorEntity[SensorCount];
            for (int i = 0; i < SensorCount; i++)
            {
                controller.Sensors[i] = Sensors[i];
            }
            controller.BlackBoard = BlackBoard.Data;
            controller.BehaviorTreeRoot = CreateTree(agentID);
            controller.behaviorType = TypeID;

            return controller;
        }
    }
}
