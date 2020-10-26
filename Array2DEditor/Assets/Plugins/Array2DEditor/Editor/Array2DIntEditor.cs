using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DInt))]
    public class Array2DIntEditor : Array2DEditor
    {
        protected override int CellWidth => 32;
        protected override int CellHeight => 16;

        protected override void SetValue(SerializedProperty cell, int x, int y)
        {
            int[,] previousCells = (target as Array2DInt).GetCells();

            cell.intValue = default(int);

            if (x < gridSize.vector2IntValue.x && y < gridSize.vector2IntValue.y)
            {
                cell.intValue = previousCells[x, y];
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

            var count = 0;

            for (var x = 0; x < gridSize.vector2IntValue.x; x++)
            {
                for (var y = 0; y < gridSize.vector2IntValue.y; y++)
                {
                    count += cells[x, y];
                }
            }

            return count;
        }
    }
}