
using UnityEngine;

public class AppSettings : BaseMonoBehaviour
{
    [SerializeField] private bool _runInBackground;
    [SerializeField] private int _targetFrameRate;
    
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _runInBackground = true;
        _targetFrameRate = 60;
    }
#endif
    
    protected override void Awake()
    {
        base.Awake();
        Application.runInBackground = _runInBackground;
        Application.targetFrameRate = _targetFrameRate;
    }
}
