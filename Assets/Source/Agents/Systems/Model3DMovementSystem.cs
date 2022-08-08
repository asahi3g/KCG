using System;
using KMath;
using UnityEngine;

namespace Agent
{
    public class Model3DMovementSystem
    {
        public void Update(AgentContext agentContext)
        {
            
            float deltaTime = Time.deltaTime;
            var entities = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
            foreach (var entity in entities)
            {
                var pos = entity.agentPosition2D;
                var movable = entity.agentMovable;

                var model3d = entity.agentModel3D;
                model3d.GameObject.transform.position = new Vector3(pos.Value.X, pos.Value.Y, -1.0f);


                Vector3 eulers = model3d.GameObject.transform.rotation.eulerAngles;
                if (movable.Velocity.X > 0.0f)
                {
                    model3d.GameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (movable.Velocity.X < 0.0f)
                {
                    model3d.GameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
        }
    }
}
