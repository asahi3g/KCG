namespace Animation
{
    public class UpdateSystem
    {

        public void Update(float deltaTime)
        {
            var entities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AnimationState));

            foreach (var entity in entities)
            {
                var state = entity.animationState;
                state.State.Update(deltaTime, state.AnimationSpeed);

                entity.ReplaceAnimationState(state.AnimationSpeed, state.State);
            }

        }
    }
}

