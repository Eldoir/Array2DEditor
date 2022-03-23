using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SomeClassString))]
public class SomeClassStringDrawer : SomeGenericDrawer
{
    protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label, SomeInterface targetObject)
    {
        targetObject.SetValue(EditorGUI.TextField(position, label, (string)targetObject.GetValue()));
    }
}