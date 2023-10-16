using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "ScriptableObjects/BoolVariable", order = 6)]
public class BoolVariable : Variable<bool>
{
    public override void SetValue(bool value)
    {
        _value = value;
    }

    public void SetValue(BoolVariable value)
    {
        SetValue(value.value);
    }

    public void Toggle()
    {
        _value = !_value;
    }
}
