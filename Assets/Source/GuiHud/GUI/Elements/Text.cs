using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KGUI.Elements
{
    public class Text : ElementManager
    {
        // Image Gameobject
        private GameObject textCanvas;

        // Countdown
        float timeLeft;

        // Start Life Time Condition
        public bool startLifeTime;

        // Start Life Time Condition
        public UIElementEntity entity;

        // Constructor
        public void Create(string objectName, string text, Transform parent, float lifeTime)
        {
            if (textCanvas != null)
                return;

            timeLeft = lifeTime;

            // Create Gameobject
            textCanvas = new GameObject(objectName);

            // Set Object Parent To Canvas
            textCanvas.transform.parent = parent;

            textCanvas.transform.position = Vector3.zero;
            textCanvas.transform.rotation = Quaternion.identity;

            // Add Rect Transform to Manage UI Scaling
            textCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            textCanvas.AddComponent<UnityEngine.UI.Text>();

            // Set Image Sprite
            textCanvas.GetComponent<UnityEngine.UI.Text>().text = text;
            textCanvas.GetComponent<UnityEngine.UI.Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font; ;
            textCanvas.GetComponent<UnityEngine.UI.Text>().fontSize = 20;
            textCanvas.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.Normal;

            // Set Anchor Min
            textCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            textCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            // Set Pivot
            textCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            textCanvas.GetComponent<UnityEngine.UI.Text>().enabled = false;

            // Reset Transform
            SetPosition(Vector3.zero);
        }

        public override void Draw()
        {
            textCanvas.GetComponent<UnityEngine.UI.Text>().enabled = true;
        }

        public GameObject GetGameObject()
        {
            return textCanvas;
        }

        public void SetPosition(Vector3 newPos)
        {
            // Set Local Rect Position
            textCanvas.GetComponent<RectTransform>().localPosition = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            // Set Local Rect Rotation
            textCanvas.GetComponent<RectTransform>().localRotation = newRot;
        }

        public void SetScale(Vector3 newScale)
        {
            // Set Local Rect Scale
            textCanvas.GetComponent<RectTransform>().localScale = newScale;
        }

        public void SetSizeDelta(Vector2 newSize)
        {
            // Set Local Rect Scale
            textCanvas.GetComponent<RectTransform>().sizeDelta = newSize;
        }

        public void UpdateText(string newText)
        {
            textCanvas.GetComponent<UnityEngine.UI.Text>().text = newText;
        }

        public override void Update()
        {
            if(startLifeTime)
            {
                timeLeft -= Time.deltaTime;
                if(timeLeft <= 0)
                {
                    GameObject.Destroy(textCanvas);
                    if(entity != null)
                        entity.Destroy();
                }
            }
        }

        public Transform GetTransform()
        {
            if (textCanvas == null)
                return null;

            // Return Transform
            return textCanvas.transform;
        }
    }
}
