using UnityEngine;

public class SOValueArray<T> : SOValueBase<T>
{
    [SerializeField] private AccessKind _access;
    [SerializeField] private T[] _value;
    
    private int _index = -1;

    private enum AccessKind
    {
        Random,
        Iterative
    }

    public bool GetRandom(out T value)
    {
        value = default;
        bool success = false;

        
        
        return success;
    }

    public override T GetValue()
    {
        int length = _value.Length;
        if (length <= 0)
        {
            return default;
        }
        
        switch (_access)
        {
            case AccessKind.Random:
            {
                _index = 0;

                if (length > 1)
                {
                    _index = Random.Range(0, length);
                }
                return _value[_index];
            }
            case AccessKind.Iterative:
            {
                _index = GetNextIndex();
                return _value[_index];
            }
        }

        return default;
    }

    private int GetNextIndex()
    {
        return (_index + 1) % _value.Length;
    }
}