using System;
using Agent;
using Engine3D;
using Enums;
using Inventory;
using Item;
using KMath;
using Physics;

public partial class AgentEntity 
{
    public ItemInventoryEntity GetItem()
    {
        if (!hasAgentInventory)
            return null;

        int inventoryID = agentInventory.InventoryID;
        EntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
        int selectedSlot = inventory.SelectedSlotID;
        return GameState.InventoryManager.GetItemInSlot(agentInventory.InventoryID, selectedSlot);
    }
    public void DestroyModel()
    {
        if (hasAgentModel3D)
        {
            var model3D = agentModel3D;
            if (model3D.GameObject != null)
            {
                UnityEngine.Object.Destroy(model3D.GameObject);
            }
        }
    }

    public bool CanMove()
    {
        var physicsState = agentPhysicsState;
        return physicsState.MovementState != AgentMovementState.IdleAfterShooting;
    }

    public bool IsStateFree()
    {
        var physicsState = agentPhysicsState;

        return isAgentAlive &&
        physicsState.MovementState != AgentMovementState.Dashing &&
        physicsState.MovementState != AgentMovementState.SwordSlash && 
        physicsState.MovementState != AgentMovementState.MonsterAttack &&
        physicsState.MovementState != AgentMovementState.FireGun &&
        physicsState.MovementState != AgentMovementState.PickaxeHit &&
        physicsState.MovementState != AgentMovementState.ChoppingTree &&
        physicsState.MovementState != AgentMovementState.Stagger &&
        physicsState.MovementState != AgentMovementState.Rolling &&
        physicsState.MovementState != AgentMovementState.StandingUpAfterRolling &&
        physicsState.MovementState != AgentMovementState.UseTool &&
        physicsState.MovementState != AgentMovementState.Drink;
    }

    public bool IsCrouched()
    {
        var physicsState = agentPhysicsState;

        return physicsState.MovementState == AgentMovementState.Crouch ||
                physicsState.MovementState == AgentMovementState.Crouch_Move ||
                physicsState.MovementState == AgentMovementState.Crouch_MoveBackward;
    }

    public bool IsAffectedByGravity()
    {
        var physicsState = agentPhysicsState;

        return physicsState.MovementState == AgentMovementState.Idle ||
                physicsState.MovementState == AgentMovementState.None ||
                physicsState.MovementState == AgentMovementState.Move ||
                physicsState.MovementState == AgentMovementState.MoveBackward ||
                physicsState.MovementState == AgentMovementState.Crouch ||
                physicsState.MovementState == AgentMovementState.Crouch_Move ||
                physicsState.MovementState == AgentMovementState.Crouch_MoveBackward ||
                physicsState.MovementState == AgentMovementState.JetPackFlying;
    }

    public bool IsIdle()
    {
        var physicsState = agentPhysicsState;

        return isAgentAlive && (
        physicsState.MovementState == AgentMovementState.Idle ||
        physicsState.MovementState == AgentMovementState.IdleAfterShooting);

    }

    public bool CanFaceMouseDirection()
    {
        var physicsState = agentPhysicsState;

        return isAgentAlive && 
        physicsState.MovementState != AgentMovementState.Dashing &&
        physicsState.MovementState != AgentMovementState.SlidingLeft &&
        physicsState.MovementState != AgentMovementState.SlidingRight && 
        physicsState.MovementState != AgentMovementState.Rolling &&
        physicsState.MovementState != AgentMovementState.MonsterAttack &&
        physicsState.MovementState != AgentMovementState.StandingUpAfterRolling;
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
            case ItemAnimationSet.HoldingRifle:
            {
                position += new Vec2f(0.6f * physicsState.FacingDirection, 1.0f);
                break;
            }
            case ItemAnimationSet.HoldingPistol:
            {
                position += new Vec2f(0.35f * physicsState.FacingDirection, 1.0f);
                break;
            }
        }

        return position;
    }

    
    public void HandleItemSelected(ItemInventoryEntity item)
    {
        var itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

        if (hasAgentModel3D)
        {
            var model3d = agentModel3D;

            model3d.ItemAnimationSet = itemProperty.AnimationSet;

            if (model3d.Weapon != null)
            {
                UnityEngine.Object.Destroy(model3d.Weapon);
            }

            switch(itemProperty.ToolType)
            {
                case ItemToolType.Pistol:
                {
                    SetAgentWeapon(Model3DWeapon.Pistol);

                    break;
                }
                case ItemToolType.Rifle:
                {
                    SetAgentWeapon(Model3DWeapon.Rifle);
                    break;
                }
                case ItemToolType.Sword:
                {
                    SetAgentWeapon(Model3DWeapon.Sword);
                    break;
                }
                default:
                    SetAgentWeapon(Model3DWeapon.None);
                    break;
            }
        }

        if (isAgentPlayer && itemProperty.HasUI())
        {
            GameState.GUIManager.SetPanelActive(itemProperty.ItemPanelEnums);
        }
    }
    
    public void HandleItemDeselected(ItemInventoryEntity item)
    {
        var itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

        if (isAgentPlayer && itemProperty.HasUI())
        {
            GameState.GUIManager.SetPanelActive(itemProperty.ItemPanelEnums, false);
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
                UnityEngine.Object.Destroy(model3d.Weapon);
            }

            switch(weapon)
            {
                case Model3DWeapon.Sword:
                {
                    UnityEngine.GameObject hand = model3d.LeftHand;

                    UnityEngine.GameObject rapierPrefab = AssetManager.Singelton.GetModel(ModelType.Rapier);
                        UnityEngine.GameObject rapier = UnityEngine.Object.Instantiate(rapierPrefab);

                    var gunRotation = rapier.transform.rotation;
                    rapier.transform.parent = hand.transform;
                    rapier.transform.position = hand.transform.position;
                    rapier.transform.localRotation = gunRotation;
                    rapier.transform.localScale = new UnityEngine.Vector3(1.0f, 1.0f, 1.0f);

                    model3d.Weapon = rapier;
                    break;
                }

                case Model3DWeapon.Pistol:
                {
                    UnityEngine.GameObject hand = model3d.RightHand;
                    if (hand != null)
                    {
                        UnityEngine.GameObject prefab = AssetManager.Singelton.GetModel(ModelType.Pistol);
                            UnityEngine.GameObject gun = UnityEngine.Object.Instantiate(prefab);

                        var gunRotation = gun.transform.rotation;
                        gun.transform.parent = hand.transform;
                        gun.transform.position = hand.transform.position;
                        gun.transform.localRotation = gunRotation;
                        gun.transform.localScale = new UnityEngine.Vector3(1.0f, 1.0f, 1.0f);

                        model3d.Weapon = gun;

                    }
                    break;
                }

                case Model3DWeapon.Rifle:
                {
                    UnityEngine.GameObject hand = model3d.RightHand;
                    if (hand != null)
                    {
                        //UnityEngine.Transform ref_right_hand_grip = model3d.GameObject.transform.Find("ref_right_hand_grip");
                        //UnityEngine.Transform ref_left_hand_grip = model3d.GameObject.transform.Find("ref_left_hand_grip");
                        if(model3d.Weapon == null)
                        {
                            UnityEngine.Transform RiflePivot = model3d.GameObject.transform.Find("RiflePivot");
                            model3d.Weapon = RiflePivot.GetChild(0).gameObject;
                        }

                            //    UnityEngine.GameObject prefab = AssetManager.Singelton.GetModel(ModelType.SpaceGun);
                            //UnityEngine.GameObject gun = UnityEngine.Object.Instantiate(prefab);


                            //var gunRotation = gun.transform.rotation;
                            //gun.transform.parent = RiflePivot.transform;
                            //gun.transform.position = UnityEngine.Vector3.zero;
                            //gun.transform.localRotation = UnityEngine.Quaternion.identity;
                            //gun.transform.localScale = new UnityEngine.Vector3(100.0f, 100.0f, 100.0f);

                            //ref_right_hand_grip.transform.parent = gun.transform;
                            //ref_left_hand_grip.transform.parent = gun.transform;
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
            physicsState.MovementState = AgentMovementState.SlidingRight;
            physicsState.JumpCounter = 0;
        }
    }

    public void SlideLeft()
    {
        var physicsState = agentPhysicsState;

        if (IsStateFree() && isAgentPlayer)
        {
            physicsState.MovementState = AgentMovementState.SlidingLeft;
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

    public void PickaxeHit(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.PickaxeHit;

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = cooldown;
            physicsState.ActionCooldown = cooldown;
        }
    }

    public void ChopTree(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.ChoppingTree;

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


    public void MonsterAttack(float duration, float cooldown)
    {
        var physicsState = agentPhysicsState;
        var model3d = agentModel3D; 

        if (IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.MonsterAttack;
            physicsState.SetMovementState = true;
            

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = duration;
            physicsState.ActionCooldown = cooldown;      
        }
    }

    public void Dash(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;

        if (PhysicsState.DashCooldown <= 0.0f &&
        IsStateFree() && CanMove())
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

        if (IsStateFree() && PhysicsState.OnGrounded && CanMove())
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
        PhysicsState.MovementState != AgentMovementState.Crouch_Move &&
        CanMove())
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
                if (PhysicsState.MovingDirection != PhysicsState.FacingDirection)
                {
                    PhysicsState.MovementState = AgentMovementState.Crouch_MoveBackward;
                }
                else
                {
                    PhysicsState.MovementState = AgentMovementState.Crouch_Move;
                }
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

        if (IsStateFree() && !stats.IsLimping && CanMove())
        {
            // handling horizontal movement (left/right)
            if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed)
            {
                PhysicsState.Acceleration.X = horizontalDir * 2 * PhysicsState.Speed / Constants.TimeToMax;
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
        
        if (IsStateFree() && CanMove())
        {
            if (IsCrouched())
            {
                if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed/3) 
                {
                    PhysicsState.Acceleration.X = 2.0f * horizontalDir * PhysicsState.Speed / Constants.TimeToMax;
                }
                else if (Math.Abs(PhysicsState.Velocity.X) == PhysicsState.Speed/3) // Velocity equal drag.
                {
                    PhysicsState.Acceleration.X = 1.0f * horizontalDir * PhysicsState.Speed / Constants.TimeToMax;
                }
            }
            else
            {
                if (stats.IsLimping)
                {
                    if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed/3) 
                    {
                        PhysicsState.Acceleration.X = 2 * horizontalDir * PhysicsState.Speed / Constants.TimeToMax;
                    }
                    else if (Math.Abs(PhysicsState.Velocity.X) == PhysicsState.Speed/3) // Velocity equal drag.
                    {
                        PhysicsState.Acceleration.X = horizontalDir * PhysicsState.Speed / Constants.TimeToMax;
                    }
                }
                else
                {
                    if (Math.Abs(PhysicsState.Velocity.X) < PhysicsState.Speed/2) 
                    {
                       // PhysicsState.Acceleration.X = 2 * horizontalDir * PhysicsState.Speed / Constants.TimeToMax;
                       if (PhysicsState.OnGrounded)
                       {
                        if (horizontalDir != 0)
                        {
                        //PhysicsState.Acceleration = 500.0f * new Vec2f(PhysicsState.GroundNormal.Y, -PhysicsState.GroundNormal.X);
                        //PhysicsState.Acceleration.X *= horizontalDir;

                        PhysicsState.Velocity = PhysicsState.Speed * new Vec2f(PhysicsState.GroundNormal.Y, -PhysicsState.GroundNormal.X);
                        PhysicsState.Velocity *= horizontalDir;
                        }
                       }
                       else 
                       {
                        PhysicsState.Velocity.X = 1 * horizontalDir * PhysicsState.Speed;
                        //PhysicsState.Acceleration.X = 2 * horizontalDir * PhysicsState.Speed / Constants.TimeToMax;
                       }
                    }
                    else if (Math.Abs(PhysicsState.Velocity.X) == PhysicsState.Speed/2) // Velocity equal drag.
                    {
                     //   PhysicsState.Acceleration.X = horizontalDir * PhysicsState.Speed / Constants.TimeToMax;
                      if (PhysicsState.OnGrounded)
                       {
                        if (horizontalDir != 0)
                        {
                        //PhysicsState.Acceleration = 500.0f * new Vec2f(PhysicsState.GroundNormal.Y, -PhysicsState.GroundNormal.X);
                        //PhysicsState.Acceleration.X *= horizontalDir;


                        PhysicsState.Velocity = PhysicsState.Speed * new Vec2f(PhysicsState.GroundNormal.Y, -PhysicsState.GroundNormal.X);
                        PhysicsState.Velocity *= horizontalDir;
                        }
                       }
                       else 
                       {
                        PhysicsState.Velocity.X = horizontalDir * PhysicsState.Speed;
                        //PhysicsState.Acceleration.X = horizontalDir * PhysicsState.Speed / Constants.TimeToMax;
                       }
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


    public void Jump()
        {
            var physicsState = agentPhysicsState;
            if (IsStateFree() && CanMove())
            {
                // we can start jumping only if the jump counter is 0
                if (physicsState.JumpCounter == 0)
                {
                    
                        // first jump

                        // if we are sticking to a wall 
                        // throw the agent in the opphysicsStateite direction
                        // Inpulse so use immediate speed intead of acceleration.
                        if (physicsState.MovementState == AgentMovementState.SlidingLeft)
                        {
                            physicsState.Velocity.X = physicsState.Speed * 1.0f;
                        }
                        else if (physicsState.MovementState == AgentMovementState.SlidingRight)
                        {
                            physicsState.Velocity.X = - physicsState.Speed * 1.0f;
                        }

                        // jumping
                        physicsState.Velocity.Y = physicsState.InitialJumpVelocity;
                        physicsState.JumpCounter++;
                }
                else
                {
                    // double jump
                    if (physicsState.JumpCounter <= 1)
                    {
                        physicsState.Velocity.Y = physicsState.InitialJumpVelocity * 0.75f;
                        physicsState.JumpCounter++;
                    }
                }

                physicsState.OnGrounded = false;
            }
        }

}