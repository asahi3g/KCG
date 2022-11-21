using UnityEngine;

public class UIViewGame : UIView
{
    [Header(H_A + "Game" + H_B)]
    [SerializeField] private UIContent _contentQuickSlots;

    [SerializeField] private UIStatsComponentRendererFloat _food;
    [SerializeField] private UIStatsComponentRendererFloat _water;
    [SerializeField] private UIStatsComponentRendererFloat _oxygen;
    [SerializeField] private UIStatsComponentRendererFloat _fuel;
    
    
    protected override void OnGroupOpened()
    {
        
    }

    protected override void OnGroupClosed()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        Game.Instance.onPlayerCharacterAdded.AddListener(OnPlayerCharacterAdded);
    }

    public bool GetQuickSlot(int index, out UIContentElementInventory slot)
    {
        slot = null;
        UIContentElementInventory[] slots = _contentQuickSlots.GetComponentsInChildren<UIContentElementInventory>();
        if (index >= 0 && index < slots.Length)
        {
            slot = slots[index];
        }
        return slot != null;
    }

    private void OnPlayerCharacterAdded(CharacterRenderer cr)
    {
        Agent.StatsComponent stats = cr.GetAgent().agentStats;
        
        _food.SetStats(stats);
        _water.SetStats(stats);
        _oxygen.SetStats(stats);
        _fuel.SetStats(stats);
    }
}
