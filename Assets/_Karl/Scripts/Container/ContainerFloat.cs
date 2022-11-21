using UnityEngine;

[System.Serializable]
public class ContainerFloat : ContainerPrimitive<float>
{
    
    public ContainerFloat(float value, float min, float max) : base(min, max)
    {
        SetInternal_Value(Mathf.Clamp(value, GetMin(), GetMax()));
    }

    public override void Add(float amount)
    {
        Set(GetValue() + amount);
    }

    public override void Remove(float amount)
    {
        Set(GetValue() - amount);
    }

    public override void Set(float value)
    {
        value = Mathf.Clamp(value, GetMin(), GetMax());
        float previousValue = GetValue();
        
        // Up to debate
        //if (Mathf.Approximately(previousValue, value)) return;
        //or
        if (previousValue == value) return;
        
        SetInternal_Value(value);
        onChanged.Invoke(new AlterData<float>(this, previousValue, value));
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
