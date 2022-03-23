using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public abstract class SomeGenericDrawer : PropertyDrawer
{
    protected abstract void DrawProperty(Rect position, SerializedProperty property, GUIContent label, SomeInterface targetObject);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!Helpers.TryGetFieldInfo(property, out FieldInfo fieldInfo))
        {
            EditorGUI.HelpBox(position, $"Can't get property \"{property.propertyPath}\". It should be private serialized.", MessageType.Error);
            return;
        }

        EditorGUI.BeginProperty(position, label, property);
        {
            object targetObject = fieldInfo.GetValue(property.serializedObject.targetObject);
            DrawProperty(position, property, label, targetObject as SomeInterface);
        }
        EditorGUI.EndProperty();
    }

    private static class Helpers
    {
        public static bool TryGetFieldInfo(SerializedProperty property, out FieldInfo fieldInfo)
        {
            Type propertyType = property.serializedObject.targetObject.GetType();

            if (propertyType == null)
            {
                throw new NullReferenceException($"{nameof(propertyType)} is null. This should not happen.");
            }

            // Try get it as a public member variable
            fieldInfo = propertyType.GetField(property.propertyPath, BindingFlags.Instance | BindingFlags.Public);

            if (fieldInfo != null)
                return true;

            // Try get it as a private member variable
            fieldInfo = propertyType.GetField(property.propertyPath, BindingFlags.Instance | BindingFlags.NonPublic);

            return fieldInfo != null;
        }
    }
}