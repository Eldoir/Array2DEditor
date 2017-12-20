/*
 * Arthur Cousseau, 2017
 * https://www.linkedin.com/in/arthurcousseau/
 * Please share this if you enjoy it! :)
*/

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PieceData))]
public class PieceDataEditor : Editor
{
    private const int defaultCellSize = 25; // px

    SerializedProperty gridSize;
    SerializedProperty cells;

    Rect lastRect;

	void OnEnable()
	{
        gridSize = serializedObject.FindProperty("gridSize");
        cells = serializedObject.FindProperty("cells");
	}

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Always do this at the beginning of InspectorGUI.

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(gridSize);
        if (EditorGUI.EndChangeCheck()) // Code to execute if grid size changed
        {
            InitNewGrid(gridSize.intValue);
        }

        EditorGUILayout.Space();

        if (Event.current.type == EventType.Repaint)
        {
            lastRect = GUILayoutUtility.GetLastRect();
        }

        DisplayGrid(lastRect);

        if (GUILayout.Button("Count active cells")) // Just an example, you can remove this.
        {
            Debug.Log((target as PieceData).GetCountActiveCells());
        }

        serializedObject.ApplyModifiedProperties(); // Apply changes to all serializedProperties - always do this at the end of OnInspectorGUI.
    }

    private void InitNewGrid(int newSize)
    {
        cells.ClearArray();

        for(int i = 0; i < newSize; i++)
        {
            cells.InsertArrayElementAtIndex(i);
            SerializedProperty row = GetRowAt(i);

            for(int j = 0; j < newSize; j++)
            {
                row.InsertArrayElementAtIndex(j);
            }
        }
    }

    private void DisplayGrid(Rect startRect)
    {
        Rect cellPosition = startRect;

        cellPosition.y += 10; // Same as EditorGUILayout.Space(), but in Rect

        cellPosition.size = Vector2.one * defaultCellSize;

        float startLineX = cellPosition.x;

        for (int i = 0; i < gridSize.intValue; i++)
        {
            SerializedProperty row = GetRowAt(i);
            cellPosition.x = startLineX; // Get back to the beginning of the line

            for (int j = 0; j < gridSize.intValue; j++)
            {
                EditorGUI.PropertyField(cellPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                cellPosition.x += defaultCellSize;
            }

            cellPosition.y += defaultCellSize;
            GUILayout.Space(defaultCellSize); // If we don't do this, the next things we're going to draw after the grid will be drawn on top of the grid
        }
    }

    private SerializedProperty GetRowAt(int idx)
    {
        return cells.GetArrayElementAtIndex(idx).FindPropertyRelative("row");
    }
}
#endif
