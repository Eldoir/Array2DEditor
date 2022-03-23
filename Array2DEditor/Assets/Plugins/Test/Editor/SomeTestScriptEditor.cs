using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SomeTestScript))]
public class SomeTestScriptEditor : Editor
{
    private readonly (string, Action<SomeTestScript>)[] buttons = new (string, Action<SomeTestScript>)[]
    {
        ("Do Stuff", (script) => script.DoStuff()),
        ("Add One", (script) => script.AddOne()),
        ("Reset", (script) => script.Reset()),
    };

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = target as SomeTestScript;

        foreach (var button in buttons)
        {
            if (GUILayout.Button(button.Item1))
            {
                button.Item2(script);
            }
        }
    }
}
