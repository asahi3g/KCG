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

        return isAgentAlive &&
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

    public bool CanFaceMouseDirection()
    {
        var physicsState = agentPhysicsState;

        return isAgentAlive && 
        physicsState.MovementState != AgentMovementState.Dashing &&
        physicsState.MovementState != AgentMovementState.SlidingLeft &&
        physicsState.MovementState != AgentMovementState.SlidingRight && 
        physicsState.MovementState != AgentMovementState.Rolling;
    }

    public void DieInPlace()
    {
        isAgentAlive = false;
        
        var physicsState = agentPhysicsState;
        physicsState.MovementState = AgentMovementState.KnockedDownFront;

        physicsState.DyingDuration = 0.25f;
    }

    public void DieKnockBack()
    {
        isAgentAlive = false;
        
        var physicsState = agentPhysicsState;
        physicsState.MovementState = AgentMovementState.KnockedDownBack;

        physicsState.DyingDuration = 1.5f;
    }

    public Vec2f GetGunFiringPosition()
    {
        var physicsState = agentPhysicsState;
        var model3d = agentModel3D;

        Vec2f position = physicsState.Position;
        switch(model3d.ItemAnimationSet)
        {
            case Enums.ItemAnimationSet.HoldingRifle:
            {
                position += new Vec2f(0.5f * physicsState.FacingDirection, 1.0f);
                break;
            }
            case Enums.ItemAnimationSet.HoldingPistol:
            {
                break;
            }
            default:
            {
                break;
            }
        }

        return position;
    }

    
    public void HandleItemSelected(ItemInventoryEntity item)
    {
        Item.ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

        if (hasAgentModel3D)
        {
            var model3d = agentModel3D;

            model3d.ItemAnimationSet = itemProperty.AnimationSet;

            if (model3d.Weapon != null)
            {
                GameObject.Destroy(model3d.Weapon);
            }

            switch(itemProperty.ToolType)
            {
                case Enums.ItemToolType.Pistol:
                {
                    SetAgentWeapon(Model3DWeapon.Pistol);

                    break;
                }
                case Enums.ItemToolType.Rifle:
                {
                    SetAgentWeapon(Model3DWeapon.Rifle);
                    break;
                }
                case Enums.ItemToolType.Sword:
                {
                    SetAgentWeapon(Model3DWeapon.Sword);
                    break;
                }
            }
        }
    }

    public void SetAgentWeapon(Model3DWeapon weapon)
    {
        if (hasAgentModel3D)
        {
            Model3DComponent model3d = agentModel3D;
            model3d.CurrentWeapon = weapon;

            if (model3d.Weapon != null)
            {
                GameObject.Destroy(model3d.Weapon);
            }

            switch(weapon)
            {
                case Model3DWeapon.Sword:
                {
                    GameObject hand = model3d.LeftHand;

                    GameObject rapierPrefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Rapier);
                    GameObject rapier = GameObject.Instantiate(rapierPrefab);

                    rapier.transform.parent = hand.transform;
                    rapier.transform.position = hand.transform.position;
                    rapier.transform.rotation = hand.transform.rotation;
                    rapier.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    model3d.Weapon = rapier;
                    break;
                }

                case Model3DWeapon.Pistol:
                {
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
                    break;
                }

                case Model3DWeapon.Rifle:
                {
                    GameObject hand = model3d.RightHand;
                    if (hand != null)
                    {

                        GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.SpaceGun);
                        GameObject gun = GameObject.Instantiate(prefab);

                        var gunRotation = gun.transform.rotation;
                        gun.transform.parent = hand.transform;
                        gun.transform.position = hand.transform.position;
                        gun.transform.localRotation = gunRotation;
                        gun.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                        model3d.Weapon = gun;
                    }
                    break;
                }
            }
        }
        
    }

    public void SlideRight()
    {
         var physicsState = agentPhysicsState;

        if (IsStateFree() && isAgentPlayer)
        {
            physicsState.MovementState = Enums.AgentMovementState.SlidingRight;
            physicsState.JumpCounter = 0;
        }
    }

    public void SlideLeft()
    {
        var physicsState = agentPhysicsState;

        if (IsStateFree() && isAgentPlayer)
        {
            physicsState.MovementState = Enums.AgentMovementState.SlidingLeft;
            physicsState.JumpCounter = 0;
        }
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

        if (IsStateFree() && PhysicsState.OnGrounded)
        {
            PhysicsState.Velocity.X = 1.75f * PhysicsState.Speed * horizontalDir;
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

                if (PhysicsState.MovingDirection != PhysicsState.FacingDirection)
                {
                    PhysicsState.MovementState = AgentMovementState.MoveBackward;
                }
                else
                {
                    PhysicsState.MovementState = AgentMovementState.Move;
                }
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