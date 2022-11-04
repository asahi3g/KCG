//imports UnityEngine

using System.Diagnostics;
using UnityEngine.Animations.Rigging;

namespace Agent
{
    // Animation Rigging System for Agents
    // Turn on/off when aiming.
    // Find Rig Gameobjects from the childs of character object.
    // Set AimTargetPosition to cursor position.

    // This system work for each agent that has IK.
    // First, system searches for a rig object in childs.
    // After found one, controls the weight of the ik depends on the agent movement state.

    //What is Animation Rigging ?

    //Animation Rigging allows the user to create and organize different sets of constraints based on the C# Animation Jobs API to address
    //different requirements related to animation rigging. This includes deform rigs (procedural secondary animation) for such things as
    //character armor, accessories and much more. World interaction rigs (IK, Aim, etc.) for interactive adjustments, targeting, animation
    //correction, and so on.

    // For more info: https://docs.unity3d.com/Packages/com.unity.animation.rigging@0.2/manual/index.html


    public class AgentIKSystem
    {
        UnityEngine.Transform transform;
        //UnityEngine.Transform PistolIK;
        UnityEngine.Transform RigLayerRifle_BodyAim;
        UnityEngine.Transform RigLayerRifle_WeaponPose;
        UnityEngine.Transform RigLayerRifle_WeaponAiming;
        UnityEngine.Transform RigLayerRifle_HandIK;
        UnityEngine.Transform AimTarget;

        //UnityEngine.Transform RigLayerPistol_BodyAim;
        //UnityEngine.Transform RigLayerPistol_WeaponPose;
        //UnityEngine.Transform RigLayerPistol_WeaponAiming;
        //UnityEngine.Transform RigLayerPistol_HandIK;


        public void Update(AgentContext agentContext)
        {
            var entities = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
            foreach (var entity in entities)
            {
                var physicsState = entity.agentPhysicsState;

                var model3d = entity.agentModel3D;
                model3d.GameObject.transform.position = new UnityEngine.Vector3(physicsState.Position.X, physicsState.Position.Y, -1.0f);

                UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                
                transform = entity.agentModel3D.GameObject.transform;
                //PistolIK = transform.Find("Pistol");
                RigLayerRifle_BodyAim = transform.Find("RigLayerRifle_BodyAim");
                RigLayerRifle_WeaponPose = transform.Find("RigLayerRifle_WeaponPose");
                RigLayerRifle_WeaponAiming = transform.Find("RigLayerRifle_WeaponAiming");
                RigLayerRifle_HandIK = transform.Find("RigLayerRifle_HandIK");

                //RigLayerPistol_BodyAim = transform.Find("RigLayerPistol_BodyAim");
                //RigLayerPistol_WeaponPose = transform.Find("RigLayerPistol_WeaponPose");
                //RigLayerPistol_WeaponAiming = transform.Find("RigLayerPistol_WeaponAiming");
                //RigLayerPistol_HandIK = transform.Find("RigLayerPistol_HandIK");

                AimTarget = transform.Find("AimTarget");

                if (entity.hasAgentModel3D)
                {
                    if (transform != null)
                    {
                        if (RigLayerRifle_BodyAim != null && RigLayerRifle_WeaponPose != null &&
                             RigLayerRifle_WeaponAiming != null && RigLayerRifle_HandIK != null && AimTarget != null)
                        {
                            if (AimTarget != null)
                            {
                                if (entity.hasAgentController || entity.hasAgentEnemy)
                                {
                                    if (entity.agentPhysicsState.FacingDirection == 1)
                                    {
                                        AimTarget.position = new UnityEngine.Vector3(model3d.GameObject.transform.position.x - 1f, model3d.GameObject.transform.position.y, model3d.GameObject.transform.position.z -
                                              1f);

                                    }
                                    else if (entity.agentPhysicsState.FacingDirection == -1)
                                    {
                                        AimTarget.position = new UnityEngine.Vector3(worldPosition.x - 10f, worldPosition.y, worldPosition.z + 15f);
                                    }
                                }
                                else
                                {
                                    if(entity.agentPhysicsState.FacingDirection == 1)
                                    {
                                        AimTarget.position = new UnityEngine.Vector3(worldPosition.x - 1f, worldPosition.y, worldPosition.z - 
                                              1f);

                                    }
                                    else if (entity.agentPhysicsState.FacingDirection == -1)
                                    {
                                        AimTarget.position = new UnityEngine.Vector3(worldPosition.x - 1f, worldPosition.y, worldPosition.z + 10f);
                                    }

                                }
                            }

                            if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Rifle)
                            {
                                if (entity.agentModel3D.Weapon != null)
                                    entity.agentModel3D.Weapon.gameObject.SetActive(true);

                                RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 1.0f;
                                RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 1.0f;
                                RigLayerRifle_WeaponAiming.GetComponent<Rig>().weight = 1.0f;
                                RigLayerRifle_HandIK.GetComponent<Rig>().weight = 1.0f;

                                entity.agentAction.Action = AgentAlertState.Alert;
                            }
                            else if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Pistol)
                            {
                                //if (entity.agentModel3D.Weapon != null)
                                //    entity.agentModel3D.Weapon.gameObject.SetActive(false);

                                //RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 0.0f;
                                //RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                                //RigLayerRifle_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                                //RigLayerRifle_HandIK.GetComponent<Rig>().weight = 0.0f;

                                //if (entity.agentModel3D.Weapon != null)
                                //    entity.agentModel3D.Weapon.gameObject.SetActive(true);

                                //RigLayerPistol_BodyAim.GetComponent<Rig>().weight = 1.0f;
                                //RigLayerPistol_WeaponPose.GetComponent<Rig>().weight = 1.0f;
                                //RigLayerPistol_WeaponAiming.GetComponent<Rig>().weight = 1.0f;
                                //RigLayerPistol_HandIK.GetComponent<Rig>().weight = 1.0f;
                            }
                            else
                            {
                                RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 0.0f;
                                RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                                RigLayerRifle_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                                RigLayerRifle_HandIK.GetComponent<Rig>().weight = 0.0f;

                                //RigLayerPistol_BodyAim.GetComponent<Rig>().weight = 0.0f;
                                //RigLayerPistol_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                                //RigLayerPistol_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                                //RigLayerPistol_HandIK.GetComponent<Rig>().weight = 0.0f;

                                if (entity.agentModel3D.Weapon != null)
                                    entity.agentModel3D.Weapon.gameObject.SetActive(false);

                                entity.agentAction.Action = AgentAlertState.UnAlert;
                            }
                        }
                    }
                }
            }
        }
    }
}
