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


        private AgentRenderer _renderer;
        private bool _isActive = true;
        private Vector3 _position = new Vector3(0f, 0f, -1f);
        private Quaternion _rotation = Quaternion.Euler(0f, 0f, 0f);
        private Vector3 _localScale = Vector3.one;


        public AgentRenderer Renderer => _renderer;
        public bool RendererExists => _renderer != null;
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

        public void SetGameObject(AgentRenderer value)
        {
            _renderer = value;
            if (_renderer == null) return;
            UpdateIsActive();
            UpdatePosition();
            UpdateRotation();
            UpdateLocalScale();
        }
        

        private void UpdateIsActive()
        {
            if (_renderer == null) return;
            _renderer.SetIsActive(_isActive);
        }

        private void UpdateRotation()
        {
            if (_renderer == null) return;
            _renderer.SetRotation(_rotation);
        }

        private void UpdatePosition()
        {
            if (_renderer == null) return;
            _renderer.SetPosition(_position);
        }

        private void UpdateLocalScale()
        {
            if (_renderer == null) return;
            _renderer.SetLocalScale(_localScale);
        }

        public void DestroyRenderer()
        {
            if (_renderer == null) return;
            MonoBehaviour.Destroy(_renderer.gameObject);
            _renderer = null;
        }
    }

        
}
