using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    public class ChangeCellSizeWindow : EditorWindow
    {
        static SerializedProperty cellSizeProperty;
        private static Vector2Int newCellSize;
    
        public static void ShowWindow(SerializedProperty cellSizeProperty)
        {
            ChangeCellSizeWindow.cellSizeProperty = cellSizeProperty;
            newCellSize = cellSizeProperty.vector2IntValue;
        
            // Get existing open window or if none, make a new one
            var window = (ChangeCellSizeWindow)GetWindow(typeof(ChangeCellSizeWindow));
            // Set the window to be at the center of the screen
            window.position = new Rect(Screen.width / 2f, Screen.height / 2f, 250, 120);
            // Showing it as a modal forces the user to process it immediately
            window.ShowModalUtility();
        }
    
        void OnGUI()
        {
            newCellSize = EditorGUILayout.Vector2IntField("Cell Size", newCellSize);
        
            var wrongCellSize = (newCellSize.x <= 0 || newCellSize.y <= 0);

            if (wrongCellSize)
            {
                EditorGUILayout.HelpBox("Wrong cell size.", MessageType.Error);
            }
        
            GUI.enabled = !wrongCellSize;

            if (GUILayout.Button("Apply"))
            {
                cellSizeProperty.vector2IntValue = newCellSize;
                cellSizeProperty.serializedObject.ApplyModifiedProperties();
                Close();
            }

            GUI.enabled = true;
        }
    }
}
