using UnityEngine;

public class OnIdentifierAlter : OnIdentifier
{
    [SerializeField] private Identifier _target;

    protected override void OnIdentifierDependenciesChanged(IdentifierObjectCollection collection)
    {
        bool contains = collection.Contains();
        _target.Alter(GetIdentifier(), contains);
    }
}
