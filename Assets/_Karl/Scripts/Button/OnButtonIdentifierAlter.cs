using UnityEngine;

public class OnButtonIdentifierAlter : OnButton
{
    [SerializeField] private AlterKind _kind;
    [SerializeField] private Identifier _identifier;
    [SerializeField] private Identifier _target;

    private enum AlterKind
    {
        Add,
        Remove
    }
    
    
    protected override void OnClick()
    {
        _target.Alter(_identifier, _kind == AlterKind.Add);
    }
}
