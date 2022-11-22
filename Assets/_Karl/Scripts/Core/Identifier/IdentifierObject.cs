

using UnityEngine;

public class IdentifierObject
{
    private string _key;
    private UnityEngine.Object _target;
    private IdentifierObjectCollection _dependencies;
    
    public void SetKey(string value)
    {
        _key = value;
    }
    
    public IdentifierObject(string key)
    {
        _key = key;
    }
    
    public IdentifierObject(string key, UnityEngine.Object target)
    {
        _key = key;
        _target = target;
    }


    public string GetKey() => _key;

    public IdentifierObjectCollection GetDependencies()
    {
        if(_dependencies == null) _dependencies = new IdentifierObjectCollection();
        return _dependencies;
    }
}
