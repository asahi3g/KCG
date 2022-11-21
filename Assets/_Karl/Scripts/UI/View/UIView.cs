using UnityEngine;

public abstract class UIView : UIMonoBehaviour
{
    [Header(H_A + "View" + H_B)]
    [SerializeField] private Identifier _identifier;
    [SerializeField] private UIGroup _group;

    public Identifier GetIdentifier() => _identifier;
    public UIGroup GetGroup() => _group;

    protected override void Start()
    {
        base.Start();
        _group.onOpened.AddListener(OnGroupOpened);
        _group.onClosed.AddListener(OnGroupClosed);
    }

    protected abstract void OnGroupOpened();

    protected abstract void OnGroupClosed();
}
