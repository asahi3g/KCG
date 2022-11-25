using UnityEngine;

public abstract class OnContentSelection : BaseMonoBehaviour
{
    [SerializeField] private Identifier _identifier;
    [SerializeField] private UIGroup _group;
    [SerializeField] private UIContentSelection _selection;

    public UIGroup GetGroup() => _group;

    protected override void OnEnable()
    {
        base.OnEnable();
        _selection.onSelectWithPrevious.AddListener(OnContentSelectionEvent);
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        _selection.onSelectWithPrevious.RemoveListener(OnContentSelectionEvent);
    }

    protected abstract void OnContentSelectionEvent(UIContentElement previous, UIContentElement element);

    public void AlterGroup(bool alter)
    {
        _group.GetIdentifier().Alter(_identifier, alter);
    }
}
