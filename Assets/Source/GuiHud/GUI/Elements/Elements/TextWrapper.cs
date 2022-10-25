using TMPro;
using UnityEngine;

namespace KGUI.Elements
{
    public class TextWrapper : ElementManager
    {
        private GameObject textCanvas;
        private RectTransform rectTransform;
        private TextMeshProUGUI textComponent;

        // Countdown
        private float timeLeft;

        // Start Life Time Condition
        public bool StartLifeTime;

        // Start Life Time Condition
        public UIElementEntity Entity;

        // Constructor
        public void Create(string objectName, string text, Transform parent, float lifeTime)
        {
            if (textCanvas != null)
                return;

            timeLeft = lifeTime;
            
            textCanvas = new GameObject(objectName)
            {
                transform =
                {
                    parent = parent,
                    position = Vector3.zero,
                    rotation = Quaternion.identity
                }
            };
            
            rectTransform = textCanvas.AddComponent<RectTransform>();
            textComponent = textCanvas.AddComponent<TextMeshProUGUI>();
            
            textComponent.text = text;
            textComponent.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as TMP_FontAsset;
            textComponent.fontSize = 20;
            textComponent.fontStyle = FontStyles.Normal;
            
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(0, 0);

            textComponent.enabled = false;

            // Reset Transform
            SetPosition(Vector3.zero);
        }

        public override void Draw()
        {
            if (!textComponent.enabled)
            {
                textComponent.enabled = true;
            }
        }

        public GameObject GetGameObject()
        {
            return textCanvas;
        }

        public void SetPosition(Vector3 newPos)
        {
            // Set Local Rect Position
            rectTransform.localPosition = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            // Set Local Rect Rotation
            rectTransform.localRotation = newRot;
        }

        public void SetScale(Vector3 newScale)
        {
            // Set Local Rect Scale
            rectTransform.localScale = newScale;
        }

        public void SetSizeDelta(Vector2 newSize)
        {
            // Set Local Rect Scale
            rectTransform.sizeDelta = newSize;
        }

        public void UpdateText(string newText)
        {
            textComponent.text = newText;
        }

        public override void Update()
        {
            if(StartLifeTime)
            {
                timeLeft -= Time.deltaTime;
                if(timeLeft <= 0)
                {
                    GameObject.Destroy(textCanvas);
                    Entity?.Destroy();
                }
            }
        }

        public Transform GetTransform()
        {
            return textCanvas == null ? null : textCanvas.transform;
        }
    }
}
