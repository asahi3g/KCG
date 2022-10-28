//imports UnityEngine

using Enums;

namespace Agent
{
    public class Model3DMovementSystem
    {
        public void Update()
        {
            var entities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
            foreach (var entity in entities)
            {
                ref Agent.AgentProperties properties = ref GameState.AgentCreationApi.GetRef((int)AgentType.EnemyMarine);

                var physicsState = entity.agentPhysicsState;

                var model3d = entity.agentModel3D;
                model3d.GameObject.transform.position = new UnityEngine.Vector3(physicsState.Position.X, physicsState.Position.Y, -2.0f);

                // This is for testin purposes,
                // will be deleted soon.
                properties.TrackStub.transform.position = new UnityEngine.Vector3(physicsState.Position.X, physicsState.Position.Y + 1.5f, -3.0f);
                properties.TrackStub.transform.localScale = new UnityEngine.Vector3(2, 2, 2);

                if (physicsState.FacingDirection == 1)
                {
                    model3d.GameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, 90, 0);
                    model3d.GameObject.transform.localScale = new UnityEngine.Vector3(model3d.ModelScale.X, model3d.ModelScale.Y, 
                        model3d.ModelScale.Z);
                }
                else if (physicsState.FacingDirection == -1)
                {
                    model3d.GameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, -90, 0);
                    model3d.GameObject.transform.localScale = new UnityEngine.Vector3(model3d.ModelScale.X, 
                        model3d.ModelScale.Y,model3d.ModelScale.Z);
                }
            }
        }
    }
}
