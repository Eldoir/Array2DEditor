using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Array2DEditor
{
    public abstract class AbstractPropertyDrawer : PropertyDrawer
    {
        protected bool VerticalScrollBarIsVisible { get; private set; }

        private ScrollView inspectorScrollView = null;
        private MethodInfo boldFontMethodInfo = null;
        

        protected abstract void _OnGUI(Rect position, SerializedProperty property, GUIContent label);


        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            VerticalScrollBarIsVisible = GetVerticalScrollBarIsVisible(); // We have to check this every frame
            
            _OnGUI(position, property, label);
        }

        protected void TryFindPropertyRelative(SerializedProperty parent, string relativePropertyPath, out SerializedProperty prop)
        {
            prop = parent.FindPropertyRelative(relativePropertyPath);

            if (prop == null)
            {
                Debug.LogError($"Couldn't find variable \"{relativePropertyPath}\" in {parent.name}");
            }
        }

        protected void SetBoldDefaultFont(bool bold)
        {
            if (boldFontMethodInfo == null)
                boldFontMethodInfo = typeof(EditorGUIUtility).GetMethod("SetBoldDefaultFont",
                    BindingFlags.Static | BindingFlags.NonPublic);
            
            boldFontMethodInfo?.Invoke(null, new[] { bold as object });
        }
        
        private bool GetVerticalScrollBarIsVisible()
        {
            if (inspectorScrollView == null)
            {
                var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();

                foreach (var editor in windows)
                {
                    if (editor.titleContent.text == "Inspector")
                    {
                        var scrollViewFieldInfo = editor.GetType()
                            .GetField("m_ScrollView",
                                BindingFlags.NonPublic | BindingFlags.Instance);
                        
                        if (scrollViewFieldInfo != null)
                        {
                            inspectorScrollView = (ScrollView) scrollViewFieldInfo.GetValue(editor);
                            break;
                        }
                        else
                        {
                            Debug.LogError("m_ScrollView couldn't be found in InspectorWindow.");
                            return false;
                        }
                    }
                }
            }

            return inspectorScrollView.verticalScroller.visible;
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
