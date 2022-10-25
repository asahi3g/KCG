using TMPro;
using UnityEngine;

namespace Utility
{
    public class TextWrapper
    {
        private GameObject textObject;
        private RectTransform rectTransform;
        private TextMeshProUGUI textComponent;

        // Countdown
        private float timeLeft;

        // Start Life Time Condition
        public bool StartLifeTime;

        // Constructor
        public void Create(string objectName, string text, Transform parent, float lifeTime)
        {
            if (textObject != null)
                return;

            timeLeft = lifeTime;
            
            textObject = new GameObject(objectName)
            {
                transform =
                {
                    parent = parent,
                    position = Vector3.zero,
                    rotation = Quaternion.identity
                }
            };
            
            rectTransform = textObject.AddComponent<RectTransform>();
            textComponent = textObject.AddComponent<TextMeshProUGUI>();
            
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

        public void Draw()
        {
            if (!textComponent.enabled)
            {
                textComponent.enabled = true;
            }
        }

        public GameObject GetGameObject()
        {
            return textObject;
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

        public void SetSizeDelta(Vector2 newSize)
        {
            rectTransform.sizeDelta = newSize;
        }

        public void UpdateText(string newText)
        {
            textComponent.text = newText;
        }

        public void Update()
        {
            if(StartLifeTime)
            {
                timeLeft -= Time.deltaTime;
                if(timeLeft <= 0)
                {
                    GameObject.Destroy(textObject);
                }
            }
        }
    }
}
