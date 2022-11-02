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
        UnityEngine.Transform PistolIK;
        UnityEngine.Transform RifleIK;
        UnityEngine.Transform AimTarget;

        public void Initialize(AgentContext agentContext)
        {
            var entities = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentModel3D));
            foreach (var entity in entities)
            {
                var physicsState = entity.agentPhysicsState;

                var model3d = entity.agentModel3D;
                model3d.GameObject.transform.position = new UnityEngine.Vector3(physicsState.Position.X, physicsState.Position.Y, -1.0f);

                transform = entity.agentModel3D.GameObject.transform;
                PistolIK = transform.Find("Pistol");
                RifleIK = transform.Find("Rifle");
                AimTarget = transform.Find("AimTargetTest");
            }
        }

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
                PistolIK = transform.Find("Pistol");
                RifleIK = transform.Find("Rifle");
                AimTarget = transform.Find("AimTargetTest");

                if (entity.hasAgentModel3D)
                {
                    if (transform != null)
                    {
                        if (PistolIK != null && RifleIK != null && AimTarget != null)
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

                            if (entity.agentModel3D.CurrentWeapon == Model3DWeapon.Pistol)
                            {

                                if (entity.agentPhysicsState.MovementState == Enums.AgentMovementState.FireGun)
                                {
                                    PistolIK.GetComponent<Rig>().weight = UnityEngine.Mathf.Lerp(
                                    PistolIK.GetComponent<Rig>().weight, 1.0f, UnityEngine.Time.deltaTime * 10f);
                                    entity.agentAction.Action = AgentAlertState.Aiming;
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
                                    entity.agentAction.Action = AgentAlertState.Aiming;
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

                                entity.agentAction.Action = AgentAlertState.UnAlert;
                            }
                        }
                    }
                }
            }
        }
    }
}
