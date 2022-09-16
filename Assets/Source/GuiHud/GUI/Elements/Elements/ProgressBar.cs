using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KGUI.Elements
{
    public class ProgressBar
    {
        // Bar Gameobject
        private GameObject Bar;

        // Bar Fill Value
        public float _fillValue;

        RectTransform rectTransform;
        UnityEngine.UI.Image imageComponent;

        // Constructor
        public ProgressBar(string barName, Transform parent, Sprite barTexture, float fillValue)
        {
            // Set Fill Value
            _fillValue = fillValue;

            // Create Gameobject
            Bar = new GameObject(barName);

            // Set Parent
            Bar.transform.parent = parent;

            // Add Rect Transform Component
            rectTransform = Bar.AddComponent<RectTransform>();

            // Add Image Component
            imageComponent = Bar.AddComponent<UnityEngine.UI.Image>();

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            // Set Sprite
            imageComponent.sprite = barTexture;

            // Set Raycast Target
            imageComponent.raycastTarget = true;

            // Set Maskable
            imageComponent.maskable = true;

            // Set Image Type
            imageComponent.type = UnityEngine.UI.Image.Type.Filled;

            // Set Fill Method
            imageComponent.fillMethod = UnityEngine.UI.Image.FillMethod.Horizontal;

            // Set Fill Origin
            imageComponent.fillOrigin = 0;

            // Set Fill Value
            imageComponent.fillAmount = fillValue;

            // Set Fil Clockwise
            imageComponent.fillClockwise = false;
        }

        public void SetType(UnityEngine.UI.Image.Type type)
        {
            // Set Image Type
            imageComponent.type = type;
        }

        public void SetFillMethod(UnityEngine.UI.Image.FillMethod type)
        {
            // Set Fill Method
            imageComponent.fillMethod = type;
        }

        public void SetSprite(Sprite sprite)
        {
            // Set Fill Method
            imageComponent.sprite = sprite;
        }

        public void SetRaycastTarget(bool raycastTarget)
        {
            // Set Fill Method
            imageComponent.raycastTarget = raycastTarget;
        }

        public void SetPosition(Vector3 newPos)
        {
            // Set Position
            rectTransform.localPosition = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            // Set Rotation
            rectTransform.localRotation = newRot;
        }

        public void SetScale(Vector3 newScale)
        {
            // Set Scale
            rectTransform.localScale = newScale;
        }

        public void Update(float fillValue)
        {
            // Update Fill Value
            _fillValue = fillValue;

            // Update Fill Value
            imageComponent.fillAmount = fillValue;
        }
    }
}
