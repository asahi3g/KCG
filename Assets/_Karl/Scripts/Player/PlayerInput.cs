using Enums;
using Inventory;
using Item;
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
        
        if (App.Instance.GetUI().GetView<UIViewGame>().GetInventory().GetSlot(slotIndex, out UIContentElementInventorySlot slot))
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

    public void DoPlayerJump()
    {
        if (IsGameplayBlocked()) return;
        
        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().Jump();
    }

    public void DoPlayerJetpackBegin()
    {
        if (IsGameplayBlocked()) return;

        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().JetPackFlyingBegin();
    }
    
    public void DoPlayerJetpackEnd()
    {
        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().JetPackFlyingEnd();
    }

    public bool DoPlayerWalk(bool left, bool right)
    {
        if (IsGameplayBlocked()) return false;
        
        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().Walk(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerCrouchBegin(bool left, bool right)
    {
        if (IsGameplayBlocked()) return false;
        
        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().CrouchBegin(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerCrouchEnd(bool left, bool right)
    {
        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().CrouchEnd(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerSprint(bool left, bool right)
    {
        if (IsGameplayBlocked()) return false;
        
        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().Run(GetPlayerDirection(left, right));
        return true;
    }

    public void DoPlayerDash(bool left, bool right)
    {
        if (IsGameplayBlocked()) return;
        
        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer)) agentRenderer.GetAgent().Dash(GetPlayerDirection(left, right));
    }

    public void DoPlayerFire()
    {
        if (IsGameplayBlocked()) return;

        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
        {
            if (agentRenderer.GetInventory(out InventoryEntityComponent inventoryEntityComponent))
            {
                if (GameState.InventoryManager.GetItemInSlot(inventoryEntityComponent.Index, inventoryEntityComponent.SelectedSlotIndex, out ItemInventoryEntity itemInventoryEntity))
                {
                    ItemProperties itemProperty = GameState.ItemCreationApi.Get(itemInventoryEntity.itemType.Type);
            
                    if (itemProperty.IsTool())
                    {
                        GameState.ActionCreationSystem.CreateAction(itemProperty.ToolActionType, agentRenderer.GetAgent().agentID.ID);
                    }
                }
            }
        }
    }

    public void DoPlayerReload()
    {
        if (IsGameplayBlocked()) return;
        
        if (Game.Instance.GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
        {
            GameState.ActionCreationSystem.CreateAction(ItemUsageActionType.ReloadAction, agentRenderer.GetAgent().agentID.ID);
        }
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
