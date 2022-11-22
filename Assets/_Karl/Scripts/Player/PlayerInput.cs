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
        
        if (App.Instance.GetUI().GetView<UIViewGame>().GetQuickSlot(slotIndex, out UIContentElementInventory slot))
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
        
        if (Game.Instance.GetCurrentPlayerCharacter(out CharacterRenderer character)) character.GetAgent().Jump();
    }

    public void DoPlayerJetpackBegin()
    {
        if (IsGameplayBlocked()) return;

        if (Game.Instance.GetCurrentPlayerCharacter(out CharacterRenderer character)) character.GetAgent().JetPackFlyingBegin();
    }
    
    public void DoPlayerJetpackEnd()
    {
        if (Game.Instance.GetCurrentPlayerCharacter(out CharacterRenderer character)) character.GetAgent().JetPackFlyingEnd();
    }

    public bool DoPlayerWalk(bool left, bool right)
    {
        if (Game.Instance.GetCurrentPlayerCharacter(out CharacterRenderer character)) character.GetAgent().Walk(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerCrouchBegin(bool left, bool right)
    {
        if (Game.Instance.GetCurrentPlayerCharacter(out CharacterRenderer character)) character.GetAgent().CrouchBegin(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerCrouchEnd(bool left, bool right)
    {
        if (Game.Instance.GetCurrentPlayerCharacter(out CharacterRenderer character)) character.GetAgent().CrouchEnd(GetPlayerDirection(left, right));
        return true;
    }
    
    public bool DoPlayerSprint(bool left, bool right)
    {
        if (Game.Instance.GetCurrentPlayerCharacter(out CharacterRenderer character)) character.GetAgent().Run(GetPlayerDirection(left, right));
        return true;
    }

    public void DoPlayerDash(bool left, bool right)
    {
        if (Game.Instance.GetCurrentPlayerCharacter(out CharacterRenderer character)) character.GetAgent().Dash(GetPlayerDirection(left, right));
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
