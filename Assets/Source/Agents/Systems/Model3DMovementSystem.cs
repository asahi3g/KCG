using Enums;
using System.Diagnostics;

namespace Agent
{
    public class Model3DMovementSystem
    {
        public void Update()
        {
            var entities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
            foreach (var entity in entities)
            {
                ref Agent.AgentPropertiesTemplate properties = ref GameState.AgentCreationApi.GetRef((int)AgentType.EnemyMarine);

                var physicsState = entity.agentPhysicsState;

                var model3d = entity.agentModel3D;
                model3d.GameObject.transform.position = new UnityEngine.Vector3(physicsState.Position.X, physicsState.Position.Y, -2.0f);

                var renderer = model3d.GameObject.transform.GetChild(0).GetComponent<UnityEngine.Renderer>();

                if (physicsState.FacingDirection == 1)
                {
                    if(model3d.CurrentWeapon != Model3DWeapon.Pistol || model3d.CurrentWeapon != Model3DWeapon.Rifle)
                    {
                        model3d.GameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, 90, 0);
                    }
                    else
                    {
                        model3d.GameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 0);
                    }
                    model3d.GameObject.transform.localScale = new UnityEngine.Vector3(model3d.ModelScale.X, model3d.ModelScale.Y, model3d.ModelScale.Z);
                }
                else if (physicsState.FacingDirection == -1)
                {
                    if (model3d.CurrentWeapon != Model3DWeapon.Pistol || model3d.CurrentWeapon != Model3DWeapon.Rifle)
                    {
                        model3d.GameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, -120, 0);
                    }
                    else
                    {
                        model3d.GameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 0);
                    }

                    model3d.GameObject.transform.localScale = new UnityEngine.Vector3(model3d.ModelScale.X, model3d.ModelScale.Y, model3d.ModelScale.Z);
                }
            }
        }
    }
}