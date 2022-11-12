//imports UnityEngine

using System;
using Enums;
using KMath;

namespace Utility
{
    [Serializable]
    public class ImageWrapper
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Image unityImage;
        
        public UnityEngine.UI.Image UnityImage
        {
            get => unityImage;
            private set => unityImage = value;
        }
        public UnityEngine.Transform Transform => GameObject.transform;
        public UnityEngine.GameObject GameObject => unityImage.gameObject;
        public UnityEngine.RectTransform RectTransform { get; private set; }
        
        // If unityImage not serialized then
        // Use constructor to create new gameObject, new Unity Image
        // While getting sprite from SpriteAtlasManager
        public ImageWrapper(string imageName, UnityEngine.Transform parent, int width, int height, int spriteID)
        {
            var sprite = GameState.Renderer.CreateUnitySprite(spriteID, width, height, AtlasType.TGen);
            
            var gameObject = new UnityEngine.GameObject(imageName)
            {
                transform =
                {
                    // Set Object Parent To Canvas
                    parent = parent
                }
            };
            
            RectTransform = gameObject.AddComponent<UnityEngine.RectTransform>();
            UnityImage = gameObject.AddComponent<UnityEngine.UI.Image>();
            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new UnityEngine.Vector2(0, 0);
            RectTransform.position = new UnityEngine.Vector3(0, 0, 0);
            RectTransform.anchorMax = new UnityEngine.Vector2(0, 0);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);

            UnityImage.enabled = false;
        }

        // If unityImage field serialized then use that method
        // to attach ready-made sprite
        public void Init(UnityEngine.Sprite sprite)
        {
            RectTransform = GameObject.GetComponent<UnityEngine.RectTransform>();

            unityImage.sprite = sprite;
            unityImage.enabled = false;
        }
        
        // If unityImage field serialized then use that method
        // to create new sprite from path and attach that sprite to unityImage
        public void Init(int width, int height, string path, AtlasType atlasType)
        {
            var sprite = GameState.Renderer.CreateUnitySprite(path, width, height, atlasType);
            
            RectTransform = GameObject.GetComponent<UnityEngine.RectTransform>();

            UnityImage.sprite = sprite;
            
            RectTransform.anchorMin = new UnityEngine.Vector2(0.5f, 0.5f);
            RectTransform.anchorMax = new UnityEngine.Vector2(0.5f, 0.5f);
            RectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.5f);
            
            UnityImage.enabled = false;
        }
        
        // If unityImage field serialized then use that method
        // to get sprite from tile atlas manager and attach that sprite to unityImage
        public void Init(int width, int height, int spriteID)
        {
            var sprite = GameState.Renderer.CreateUnitySprite(spriteID, width, height);
            
            RectTransform = GameObject.GetComponent<UnityEngine.RectTransform>();
            
            UnityImage.sprite = sprite;
            
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
            var bottomLeft = (UnityEngine.Vector2)position - halfSize;
            DrawDebug.DrawBox(new Vec2f(bottomLeft.x, bottomLeft.y), new Vec2f(sizeDelta.x, sizeDelta.y));

            return Collisions.Collisions.PointOverlapRect(cursor.X, cursor.Y, bottomLeft.x, bottomLeft.x + sizeDelta.x,
                bottomLeft.y, bottomLeft.y + sizeDelta.y);
        }

        public void SetPosition(UnityEngine.Vector3 newPos)
        {
            RectTransform.localPosition = newPos;
        }
        
        public void SetScale(UnityEngine.Vector3 newScale)
        {
            RectTransform.localScale = newScale;
        }
    }
}
