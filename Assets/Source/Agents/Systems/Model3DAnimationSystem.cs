//import UnityEngine

namespace Agent
{
    public class Model3DAnimationSystem
    {
        public void Update()
        {
            
            float deltaTime = UnityEngine.Time.deltaTime;
            var entities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentAgent3DModel));

            foreach (var entity in entities)
            {
                PhysicsStateComponent physicsState = entity.agentPhysicsState;        
                Agent3DModel agent3DModel = entity.agentAgent3DModel;

                /*if (entity.isAgentPlayer)
                {
                    Debug.Log(physicsState.MovementState);
                }*/

                Animancer.AnimancerState currentClip = null;

                {
                    AgentAnimation agentAnimation = 
                        GameState.AgentMovementAnimationTable.GetAnimation(physicsState.MovementState, agent3DModel.AnimationType, agent3DModel.ItemAnimationSet);


                    UnityEngine.AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(agentAnimation.Animation);
                    currentClip = agent3DModel.Renderer.GetAnimancer().Play(animation, agentAnimation.FadeTime);
                    currentClip.Speed = agentAnimation.Speed + agentAnimation.MovementSpeedFactor * (System.Math.Abs(physicsState.Velocity.X) / 7.0f);


                    if (currentClip.RemainingDuration <= 0.0f && agentAnimation.Looping)
                    {
                        currentClip.Time = agentAnimation.StartTime;
                    }

                    if (agentAnimation.UseActionDurationForSpeed)
                    {
                        currentClip.Speed = currentClip.Speed / physicsState.ActionDuration;
                    }


                    if (currentClip != null && (physicsState.SetMovementState || physicsState.LastAgentAnimation.Equals(agentAnimation)))
                    {
                        physicsState.SetMovementState = false;
                        currentClip.Time = agentAnimation.StartTime;
                    }

                    physicsState.LastAgentAnimation = agentAnimation;
                }
            }
        }
    }
}
