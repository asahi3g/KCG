using UnityEngine;

public class PlayerInputStandalone : BaseMonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private UIInputArea _inputArea;
    [Header("Player Controls")]
    [SerializeField] private SOInput _moveUp;
    [SerializeField] private SOInput _moveDown;
    [SerializeField] private SOInput _moveLeft;
    [SerializeField] private SOInput _moveRight;
    [SerializeField] private SOInput _jump;
    [SerializeField] private SOInput _crouch;
    [SerializeField] private SOInput _sprint;
    [SerializeField] private SOInput _speedDash;
    [SerializeField] private SOInput _fire;
    [SerializeField] private SOInput _secondAction;
    [SerializeField] private SOInput _jetpack;
    [SerializeField] private SOInput _reload;
    [SerializeField] private SOInput _inventory;
    [Header("Other")]
    [SerializeField] private SOInput _mainMenu;
    [SerializeField] private SOInput[] _quickSlots;
    

    protected override void OnEnable()
    {
        base.OnEnable();
        _input.onTick.AddListener(OnInputTick);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _input.onTick.RemoveListener(OnInputTick);
    }


    private void OnInputTick()
    {
        UpdateInputs();
    }

    private void UpdateInputs()
    {
        // Main menu
        if (IsKeyDown(_mainMenu)) _input.DoOpenEscapeMenu();

        if (IsKeyDown(_inventory)) _input.DoToggleInventory();

        // Movement
        bool left = IsKey(_moveLeft);
        bool right = IsKey(_moveRight);
        bool up = IsKey(_moveUp);
        bool down = IsKey(_moveDown);
        
        // Look Target
        _input.DoPlayerLookTarget(_inputArea.GetLastMove().position);

        // Crouch
        if (IsKeyDown(_crouch)) _input.DoPlayerCrouchBegin(left, right);
        if (IsKeyUp(_crouch)) _input.DoPlayerCrouchEnd(left, right);

        // Walk
        _input.DoPlayerWalk(left, right);
        
        // Sprint
        if (IsKey(_sprint)) _input.DoPlayerSprint(left, right);

        // Dash
        if(IsKeyDown(_speedDash)) _input.DoPlayerDash(left, right);
        
        // Jump
        if(IsKeyDown(_jump)) _input.DoPlayerJump();
        
        // Jetpack
        if(IsKeyDown(_jetpack)) _input.DoPlayerJetpackBegin();
        if(IsKeyUp(_jetpack)) _input.DoPlayerJetpackEnd();
        
        // Fire
        if(IsKeyDown(_fire)) _input.DoPlayerFire();

        // Second Action
        if (IsKeyDown(_secondAction)) _input.DoSecondAction();

        // Reload
        if (IsKeyDown(_reload)) _input.DoPlayerReload();
        

        // Footer quick slots inventory
        UpdateQuickSlots();
    }

    private void UpdatePlayer()
    {
        bool left = IsKey(_moveLeft);
        bool right = IsKey(_moveLeft);
        bool leftAndRight = left && right;

        // If both are pressed we cancel them
        if (!leftAndRight && (left || right))
        {
            float x = 0f;
            if (left) x = -1;
            if (right) x = 1;
            
            
        }
    }

    private void UpdateQuickSlots()
    {
        int length = _quickSlots.Length;
        for (int i = 0; i < length; i++)
        {
            SOInput input = _quickSlots[i];
            if(input == null) continue;
            
            if (IsKeyDown(input))
            {
                _input.DoSelectQuickSlot(i);
            }
        }
    }


    private bool IsKeyDown(SOInput input)
    {
        return Input.GetKeyDown(input.GetPrimary()) || Input.GetKeyDown(input.GetSecondary());
    }
    
    private bool IsKeyUp(SOInput input)
    {
        return Input.GetKeyUp(input.GetPrimary()) || Input.GetKeyUp(input.GetSecondary());
    }

    private bool IsKey(SOInput input)
    {
        return Input.GetKey(input.GetPrimary()) || Input.GetKey(input.GetSecondary());
    }
}
