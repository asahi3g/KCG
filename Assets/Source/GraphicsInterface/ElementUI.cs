//imports UnityEngine

using KMath;
using Utility;

namespace KGUI
{
    [UnityEngine.DefaultExecutionOrder (100)]
    public class ElementUI : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] protected ImageWrapper icon;
        
        protected UnityEngine.GameObject HitBoxObject;
        public ElementEnums ID { get; protected set; }
        public UnityEngine.Vector3 HitBoxPosition { get; private set; }
        public UnityEngine.Vector2 HitBoxSize { get; private set; }
        public AABox2D HitBox { get; private set; }

        public void Start()
        {
            Init();
        }

        public virtual void Update()
        {
            if (transform.hasChanged)
            {
                var rect = HitBoxObject.GetComponent<UnityEngine.RectTransform>();
                var corners = new UnityEngine.Vector3[4];
                rect.GetWorldCorners(corners);
                HitBoxPosition = corners[0]; // 0 - bottom left, 1 - top left, 2 - top right, 3 - bottom right
                HitBoxSize = rect.sizeDelta * rect.localScale * rect.lossyScale;
                HitBox = new AABox2D(new Vec2f(HitBoxPosition.x, HitBoxPosition.y), new Vec2f(HitBoxSize.x, HitBoxSize.y));
                transform.hasChanged = false;
            }
            HitBox.DrawBox();
        }

        public virtual void Init()
        {
            var tr = transform;
            HitBoxObject = tr.gameObject;
            tr.hasChanged = true;
        }

        public virtual void Draw() {}
        
        public virtual void OnMouseClick() { }
        public virtual void OnMouseStay() { }
        public virtual void OnMouseEntered() { }
        public virtual void OnMouseExited() { }
    }
}

