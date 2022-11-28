using UnityEngine.Events;


// Note(Brandon): we should be using a struct instead of a class for Agent Stats
// right now this code is using a class for each agent stat (Health, Food ...)
// --- 
// The Gui should use GetStat to determine if the value has changed instead of onChange Event
// ---
// We Should have an enum for each Stat Type (Health, Food ...)

[System.Serializable]
public abstract class Container<T>
{
    private T _value;
    public readonly AlterData<T>.Event onChanged = new AlterData<T>.Event();
    
    [System.Serializable]
    public class Event : UnityEvent<T>{}

    public class AlterData<T>
    {
        public Container<T> container;
        public T previousValue;
        public T newValue;
        
        [System.Serializable]
        public class Event : UnityEvent<AlterData<T>>{}

        public AlterData(Container<T> container, T previousValue, T newValue)
        {
            this.container = container;
            this.previousValue = previousValue;
            this.newValue = newValue;
        }
    }

    public T GetValue() => _value;

    protected void SetInternal_Value(T value)
    {
        _value = value;
    }

    public abstract void Add(T amount);
    
    public abstract void Remove(T amount);

    public abstract void Set(T value);
}
