using KGUI.Elements;
using KMath;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace KGUI
{
    [DefaultExecutionOrder (100)]
    public class ElementUI : MonoBehaviour
    {
        [SerializeField] protected Image iconImage;

        protected ImageWrapper Icon;
        // Set this to any initialized image
        protected GameObject HitBoxObject;

        public ElementEnums ID { get; protected set; }
        public Vector3 HitBoxPosition { get; private set; }
        public Vector2 HitBoxSize { get; private set; }
        public AABox2D HitBox { get; private set; }

        public void Start()
        {
            Init();
        }

        public virtual void Update()
        {
            if (transform.hasChanged)
            {
                var hitBoxTransform = HitBoxObject.transform;
                HitBoxPosition = hitBoxTransform.position - hitBoxTransform.localPosition;
                HitBoxSize = HitBoxObject.GetComponent<RectTransform>().sizeDelta;
                HitBox = new AABox2D(new Vec2f(HitBoxPosition.x, HitBoxPosition.y), new Vec2f(HitBoxSize.x, HitBoxSize.y));
                transform.hasChanged = false;
            }
            HitBox.DrawBox();
        }

        public virtual void Init()
        {
            HitBoxObject = iconImage.gameObject;
            transform.hasChanged = true;
        }

        public virtual void Draw() {}

        public virtual void OnMouseClick() { }
        public virtual void OnMouseStay() { }
        public virtual void OnMouseEntered() { }
        public virtual void OnMouseExited() { }
    }
}

