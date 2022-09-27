using KGUI.Elements;
using KMath;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace KGUI
{
    [DefaultExecutionOrder (100)]
    public class UIElement : MonoBehaviour
    {
        [SerializeField] protected Image iconImage;
        
        protected ImageWrapper Icon;
        private RectTransform rectTransform;

        public UIElementID ID { get; protected set; }
        public Vector3 Position { get; private set; }
        public Vector2 Size { get; private set; }
        public AABox2D HitBox { get; private set; }
        
        public void Start()
        {
            Init();
        }

        public virtual void Update()
        {
            if (transform.hasChanged)
            {
                Position = transform.position;
                Size = rectTransform.sizeDelta;
                HitBox = new AABox2D(new Vec2f(Position.x, Position.y), new Vec2f(Size.x, Size.y));
                transform.hasChanged = false;
            }
            HitBox.DrawBox();
        }

        public virtual void Init()
        {
            rectTransform = GetComponent<RectTransform>();
            Position = transform.position;
            Size = rectTransform.sizeDelta;
            HitBox = new AABox2D(new Vec2f(Position.x, Position.y), new Vec2f(Size.x, Size.y));
        }

        public virtual void Draw() {}

        public virtual void OnMouseClick() { }
        public virtual void OnMouseStay() { }
        public virtual void OnMouseEntered() { }
        public virtual void OnMouseExited() { }
    }
}

