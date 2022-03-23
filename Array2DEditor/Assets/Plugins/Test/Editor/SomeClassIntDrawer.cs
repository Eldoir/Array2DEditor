using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SomeClassInt))]
public class SomeClassIntDrawer : SomeGenericDrawer
{
    protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label, SomeInterface targetObject)
    {
        targetObject.SetValue(EditorGUI.IntField(position, label, (int)targetObject.GetValue()));
    }
}