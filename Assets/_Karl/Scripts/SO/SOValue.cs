using UnityEngine;

public class SOValue<T> : SOValueBase<T>
{
    [SerializeField] private T _value;

    public override T GetValue()
    {
        return _value;
    }

    public void SetValue(T value)
    {
        _value = value;
    }
}
