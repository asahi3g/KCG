using UnityEngine;

public class PlayerCamera : BaseMonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed;
    [Space]
    [SerializeField] private Camera _main;
    [SerializeField] private TransformShaker _shaker;
    
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _target = null;
        _followSpeed = 3f;
        _main = Camera.main;
        _shaker = GetComponentInChildren<TransformShaker>();
    }
#endif

    public Camera GetMain() => _main;
    public TransformShaker GetShaker() => _shaker;

    
    public void Tick(float deltaTime)
    {
        if (_target == null) return;
        transform.position = Vector3.Slerp(transform.position, GetTargetPosition(_target.position), _followSpeed * deltaTime);
    }

    public void SetTarget(Transform target, bool instant)
    {
        _target = target;
        if (_target != null)
        {
            if (instant)
            {
                transform.position = GetTargetPosition(_target.position);
            }
        }
    }

    private Vector3 GetTargetPosition(Vector3 vector)
    {
        vector.z = 0f;
        return vector;
    }
}
