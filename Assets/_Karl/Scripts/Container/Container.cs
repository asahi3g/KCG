using UnityEngine.Events;

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
