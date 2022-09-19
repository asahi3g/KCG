using UnityEngine;
using Agent;
using Enums;
using System;
using KMath;
using Inventory;

public partial class AgentEntity 
{

    public void Destroy()
    {
        if (hasAgentModel3D)
        {
            var model3D = agentModel3D;
            if (model3D.GameObject != null)
            {
                GameObject.Destroy(model3D.GameObject);
            }
        }
    }

    public bool IsStateFree()
    {
        var physicsState = agentPhysicsState;
        var state = agentState;

        return state.State == AgentState.Alive &&
        physicsState.MovementState != AgentMovementState.Dashing &&
        physicsState.MovementState != AgentMovementState.SwordSlash && 
        //physicsState.MovementState != AgentMovementState.MonsterAttack &&
        physicsState.MovementState != AgentMovementState.FireGun &&
        physicsState.MovementState != AgentMovementState.Stagger &&
        physicsState.MovementState != AgentMovementState.Rolling &&
        physicsState.MovementState != AgentMovementState.StandingUpAfterRolling &&
        physicsState.MovementState != AgentMovementState.UseTool &&
        physicsState.MovementState != AgentMovementState.Drink;
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
        if (hasAgentModel3D)
        {
            model3d = agentModel3D;
        }
        else
            return;

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
                if (hand != null)
                {

                    GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Pistol);
                    GameObject gun = GameObject.Instantiate(prefab);

                    gun.transform.parent = hand.transform;
                    gun.transform.position = hand.transform.position;
                    gun.transform.rotation = hand.transform.rotation;
                    gun.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    model3d.Weapon = gun;
                }
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

    public void FireGun(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.FireGun;

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = cooldown;
            physicsState.ActionCooldown = cooldown;
        }
    }

    public void UseTool(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.UseTool;

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = cooldown;
            physicsState.ActionCooldown = cooldown;
        }
    }

    public void UsePotion(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.Drink;

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = cooldown;
            physicsState.ActionCooldown = cooldown;
        }
    }


    public void MonsterAttack(float cooldown)
    {
        var physicsState = agentPhysicsState;
        var model3d = agentModel3D; 

        if (IsStateFree())
        {
            //physicsState.MovementState = AgentMovementState.MonsterAttack;
            physicsState.SetMovementState = true;
            

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = cooldown;
            physicsState.ActionCooldown = cooldown;      
        }
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

        if (
        IsStateFree() && PhysicsState.OnGrounded)
        {
            PhysicsState.Velocity.X = 1.35f * PhysicsState.Speed * horizontalDir;
            PhysicsState.Velocity.Y = 0.0f;

            PhysicsState.Invulnerable = true;
            PhysicsState.AffectedByGravity = true;
            PhysicsState.AffectedByFriction = false;
            PhysicsState.MovementState = AgentMovementState.Rolling;
            PhysicsState.ActionInProgress = true;
            PhysicsState.ActionJustEnded = false;
            PhysicsState.ActionDuration = 0.5f;
            PhysicsState.ActionCooldown = 1.75f;
        }
    }

    public void Crouch(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;

        if (IsStateFree() && PhysicsState.OnGrounded && 
        PhysicsState.MovementState != AgentMovementState.Crouch &&
        PhysicsState.MovementState != AgentMovementState.Crouch_Move)
        {
            
            PhysicsState.Invulnerable = false;
            PhysicsState.AffectedByGravity = true;
            PhysicsState.AffectedByFriction = true;
            if (horizontalDir == 0)
            {
                PhysicsState.MovementState = AgentMovementState.Crouch;
            } 
            else
            {
                PhysicsState.MovementState = AgentMovementState.Crouch_Move;
            }
        }
    }

    public void UnCrouch(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;

        if (PhysicsState.MovementState == AgentMovementState.Crouch ||
        PhysicsState.MovementState == AgentMovementState.Crouch_Move)
        {
            if (horizontalDir == 0)
            {
                PhysicsState.MovementState = AgentMovementState.Idle;
            }
            else
            {
                PhysicsState.MovementState = AgentMovementState.Move;
            }
        }
    }



    public void Run(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;
        var stats = agentStats;

        if (IsStateFree() && !stats.IsLimping)
        {
            // handling horizontal movement (left/right)
            if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed)
            {
                PhysicsState.Acceleration.X = horizontalDir * 2 * PhysicsState.Speed / Physics.Constants.TimeToMax;
            }

            if (horizontalDir > 0 && PhysicsState.MovementState == AgentMovementState.SlidingLeft)
            {
                // if we move to the right
                // that means we are no longer sliding down on the left
                PhysicsState.MovementState = AgentMovementState.None;
            }
            else if (horizontalDir < -1.0f && PhysicsState.MovementState == AgentMovementState.SlidingRight)
            {
                // if we move to the left
                // that means we are no longer sliding down on the right
                PhysicsState.MovementState = AgentMovementState.None;
            }
        }

    }

    public void Walk(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;
        var stats = agentStats;
        
        if (IsStateFree())
        {
            if (PhysicsState.MovementState == AgentMovementState.Crouch ||
            PhysicsState.MovementState == AgentMovementState.Crouch_Move)
            {
                if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed/3) 
                {
                    PhysicsState.Acceleration.X = 2.0f * horizontalDir * PhysicsState.Speed / Physics.Constants.TimeToMax;
                }
                else if (Math.Abs(PhysicsState.Velocity.X) == PhysicsState.Speed/3) // Velocity equal drag.
                {
                    PhysicsState.Acceleration.X = 1.0f * horizontalDir * PhysicsState.Speed / Physics.Constants.TimeToMax;
                }
            }
            else
            {
                if (stats.IsLimping)
                {
                    if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed/3) 
                    {
                        PhysicsState.Acceleration.X = 2 * horizontalDir * PhysicsState.Speed / Physics.Constants.TimeToMax;
                    }
                    else if (Math.Abs(PhysicsState.Velocity.X) == PhysicsState.Speed/3) // Velocity equal drag.
                    {
                        PhysicsState.Acceleration.X = horizontalDir * PhysicsState.Speed / Physics.Constants.TimeToMax;
                    }
                }
                else
                {
                    if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed/2) 
                    {
                        PhysicsState.Acceleration.X = 2 * horizontalDir * PhysicsState.Speed / Physics.Constants.TimeToMax;
                    }
                    else if (Math.Abs(PhysicsState.Velocity.X) == PhysicsState.Speed/2) // Velocity equal drag.
                    {
                        PhysicsState.Acceleration.X = horizontalDir * PhysicsState.Speed / Physics.Constants.TimeToMax;
                    }
                }
            }

            if (horizontalDir > 0 && PhysicsState.MovementState == AgentMovementState.SlidingLeft)
            {   
                // if we move to the right
                // that means we are no longer sliding down on the left
                PhysicsState.MovementState = AgentMovementState.None;
            }
            else if (horizontalDir < -0.0f && PhysicsState.MovementState == AgentMovementState.SlidingRight)
            {
                // if we move to the left
                // that means we are no longer sliding down on the right
                PhysicsState.MovementState = AgentMovementState.None;
            }
        }
    }
}