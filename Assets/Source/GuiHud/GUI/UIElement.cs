using System;
using KMath;
using UnityEngine;
using UnityEngine.UI;

namespace KGUI
{
    [DefaultExecutionOrder (100)]
    public class UIElement : MonoBehaviour
    {
        public UIElementID ID;
        
        public Vector3 Position;
        public Vector2 Size;

        public Image Icon;
        public AABox2D HitBox;

        private bool isMouseEntered;
        private bool useEvent = true;

        public void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            var rect = GetComponent<RectTransform>();
            Position = transform.position;
            Size = rect.sizeDelta;
        }

        public virtual void OnMouseClick() { }
        public virtual void OnMouseStay() { }
        public virtual void OnMouseEntered() { }
        public virtual void OnMouseExited() { }
    }
}

