using Enums;
using Inventory;
using Item;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : BaseMonoBehaviour
{
    [SerializeField] private Player _player;
    [Space]
    [SerializeField] private Identifier _identifier;
    [SerializeField] private Identifier _gameplay;

    public readonly UnityEvent onTick = new UnityEvent();

    public Identifier GetGameplay() => _gameplay;

    public bool IsGameplayBlocked() => _gameplay.GetObject().GetDependencies().Contains();

    public void Tick()
    {
        onTick.Invoke();
    }

    public void DoSelectQuickSlot(int slotIndex)
    {
        if (IsGameplayBlocked()) return;
        
        if (App.Instance.GetUI().GetView<UIViewInventory>().GetSlot(slotIndex, out UIContentElementInventorySlot slot))
        {
            slot.Select();
            //Debug.Log($"Quick slot index[{slotIndex}] selected by keyboard");
        }
        else
        {
            Debug.LogWarning($"Quick slot with index[{slotIndex}] does not exist, skipping..");
        }
    }

    public void DoOpenEscapeMenu()
    {
        if (IsGameplayBlocked()) return;
        
        App.Instance.GetUI().GetView<UIViewMenu>().GetGroup().GetIdentifier().Alter(_identifier, true);
    }

    public void DoToggleInventory()
    {
        if (IsGameplayBlocked()) return;
        
        App.Instance.GetUI().GetView<UIViewInventory>().GetToggableInventoryGroup().GetIdentifier().Toggle(_identifier);
    }

    public void DoPlayerJump()
    {
        if (IsGameplayBlocked()) return;
        
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().Jump();
    }

    public void DoPlayerJetpackBegin()
    {
        if (IsGameplayBlocked()) return;

        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().JetPackFlyingBegin();
    }
    
    public void DoPlayerJetpackEnd()
    {
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().JetPackFlyingEnd();
    }

    public bool DoPlayerWalk(bool left, bool right)
    {
        if (IsGameplayBlocked()) return false;
        
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().Walk(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerCrouchBegin(bool left, bool right)
    {
        if (IsGameplayBlocked()) return false;
        
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().CrouchBegin(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerCrouchEnd(bool left, bool right)
    {
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().CrouchEnd(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerSprint(bool left, bool right)
    {
        if (IsGameplayBlocked()) return false;
        
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().Run(GetPlayerDirection(left, right));
        return true;
    }

    public void DoPlayerDash(bool left, bool right)
    {
        if (IsGameplayBlocked()) return;
        
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().Dash(GetPlayerDirection(left, right));
    }

    public void DoPlayerFire()
    {
        if (IsGameplayBlocked()) return;

        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
        {
            if (agentRenderer.GetInventory(out InventoryEntityComponent inventoryEntityComponent))
            {
                if (GameState.InventoryManager.GetItemInSlot(inventoryEntityComponent.Index, inventoryEntityComponent.SelectedSlotIndex, out ItemInventoryEntity itemInventoryEntity))
                {
                    ItemProperties itemProperty = GameState.ItemCreationApi.GetItemProperties(itemInventoryEntity.itemType.Type);
                    
                    if (itemProperty.IsTool() && agentRenderer.GetAgent().isAgentAlive)
                    {
                        GameState.ActionCreationSystem.CreateAction(itemProperty.ToolActionType, agentRenderer.GetAgent().agentID.ID, itemInventoryEntity.itemID.ID);
                    }
                }
            }
        }
    }

    public void DoSecondAction()
    {
        if (IsGameplayBlocked()) return;

        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
        {
            if (agentRenderer.GetInventory(out InventoryEntityComponent inventoryEntityComponent))
            {
                if (GameState.InventoryManager.GetItemInSlot(inventoryEntityComponent.Index, inventoryEntityComponent.SelectedSlotIndex, out ItemInventoryEntity itemInventoryEntity))
                {
                    ItemProperties itemProperty = GameState.ItemCreationApi.GetItemProperties(itemInventoryEntity.itemType.Type);

                    if (itemProperty.IsTool() && agentRenderer.GetAgent().isAgentAlive)
                    {
                        GameState.ActionCreationSystem.CreateAction(itemProperty.SecondToolActionType, agentRenderer.GetAgent().agentID.ID, itemInventoryEntity.itemID.ID);
                    }
                }
            }
        }
    }

    public void DoPlayerReload()
    {
        if (IsGameplayBlocked()) return;
        
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
        {
            if(agentRenderer.GetAgent().GetItem() != null)
            {
                if (GameState.ItemCreationApi.GetItemProperties(agentRenderer.GetAgent().GetItem().itemType.Type).Group == 
                    ItemGroups.ToolRangedWeapon)
                {
                    GameState.ActionCreationSystem.CreateAction(ActionType.ReloadAction, agentRenderer.GetAgent().agentID.ID);
                }
            }
        }
    }

    public void DoScreenshot()
    {
        if (IsGameplayBlocked()) return;

        var date = DateTime.Now;
        var fileName = date.Year.ToString() + "-" + date.Month.ToString() +
            "-" + date.Day.ToString() + "-" + date.Hour.ToString() + "-" + date.Minute.ToString() +
            "-" + date.Second.ToString() + "-" + date.Millisecond + ".png";
        ScreenCapture.CaptureScreenshot("Assets\\Screenshots\\" + fileName);

        GameState.AudioSystem.PlayOneShot("AudioClips\\steam_screenshot_effect");
    }

    public void DoPlayerLookTarget(Vector2 screenPosition)
    {
        if (IsGameplayBlocked()) return;
        
        if (_player.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent()
            .SetAimTarget(new KMath.Vec2f(screenPosition.x, screenPosition.y));
    }

    public int GetPlayerDirection(bool left, bool right)
    {
        // Either both are turned on or both are off - which means they cancel each other out and no need to do anything
        bool leftAndRight = left == right;
        if (leftAndRight) return 0;

        if (left) return -1;
        return 1;
    }
}
