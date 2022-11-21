using UnityEngine;
using UnityEngine.Events;

public class UIGroup : UIMonoBehaviour
{
    [SerializeField] private Identifier _identifier;

    public readonly UnityEvent onOpened = new UnityEvent();
    public readonly UnityEvent onClosed = new UnityEvent();

    public Identifier GetIdentifier() => _identifier;

    protected override void Awake()
    {
        base.Awake();
        _identifier.GetObject().GetDependencies().onChanged.AddListener(OnIdentifierDependenciesChanged);
    }

    private void OnIdentifierDependenciesChanged(IdentifierObjectCollection collection)
    {
        if (collection.Contains())
        {
            onOpened.Invoke();
        }
        else
        {
            onClosed.Invoke();
        }
    }

}
