using System.Collections;
using UnityEngine;

public class TransformShaker : BaseMonoBehaviour
{
    [SerializeField] private bool _x;
    [SerializeField] private bool _y;
    [SerializeField] private bool _z;
    [Space]
    [SerializeField] private float _shakeSpeed;
    [SerializeField] private float _fadeAwaySpeed;
    [SerializeField] private Transform _pivot;

    private IEnumerator _handler;
    private bool _isShaking;
    private float _power;
    private Vector3 _pivotTarget;

#if UNITY_EDITOR
    
#endif
    

    public void Shake(float power)
    {
        if (!IsPlaying())
        {
            Debug.LogWarning("Allowed only in play mode");
            return;
        }

        if (power > _power)
        {
            _power = power;
            _isShaking = true;
        }

        if (_handler == null)
        {
            _handler = Handler();
            StartCoroutine(Handler());
        }
    }

    private IEnumerator Handler()
    {
        while (_handler != null)
        {
            if (_power <= 0f)
            {
                Finish();
            }

            _pivot.localPosition = Vector3.MoveTowards(_pivot.localPosition, _pivotTarget, Time.deltaTime * _shakeSpeed);

            if (Vector3.Distance(_pivot.localPosition, _pivotTarget) <= Mathf.Epsilon)
            {
                if (_isShaking)
                {
                    RenewTarget();
                }
                else
                {
                    Finish();
                }
            }

            _power = Mathf.MoveTowards(_power, -0.4f, Time.deltaTime * _fadeAwaySpeed);
            yield return null;
        }

        void Finish()
        {
            _handler = null;
            _pivot.localPosition = Vector3.zero;
            _pivotTarget = Vector3.zero;
        }
    }

    private void RenewTarget()
    {
        if (_isShaking)
        {
            Vector3 vector = new Vector3(Random.value, Random.value, Random.value) * Mathf.Max(_power, 0f);
            if (!_x) vector.x = 0f;
            if (!_y) vector.y = 0f;
            if (!_z) vector.z = 0f;
            _pivotTarget = vector;
        }
        else
        {
            _pivotTarget = Vector3.zero;
        }
    }
}
