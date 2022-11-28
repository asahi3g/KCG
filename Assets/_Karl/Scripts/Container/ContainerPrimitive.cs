using System;

// Note(Brandon): we should be using a struct instead of a class for Agent Stats
// right now this code is using a class for each agent stat (Health, Food ...)
// --- 
// The Gui should use GetStat to determine if the value has changed instead of onChange Event
// ---
// We Should have an enum for each Stat Type (Health, Food ...)

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
