using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KGUI.Elements
{
    public class Image : ElementManager
    {
        // Image GameObject
        private GameObject iconCanvas;

        // Constructor
        public Image(string imageName, Sprite image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.parent = GameObject.Find("Canvas").transform;

            // Add Rect Transform to Manage UI Scaling
            iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            iconCanvas.GetComponent<UnityEngine.UI.Image>().sprite = image;

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            iconCanvas.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }

        // Constructor
        public Image(string imageName, Transform parent, Sprite image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.parent = parent;

            // Add Rect Transform to Manage UI Scaling
            iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            iconCanvas.GetComponent<UnityEngine.UI.Image>().sprite = image;

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            iconCanvas.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }

        public void Init(string imageName, Transform parent, Sprite image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName);

            // Set Object Parent To Canvas
            iconCanvas.transform.parent = parent;

            // Add Rect Transform to Manage UI Scaling
            iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            iconCanvas.GetComponent<UnityEngine.UI.Image>().sprite = image;

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            iconCanvas.GetComponent<UnityEngine.UI.Image>().enabled = false;
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
            iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            iconCanvas.GetComponent<UnityEngine.UI.Image>().sprite = sprite;

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            iconCanvas.GetComponent<UnityEngine.UI.Image>().enabled = false;
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
            iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            iconCanvas.GetComponent<UnityEngine.UI.Image>().sprite = sprite;

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            iconCanvas.GetComponent<UnityEngine.UI.Image>().enabled = false;
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
            iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            iconCanvas.GetComponent<UnityEngine.UI.Image>().sprite = sprite;

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            iconCanvas.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }

        public override void Update()
        {
             
        }

        public override void Draw()
        {
            iconCanvas.GetComponent<UnityEngine.UI.Image>().enabled = true;
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
            iconCanvas.GetComponent<RectTransform>().localPosition = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            // Set Local Rect Rotation
            iconCanvas.GetComponent<RectTransform>().localRotation = newRot;
        }

        public void SetScale(Vector3 newScale)
        {
            // Set Local Rect Scale
            iconCanvas.GetComponent<RectTransform>().localScale = newScale;
        }

        public void SetImageType(UnityEngine.UI.Image.Type newType)
        {
            // Set Image Type
            iconCanvas.GetComponent<UnityEngine.UI.Image>().type = newType;
        }

        public void SetImageTopLeft()
        {
            // Set Image Anchor
            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImageBottomRight()
        {
            // Set Image Anchor
            // Set Anchor Min
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImageMidBottom()
        {
            // Set Image Anchor
            iconCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);

            // Set Anchor Max
            iconCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);

            // Set Pivot
            iconCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImage(Sprite sprite)
        {
            // Set New Image
            iconCanvas.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }

        public void SetImageColor(Color color)
        {
            // Set New Image
            iconCanvas.GetComponent<UnityEngine.UI.Image>().color = color;
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
