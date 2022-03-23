using System;
using UnityEngine;

[Serializable]
public class SomeClassString : SomeGenericClass<string>
{
    private string myString;

    public override void DoStuff()
    {
        Debug.Log($"<color=purple>Value: </color>{myString}");
    }

    public override object GetDefaultValue() => "Coucou";

    public override object GetValue() => myString;

    public override void SetValue(object value) => myString = value as string;
}