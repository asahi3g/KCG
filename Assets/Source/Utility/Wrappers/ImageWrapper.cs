using Enums;
using KMath;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Texture = Utility.Texture;

namespace Utility
{
    public class ImageWrapper
    {
        public Image UnityImage { get; private set; }
        
        public Transform Transform => GameObject.transform;
        public GameObject GameObject { get; private set; }
        public RectTransform RectTransform { get; private set; }

        public ImageWrapper(Image image, Sprite sprite)
        {
            GameObject = image.gameObject;
            
            RectTransform = GameObject.GetComponent<RectTransform>();
            UnityImage = image;
            
            UnityImage.sprite = sprite;
            UnityImage.enabled = false;
            
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 0);
            RectTransform.pivot = new Vector2(0, 0);
        }
        public ImageWrapper(string imageName, Transform parent, Sprite sprite)
        {
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 0);
            RectTransform.pivot = new Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, Transform parent, int width, int height, string path)
        {
            var sprite = GameState.Renderer.CreateSprite(path, width, height, AtlasType.Gui);
            
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new Vector2(0f, 0f);
            RectTransform.position = new Vector3(0f, 0f, 0f);
            RectTransform.anchorMax = new Vector2(0f, 0f);
            RectTransform.pivot = new Vector2(0f, 0f);
            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, Transform parent, int width, int height, int tileSpriteID)
        {
            var sprite = GameState.Renderer.CreateSprite(tileSpriteID, width, height, AtlasType.TGen);
            
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.position = new Vector3(0, 0, 0);
            RectTransform.anchorMax = new Vector2(0, 0);
            RectTransform.pivot = new Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, Transform parent, int width, int height, int tileSpriteID, AtlasType atlasType)
        {
            var sprite = GameState.Renderer.CreateSprite(tileSpriteID, width, height, atlasType);
            
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 0);
            RectTransform.pivot = new Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(Image image, int width, int height, string path, AtlasType atlasType)
        {
            GameObject = image.gameObject;
            
            var sprite = GameState.Renderer.CreateSprite(path, width, height, atlasType);

            RectTransform = GameObject.GetComponent<RectTransform>();
            UnityImage = image;
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            RectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, Transform parent, int width, int height, string path, AtlasType atlasType)
        {
            var sprite = GameState.Renderer.CreateSprite(path, width, height, atlasType);
            
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            RectTransform = GameObject.AddComponent<RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new Vector2(0f, 0f);
            RectTransform.anchorMax = new Vector2(0f, 0f);
            RectTransform.pivot = new Vector2(0f, 0f);
            RectTransform.localPosition = Vector3.zero;
            UnityImage.enabled = false;
        }
        
        public void Init(string imageName, Transform parent, Sprite sprite)
        {
            GameObject = new GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 0);
            RectTransform.pivot = new Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }

        public void Draw()
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
            RectTransform.localPosition = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            RectTransform.localRotation = newRot;
        }

        public void SetScale(Vector3 newScale)
        {
            RectTransform.localScale = newScale;
        }
        
        public void SetSize(Vector2 newSize)
        {
            RectTransform.sizeDelta = newSize;
        }

        public void SetImageType(Image.Type newType)
        {
            UnityImage.type = newType;
        }

        public void SetImageTopLeft()
        {
            RectTransform.anchorMin = new Vector2(0, 1);
            RectTransform.anchorMax = new Vector2(0, 1);
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImageBottomRight()
        {
            RectTransform.anchorMin = new Vector2(1, 0);
            RectTransform.anchorMax = new Vector2(1, 0);
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImageMidBottom()
        {
            RectTransform.anchorMin = new Vector2(0.5f, 0);
            RectTransform.anchorMax = new Vector2(0.5f, 0);
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        public void SetImage(Sprite sprite)
        {
            UnityImage.sprite = sprite;
        }

        public void SetImageColor(Color color)
        {
            UnityImage.color = color;
        }
    }
}
