using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KGUI.Elements
{
    public class Image : ElementManager
    {
        // Image GameObject
        private GameObject iconCanvas;
        RectTransform rectTransform;
        UnityEngine.UI.Image imageComponent;

        // Constructor
        public Image(string imageName, Sprite image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.SetParent(GameObject.Find("Canvas").transform, false);

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            imageComponent = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            imageComponent.sprite = image;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            imageComponent.enabled = false;
        }

        // Constructor
        public Image(string imageName, Transform parent, Sprite image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.parent = parent;

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            imageComponent = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            imageComponent.sprite = image;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            imageComponent.enabled = false;
        }

        public void Init(string imageName, Transform parent, Sprite image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.parent = parent;

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            imageComponent = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            imageComponent.sprite = image;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            imageComponent.enabled = false;
        }

        // Constructor
        public Image(string imageName, Transform parent, int width, int height, string path)
        {
            // Set Width and Height
            Vector2Int iconPngSize = new Vector2Int(width, height);

            // Load image from file
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID(path, width, height);

            // Set Sprite ID from Sprite Atlas
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, Enums.AtlasType.Gui);

            // Set Sprite Data
            byte[] iconSpriteData = new byte[iconPngSize.x * iconPngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, Enums.AtlasType.Gui);

            // Set Texture
            Texture2D iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, iconPngSize.x, iconPngSize.y);

            // Create Sprite
            Sprite sprite = Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.parent = parent;

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            imageComponent = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            imageComponent.sprite = sprite;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            rectTransform.position = new Vector3(0, 0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            imageComponent.enabled = false;
        }

        public Image(string imageName, Transform parent, int width, int height, int tileSpriteID)
        {
            // Set Width and Height
            Vector2Int iconPngSize = new Vector2Int(width, height);

            // Set Sprite Data
            byte[] iconSpriteData = new byte[iconPngSize.x * iconPngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(tileSpriteID, iconSpriteData, Enums.AtlasType.TGen);

            // Set Texture
            Texture2D iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, iconPngSize.x, iconPngSize.y);

            // Create Sprite
            Sprite sprite = Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.parent = parent;

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            imageComponent = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            imageComponent.sprite = sprite;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            rectTransform.position = new Vector3(0, 0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            imageComponent.enabled = false;
        }

        public Image(string imageName, Transform parent, int width, int height, int tileSpriteID, Enums.AtlasType atlasType)
        {
            // Set Width and Height
            Vector2Int iconPngSize = new Vector2Int(width, height);

            // Set Sprite Data
            byte[] iconSpriteData = new byte[iconPngSize.x * iconPngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(tileSpriteID, iconSpriteData, atlasType);

            // Set Texture
            Texture2D iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, iconPngSize.x, iconPngSize.y);

            // Create Sprite
            Sprite sprite = Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.parent = parent;

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            imageComponent = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            imageComponent.sprite = sprite;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            rectTransform.position = new Vector3(0, 0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            imageComponent.enabled = false;
        }

        public override void Update()
        {
             
        }

        public override void Draw()
        {
            imageComponent.enabled = true;
        }

        public bool IsMouseOver(KMath.Vec2f cursor)
        {
            if(KMath.Vec2f.Distance(new KMath.Vec2f(cursor.X, cursor.Y), new KMath.Vec2f(iconCanvas.transform.position.x, iconCanvas.transform.position.y)) < 20.0f)
            {
                return true;
            }

            return false;
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

        public void SetImageType(UnityEngine.UI.Image.Type newType)
        {
            // Set Image Type
            imageComponent.type = newType;
        }

        public void SetImageTopLeft()
        {
            // Set Image Anchor
            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 1);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 1);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImageBottomRight()
        {
            // Set Image Anchor
            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(1, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(1, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImageMidBottom()
        {
            // Set Image Anchor
            rectTransform.anchorMin = new Vector2(0.5f, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0.5f, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImage(Sprite sprite)
        {
            // Set New Image
            imageComponent.sprite = sprite;
        }

        public void SetImageColor(Color color)
        {
            // Set New Image
            imageComponent.color = color;
        }

        public Texture2D GetTexture()
        {
            return imageComponent.sprite.texture;
        }

        public Transform GetTransform()
        {
            // Return Transform
            return iconCanvas.transform;
        }

        public GameObject GetGameObject()
        {
            return iconCanvas;
        }
    }
}
