using System;
using KMath;

using UnityEngine.Animations.Rigging;

namespace Agent
{
    public class Model3DMovementSystem
    {
        public void Update(AgentContext agentContext)
        {

            float deltaTime = UnityEngine.Time.deltaTime;
            var entities = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
            foreach (var entity in entities)
            {
                var physicsState = entity.agentPhysicsState;

                var model3d = entity.agentModel3D;
                model3d.GameObject.transform.position = new UnityEngine.Vector3(physicsState.Position.X, physicsState.Position.Y, -1.0f);

                UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

                var transform = entity.agentModel3D.GameObject.transform;
                var PistolIK = transform.Find("Pistol");
                var RifleIK = transform.Find("Rifle");
                var AimTarget = transform.Find("AimTargetTest");

                if (entity.hasAgentModel3D)
                {
                    if (transform != null)
                    {
                        if (AimTarget != null)
                            AimTarget.position = new UnityEngine.Vector3(worldPosition.x, worldPosition.y, 0.0f);

                        if (PistolIK != null && RifleIK != null && AimTarget != null)
                        {
                            if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Pistol)
                            {
                                if(entity.agentPhysicsState.MovementState == Enums.AgentMovementState.FireGun)
                                {
                                    PistolIK.GetComponent<Rig>().weight = UnityEngine.Mathf.Lerp(
                                    PistolIK.GetComponent<Rig>().weight, 1.0f, UnityEngine.Time.deltaTime * 10f);
                                    entity.agentAction.Action = AgentAction.Aiming;
                                }
                                else
                                {
                                    PistolIK.GetComponent<Rig>().weight = UnityEngine.Mathf.Lerp(
                                        PistolIK.GetComponent<Rig>().weight, 0.0f, UnityEngine.Time.deltaTime * 0.3f);
                                }
                            }
                            else if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Rifle)
                            {
                                PistolIK.GetComponent<Rig>().weight = 0.0f;

                                if (entity.agentPhysicsState.MovementState == Enums.AgentMovementState.FireGun)
                                {
                                    RifleIK.GetComponent<Rig>().weight = UnityEngine.Mathf.Lerp(
                                    RifleIK.GetComponent<Rig>().weight, 1.0f, UnityEngine.Time.deltaTime * 10f);
                                    entity.agentAction.Action = AgentAction.Aiming;
                                }
                                else
                                {
                                    RifleIK.GetComponent<Rig>().weight = UnityEngine.Mathf.Lerp(
                                        RifleIK.GetComponent<Rig>().weight, 0.0f, UnityEngine.Time.deltaTime * 0.3f);
                                }
                            }
                            else
                            {
                                PistolIK.GetComponent<Rig>().weight = UnityEngine.Mathf.Lerp(
                                PistolIK.GetComponent<Rig>().weight, 0.0f, UnityEngine.Time.deltaTime * 10f);

                                RifleIK.GetComponent<Rig>().weight = UnityEngine.Mathf.Lerp(
                                RifleIK.GetComponent<Rig>().weight, 0.0f, UnityEngine.Time.deltaTime * 10f);

                                entity.agentAction.Action = AgentAction.UnAlert;
                            }
                        }
                    }

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
}
