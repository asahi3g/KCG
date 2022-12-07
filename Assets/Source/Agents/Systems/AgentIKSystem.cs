
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
                        if (entity.agentID.Type == AgentType.Player || entity.agentID.Type == AgentType.Marine)
                        {
                            agentRenderer.GetAnimancer().Playable.Evaluate();

                            if (entity.hasAgentAgent3DModel)
                            {
                                if (transform != null)
                                {
                                    if (agentRenderer.GetRifleBodyAim() != null && agentRenderer.GetRifleWeaponPose() != null &&
                                         agentRenderer.GetRifleWeaponAiming() != null && agentRenderer.GetRifleHandIK() != null && agentRenderer.GetAimTarget() != null)
                                    {
                                        if (agentRenderer.GetAimTarget() != null)
                                        {
                                            if (entity.hasAgentController)
                                            {
                                                if (entity.agentPhysicsState.FacingDirection == 1)
                                                {
                                                    agentRenderer.GetAimTarget().position = new UnityEngine.Vector3(entity.GetAIGunFiringPosition().X,
                                                      entity.GetAIGunFiringPosition().Y, agentRenderer.GetAimTarget().position.z);
                                                }
                                                else if (entity.agentPhysicsState.FacingDirection == -1)
                                                {
                                                    agentRenderer.GetAimTarget().position = new UnityEngine.Vector3(entity.GetAIGunFiringPosition().X,
                                                      entity.GetAIGunFiringPosition().Y, agentRenderer.GetAimTarget().position.z);
                                                }
                                            }
                                            else
                                            {
                                                if (entity.agentPhysicsState.FacingDirection == 1)
                                                {
                                                    agentRenderer.GetAimTarget().position = new UnityEngine.Vector3(entity.GetGunFiringPosition().X,
                                                       entity.GetGunFiringPosition().Y, agentRenderer.GetAimTarget().position.z);

                                                }
                                                else if (entity.agentPhysicsState.FacingDirection == -1)
                                                {
                                                    agentRenderer.GetAimTarget().position = new UnityEngine.Vector3(entity.GetGunFiringPosition().X,
                                                       entity.GetGunFiringPosition().Y, agentRenderer.GetAimTarget().position.z);
                                                }
                                            }
                                        }

                                        if (entity.agentAgent3DModel.CurrentWeapon == Model3DWeaponType.Rifle)
                                        {
                                            agentRenderer.GetPivotPistol().gameObject.SetActive(false);
                                            agentRenderer.GetPivotRifle().gameObject.SetActive(true);

                                            agentRenderer.GetRifleBodyAim().GetComponent<Rig>().weight = 1.0f;
                                            agentRenderer.GetRifleWeaponPose().GetComponent<Rig>().weight = 1.0f;
                                            agentRenderer.GetRifleWeaponAiming().GetComponent<Rig>().weight = 1.0f;
                                            agentRenderer.GetRifleHandIK().GetComponent<Rig>().weight = 1.0f;

                                            entity.agentAction.Action = AgentAlertState.Alert;
                                        }
                                        else if (entity.agentAgent3DModel.CurrentWeapon == Model3DWeaponType.Pistol)
                                        {
                                            agentRenderer.GetPivotPistol().gameObject.SetActive(true);
                                            agentRenderer.GetPivotRifle().gameObject.SetActive(false);

                                            agentRenderer.GetRifleBodyAim().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetRifleWeaponPose().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetRifleWeaponAiming().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetRifleHandIK().GetComponent<Rig>().weight = 0.0f;

                                            agentRenderer.GetPistolBodyAim().GetComponent<Rig>().weight = 1.0f;
                                            agentRenderer.GetPistolWeaponPose().GetComponent<Rig>().weight = 1.0f;
                                            agentRenderer.GetPistolWeaponAiming().GetComponent<Rig>().weight = 1.0f;
                                            agentRenderer.GetPistolHandIK().GetComponent<Rig>().weight = 1.0f;
                                        }
                                        else
                                        {
                                            agentRenderer.GetRifleBodyAim().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetRifleWeaponPose().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetRifleWeaponAiming().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetRifleHandIK().GetComponent<Rig>().weight = 0.0f;

                                            agentRenderer.GetPistolBodyAim().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetPistolWeaponPose().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetPistolWeaponAiming().GetComponent<Rig>().weight = 0.0f;
                                            agentRenderer.GetPistolHandIK().GetComponent<Rig>().weight = 0.0f;

                                            agentRenderer.GetPivotPistol().gameObject.SetActive(false);
                                            agentRenderer.GetPivotRifle().gameObject.SetActive(false);

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
                GameState.Planet.Player.agentAgent3DModel.Renderer.GetRifleBodyAim().weight = 0.0f;
                GameState.Planet.Player.agentAgent3DModel.Renderer.GetRifleWeaponPose().weight = 0.0f;
                GameState.Planet.Player.agentAgent3DModel.Renderer.GetRifleWeaponAiming().weight = 0.0f;
                GameState.Planet.Player.agentAgent3DModel.Renderer.GetRifleHandIK().weight = 0.0f;

                GameState.Planet.Player.agentAgent3DModel.Renderer.GetPistolBodyAim().weight = 0.0f;
                GameState.Planet.Player.agentAgent3DModel.Renderer.GetPistolWeaponPose().weight = 0.0f;
                GameState.Planet.Player.agentAgent3DModel.Renderer.GetPistolWeaponAiming().weight = 0.0f;
                GameState.Planet.Player.agentAgent3DModel.Renderer.GetPistolHandIK().weight = 0.0f;
            }
        }
    }
}
