using Enums;
using KGUI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace KGUI
{
    public class BedrockElementUI : UIElement
    {
        [SerializeField] private Image borderImage;

        public ImageWrapper Border;

        public override void Init()
        {
            base.Init();
            
            ID = UIElementID.BedrockElement;

            Icon = new ImageWrapper(iconImage, 16, 16,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Fuel\\hud_status_fuel.png", AtlasType.Gui);
            
            Border = new ImageWrapper(borderImage, 225, 225,
                "Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", AtlasType.Gui);
        }
    }
}
