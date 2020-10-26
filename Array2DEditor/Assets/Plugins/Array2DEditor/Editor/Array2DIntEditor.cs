using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DInt))]
    public class Array2DIntEditor : Array2DEditor
    {
        protected override int CellWidth => 32;
        protected override int CellHeight => 16;

        protected override void SetValue(SerializedProperty cell, int i, int j)
        {
            int[,] previousCells = (target as Array2DInt).GetCells();

            cell.intValue = default(int);

            if (i < gridSize.vector2IntValue.y && j < gridSize.vector2IntValue.x)
            {
                cell.intValue = previousCells[i, j];
            }
        }

        protected override void OnEndInspectorGUI()
        {
            if (GUILayout.Button("Count sum")) // Just an example, you can remove this.
            {
                EditorUtility.DisplayDialog("Sum", "Sum: " + GetSum(), "OK");
            }
        }

        /// <summary>
        /// Just an example, you can remove this.
        /// </summary>
        private int GetSum()
        {
            int[,] cells = (target as Array2DInt).GetCells();

            int count = 0;

            for (int i = 0; i < gridSize.vector2IntValue.y; i++)
            {
                for (int j = 0; j < gridSize.vector2IntValue.x; j++)
                {
                    count += cells[i, j];
                }
            }

            return count;
        }
    }
}