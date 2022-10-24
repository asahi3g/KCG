//imports UnityEngine

using Enums;
using KMath;
using UnityEngine.UI;
using Utility;
using Texture = Utility.Texture;

namespace KGUI.Elements
{
    public class ImageWrapper : ElementManager
    {
        public Image UnityImage { get; private set; }
        
        public UnityEngine.Transform Transform => GameObject.transform;
        public UnityEngine.GameObject GameObject { get; private set; }
        public UnityEngine.RectTransform RectTransform { get; private set; }

        public ImageWrapper(Image image, UnityEngine.Sprite sprite)
        {
            GameObject = image.gameObject;
            
            RectTransform = GameObject.GetComponent<UnityEngine.RectTransform>();
            UnityImage = image;
            
            UnityImage.sprite = sprite;
            UnityImage.enabled = false;
            
            RectTransform.anchorMin = new UnityEngine.Vector2(0, 0);
            RectTransform.anchorMax = new UnityEngine.Vector2(0, 0);
            RectTransform.pivot = new UnityEngine.Vector2(0, 0);
        }
        public ImageWrapper(string imageName, UnityEngine.Transform parent, UnityEngine.Sprite sprite)
        {
            GameObject = new UnityEngine.GameObject(imageName)
            {
                transform =
                {
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<UnityEngine.RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new UnityEngine.Vector2(0, 0);
            RectTransform.anchorMax = new UnityEngine.Vector2(0, 0);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, UnityEngine.Transform parent, int width, int height, string path)
        {
            var sprite = GameState.Renderer.CreateSprite(path, width, height, AtlasType.Gui);
            
            GameObject = new UnityEngine.GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<UnityEngine.RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new UnityEngine.Vector2(0f, 0f);
            RectTransform.position = new UnityEngine.Vector3(0f, 0f, 0f);
            RectTransform.anchorMax = new UnityEngine.Vector2(0f, 0f);
            RectTransform.pivot = new UnityEngine.Vector2(0f, 0f);
            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, UnityEngine.Transform parent, int width, int height, int tileSpriteID)
        {
            var sprite = GameState.Renderer.CreateSprite(tileSpriteID, width, height, AtlasType.TGen);
            
            GameObject = new UnityEngine.GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<UnityEngine.RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new UnityEngine.Vector2(0, 0);
            RectTransform.position = new UnityEngine.Vector3(0, 0, 0);
            RectTransform.anchorMax = new UnityEngine.Vector2(0, 0);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, UnityEngine.Transform parent, int width, int height, int tileSpriteID, AtlasType atlasType)
        {
            var sprite = GameState.Renderer.CreateSprite(tileSpriteID, width, height, atlasType);
            
            GameObject = new UnityEngine.GameObject(imageName)
            {
                transform =
                {
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<UnityEngine.RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new UnityEngine.Vector2(0, 0);
            RectTransform.anchorMax = new UnityEngine.Vector2(0, 0);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }
        public ImageWrapper(Image image, int width, int height, string path, AtlasType atlasType)
        {
            GameObject = image.gameObject;
            
            var sprite = GameState.Renderer.CreateSprite(path, width, height, atlasType);

            RectTransform = GameObject.GetComponent<UnityEngine.RectTransform>();
            UnityImage = image;
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new UnityEngine.Vector2(0.5f, 0.5f);
            RectTransform.anchorMax = new UnityEngine.Vector2(0.5f, 0.5f);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);
            UnityImage.enabled = false;
        }
        public ImageWrapper(string imageName, UnityEngine.Transform parent, int width, int height, string path, AtlasType atlasType)
        {
            var sprite = GameState.Renderer.CreateSprite(path, width, height, atlasType);
            
            GameObject = new UnityEngine.GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };

            RectTransform = GameObject.AddComponent<UnityEngine.RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            
            UnityImage.sprite = sprite;
            RectTransform.anchorMin = new UnityEngine.Vector2(0f, 0f);
            RectTransform.anchorMax = new UnityEngine.Vector2(0f, 0f);
            RectTransform.pivot = new UnityEngine.Vector2(0f, 0f);
            RectTransform.localPosition = UnityEngine.Vector3.zero;
            UnityImage.enabled = false;
        }
        
        public void Init(string imageName, UnityEngine.Transform parent, UnityEngine.Sprite sprite)
        {
            GameObject = new UnityEngine.GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };
            
            RectTransform = GameObject.AddComponent<UnityEngine.RectTransform>();
            UnityImage = GameObject.AddComponent<Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new UnityEngine.Vector2(0, 0);
            RectTransform.anchorMax = new UnityEngine.Vector2(0, 0);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);

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
            var bottomLeft = (UnityEngine.Vector2)position - halfSize;
            DrawDebug.DrawBox(new Vec2f(bottomLeft.x, bottomLeft.y), new Vec2f(sizeDelta.x, sizeDelta.y));

            return Collisions.Collisions.PointOverlapRect(cursor.X, cursor.Y, bottomLeft.x, bottomLeft.x + sizeDelta.x,
                bottomLeft.y, bottomLeft.y + sizeDelta.y);
        }

        public void SetPosition(UnityEngine.Vector3 newPos)
        {
            RectTransform.localPosition = newPos;
        }

        public void SetRotation(UnityEngine.Quaternion newRot)
        {
            RectTransform.localRotation = newRot;
        }

        public void SetScale(UnityEngine.Vector3 newScale)
        {
            RectTransform.localScale = newScale;
        }
        
        public void SetSize(UnityEngine.Vector2 newSize)
        {
            RectTransform.sizeDelta = newSize;
        }

        public void SetImageType(Image.Type newType)
        {
            UnityImage.type = newType;
        }

        public void SetImageTopLeft()
        {
            RectTransform.anchorMin = new UnityEngine.Vector2(0, 1);
            RectTransform.anchorMax = new UnityEngine.Vector2(0, 1);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);
        }

        public void SetImageBottomRight()
        {
            RectTransform.anchorMin = new UnityEngine.Vector2(1, 0);
            RectTransform.anchorMax = new UnityEngine.Vector2(1, 0);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);
        }

        public void SetImageMidBottom()
        {
            RectTransform.anchorMin = new UnityEngine.Vector2(0.5f, 0);
            RectTransform.anchorMax = new UnityEngine.Vector2(0.5f, 0);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);
        }

        public void SetImage(UnityEngine.Sprite sprite)
        {
            UnityImage.sprite = sprite;
        }

        public void SetImageColor(UnityEngine.Color color)
        {
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
