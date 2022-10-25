namespace NodeSystem.BehaviorTree
{
    public class UpdateSystem
    {
        public void Update()
        {
            int length = GameState.BehaviorTreeManager.GetLength();
            for (int i = 0; i < length; i++)
            {
                GameState.BehaviorTreeManager.Get(i).UpdateTree();
            }
        }
    }
}
