using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnIdentifierToggleCanvas : OnIdentifier
{
    [SerializeField] private bool _invert;
    [SerializeField] private Canvas _canvas;

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
        _canvas.enabled = contains;
    }
}
