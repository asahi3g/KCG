
using Enums;
using KMath;
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

        public UnityEngine.Transform Pistol;
        public UnityEngine.Transform Rifle;

        UnityEngine.Transform RigLayerRifle_BodyAim;
        UnityEngine.Transform RigLayerRifle_WeaponPose;
        UnityEngine.Transform RigLayerRifle_WeaponAiming;
        UnityEngine.Transform RigLayerRifle_HandIK;
        UnityEngine.Transform AimTarget;

        UnityEngine.Transform RigLayerPistol_BodyAim;
        UnityEngine.Transform RigLayerPistol_WeaponPose;
        UnityEngine.Transform RigLayerPistol_WeaponAiming;
        UnityEngine.Transform RigLayerPistol_HandIK;

        public bool IKEnabled = true;

        public void SetIKEnabled(bool value)
        {
            IKEnabled = value;
        }

        public void Update(AgentContext agentContext)
        {
            if(IKEnabled)
            {
                var entities = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentAgent3DModel));
                foreach (var entity in entities)
                {
                    
                    PhysicsStateComponent physicsStateComponent = entity.agentPhysicsState;
                    Agent3DModel agent3DModel = entity.agentAgent3DModel;
                    AgentRenderer agentRenderer = entity.agentAgent3DModel.Renderer;
                    transform = agentRenderer.GetModel().transform;
                    
                    agent3DModel.SetPosition(physicsStateComponent.Position.X, physicsStateComponent.Position.Y);
                    
                    if(entity.isAgentAlive)
                    {
                        if (entity.agentID.Type == AgentType.Player || entity.agentID.Type == AgentType.EnemyMarine)
                        {
                            agentRenderer.GetAnimancer().Playable.Evaluate();

                            Pistol = agentRenderer.GetPivotPistol();
                            Rifle = agentRenderer.GetPivotRifle();

                            RigLayerRifle_BodyAim = transform.Find("RigLayerRifle_BodyAim");
                            RigLayerRifle_WeaponPose = transform.Find("RigLayerRifle_WeaponPose");
                            RigLayerRifle_WeaponAiming = transform.Find("RigLayerRifle_WeaponAiming");
                            RigLayerRifle_HandIK = transform.Find("RigLayerRifle_HandIK");

                            RigLayerPistol_BodyAim = transform.Find("RigLayerPistol_BodyAim");
                            RigLayerPistol_WeaponPose = transform.Find("RigLayerPistol_WeaponPose");
                            RigLayerPistol_WeaponAiming = transform.Find("RigLayerPistol_WeaponAiming");
                            RigLayerPistol_HandIK = transform.Find("RigLayerPistol_HandIK");

                            AimTarget = agentRenderer.GetAimTarget();

                            if (entity.hasAgentAgent3DModel)
                            {
                                if (transform != null)
                                {
                                    if (RigLayerRifle_BodyAim != null && RigLayerRifle_WeaponPose != null &&
                                         RigLayerRifle_WeaponAiming != null && RigLayerRifle_HandIK != null && AimTarget != null)
                                    {
                                        if (AimTarget != null)
                                        {
                                            if (entity.hasAgentController)
                                            {
                                                if (entity.agentPhysicsState.FacingDirection == 1)
                                                {
                                                    AimTarget.position = new UnityEngine.Vector3(agent3DModel.AimTarget.X, agent3DModel.AimTarget.Y, -6.0f);
                                                }
                                                else if (entity.agentPhysicsState.FacingDirection == -1)
                                                {
                                                    AimTarget.position = new UnityEngine.Vector3(agent3DModel.AimTarget.X, agent3DModel.AimTarget.Y, 1.0f);
                                                }
                                            }
                                            else
                                            {
                                                if (entity.agentPhysicsState.FacingDirection == 1)
                                                {
                                                    AimTarget.position = new UnityEngine.Vector3(entity.GetGunFiringPosition().X,
                                                       entity.GetGunFiringPosition().Y, AimTarget.position.z);

                                                }
                                                else if (entity.agentPhysicsState.FacingDirection == -1)
                                                {
                                                    AimTarget.position = new UnityEngine.Vector3(entity.GetGunFiringPosition().X,
                                                       entity.GetGunFiringPosition().Y, AimTarget.position.z);
                                                }
                                            }
                                        }

                                        if (entity.agentAgent3DModel.CurrentWeapon == Model3DWeaponType.Rifle)
                                        {
                                            Pistol.gameObject.SetActive(false);
                                            Rifle.gameObject.SetActive(true);

                                            RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 1.0f;
                                            RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 1.0f;
                                            RigLayerRifle_WeaponAiming.GetComponent<Rig>().weight = 1.0f;
                                            RigLayerRifle_HandIK.GetComponent<Rig>().weight = 1.0f;

                                            entity.agentAction.Action = AgentAlertState.Alert;
                                        }
                                        else if (entity.agentAgent3DModel.CurrentWeapon == Model3DWeaponType.Pistol)
                                        {
                                            Pistol.gameObject.SetActive(true);
                                            Rifle.gameObject.SetActive(false);

                                            RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerRifle_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerRifle_HandIK.GetComponent<Rig>().weight = 0.0f;

                                            RigLayerPistol_BodyAim.GetComponent<Rig>().weight = 1.0f;
                                            RigLayerPistol_WeaponPose.GetComponent<Rig>().weight = 1.0f;
                                            RigLayerPistol_WeaponAiming.GetComponent<Rig>().weight = 1.0f;
                                            RigLayerPistol_HandIK.GetComponent<Rig>().weight = 1.0f;
                                        }
                                        else
                                        {
                                            RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerRifle_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerRifle_HandIK.GetComponent<Rig>().weight = 0.0f;

                                            RigLayerPistol_BodyAim.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerPistol_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerPistol_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                                            RigLayerPistol_HandIK.GetComponent<Rig>().weight = 0.0f;

                                            Pistol.gameObject.SetActive(false);
                                            Rifle.gameObject.SetActive(false);

                                            entity.agentAction.Action = AgentAlertState.UnAlert;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 0.0f;
                RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                RigLayerRifle_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                RigLayerRifle_HandIK.GetComponent<Rig>().weight = 0.0f;

                RigLayerPistol_BodyAim.GetComponent<Rig>().weight = 0.0f;
                RigLayerPistol_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                RigLayerPistol_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                RigLayerPistol_HandIK.GetComponent<Rig>().weight = 0.0f;
            }
        }
    }
}
