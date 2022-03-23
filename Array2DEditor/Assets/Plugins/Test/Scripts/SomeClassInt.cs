using System;
using UnityEngine;

[Serializable]
public class SomeClassInt : SomeGenericClass<int>
{
    private int myInt;

    public override void DoStuff()
    {
        Debug.Log($"<color=blue>Value: </color>{Value}");
    }

    public override object GetDefaultValue() => 42;

    public override object GetValue() => myInt;

    public override void SetValue(object value) => myInt = (int)value;
}