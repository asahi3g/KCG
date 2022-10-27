//imports UnityEngine

using TMPro;

namespace Utility
{
    public class TextWrapper
    {
        private UnityEngine.GameObject textObject;
        private UnityEngine.RectTransform rectTransform;
        private TextMeshProUGUI textComponent;

        // Countdown
        private float timeLeft;

        // Start Life Time Condition
        public bool StartLifeTime;

        // Constructor
        public void Create(string objectName, string text, UnityEngine.Transform parent, float lifeTime)
        {
            if (textObject != null)
                return;

            timeLeft = lifeTime;
            
            textObject = new UnityEngine.GameObject(objectName)
            {
                transform =
                {
                    parent = parent,
                    position = UnityEngine.Vector3.zero,
                    rotation = UnityEngine.Quaternion.identity
                }
            };
            
            rectTransform = textObject.AddComponent<UnityEngine.RectTransform>();
            textComponent = textObject.AddComponent<TextMeshProUGUI>();
            
            textComponent.text = text;
            textComponent.font = UnityEngine.Resources.GetBuiltinResource(typeof(UnityEngine.Font), "Arial.ttf") as TMP_FontAsset;
            textComponent.fontSize = 20;
            textComponent.fontStyle = FontStyles.Normal;
            
            rectTransform.anchorMin = new UnityEngine.Vector2(0, 0);
            rectTransform.anchorMax = new UnityEngine.Vector2(0, 0);
            rectTransform.pivot = new UnityEngine.Vector2(0, 0);

            textComponent.enabled = false;

            // Reset Transform
            SetPosition(UnityEngine.Vector3.zero);
        }

        public void Draw()
        {
            if (!textComponent.enabled)
            {
                textComponent.enabled = true;
            }
        }

        public UnityEngine.GameObject GetGameObject()
        {
            return textObject;
        }

        public void SetPosition(UnityEngine.Vector3 newPos)
        {
            rectTransform.localPosition = newPos;
        }

        public void SetRotation(UnityEngine.Quaternion newRot)
        {
            rectTransform.localRotation = newRot;
        }

        public void SetScale(UnityEngine.Vector3 newScale)
        {
            rectTransform.localScale = newScale;
        }

        public void SetSizeDelta(UnityEngine.Vector2 newSize)
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
                timeLeft -= UnityEngine.Time.deltaTime;
                if(timeLeft <= 0)
                {
                    UnityEngine.GameObject.Destroy(textObject);
                }
            }
        }
    }
}
