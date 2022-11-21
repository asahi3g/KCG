using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseMonoBehaviour : MonoBehaviour
{

    public readonly MonoEvent onEnable = new MonoEvent();
    public readonly MonoEvent onDisable = new MonoEvent();
    public readonly MonoEvent onDestroy = new MonoEvent();

    protected const string H_A = "---[";
    protected const string H_B = "]---";
    
    [NonSerialized]
    private static bool _isPlaying = false;
    [NonSerialized]
    private static bool _applicationQuit = false;

    private bool _isDestroyed;

    public class MonoEvent : UnityEvent<BaseMonoBehaviour> { };

#if UNITY_EDITOR
    protected virtual void Reset(){}

    protected virtual void OnValidate(){}
#endif

    public bool IsDestroyed() => _isDestroyed || this == null;

    public bool IsPlaying() => _isPlaying && Application.isPlaying;
    
    public bool IsApplicationQuit() => _applicationQuit;

    protected virtual void Awake()
    {
        if (!_isPlaying)
        {
            _isPlaying = true;
        }
    }

    protected virtual void Start(){}

    protected virtual void OnEnable(){
        onEnable.Invoke(this);
    }

    protected virtual void OnDisable(){
        onDisable.Invoke(this);
    }

    protected virtual void OnDestroy()
    {
        _isDestroyed = true;
        onDestroy.Invoke(this);
        onEnable.RemoveAllListeners();
        onDisable.RemoveAllListeners();
        onDestroy.RemoveAllListeners();
    }

    protected virtual void OnApplicationQuit()
    {
        if (!_applicationQuit)
        {
            _applicationQuit = true;
        }

        if (_isPlaying)
        {
            _isPlaying = false;
        }
    }

    protected virtual void OnTransformChildrenChanged()
    {
        
    }
}
