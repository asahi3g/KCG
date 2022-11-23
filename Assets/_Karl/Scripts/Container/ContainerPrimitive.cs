using System;

[System.Serializable]
public abstract class ContainerPrimitive<T> : Container<T> where T : IComparable, IFormattable, IConvertible
{
    private T _min;
    private T _max;

    public T GetMin() => _min;
    public T GetMax() => _max;

    public abstract float GetValueNormalized();
    
    public abstract float GetPercentage();

    public ContainerPrimitive(T min, T max)
    {
        _min = min;
        _max = max;
    }
    
    public void SetAsMin()
    {
        Set(_min);
    }

    public void SetAsMax()
    {
        Set(_max);
    }
}
