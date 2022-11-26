using Agent;
using UnityEngine;

public class UIViewStats : UIView
{
    [Header(H_A + "Stats" + H_B)]
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

    public void SetStats(StatsComponent statsComponent)
    {
        _food.SetStats(statsComponent);
        _water.SetStats(statsComponent);
        _oxygen.SetStats(statsComponent);
        _fuel.SetStats(statsComponent);
    }

}
