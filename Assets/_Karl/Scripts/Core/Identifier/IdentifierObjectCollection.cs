using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IdentifierObjectCollection
{
    private List<IdentifierObject> _objects;
    public readonly Event onChanged = new Event();

    public class Event: UnityEvent<IdentifierObjectCollection>{}

    public List<IdentifierObject> Get()
    {
        if(_objects == null) _objects = new List<IdentifierObject>();
        RemoveHelper(null);
        return _objects;
    }
    
    public bool Contains()
    {
        if (_objects == null) return false;
        return Get().Count > 0;
    }

    public bool GetBoolValue(bool invert)
    {
        bool value = Contains();
        if (invert) value = !value;
        return value;
    }
    
    public bool Alter(IdentifierObject obj, bool add)
    {
        if (obj == null) return false;
        if (add)
        {
            return Add(obj);
        }
        else
        {
            return Remove(obj);
        }
    }

    public bool Add(IdentifierObject obj)
    {
        if (obj == null) return false;
        if (Contains(obj)) return false;
        Get().Add(obj);
        onChanged.Invoke(this);
        return true;
    }
    
    public bool Remove(IdentifierObject obj)
    {
        if (_objects == null) return false;
        if (obj == null) return false;
        return RemoveHelper(obj);
    }

    private bool RemoveHelper(IdentifierObject obj)
    {
        if (_objects == null) return false;
        bool removed = _objects.Remove(obj);
        if (removed)
        {
            onChanged.Invoke(this);
        }
        return removed;
    }

    public bool Contains(IdentifierObject obj)
    {
        if (_objects == null) return false;
        return _objects.Contains(obj);
    }

    public bool Clear()
    {
        if (!Contains()) return false;
        Get().Clear();
        onChanged.Invoke(this);
        return true;
    }


}
