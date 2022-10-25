using UnityEngine;
using Utility;

namespace KGUI.Elements
{
    // Progress bar
    // Fill amount from 0 to 1
    // means if 0.5 then it will be half of an image rendered
    public class ProgressBar
    {
        private UnityEngine.UI.Image UnityImage;
        
        private float fillValue;
        
        private readonly GameObject progressBar;
        private readonly RectTransform rectTransform;

        
        public ProgressBar(string barName, Transform parent, Sprite barTexture, float fillValue, UnityEngine.UI.Image.FillMethod fillMethod)
        {
            this.fillValue = fillValue;
            progressBar = new GameObject(barName)
            {
                transform =
                {
                    // Set Parent
                    parent = parent
                }
            };
            
            rectTransform = progressBar.AddComponent<RectTransform>();
            UnityImage = progressBar.AddComponent<UnityEngine.UI.Image>();
            
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(0, 0);
            rectTransform.localPosition = Vector3.zero;
            
            UnityImage.sprite = barTexture;
            UnityImage.raycastTarget = true;
            UnityImage.maskable = true;
            UnityImage.type = UnityEngine.UI.Image.Type.Filled;
            UnityImage.fillMethod = fillMethod;
            UnityImage.fillOrigin = 0;
            UnityImage.fillAmount = fillValue;
            UnityImage.fillClockwise = true;
            UnityImage.enabled = false;
        }

        public ProgressBar(ImageWrapper progressBarImageWrapper, float fillValue, UnityEngine.UI.Image.FillMethod fillMethod)
        {
            this.fillValue = fillValue;
            progressBar = progressBarImageWrapper.GameObject;
            rectTransform = progressBarImageWrapper.RectTransform;
            UnityImage = progressBarImageWrapper.UnityImage;

            UnityImage.raycastTarget = true;
            UnityImage.maskable = true;
            UnityImage.type = UnityEngine.UI.Image.Type.Filled;
            UnityImage.fillMethod = fillMethod;
            UnityImage.fillOrigin = 0;
            UnityImage.fillAmount = fillValue;
            UnityImage.fillClockwise = true;
            UnityImage.enabled = false;
        }

        public void SetPosition(Vector3 newPos)
        {
            rectTransform.localPosition = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            rectTransform.localRotation = newRot;
        }

        public void SetScale(Vector3 newScale)
        {
            rectTransform.localScale = newScale;
        }

        public void SetSize(Vector2 newSize)
        {
            rectTransform.sizeDelta = newSize;
        }

        public void Update(float fillValue)
        {
            this.fillValue = fillValue / 100;
            UnityImage.fillAmount = this.fillValue;
        }

        public void Draw()
        {
            if (!UnityImage.enabled)
            {
                UnityImage.enabled = true;
            }
        }
    }
}
