using Entitas;
using UnityEngine;

namespace Agent
{
    [Agent]
    public class Agent3DModel : IComponent
    {
        public Model3DWeaponType CurrentWeapon;
        public GameObject Weapon;
        public Enums.AgentAnimationType AnimationType;
        public Enums.ItemAnimationSet ItemAnimationSet;
        
        public KMath.Vec2f AimTarget;


        public AgentRenderer Renderer;
        private Vector3 _localScale = Vector3.one;

        private bool _isActive = true;
        private Vector3 _position = new Vector3(0f, 0f, -1f);
        private Quaternion _rotation = Quaternion.Euler(0f, 0f, 0f);
    
        public bool RendererExists => Renderer != null;
        public bool IsActive => _isActive;
        public Vector3 LocalScale => _localScale;


        public void SetPosition(float x, float y)
        {
            _position.x = x;
            _position.y = y;
            UpdatePosition();
        }
        
        public void SetPositionZ(float z)
        {
            _position.z = z;
            UpdatePosition();
        }
        
        public void SetRotation(float y)
        {
            _rotation = Quaternion.Euler(0f, y, 0f);
            UpdateRotation();
        }

        public void SetLocalScale(Vector3 value)
        {
            _localScale = value;
            UpdateLocalScale();
        }
        
        public void SetLocalScale(KMath.Vec3f value)
        {
            SetLocalScale(value.GetVector3());
        }

        public void SetIsActive(bool isActive)
        {
            _isActive = isActive;
            UpdateIsActive();
        }

        public void SetRenderer(AgentRenderer value)
        {
            Renderer = value;
            if (Renderer == null) return;
            UpdateIsActive();
            UpdatePosition();
            UpdateRotation();
            UpdateLocalScale();
        }
        

        private void UpdateIsActive()
        {
            if (Renderer == null) return;
            Renderer.SetIsActive(_isActive);
        }

        private void UpdateRotation()
        {
            if (Renderer == null) return;
            Renderer.SetRotation(_rotation);
        }

        private void UpdatePosition()
        {
            if (Renderer == null) return;
            Renderer.SetPosition(_position);
        }

        private void UpdateLocalScale()
        {
            if (Renderer == null) return;
            Renderer.SetLocalScale(_localScale);
        }

        public void DestroyRenderer()
        {
            if (Renderer == null) return;
            MonoBehaviour.Destroy(Renderer.gameObject);
            Renderer = null;
        }
    }

        
}
