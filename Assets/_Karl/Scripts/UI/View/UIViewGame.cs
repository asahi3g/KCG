using Agent;
using UnityEngine;

public class UIViewGame : UIView
{
    [Header(H_A + "Game" + H_B)]
    [SerializeField] private UIInventory _inventory;

    [SerializeField] private UIStatsComponentRendererFloat _food;
    [SerializeField] private UIStatsComponentRendererFloat _water;
    [SerializeField] private UIStatsComponentRendererFloat _oxygen;
    [SerializeField] private UIStatsComponentRendererFloat _fuel;

    public UIInventory GetInventory() => _inventory;

    protected override void OnGroupOpened()
    {
        
    }

    protected override void OnGroupClosed()
    {
        
    }

    public void SetStats(StatsComponent statsComponent)
    {
        _food.SetStats(statsComponent);
        _water.SetStats(statsComponent);
        _oxygen.SetStats(statsComponent);
        _fuel.SetStats(statsComponent);
    }
}
