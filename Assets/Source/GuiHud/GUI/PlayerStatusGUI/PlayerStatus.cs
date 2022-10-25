using UnityEngine;

namespace KGUI
{
    public class PlayerStatus : PanelUI
    {
        [SerializeField] private HealthElementUI healthElementUI;
        [SerializeField] private FoodElementUI foodElementUI;
        [SerializeField] private WaterElementUI waterElementUI;
        [SerializeField] private OxygenElementUI oxygenElementUI;
        [SerializeField] private FuelElementUI fuelElementUI;

        public override void Init()
        {
            ID = PanelEnums.PlayerStatus;

            UIElementList.Add(foodElementUI.ID, foodElementUI);
            UIElementList.Add(waterElementUI.ID, waterElementUI);
            UIElementList.Add(oxygenElementUI.ID, oxygenElementUI);
            UIElementList.Add(fuelElementUI.ID, fuelElementUI);
            UIElementList.Add(healthElementUI.ID, healthElementUI);
            
            base.Init();
        }
    }
}

