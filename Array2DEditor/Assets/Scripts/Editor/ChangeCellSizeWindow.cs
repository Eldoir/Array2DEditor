using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    public class ChangeCellSizeWindow : EditorWindow
    {
        static SerializedProperty cellSizeProperty;
        private static Vector2Int newCellSize;

        private const string cellSizeControlName = "CellSize";
        private static bool cellSizeControlFocused;
    
        public static void ShowWindow(SerializedProperty cellSizeProperty)
        {
            ChangeCellSizeWindow.cellSizeProperty = cellSizeProperty;
            newCellSize = cellSizeProperty.vector2IntValue;

            cellSizeControlFocused = false;
        
            // Get existing open window or if none, make a new one
            var window = GetWindow<ChangeCellSizeWindow>();
            
            // Showing it as a modal forces the user to process it immediately
            window.ShowModalUtility();
        }

        void OnGUI()
        {
            GUI.SetNextControlName(cellSizeControlName);
            newCellSize = EditorGUILayout.Vector2IntField("Cell Size", newCellSize);

            if (!cellSizeControlFocused)
            {
                EditorGUI.FocusTextInControl(cellSizeControlName);
                cellSizeControlFocused = true;
            }
            
            var wrongCellSize = (newCellSize.x <= 0 || newCellSize.y <= 0);

            if (wrongCellSize)
            {
                EditorGUILayout.HelpBox("Wrong cell size.", MessageType.Error);
            }
        
            GUI.enabled = !wrongCellSize;

            if (GUILayout.Button("Apply"))
            {
                Apply();
            }

            GUI.enabled = true;

            // We're doing this in OnGUI() since the Update() function doesn't seem to get called when we show the window with ShowModalUtility().
            if ((Event.current.type == EventType.KeyDown || Event.current.type == EventType.KeyUp) &&
                (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
            {
                Apply();
            }
        }

        private void Apply()
        {
            if (newCellSize != cellSizeProperty.vector2IntValue)
            {
                cellSizeProperty.vector2IntValue = newCellSize;
                cellSizeProperty.serializedObject.ApplyModifiedProperties();
            }
            
            Close();
        }
    }
}
