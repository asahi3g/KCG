using KMath;
using UnityEngine;
using Utility;

namespace KGUI.Elements
{
    public class Image : ElementManager
    {
        // Image GameObject
        private GameObject iconCanvas;
        private RectTransform rectTransform;
        private UnityEngine.UI.Image image;

        // Constructor
        public Image(string imageName, Sprite Image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = GameObject.Find("Canvas").transform
                }
            };

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            image = iconCanvas.AddComponent<UnityEngine.UI.Image>();
            
            // Set Image Sprite
            image.sprite = Image;
            image.enabled = false;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        // Constructor
        public Image(string imageName, Transform parent, Sprite Image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            image = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            image.sprite = Image;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            image.enabled = false;
        }

        public void Init(string imageName, Transform parent, Sprite Image)
        {
            // Create Gameobject
            iconCanvas = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            image = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            image.sprite = Image;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            image.enabled = false;
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
            iconCanvas = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            image = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            image.sprite = sprite;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            rectTransform.position = new Vector3(0, 0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            image.enabled = false;
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
            iconCanvas = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            image = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            image.sprite = sprite;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            rectTransform.position = new Vector3(0, 0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            image.enabled = false;
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
            iconCanvas = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            rectTransform = iconCanvas.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            image = iconCanvas.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            image.sprite = sprite;

            // Set Anchor Min
            rectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            rectTransform.position = new Vector3(0, 0, 0);

            // Set Anchor Max
            rectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            image.enabled = false;
        }

        public override void Update()
        {
             
        }

        public override void Draw()
        {
            image.enabled = true;
        }

        public bool IsMouseOver(Vec2f cursor)
        {
            var position = rectTransform.position;
            var sizeDelta = rectTransform.sizeDelta;
            var halfSize = sizeDelta / 2f;
            var bottomLeft = (Vector2)position - halfSize;
            DrawDebug.DrawBox(new Vec2f(bottomLeft.x, bottomLeft.y), new Vec2f(sizeDelta.x, sizeDelta.y));

            return Collisions.Collisions.PointOverlapRect(cursor.X, cursor.Y, bottomLeft.x, bottomLeft.x + sizeDelta.x,
                bottomLeft.y, bottomLeft.y + sizeDelta.y);
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
        
        public void SetSize(Vector2 newSize)
        {
            // Set Local Rect Scale
            rectTransform.sizeDelta = newSize;
        }

        public void SetImageType(UnityEngine.UI.Image.Type newType)
        {
            // Set Image Type
            image.type = newType;
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
            image.sprite = sprite;
        }

        public void SetImageColor(Color color)
        {
            // Set New Image
            image.color = color;
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
