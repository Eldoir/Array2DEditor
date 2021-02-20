using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Array2DEditor
{
    public abstract class AbstractPropertyDrawer : PropertyDrawer
    {
        private MethodInfo boldFontMethodInfo = null;
        
        protected void TryFindPropertyRelative(SerializedProperty parent, string relativePropertyPath, out SerializedProperty prop)
        {
            prop = parent.FindPropertyRelative(relativePropertyPath);

            if (prop == null)
            {
                Debug.LogError($"Couldn't find variable \"{relativePropertyPath}\" in {parent.name}");
            }
        }

        protected bool VerticalScrollBarIsVisible()
        {
            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();

            foreach (var editor in windows)
            {
                if (editor.titleContent.text == "Inspector")
                {
                    var scrollViewFieldInfo = editor.GetType()
                        .GetField("m_ScrollView",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                    if (scrollViewFieldInfo != null)
                    {
                        var scrollView = (UnityEngine.UIElements.ScrollView) scrollViewFieldInfo.GetValue(editor);

                        if (scrollView != null) // is null once after a recompile
                            return scrollView.verticalScroller.visible;
                        else
                            return false;
                    }
                    else
                    {
                        Debug.LogError("m_ScrollView couldn't be found in InspectorWindow.");
                        return false;
                    }
                }
            }

            Debug.LogError("InspectorWindow couldn't be found.");
            return false;
        }
        
        protected void SetBoldDefaultFont(bool bold)
        {
            if (boldFontMethodInfo == null)
                boldFontMethodInfo = typeof(EditorGUIUtility).GetMethod("SetBoldDefaultFont",
                    BindingFlags.Static | BindingFlags.NonPublic);
            
            boldFontMethodInfo?.Invoke(null, new[] { bold as object });
        }

        #region Debug

        protected void DrawDebugRect(Rect rect) => DrawDebugRect(rect, new Color(1f, 0f, 1f, .2f));

        protected void DrawDebugRect(Rect rect, Color color)
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            var prevBoxTex = GUI.skin.box.normal.background;
            GUI.skin.box.normal.background = texture;
            GUI.Box(rect, GUIContent.none);
            GUI.skin.box.normal.background = prevBoxTex;
        }

        #endregion
    }
}
