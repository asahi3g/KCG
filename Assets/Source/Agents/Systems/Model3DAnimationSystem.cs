//import UnityEngine

namespace Agent
{
    public class Model3DAnimationSystem
    {
        public void Update()
        {
            
            float deltaTime = UnityEngine.Time.deltaTime;
            var entities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));

            foreach (var entity in entities)
            {
                var physicsState = entity.agentPhysicsState;        
                var model3d = entity.agentModel3D;

                /*if (entity.isAgentPlayer)
                {
                    Debug.Log(physicsState.MovementState);
                }*/

                Animancer.AnimancerState currentClip = null;

                {
                    AgentAnimation agentAnimation = 
                        GameState.AgentMovementAnimationTable.GetAnimation(physicsState.MovementState, model3d.AnimationType, model3d.ItemAnimationSet);


                    UnityEngine.AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(agentAnimation.Animation);
                    currentClip = model3d.Renderer.GetAnimancer().Play(animation, agentAnimation.FadeTime);
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
