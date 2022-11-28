using UnityEngine;

// Note(Brandon): we should be using a struct instead of a class for Agent Stats
// right now this code is using a class for each agent stat (Health, Food ...)
// --- 
// The Gui should use GetStat to determine if the value has changed instead of onChange Event
// ---
// We Should have an enum for each Stat Type (Health, Food ...)

[System.Serializable]
public class ContainerInt : ContainerPrimitive<int>
{

    public ContainerInt(int value, int min, int max) : base(min, max)
    {
        SetInternal_Value(Mathf.Clamp(value, GetMin(), GetMax()));
    }

    public override void Add(int amount)
    {
        Set(GetValue() + amount);
    }
    
    public override void Remove(int amount)
    {
        Set(GetValue() - amount);
    }

    public override void Set(int value)
    {
        value = Mathf.Clamp(value, GetMin(), GetMax());
        int previousValue = GetValue();
        if (previousValue == value) return;
        SetInternal_Value(value);
        onChanged.Invoke(new AlterData<int>(this, previousValue, value));
    }

    public override float GetValueNormalized()
    {
        return Mathf.InverseLerp(GetMin(), GetMax(), GetValue());
    }

    public override float GetPercentage()
    {
        return GetValueNormalized() * 100f;
    }
}
