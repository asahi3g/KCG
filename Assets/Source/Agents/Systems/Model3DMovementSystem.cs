using Enums;
using System.Diagnostics;

namespace Agent
{
    public class Model3DMovementSystem
    {
        public void Update()
        {
            var entities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
            
            foreach (AgentEntity agentEntity in entities)
            {
                PhysicsStateComponent physicsStateComponent = agentEntity.agentPhysicsState;
                Model3DComponent model3DComponent = agentEntity.agentModel3D;
                
                model3DComponent.SetPosition(physicsStateComponent.Position.X, physicsStateComponent.Position.Y);

                if (physicsStateComponent.FacingDirection == 1)
                {
                    if(model3DComponent.CurrentWeapon != Model3DWeaponType.Pistol || model3DComponent.CurrentWeapon != Model3DWeaponType.Rifle)
                    {
                        model3DComponent.SetRotation(90f);
                    }
                    else
                    {
                        model3DComponent.SetRotation(0f);
                    }
                }
                else if (physicsStateComponent.FacingDirection == -1)
                {
                    if (model3DComponent.CurrentWeapon != Model3DWeaponType.Pistol || model3DComponent.CurrentWeapon != Model3DWeaponType.Rifle)
                    {
                        model3DComponent.SetRotation(-120f);
                    }
                    else
                    {
                        model3DComponent.SetRotation(0f);
                    }
                }
            }
        }
    }
}