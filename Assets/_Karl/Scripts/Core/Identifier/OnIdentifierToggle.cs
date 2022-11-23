using UnityEngine;

public class OnIdentifierToggle : OnIdentifier
{
    [SerializeField] private bool _invert;
    [SerializeField] private GameObject _target;

    protected override void OnEnable()
    {
        base.OnEnable();
        RefreshLook();
    }

    protected override void OnIdentifierDependenciesChanged(IdentifierObjectCollection collection)
    {
        RefreshLook();
    }

    private void RefreshLook()
    {
        bool contains = GetIdentifier().GetObject().GetDependencies().Contains();
        if (_invert) contains = !contains;
        _target.SetActive(!contains);
    }
}
