using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EntityCharacteristic
{
    [SerializeField] float baseValue;
    [SerializeField] Vector2 valueClamp;

    public float Value
    {
        get
        {
            return baseValue;
        }
        set
        {
            baseValue = Mathf.Clamp(value, valueClamp.x, valueClamp.y);
            onValueChanged?.Invoke(baseValue);
        }
    }

    public Vector2 Clamp
    {
        get
        {
            return valueClamp;
        }
        set
        {
            valueClamp = value;
            float clamped = Mathf.Clamp(baseValue, valueClamp.x, valueClamp.y);
            if (baseValue != clamped)
                Value = clamped;
            onClampChanged?.Invoke(valueClamp);
        }
    }
    public UnityEvent<float> onValueChanged;
    public UnityEvent<Vector2> onClampChanged;
}

[Serializable]
public class SerializableEntityCharacteristic
{
    public float value;
    public Vector2 clamp;

    public SerializableEntityCharacteristic(EntityCharacteristic characteristic)
    {
        value = characteristic.Value;
        clamp = characteristic.Clamp;
    }
}
