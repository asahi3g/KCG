namespace BehaviorTree
{
    public class BehaviorTreeManager
    {
        BehaviorTreeExecute[] BehaviorTrees = new BehaviorTreeExecute[1024];
        int Length = 0;
        public int GetLength() => Length;

        public ref BehaviorTreeExecute Get(int id) => ref BehaviorTrees[id];

        public int Instantiate(int rootNodeId, int agentId)
        {
            BehaviorTrees[Length] = new BehaviorTreeExecute(rootNodeId, agentId, Length);
            return Length++;
        }
    }
}
