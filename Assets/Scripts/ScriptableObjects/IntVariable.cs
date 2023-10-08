using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "ScriptableObjects/IntVariable", order = 2)]
public class IntVariable : Variable<int>
{
    public int previousHighestValue;
    public override void SetValue(int value)
    {
        if (value > previousHighestValue) previousHighestValue = value;

        _value = value;
    }

    //overload
    public void SetValue(IntVariable value)
    {
        SetValue(value.value);
    }

    public void ApplyChange(int amount)
    {
        this.value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        ApplyChange(amount.value);
    }

    public void ResetHighestValue()
    {
        previousHighestValue = 0;
    }
}
