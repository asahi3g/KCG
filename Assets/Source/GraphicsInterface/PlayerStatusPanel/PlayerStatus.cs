//imports UnityEngine

namespace KGUI
{
    public class PlayerStatus : PanelUI
    {
        [UnityEngine.SerializeField] private HealthElementUI healthElementUI;
        [UnityEngine.SerializeField] private FoodElementUI foodElementUI;
        [UnityEngine.SerializeField] private WaterElementUI waterElementUI;
        [UnityEngine.SerializeField] private OxygenElementUI oxygenElementUI;
        [UnityEngine.SerializeField] private FuelElementUI fuelElementUI;

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

