//imports UnityEngine

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
        public bool IKEnabled = true;

        public void SetIKEnabled(bool value)
        {
            IKEnabled = value;
        }

        public void Update(AgentContext agentContext)
        {
            if(IKEnabled)
            {
                var entities = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
                foreach (var entity in entities)
                {
                    var physicsState = entity.agentPhysicsState;

                    var model3d = entity.agentModel3D;
                    model3d.GameObject.transform.position = new UnityEngine.Vector3(physicsState.Position.X, physicsState.Position.Y, -1.0f);

                    if(entity.isAgentAlive)
                    {
                        if (entity.agentID.Type == AgentType.Player || entity.agentID.Type == AgentType.EnemyMarine)
                        {
                            model3d.AnimancerComponent.Playable.Evaluate();

                            if (entity.hasAgentModel3D)
                            {
                                if (model3d.GameObject.transform != null)
                                {
                                    if (model3d != null && model3d.RifleIKBodyParts[1] != null &&
                                         model3d.RifleIKBodyParts[2] != null && model3d.RifleIKBodyParts[3] != null && model3d.AimTargetObj != null)
                                    {
                                        if (model3d.AimTargetObj != null)
                                        {
                                            if (entity.hasAgentController)
                                            {
                                                if (entity.agentPhysicsState.FacingDirection == 1)
                                                {
                                                    model3d.AimTargetObj.position = new UnityEngine.Vector3(model3d.AimTarget.X, model3d.AimTarget.Y, -6.0f);
                                                }
                                                else if (entity.agentPhysicsState.FacingDirection == -1)
                                                {
                                                    model3d.AimTargetObj.position = new UnityEngine.Vector3(model3d.AimTarget.X, model3d.AimTarget.Y, 1.0f);
                                                }
                                            }
                                            else
                                            {
                                                if (entity.agentPhysicsState.FacingDirection == 1)
                                                {
                                                    model3d.AimTargetObj.position = new UnityEngine.Vector3(entity.GetGunFiringPosition().X,
                                                       entity.GetGunFiringPosition().Y, model3d.AimTargetObj.position.z);

                                                }
                                                else if (entity.agentPhysicsState.FacingDirection == -1)
                                                {
                                                    model3d.AimTargetObj.position = new UnityEngine.Vector3(entity.GetGunFiringPosition().X,
                                                       entity.GetGunFiringPosition().Y, model3d.AimTargetObj.position.z);
                                                }
                                            }
                                        }

                                        if (entity.agentModel3D.CurrentWeapon == Model3DWeaponType.Rifle)
                                        {
                                            model3d.PistolIKBodyParts[4].gameObject.SetActive(false);
                                            model3d.RifleIKBodyParts[4].gameObject.SetActive(true);

                                            model3d.RifleIKBodyParts[0].GetComponent<Rig>().weight = 1.0f;
                                            model3d.RifleIKBodyParts[1].GetComponent<Rig>().weight = 1.0f;
                                            model3d.RifleIKBodyParts[2].GetComponent<Rig>().weight = 1.0f;
                                            model3d.RifleIKBodyParts[3].GetComponent<Rig>().weight = 1.0f;

                                            entity.agentAction.Action = AgentAlertState.Alert;
                                        }
                                        else if (entity.agentModel3D.CurrentWeapon == Model3DWeaponType.Pistol)
                                        {
                                            model3d.PistolIKBodyParts[4].gameObject.SetActive(true);
                                            model3d.RifleIKBodyParts[4].gameObject.SetActive(false);

                                            model3d.RifleIKBodyParts[0].GetComponent<Rig>().weight = 0.0f;
                                            model3d.RifleIKBodyParts[1].GetComponent<Rig>().weight = 0.0f;
                                            model3d.RifleIKBodyParts[2].GetComponent<Rig>().weight = 0.0f;
                                            model3d.RifleIKBodyParts[3].GetComponent<Rig>().weight = 0.0f;

                                            model3d.PistolIKBodyParts[0].GetComponent<Rig>().weight = 1.0f;
                                            model3d.PistolIKBodyParts[1].GetComponent<Rig>().weight = 1.0f;
                                            model3d.PistolIKBodyParts[2].GetComponent<Rig>().weight = 1.0f;
                                            model3d.PistolIKBodyParts[3].GetComponent<Rig>().weight = 1.0f;
                                        }
                                        else
                                        {
                                            model3d.RifleIKBodyParts[0].GetComponent<Rig>().weight = 0.0f;
                                            model3d.RifleIKBodyParts[1].GetComponent<Rig>().weight = 0.0f;
                                            model3d.RifleIKBodyParts[2].GetComponent<Rig>().weight = 0.0f;
                                            model3d.RifleIKBodyParts[3].GetComponent<Rig>().weight = 0.0f;

                                            model3d.PistolIKBodyParts[0].GetComponent<Rig>().weight = 0.0f;
                                            model3d.PistolIKBodyParts[1].GetComponent<Rig>().weight = 0.0f;
                                            model3d.PistolIKBodyParts[2].GetComponent<Rig>().weight = 0.0f;
                                            model3d.PistolIKBodyParts[3].GetComponent<Rig>().weight = 0.0f;

                                            model3d.PistolIKBodyParts[4].gameObject.SetActive(false);
                                            model3d.RifleIKBodyParts[4].gameObject.SetActive(false);

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
                var entities = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
                foreach (var entity in entities)
                {
                    entity.agentModel3D.RifleIKBodyParts[0].GetComponent<Rig>().weight = 0.0f;
                    entity.agentModel3D.RifleIKBodyParts[1].GetComponent<Rig>().weight = 0.0f;
                    entity.agentModel3D.RifleIKBodyParts[2].GetComponent<Rig>().weight = 0.0f;
                    entity.agentModel3D.RifleIKBodyParts[3].GetComponent<Rig>().weight = 0.0f;

                    entity.agentModel3D.PistolIKBodyParts[0].GetComponent<Rig>().weight = 0.0f;
                    entity.agentModel3D.PistolIKBodyParts[1].GetComponent<Rig>().weight = 0.0f;
                    entity.agentModel3D.PistolIKBodyParts[2].GetComponent<Rig>().weight = 0.0f;
                    entity.agentModel3D.PistolIKBodyParts[3].GetComponent<Rig>().weight = 0.0f;
                }
            }
        }
    }
}
