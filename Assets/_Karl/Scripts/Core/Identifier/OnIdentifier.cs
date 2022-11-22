using UnityEngine;

public abstract class OnIdentifier : BaseMonoBehaviour
{
    [SerializeField] private Identifier _identifier;
    
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _identifier = GetComponent<Identifier>();
    }
#endif

    public Identifier GetIdentifier() => _identifier;

    protected override void OnEnable()
    {
        base.OnEnable();
        _identifier.GetObject().GetDependencies().onChanged.AddListener(OnIdentifierDependenciesChanged);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (IsApplicationQuit()) return;
        _identifier.GetObject().GetDependencies().onChanged.RemoveListener(OnIdentifierDependenciesChanged);
    }

    protected abstract void OnIdentifierDependenciesChanged(IdentifierObjectCollection collection);
}
