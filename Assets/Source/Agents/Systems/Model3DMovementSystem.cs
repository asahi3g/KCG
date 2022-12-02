using Enums;
using System.Diagnostics;

namespace Agent
{
    public class Model3DMovementSystem
    {
        public void Update()
        {
            var entities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentAgent3DModel));
            
            foreach (AgentEntity agentEntity in entities)
            {
                PhysicsStateComponent physicsStateComponent = agentEntity.agentPhysicsState;
                Agent3DModel agent3DModel = agentEntity.agentAgent3DModel;
                
                agent3DModel.SetPosition(physicsStateComponent.Position.X, physicsStateComponent.Position.Y);

                if (physicsStateComponent.FacingDirection == 1)
                {
                    if(agent3DModel.CurrentWeapon != Model3DWeaponType.Pistol || agent3DModel.CurrentWeapon != Model3DWeaponType.Rifle)
                    {
                        agent3DModel.SetRotation(90f);
                    }
                    else
                    {
                        agent3DModel.SetRotation(0f);
                    }
                }
                else if (physicsStateComponent.FacingDirection == -1)
                {
                    if (agent3DModel.CurrentWeapon != Model3DWeaponType.Pistol || agent3DModel.CurrentWeapon != Model3DWeaponType.Rifle)
                    {
                        agent3DModel.SetRotation(-120f);
                    }
                    else
                    {
                        agent3DModel.SetRotation(0f);
                    }
                }
            }
        }
    }
}