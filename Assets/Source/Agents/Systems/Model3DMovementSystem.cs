using System;
using KMath;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
                var physicsState = entity.agentPhysicsState;

                var model3d = entity.agentModel3D;
                model3d.GameObject.transform.position = new Vector3(physicsState.Position.X, physicsState.Position.Y, -1.0f);

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(entity.hasAgentModel3D)
                {
                    entity.agentModel3D.GameObject.transform.GetChild(3).position = new Vector3(worldPosition.x, worldPosition.y, 0.0f);
                }

                if (entity.agentPhysicsState.MovementState == Enums.AgentMovementState.FireGun)
                {
                    entity.agentModel3D.GameObject.transform.GetChild(2).GetComponent<Rig>().weight = Mathf.Lerp(
                        entity.agentModel3D.GameObject.transform.GetChild(2).GetComponent<Rig>().weight, 1.0f, Time.deltaTime * 20f);
                }
                else
                {
                    entity.agentModel3D.GameObject.transform.GetChild(2).GetComponent<Rig>().weight = Mathf.Lerp(
                    entity.agentModel3D.GameObject.transform.GetChild(2).GetComponent<Rig>().weight, 0.0f, Time.deltaTime * 20f);
                }

                Vector3 eulers = model3d.GameObject.transform.rotation.eulerAngles;
                if (physicsState.Direction == 1)
                {
                    model3d.GameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (physicsState.Direction == -1)
                {
                    model3d.GameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
        }
    }
}
