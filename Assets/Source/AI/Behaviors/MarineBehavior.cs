

namespace AI.BehaviorTree
{
    public static class MarineBehavior
    {
        /// <returns> return Id of root</returns>
        public static int GetBehaviorTree(Contexts enititasContext, int agentID)
        { 
            NodeEntity root = GameState.BehaviorTreeCreationAPI.CreateTree(enititasContext, agentID);
            // Add Selector if false idle if not combat.

            NodeEntity child = GameState.BehaviorTreeCreationAPI.AddReapterNode(root);
            child = GameState.BehaviorTreeCreationAPI.AddSequence(child);
            NodeEntity waitNode = GameState.BehaviorTreeCreationAPI.AddLeaf(child, Enums.NodeType.WaitAction);
            waitNode.AddNodeDuration(5f);
            waitNode = GameState.BehaviorTreeCreationAPI.AddLeaf(child, Enums.NodeType.WaitAction);
            waitNode.AddNodeDuration(3f);

            return root.nodeID.ID;
        }
    }
}
