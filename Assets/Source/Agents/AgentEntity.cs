using UnityEngine;
using Agent;
using Enums;

public partial class AgentEntity 
{

    public bool IsStateFree()
    {
        var physicsState = agentPhysicsState;
        var state = agentState;

        return state.State == AgentState.Alive &&
        physicsState.MovementState != AgentMovementState.Dashing &&
        physicsState.MovementState != AgentMovementState.SwordSlash && 
        physicsState.MovementState != AgentMovementState.FireGun &&
        physicsState.MovementState != AgentMovementState.Stagger &&
        physicsState.MovementState != AgentMovementState.Rolling &&
        physicsState.MovementState != AgentMovementState.StandingUpAfterRolling;
    }

    public void DieInPlace()
    {                
        var state = agentState;
        state.State = AgentState.Dead;
        
        var physicsState = agentPhysicsState;
        physicsState.MovementState = AgentMovementState.KnockedDownFront;

        physicsState.DyingDuration = 1.5f;
    }

    public void DieKnockBack()
    {                
        var state = agentState;
        state.State = AgentState.Dead;
        
        var physicsState = agentPhysicsState;
        physicsState.MovementState = AgentMovementState.KnockedDownBack;

        physicsState.DyingDuration = 1.5f;
    }

    public void SetAgentWeapon(Model3DWeapon weapon)
    {
        Model3DComponent model3d = null;
        if(hasAgentModel3D)
        {
            model3d = agentModel3D;
        }
        if (weapon == Model3DWeapon.Sword)
        {        
            if (model3d.CurrentWeapon != Model3DWeapon.Sword)
            {
                if (model3d.Weapon != null)
                {
                    GameObject.Destroy(model3d.Weapon);
                }

                GameObject hand = model3d.LeftHand;

                GameObject rapierPrefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Rapier);
                GameObject rapier = GameObject.Instantiate(rapierPrefab);

                rapier.transform.parent = hand.transform;
                rapier.transform.position = hand.transform.position;
                rapier.transform.rotation = hand.transform.rotation;
                rapier.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                model3d.Weapon = rapier;
            }
        }
        else if (weapon == Model3DWeapon.Gun)
        {        
            if (model3d.CurrentWeapon != Model3DWeapon.Gun)
            {
                if (model3d.Weapon != null)
                {
                    GameObject.Destroy(model3d.Weapon);
                }

                GameObject hand = model3d.RightHand;

                GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Pistol);
                GameObject gun = GameObject.Instantiate(prefab);

                gun.transform.parent = hand.transform;
                gun.transform.position = hand.transform.position;
                gun.transform.rotation = hand.transform.rotation;
                gun.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                model3d.Weapon = gun;
            }
        }
        else
        {
            if (model3d == null)
                return;

            if (model3d.Weapon != null)
            {
                GameObject.Destroy(model3d.Weapon);
            }
        }

        model3d.CurrentWeapon = weapon;
    }


    public void Dash(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;

        if (PhysicsState.DashCooldown <= 0.0f &&
        IsStateFree())
        {
            PhysicsState.Velocity.X = 4 * PhysicsState.Speed * horizontalDir;
            PhysicsState.Velocity.Y = 0.0f;

            PhysicsState.Invulnerable = true;
            PhysicsState.AffectedByGravity = false;
            PhysicsState.MovementState = AgentMovementState.Dashing;
            PhysicsState.DashCooldown = 1.0f;
        }
    }


    public void Roll(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;

        if (PhysicsState.RollDuration <= 0.0f &&
        PhysicsState.RollCooldown <= 0.0f &&
        IsStateFree() && PhysicsState.OnGrounded)
        {
            PhysicsState.Velocity.X = 1.35f * PhysicsState.Speed * horizontalDir;
            PhysicsState.Velocity.Y = 0.0f;

            PhysicsState.Invulnerable = true;
            PhysicsState.AffectedByGravity = true;
            PhysicsState.AffectedByFriction = false;
            PhysicsState.MovementState = AgentMovementState.Rolling;
            PhysicsState.RollDuration = 0.5f;
            PhysicsState.RollCooldown = 1.75f;
        }
    }
}