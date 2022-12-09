using UnityEngine;

public abstract class SO : ScriptableObject
{
#if UNITY_EDITOR
    protected virtual void OnValidate() { }
#endif

    protected virtual void Awake() { }
}
