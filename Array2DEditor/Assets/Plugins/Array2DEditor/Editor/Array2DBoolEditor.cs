using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DBool))]
    public class Array2DBoolEditor : Array2DEditor
    {
        private Vector2 cellSize = new Vector2(16, 16);


        protected override Vector2 GetCellSize()
        {
            return cellSize;
        }

        protected override void SetValue(SerializedProperty cell, int i, int j)
        {
            bool[,] previousCells = (target as Array2DBool).GetCells();

            cell.boolValue = default(bool);

            if (i < gridSize.vector2IntValue.y && j < gridSize.vector2IntValue.x)
            {
                cell.boolValue = previousCells[i, j];
            }
        }

        protected override void OnEndInspectorGUI()
        {
            if (GUILayout.Button("Count active cells")) // Just an example, you can remove this.
            {
                EditorUtility.DisplayDialog("Active cells count", "Active cells count: " + GetCountActiveCells(), "OK");
            }
        }

        /// <summary>
        /// Just an example, you can remove this.
        /// </summary>
        private int GetCountActiveCells()
        {
            bool[,] cells = (target as Array2DBool).GetCells();

            int count = 0;

            for (int i = 0; i < gridSize.vector2IntValue.y; i++)
            {
                for (int j = 0; j < gridSize.vector2IntValue.x; j++)
                {
                    if (cells[i, j]) count++;
                }
            }

            return count;
        }
    }
}