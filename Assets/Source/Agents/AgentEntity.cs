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
        physicsState.MovementState != AgentMovementState.Stagger;
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
        var model3d = agentModel3D;
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
            if (model3d.Weapon != null)
            {
                GameObject.Destroy(model3d.Weapon);
            }
        }

        model3d.CurrentWeapon = weapon;
    }
}