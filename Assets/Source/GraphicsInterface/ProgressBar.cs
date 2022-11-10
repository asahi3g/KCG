// imports UnityEngine

using System;
using Utility;

namespace KGUI.Elements
{
    // Progress bar
    // Fill amount from 0 to 1
    // means if 0.5 then it will be half of an image rendered
    [Serializable]
    public class ProgressBar
    {
        [UnityEngine.SerializeField] private ImageWrapper icon;

        public ProgressBar(ImageWrapper imageWrapper, float fillValue, UnityEngine.UI.Image.FillMethod fillMethod)
        {
            icon = imageWrapper;

            icon.UnityImage.raycastTarget = true;
            icon.UnityImage.maskable = true;
            icon.UnityImage.type = UnityEngine.UI.Image.Type.Filled;
            icon.UnityImage.fillMethod = fillMethod;
            icon.UnityImage.fillOrigin = 0;
            icon.UnityImage.fillAmount = fillValue;
            icon.UnityImage.fillClockwise = true;
            icon.UnityImage.enabled = false;
        }
        
        public void Init(UnityEngine.Sprite sprite, float fillValue, UnityEngine.UI.Image.FillMethod fillMethod)
        {
            icon.Init(sprite);

            icon.UnityImage.raycastTarget = true;
            icon.UnityImage.maskable = true;
            icon.UnityImage.type = UnityEngine.UI.Image.Type.Filled;
            icon.UnityImage.fillMethod = fillMethod;
            icon.UnityImage.fillOrigin = 0;
            icon.UnityImage.fillAmount = fillValue / 100f;
            icon.UnityImage.fillClockwise = true;
            icon.UnityImage.enabled = false;
        }
        
        public void Init(int width, int height, string path, Enums.AtlasType atlasType, float fillValue, UnityEngine.UI.Image.FillMethod fillMethod)
        {
            icon.Init(width, height, path, atlasType);

            icon.UnityImage.raycastTarget = true;
            icon.UnityImage.maskable = true;
            icon.UnityImage.type = UnityEngine.UI.Image.Type.Filled;
            icon.UnityImage.fillMethod = fillMethod;
            icon.UnityImage.fillOrigin = 0;
            icon.UnityImage.fillAmount = fillValue / 100f;
            icon.UnityImage.fillClockwise = true;
            icon.UnityImage.enabled = false;
        }

        public void Update(float fillValue)
        {
            icon.UnityImage.fillAmount = fillValue / 100f;
        }

        public void Draw()
        {
            if (!icon.UnityImage.enabled)
            {
                icon.UnityImage.enabled = true;
            }
        }
    }
}
