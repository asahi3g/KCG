using KMath;
using UnityEngine;
using Utility;

namespace KGUI.Elements
{
    public class ImageWrapper : ElementManager
    {
        public UnityEngine.UI.Image UnityImage { get; private set; }
        
        public Transform Transform => GameObject.transform;
        public GameObject GameObject { get; private set; }
        public RectTransform RectTransform { get; private set; }

        public ImageWrapper(string imageName, Sprite sprite)
        {
            // Create Gameobject
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = GameObject.Find("Canvas").transform
                }
            };

            // Add Rect Transform to Manage UI Scaling
            RectTransform = GameObject.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            UnityImage = GameObject.AddComponent<UnityEngine.UI.Image>();
            
            // Set Image Sprite
            UnityImage.sprite = sprite;
            UnityImage.enabled = false;

            // Set Anchor Min
            RectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            RectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
        public ImageWrapper(string imageName, Transform parent, Sprite sprite)
        {
            // Create Gameobject
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            RectTransform = GameObject.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            UnityImage = GameObject.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            UnityImage.sprite = sprite;

            // Set Anchor Min
            RectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            RectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            RectTransform.pivot = new Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, Transform parent, int width, int height, string path)
        {
            var pngSize = new Vec2i(width, height);
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID(path, width, height);
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, Enums.AtlasType.Gui);
            var iconSpriteData = new byte[pngSize.X * pngSize.Y * 4];
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, Enums.AtlasType.Gui);
            var iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, pngSize.X, pngSize.Y);
            var sprite = Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
            
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<RectTransform>();
            UnityImage = GameObject.AddComponent<UnityEngine.UI.Image>();
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new Vector2(0f, 0f);
            RectTransform.position = new Vector3(0f, 0f, 0f);
            RectTransform.anchorMax = new Vector2(0f, 0f);
            RectTransform.pivot = new Vector2(0f, 0f);
            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, Transform parent, int width, int height, int tileSpriteID)
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
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            RectTransform = GameObject.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            UnityImage = GameObject.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            UnityImage.sprite = sprite;

            // Set Anchor Min
            RectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            RectTransform.position = new Vector3(0, 0, 0);

            // Set Anchor Max
            RectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            RectTransform.pivot = new Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, Transform parent, int width, int height, int tileSpriteID, Enums.AtlasType atlasType)
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
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            RectTransform = GameObject.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            UnityImage = GameObject.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            UnityImage.sprite = sprite;

            // Set Anchor Min
            RectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Min
            RectTransform.position = new Vector3(0, 0, 0);

            // Set Anchor Max
            RectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            RectTransform.pivot = new Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(UnityEngine.UI.Image image, int width, int height, string path, Enums.AtlasType atlasType)
        {
            GameObject = image.gameObject;
            
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID(path, width, height);
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, atlasType);
            var iconSpriteData = new byte[width * height * 4];
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, atlasType);
            var iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, width, height);
            var sprite = Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

            RectTransform = GameObject.GetComponent<RectTransform>();
            UnityImage = GameObject.GetComponent<UnityEngine.UI.Image>();
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new Vector2(0f, 0f);
            RectTransform.anchorMax = new Vector2(0f, 0f);
            RectTransform.pivot = new Vector2(0f, 0f);
            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, Transform parent, int width, int height, string path, Enums.AtlasType atlasType)
        {
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID(path, width, height);
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, atlasType);
            var iconSpriteData = new byte[width * height * 4];
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, atlasType);
            var iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, width, height);
            var sprite = Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
            
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            RectTransform = GameObject.AddComponent<RectTransform>();
            UnityImage = GameObject.AddComponent<UnityEngine.UI.Image>();
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new Vector2(0f, 0f);
            RectTransform.anchorMax = new Vector2(0f, 0f);
            RectTransform.pivot = new Vector2(0f, 0f);
            RectTransform.localPosition = Vector3.zero;
            UnityImage.enabled = false;
        }
        
        public void Init(string imageName, Transform parent, Sprite sprite)
        {
            // Create Gameobject
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            // Add Rect Transform to Manage UI Scaling
            RectTransform = GameObject.AddComponent<RectTransform>();

            // Add Image Component to Render the Sprite
            UnityImage = GameObject.AddComponent<UnityEngine.UI.Image>();

            // Set Image Sprite
            UnityImage.sprite = sprite;

            // Set Anchor Min
            RectTransform.anchorMin = new Vector2(0, 0);

            // Set Anchor Max
            RectTransform.anchorMax = new Vector2(0, 0);

            // Set Pivot
            RectTransform.pivot = new Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        
        public override void Update()
        {
             
        }

        public override void Draw()
        {
            if (!UnityImage.enabled)
            {
                UnityImage.enabled = true;
            }
        }

        public bool IsMouseOver(Vec2f cursor)
        {
            var position = RectTransform.position;
            var sizeDelta = RectTransform.sizeDelta;
            var halfSize = sizeDelta / 2f;
            var bottomLeft = (Vector2)position - halfSize;
            DrawDebug.DrawBox(new Vec2f(bottomLeft.x, bottomLeft.y), new Vec2f(sizeDelta.x, sizeDelta.y));

            return Collisions.Collisions.PointOverlapRect(cursor.X, cursor.Y, bottomLeft.x, bottomLeft.x + sizeDelta.x,
                bottomLeft.y, bottomLeft.y + sizeDelta.y);
        }

        public void SetPosition(Vector3 newPos)
        {
            // Set Local Rect Position
            RectTransform.localPosition = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            // Set Local Rect Rotation
            RectTransform.localRotation = newRot;
        }

        public void SetScale(Vector3 newScale)
        {
            // Set Local Rect Scale
            RectTransform.localScale = newScale;
        }
        
        public void SetSize(Vector2 newSize)
        {
            // Set Local Rect Scale
            RectTransform.sizeDelta = newSize;
        }

        public void SetImageType(UnityEngine.UI.Image.Type newType)
        {
            // Set Image Type
            UnityImage.type = newType;
        }

        public void SetImageTopLeft()
        {
            // Set Image Anchor
            // Set Anchor Min
            RectTransform.anchorMin = new Vector2(0, 1);

            // Set Anchor Max
            RectTransform.anchorMax = new Vector2(0, 1);

            // Set Pivot
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImageBottomRight()
        {
            // Set Image Anchor
            // Set Anchor Min
            RectTransform.anchorMin = new Vector2(1, 0);

            // Set Anchor Max
            RectTransform.anchorMax = new Vector2(1, 0);

            // Set Pivot
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImageMidBottom()
        {
            // Set Image Anchor
            RectTransform.anchorMin = new Vector2(0.5f, 0);

            // Set Anchor Max
            RectTransform.anchorMax = new Vector2(0.5f, 0);

            // Set Pivot
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImage(Sprite sprite)
        {
            // Set New Image
            UnityImage.sprite = sprite;
        }

        public void SetImageColor(Color color)
        {
            // Set New Image
            UnityImage.color = color;
        }

        public void SetImagePosition1(UIElementEntity entity)
        {
            if(entity.hasKGUIElementsMultiplePosition)
            {
                entity.kGUIElementsPosition2D.Value = entity.kGUIElementsMultiplePosition.position1;
            }
        }

        public void SetImagePosition2(UIElementEntity entity)
        {
            if (entity.hasKGUIElementsMultiplePosition)
            {
                entity.kGUIElementsPosition2D.Value = entity.kGUIElementsMultiplePosition.position2;
            }
        }
    }
}
