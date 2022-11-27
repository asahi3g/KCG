
using UnityEngine;

public class AppSettings : BaseMonoBehaviour
{
    [SerializeField] private bool _runInBackground;
    [SerializeField] private int _targetFrameRate;
    [Range(0f, 1f)]
    [SerializeField] private float _timeScale;
    
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _runInBackground = true;
        _targetFrameRate = 60;
        _timeScale = 1f;
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        UpdateSettings();
    }
#endif
    
    protected override void Awake()
    {
        base.Awake();
        UpdateSettings();
    }

    private void UpdateSettings()
    {
        Application.runInBackground = _runInBackground;
        Application.targetFrameRate = _targetFrameRate;
        Time.timeScale = _timeScale;
    }
    
}
