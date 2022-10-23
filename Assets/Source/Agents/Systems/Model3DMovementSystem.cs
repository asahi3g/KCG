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

                var transform = entity.agentModel3D.GameObject.transform;
                var PistolIK = transform.Find("Pistol");
                var RifleIK = transform.Find("Rifle");
                var AimTarget = transform.Find("AimTargetTest");

                if (entity.hasAgentModel3D)
                {
                    if (transform != null)
                    {
                        if (AimTarget != null)
                            AimTarget.position = new Vector3(worldPosition.x, worldPosition.y, 0.0f);

                        if (PistolIK != null && RifleIK != null && AimTarget != null)
                        {
                            if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Pistol)
                            {
                                if(entity.agentPhysicsState.MovementState == Enums.AgentMovementState.FireGun)
                                {
                                    PistolIK.GetComponent<Rig>().weight = Mathf.Lerp(
                                    PistolIK.GetComponent<Rig>().weight, 1.0f, Time.deltaTime * 20f);
                                    entity.agentAction.Action = AgentAction.Aiming;
                                }
                                else
                                {
                                    PistolIK.GetComponent<Rig>().weight = 0.0f;
                                }
                            }
                            else if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Rifle)
                            {
                                PistolIK.GetComponent<Rig>().weight = 0.0f;

                                if (entity.agentPhysicsState.MovementState == Enums.AgentMovementState.FireGun)
                                {
                                    RifleIK.GetComponent<Rig>().weight = Mathf.Lerp(
                                    RifleIK.GetComponent<Rig>().weight, 1.0f, Time.deltaTime * 20f);
                                    entity.agentAction.Action = AgentAction.Aiming;
                                }
                                else
                                {
                                    RifleIK.GetComponent<Rig>().weight = 0.0f;
                                }
                            }
                            else
                            {
                                PistolIK.GetComponent<Rig>().weight = Mathf.Lerp(
                                PistolIK.GetComponent<Rig>().weight, 0.0f, Time.deltaTime * 20f);

                                RifleIK.GetComponent<Rig>().weight = Mathf.Lerp(
                                RifleIK.GetComponent<Rig>().weight, 0.0f, Time.deltaTime * 20f);

                                entity.agentAction.Action = AgentAction.UnAlert;
                            }
                        }

                        if(RifleIK != null)
                        {
                            if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Rifle)
                            {
                                PistolIK.GetComponent<Rig>().weight = 0.0f;
                            
                                RifleIK.GetComponent<Rig>().weight = Mathf.Lerp(
                                RifleIK.GetComponent<Rig>().weight, 1.0f, Time.deltaTime * 20f);
                                entity.agentAction.Action = AgentAction.Aiming;
                            }
                            else
                            {
                                RifleIK.GetComponent<Rig>().weight = Mathf.Lerp(
                                RifleIK.GetComponent<Rig>().weight, 0.0f, Time.deltaTime * 20f);
                                entity.agentAction.Action = AgentAction.UnAlert;
                            }
                        }
                    }

                    if (physicsState.FacingDirection == 1)
                    {
                        model3d.GameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                        model3d.GameObject.transform.localScale = new Vector3(model3d.ModelScale.X, model3d.ModelScale.Y, model3d.ModelScale.Z);
                    }
                    else if (physicsState.FacingDirection == -1)
                    {
                        model3d.GameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                        model3d.GameObject.transform.localScale = new Vector3(model3d.ModelScale.X, model3d.ModelScale.Y, -model3d.ModelScale.Z);
                    }
                }
            }
        }
    }
}
