/*
 * Arthur Cousseau, 2019
 * https://www.linkedin.com/in/arthurcousseau/
 * Please share this if you enjoy it! :)
*/

using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Array2DEditor
{
    public abstract class Array2DEditor : Editor
    {
        private const int margin = 5;

        protected SerializedProperty gridSize;
        protected SerializedProperty cells;

        private Rect lastRect;
        protected Vector2Int newGridSize;
        private bool gridSizeChanged = false;
        private bool wrongSize = false;

        private Vector2 cellSize;

        private MethodInfo boldFontMethodInfo = null;

        /// <summary>
        /// In pixels.
        /// </summary>
        protected virtual int CellWidth { get { return 16; } }
        /// <summary>
        /// In pixels;
        /// </summary>
        protected virtual int CellHeight { get { return 16; } }

        protected abstract void SetValue(SerializedProperty cell, int i, int j);
        protected virtual void OnEndInspectorGUI() { }


        void OnEnable()
        {
            gridSize = serializedObject.FindProperty("gridSize");
            cells = serializedObject.FindProperty("cells");

            newGridSize = gridSize.vector2IntValue;

            cellSize = new Vector2(CellWidth, CellHeight);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update(); // Always do this at the beginning of InspectorGUI.

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.BeginChangeCheck();

                SetBoldDefaultFont(gridSizeChanged);
                newGridSize = EditorGUILayout.Vector2IntField("Grid Size", newGridSize);
                SetBoldDefaultFont(false);
                gridSizeChanged = newGridSize != gridSize.vector2IntValue;
                wrongSize = (newGridSize.x <= 0 || newGridSize.y <= 0);

                GUI.enabled = gridSizeChanged && !wrongSize;

                if (GUILayout.Button("Apply", EditorStyles.miniButton))
                {
                    bool operationAllowed = false;

                    if (newGridSize.x < gridSize.vector2IntValue.x || newGridSize.y < gridSize.vector2IntValue.y) // Smaller grid
                    {
                        operationAllowed = EditorUtility.DisplayDialog("Are you sure?", "You're about to reduce the width or height of the grid. This may erase some cells. Do you really want this?", "Yes", "No");
                    }
                    else // Bigger grid
                    {
                        operationAllowed = true;
                    }

                    if (operationAllowed)
                    {
                        InitNewGrid(newGridSize);
                    }
                }

                GUI.enabled = true;
            }
            EditorGUILayout.EndHorizontal();

            if (wrongSize)
            {
                EditorGUILayout.HelpBox("Wrong size.", MessageType.Error);
            }

            EditorGUILayout.Space();

            if (Event.current.type == EventType.Repaint)
            {
                lastRect = GUILayoutUtility.GetLastRect();
            }

            DisplayGrid(lastRect);

            OnEndInspectorGUI();

            serializedObject.ApplyModifiedProperties(); // Apply changes to all serializedProperties - always do this at the end of OnInspectorGUI.
        }

        private void InitNewGrid(Vector2 newSize)
        {
            cells.ClearArray();

            for (int i = 0; i < newSize.y; i++)
            {
                cells.InsertArrayElementAtIndex(i);
                SerializedProperty row = GetRowAt(i);

                for (int j = 0; j < newSize.x; j++)
                {
                    row.InsertArrayElementAtIndex(j);

                    SetValue(row.GetArrayElementAtIndex(j), i, j);
                }
            }

            gridSize.vector2IntValue = newGridSize;
        }

        private void DisplayGrid(Rect startRect)
        {
            Rect cellPosition = startRect;

            cellPosition.y += 10; // Same as EditorGUILayout.Space(), but in Rect

            cellPosition.size = cellSize;

            float startLineX = cellPosition.x;

            for (int i = 0; i < gridSize.vector2IntValue.y; i++)
            {
                SerializedProperty row = GetRowAt(i);
                cellPosition.x = startLineX; // Get back to the beginning of the line

                for (int j = 0; j < gridSize.vector2IntValue.x; j++)
                {
                    EditorGUI.PropertyField(cellPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                    cellPosition.x += cellSize.x + margin;
                }

                cellPosition.y += cellSize.y + margin;
                GUILayout.Space(cellSize.y + margin); // If we don't do this, the next things we're going to draw after the grid will be drawn on top of the grid
            }
        }

        protected SerializedProperty GetRowAt(int idx)
        {
            return cells.GetArrayElementAtIndex(idx).FindPropertyRelative("row");
        }

        private void SetBoldDefaultFont(bool value)
        {
            if (boldFontMethodInfo == null)
                boldFontMethodInfo = typeof(EditorGUIUtility).GetMethod("SetBoldDefaultFont", BindingFlags.Static | BindingFlags.NonPublic);

            boldFontMethodInfo.Invoke(null, new[] { value as object });
        }
    }
}