
using UnityEngine;

public class OnButtonIdentifierClear : OnButton
{
    [SerializeField] private Identifier _identifier;
    
    protected override void OnClick()
    {
        _identifier.Clear();
    }
}
