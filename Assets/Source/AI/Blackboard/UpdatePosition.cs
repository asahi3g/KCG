namespace AI
{
    // Todo: Temporary solution (Move this to movement system.)
    public class UpdatePosition
    {
        public void Update()
        {
            for (int i = 0; i < GameState.BlackboardManager.Length; i++)
            {
                ref Blackboard blackboard = ref GameState.BlackboardManager.Get(i);
                if (blackboard.UpdateTarget)
                {
                    AgentEntity target = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(blackboard.AgentTargetID);
                    blackboard.AttackTarget = target.agentPhysicsState.Position + target.physicsBox2DCollider.Size * 1f / 2f;
                }
            }
        }
    }
}
