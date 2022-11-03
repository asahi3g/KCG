//imports UnityEngine

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
        UnityEngine.Transform RigLayer_WeaponAiming;
        UnityEngine.Transform RigLayerRifle_HandIK;
        UnityEngine.Transform AimTarget;


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
                RigLayer_WeaponAiming = transform.Find("RigLayer_WeaponAiming");
                RigLayerRifle_HandIK = transform.Find("RigLayerRifle_HandIK");
                AimTarget = transform.Find("AimTargetTest");

                if (entity.hasAgentModel3D)
                {
                    if (transform != null)
                    {
                        if (/*PistolIK != null &&*/ RigLayerRifle_BodyAim != null && RigLayerRifle_WeaponPose != null &&
                             RigLayer_WeaponAiming != null && RigLayerRifle_HandIK != null && AimTarget != null)
                        {
                            if (AimTarget != null)
                            {
                                if (entity.hasAgentController || entity.hasAgentEnemy)
                                {
                                    AimTarget.position = new UnityEngine.Vector3(model3d.GameObject.transform.position.x,
                                        model3d.GameObject.transform.position.y, model3d.GameObject.transform.position.z);
                                }
                                else
                                {
                                    AimTarget.position = new UnityEngine.Vector3(worldPosition.x, worldPosition.y, 0.0f);
                                }
                            }

                            if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Rifle)
                            {
                                //PistolIK.GetComponent<Rig>().weight = 0.0f;

                                RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 1.0f;
                                RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 1.0f;
                                RigLayer_WeaponAiming.GetComponent<Rig>().weight = 1.0f;
                                RigLayerRifle_HandIK.GetComponent<Rig>().weight = 1.0f;

                                entity.agentAction.Action = AgentAlertState.Alert;
                            }
                            else
                            {
                                RigLayerRifle_BodyAim.GetComponent<Rig>().weight = 0.0f;
                                RigLayerRifle_WeaponPose.GetComponent<Rig>().weight = 0.0f;
                                RigLayer_WeaponAiming.GetComponent<Rig>().weight = 0.0f;
                                RigLayerRifle_HandIK.GetComponent<Rig>().weight = 0.0f;

                                entity.agentAction.Action = AgentAlertState.UnAlert;
                            }
                        }
                    }
                }
            }
        }
    }
}
