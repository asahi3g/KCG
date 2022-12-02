using System;
using Agent;
using Collisions;
using Engine3D;
using Enums;
using Inventory;
using Item;
using KMath;
using Physics;
using Unity.VisualScripting;
using UnityEngine;

// TODO(Brandon): 
// AgentSystem should not be importing GUI (for call back)

public partial class AgentEntity
{
   public ItemInventoryEntity GetItem()
    {
        if (!hasAgentInventory)
            return null;

        int inventoryID = agentInventory.InventoryID;
        Inventory.InventoryEntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
        int selectedSlot = inventory.SelectedSlotIndex;
        return GameState.InventoryManager.GetItemInSlot(agentInventory.InventoryID, selectedSlot);
    }

   public bool CanMove()
    {
        var physicsState = agentPhysicsState;
        return isAgentAlive && physicsState.MovementState != AgentMovementState.IdleAfterShooting;
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

        return isAgentAlive && physicsState.MovementState == AgentMovementState.Crouch ||
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
        if (isAgentAlive)
        {
            isAgentAlive = false;

            var physicsState = agentPhysicsState;
            physicsState.MovementState = AgentMovementState.KnockedDownBack;

            physicsState.DyingDuration = 1.5f;
        }
    }

    public void Stagger()
    {
        if (isAgentAlive)
        {
            agentStagger.Stagger = true;
            isAgentAlive = false;
            Debug.Log("Freezed");
        }
    }

    public void UnStagger()
    {
        if (!isAgentAlive)
        {
            agentStagger.Stagger = false;
            isAgentAlive = true;
            Debug.Log("UnFreezed");

        }
    }

    public Vec2f GetGunFiringTarget()
    {
        var physicsState = agentPhysicsState;

        var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();

        float rightGunXPosition = physicsState.Position.X + 10.0f;
        float leftGunXPosition = physicsState.Position.X - 10.0f;

        if (worldPosition.X < rightGunXPosition && worldPosition.X > physicsState.Position.X)
        {
            worldPosition.X = physicsState.Position.X + 10.0f;
        }


        if (worldPosition.X > leftGunXPosition && worldPosition.X < physicsState.Position.X)
        {
            worldPosition.X = physicsState.Position.X - 10.0f;
        }

        return new Vec2f(worldPosition.X, worldPosition.Y);
    }

    public Vec2f GetGunOrigin()
    {
        if(agentPhysicsState.FacingDirection == 1)
            return agentPhysicsState.Position + new Vec2f(-0.28f, 1.75f);
        else if (agentPhysicsState.FacingDirection == -1)
            return agentPhysicsState.Position + new Vec2f(+0.3f, 1.75f);
        else
            return agentPhysicsState.Position + new Vec2f(-0.28f, 1.75f);
    }

    public Vec2f GetGunFiringPosition()
    {
        var physicsState = agentPhysicsState;
        var model3d = agentAgent3DModel;

        Vec2f targetPosition = GetGunFiringTarget();

        Vec2f position = GetGunOrigin();
        Vec2f dir = (targetPosition - position);
        //UnityEngine.Debug.Log(targetPosition);
        dir.Normalize();
        Vec2f newPosition = position + dir * 1.3f;
        position = newPosition;
        return position;
    }

    public bool CanSee(int targetId)
    {
        return LineOfSight.CanSeeAlert(agentID.ID, targetId);
     }

    public void SetAimTarget(Vec2f AimTarget)
    {
        agentAgent3DModel.AimTarget = AimTarget;
    }
    
    public void HandleItemDeselected(ItemInventoryEntity item)
    {
        if (item == null)
            return;
        var itemProperty = GameState.ItemCreationApi.GetItemProperties(item.itemType.Type);

        if (isAgentPlayer && itemProperty.HasUI())
        {
            GameState.GUIManager.SetPanelActive(itemProperty.ItemPanelEnums, false);
        }
    }

    public void ClearModel3DWeapon()
    {
        SetModel3DWeapon(Model3DWeaponType.None);
    }
    
    public void SetModel3DWeapon(ItemInventoryEntity item)
    {
        if (!hasAgentAgent3DModel) return;
        
        SetModel3DWeapon(item.itemType.Type);
    }

    public void SetModel3DWeapon(Enums.ItemType itemType)
    {
        if (!hasAgentAgent3DModel) return;
        
        var itemProperty = GameState.ItemCreationApi.GetItemProperties(itemType);
        agentAgent3DModel.ItemAnimationSet = itemProperty.AnimationSet;
        SetModel3DWeapon(GetModel3DWeaponFromItemToolType(itemProperty.ToolType));
    }

    public void SetModel3DWeapon(Model3DWeaponType weapon)
    {
        if (!hasAgentAgent3DModel) return;

        Agent3DModel model3d = agentAgent3DModel;
        model3d.CurrentWeapon = weapon;
        
        switch(weapon)
        {
            case Model3DWeaponType.None:
            {
                model3d.Weapon = null;
                break;
            }
            case Model3DWeaponType.Sword:
            {
                if (AssetManager.Singelton.GetPrefabItem(ItemModelType.Rapier, out AgentEquippedItemRenderer itemRenderer))
                {
                    UnityEngine.Transform hand = model3d.Renderer.GetHandLeft();
                    UnityEngine.GameObject rapier = UnityEngine.Object.Instantiate(itemRenderer).gameObject;

                    var gunRotation = rapier.transform.rotation;
                    rapier.transform.parent = hand;
                    rapier.transform.position = hand.position;
                    rapier.transform.localRotation = gunRotation;
                    rapier.transform.localScale = new UnityEngine.Vector3(1.0f, 1.0f, 1.0f);

                    model3d.Weapon = rapier;
                }
                
                break;
            }

            case Model3DWeaponType.Pistol:
            {
                UnityEngine.Transform hand = model3d.Renderer.GetHandRight();
                if (hand != null)
                {
                    if (model3d.Weapon == null)
                    {
                        model3d.Weapon = model3d.Renderer.GetPivotPistol().gameObject;
                    }
                }
                break;
            }

            case Model3DWeaponType.Rifle:
            {
                UnityEngine.Transform hand = model3d.Renderer.GetHandRight();
                if (hand != null)
                {
                    if(model3d.Weapon == null)
                    {
                        model3d.Weapon = model3d.Renderer.GetPivotRifle().gameObject;
                    }

                }
                break;
            }
        }
        
        
    }

    public void SlideRight()
    {
         var physicsState = agentPhysicsState;

        if (isAgentAlive && IsStateFree() && isAgentPlayer)
        {
            physicsState.MovementState = AgentMovementState.SlidingRight;
            physicsState.JumpCounter = 0;
        }
    }

    public void SlideLeft()
    {
        var physicsState = agentPhysicsState;

        if (isAgentAlive && IsStateFree() && isAgentPlayer)
        {
            physicsState.MovementState = AgentMovementState.SlidingLeft;
            physicsState.JumpCounter = 0;
        }
    }


    public void FireGun(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (isAgentAlive/* && IsStateFree()*/)
        {
           // physicsState.MovementState = AgentMovementState.FireGun;
            

          //  physicsState.ActionInProgress = true;
          //  physicsState.ActionDuration = cooldown;
        }
    }

    public void PickaxeHit(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (isAgentAlive && IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.PickaxeHit;

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = cooldown;
        }
    }

    public void ChopTree(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (isAgentAlive && IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.ChoppingTree;

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = cooldown;
        }
    }

    public void UseTool(float cooldown)
    {
        var physicsState = agentPhysicsState;

        if (isAgentAlive && IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.UseTool;

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = cooldown;
        }
    }

    public void UsePotion(float duration)
    {
        var physicsState = agentPhysicsState;

        if (isAgentAlive && IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.Drink;

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = duration;
        }
    }


    public void MonsterAttack(float duration)
    {
        var physicsState = agentPhysicsState;
        var model3d = agentAgent3DModel; 

        if (isAgentAlive && IsStateFree())
        {
            physicsState.MovementState = AgentMovementState.MonsterAttack;
            physicsState.SetMovementState = true;
            

            physicsState.ActionInProgress = true;
            physicsState.ActionDuration = duration;
        }
    }

    public void SwordSlash()
    {
        var PhysicsState = agentPhysicsState;

        if (IsStateFree())
        {
            //PhysicsState.Velocity.X = 4 * PhysicsState.Speed * horizontalDir;
            //PhysicsState.Velocity.Y = 0.0f;

            //PhysicsState.Invulnerable = false;
            //PhysicsState.AffectedByGravity = true;
            PhysicsState.MovementState = AgentMovementState.SwordSlash;
        }
    }

    public void JetPackFlyingBegin()
    {
        if (agentStats.Fuel.GetValue() <= agentStats.Fuel.GetValue()) return;
        if (!IsStateFree()) return;
        
        agentPhysicsState.MovementState = AgentMovementState.JetPackFlying;
    }

    public void JetPackFlyingEnd()
    {
        if (agentPhysicsState.MovementState != AgentMovementState.JetPackFlying) return;
        
        agentPhysicsState.MovementState = AgentMovementState.None;
    }

    public void Knockback(float velocity, int horizontalDir)
    {
        var physicsState = agentPhysicsState;

        physicsState.Velocity.X = velocity * horizontalDir;
        physicsState.MovementState = AgentMovementState.Stagger;
        physicsState.StaggerDuration = 1.0f;
    }

    public void Dash(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;

        if (isAgentAlive && PhysicsState.DashCooldown <= 0.0f && IsStateFree() && CanMove())
        {
            PhysicsState.Invulnerable = true;
            PhysicsState.AffectedByGravity = false;
            PhysicsState.MovementState = AgentMovementState.Dashing;
            GameState.AgentIKSystem.SetIKEnabled(false);
            PhysicsState.DashDuration = Physics.Constants.DashTime;
            PhysicsState.DashCooldown = Physics.Constants.DashCooldown;
        }
    }


    public void Roll(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;

        if (isAgentAlive && IsStateFree() && PhysicsState.OnGrounded && CanMove())
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
            PhysicsState.RollCooldown = 1.75f;
        }
    }

    public void CrouchBegin(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;

        if (isAgentAlive && IsStateFree() && PhysicsState.OnGrounded && 
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

    public void CrouchEnd(int horizontalDir)
    {
        var s = agentPhysicsState;

        if (isAgentAlive && (s.MovementState == AgentMovementState.Crouch || s.MovementState == AgentMovementState.Crouch_Move))
        {
            if (horizontalDir == 0)
            {
                s.MovementState = AgentMovementState.Idle;
            }
            else
            {

                if (s.MovingDirection != s.FacingDirection)
                {
                    s.MovementState = AgentMovementState.MoveBackward;
                }
                else
                {
                    s.MovementState = AgentMovementState.Move;
                }
            }
        }
    }



    public void Run(int horizontalDir)
    {
        var PhysicsState = agentPhysicsState;
        var stats = agentStats;

        if (isAgentAlive && IsStateFree() && !stats.IsLimping && CanMove())
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
        Walk(horizontalDir, agentPhysicsState.Speed);
    }

    public void Walk(int horizontalDir, float speed)
    {
        var PhysicsState = agentPhysicsState;
        var stats = agentStats;
        
        if (isAgentAlive && IsStateFree() && CanMove())
        {
            if (IsCrouched())
            {
                if (Math.Abs(PhysicsState.Velocity.X) < speed / 3) 
                {
                    PhysicsState.Acceleration.X = 2.0f * horizontalDir * speed / Physics.Constants.TimeToMax;
                }
                else if (Math.Abs(PhysicsState.Velocity.X) == speed / 3) // Velocity equal drag.
                {
                    PhysicsState.Acceleration.X = 1.0f * horizontalDir * speed / Physics.Constants.TimeToMax;
                }
            }
            else
            {
                if (stats.IsLimping)
                {
                    if (Math.Abs(PhysicsState.Velocity.X) < speed / 3) 
                    {
                        PhysicsState.Acceleration.X = 2 * horizontalDir * speed / Physics.Constants.TimeToMax;
                    }
                    else if (Math.Abs(PhysicsState.Velocity.X) == speed / 3) // Velocity equal drag.
                    {
                        PhysicsState.Acceleration.X = horizontalDir * speed / Physics.Constants.TimeToMax;
                    }
                }
                else
                {
                    if (Math.Abs(PhysicsState.Velocity.X) < speed / 2) 
                    {
                       if (PhysicsState.OnGrounded)
                       {
                        if (horizontalDir != 0)
                        {
                        PhysicsState.Velocity = speed * new Vec2f(PhysicsState.GroundNormal.Y, -PhysicsState.GroundNormal.X);
                        PhysicsState.Velocity *= horizontalDir;
                        }
                       }
                       else 
                       {
                        PhysicsState.Velocity.X = 1 * horizontalDir * speed;
                       }
                    }
                    else if (Math.Abs(PhysicsState.Velocity.X) == speed / 2) // Velocity equal drag.
                    {
                      if (PhysicsState.OnGrounded)
                       {
                        if (horizontalDir != 0)
                        {
                            PhysicsState.Velocity = speed * new Vec2f(PhysicsState.GroundNormal.Y, -PhysicsState.GroundNormal.X);
                            PhysicsState.Velocity *= horizontalDir;
                        }
                       }
                       else 
                       {
                        PhysicsState.Velocity.X = horizontalDir * speed;
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


    public void Jump() {
        
        var physicsState = agentPhysicsState;
        if (isAgentAlive && IsStateFree() && CanMove())
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

    private Model3DWeaponType GetModel3DWeaponFromItemToolType(ItemToolType itemToolType)
    {
        switch (itemToolType)
        {
            case ItemToolType.None: return Model3DWeaponType.None;
            case ItemToolType.Sword: return Model3DWeaponType.Sword;
            case ItemToolType.Pistol: return Model3DWeaponType.Pistol;
            case ItemToolType.Rifle: return Model3DWeaponType.Rifle;
        }
        
        Debug.LogWarning($"Cant resolve {nameof(ItemToolType)}.{itemToolType} as {nameof(Model3DWeaponType)} type");
        return Model3DWeaponType.None;
    }

    public Vec2f GetFeetParticleSpawnPosition()
    {
        var physicsState = agentPhysicsState;

        Vec2f result = physicsState.Position;
        if (physicsState.FacingDirection == 1)
        {
            result += new Vec2f(-0.44f, 1.2f);
        }
        else if (physicsState.FacingDirection == -1)
        {
            result += new Vec2f(0.44f, 1.2f);
        }

        return result;
    }


}