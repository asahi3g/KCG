using UnityEngine;

public class Identifier : BaseMonoBehaviour
{
    [TextArea(3,6)]
    [SerializeField] private string _key;
    
    private IdentifierObject _object;

    public string GetKey() => _key;
    
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        SetKey(gameObject.name.ToLower().Replace(' ', '_'));
    }
#endif

    public void SetKey(string value)
    {
        _key = value;
        if (_object != null)
        {
            _object.SetKey(_key);
        }
    }

    
    public IdentifierObject GetObject()
    {
        if (_object == null)
        {
            _object = new IdentifierObject(_key, this);
        }
        return _object;
    }

    public bool Contains() => GetObject().GetDependencies().Contains();
    
    public bool Alter(Identifier obj, bool add) => Alter(obj.GetObject(), add);

    public bool Alter(IdentifierObject obj, bool add) => GetObject().GetDependencies().Alter(obj, add);

    public bool Add(Identifier obj) => Add(obj.GetObject());
    
    public bool Add(IdentifierObject obj) => GetObject().GetDependencies().Add(obj);
    
    public bool Remove(Identifier obj) => Remove(obj.GetObject());
    
    public bool Remove(IdentifierObject obj) => GetObject().GetDependencies().Remove(obj);

    public bool Contains(Identifier obj) => Contains(obj.GetObject());

    public bool Contains(IdentifierObject obj) => GetObject().GetDependencies().Contains(obj);

    public void Toggle(Identifier obj)
    {
        if (obj == null) return;
        if (Contains(obj))
        {
            Remove(obj);
        }
        else
        {
            Add(obj);
        }
    }

    public bool Clear() => GetObject().GetDependencies().Clear();
}
