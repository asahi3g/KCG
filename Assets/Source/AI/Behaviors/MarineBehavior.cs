

using KMath;
using System.Collections.Generic;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace AI.BehaviorTree
{
    public static class MarineBehavior
    {
        /// <returns> return Id of root</returns>
        public static AgentController GetAgentController(Contexts enititasContext, int agentID)
        {
            AgentController marineController = new AgentController();
            marineController.BlackBoard = new BlackBoard(agentID);
            marineController.Sensors = new List<Sensor.SensorBase>();
            marineController.AttachSensors(new Sensor.EnemySensor());
            marineController.BehaviorTreeRoot = GetBehaviorTree(enititasContext, marineController, agentID);

            return marineController;
        }
        public static int GetBehaviorTree(Contexts entitasContext, AgentController agentController, int agentID)
        {
            int targetID = agentController.BlackBoard.Register(typeof(Vec2f), "Target");

            NodeEntity root = GameState.BehaviorTreeCreationAPI.CreateTree(entitasContext, agentID);
            NodeEntity child = GameState.BehaviorTreeCreationAPI.AddReapterNode(root);
            
            child = GameState.BehaviorTreeCreationAPI.AddSelector(child);
            child.AddNodeBlackboardData(agentController.Sensors[0].VariableID);
            
            NodeEntity child1 = GameState.BehaviorTreeCreationAPI.AddSequence(child);
            NodeEntity leafNode = GameState.BehaviorTreeCreationAPI.AddLeaf(child1, Enums.NodeType.SelectClosestTarget);
            leafNode.AddNodeBlackboardData(targetID);
            leafNode = GameState.BehaviorTreeCreationAPI.AddLeaf(child1, Enums.NodeType.ShootFireWeaponAction);
            leafNode.AddNodeBlackboardData(targetID);

            AgentEntity agentEntity = entitasContext.agent.GetEntityWithAgentID(leafNode.nodeOwner.AgentID);
            InventoryEntity inventoryEntity = entitasContext.inventory.GetEntityWithInventoryID(agentEntity.agentInventory.InventoryID);
            ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(entitasContext,
                agentEntity.agentInventory.InventoryID, inventoryEntity.inventoryEntity.SelectedSlotID);
            leafNode.AddNodeTool(itemInventory.itemID.ID);

            NodeEntity child2 = GameState.BehaviorTreeCreationAPI.AddSequence(child);
            leafNode = GameState.BehaviorTreeCreationAPI.AddLeaf(child2, Enums.NodeType.WaitAction);
            leafNode.AddNodeDuration(1f);

            return root.nodeID.ID;
        }
    }
}
