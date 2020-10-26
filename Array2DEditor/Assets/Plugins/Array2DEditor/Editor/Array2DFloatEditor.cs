using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DFloat))]
    public class Array2DFloatEditor : Array2DEditor
    {
        protected override int CellWidth => 32;
        protected override int CellHeight => 16;

        protected override void SetValue(SerializedProperty cell, int x, int y)
        {
            float[,] previousCells = (target as Array2DFloat).GetCells();

            cell.floatValue = default(float);

            if (x < gridSize.vector2IntValue.x && y < gridSize.vector2IntValue.y)
            {
                cell.floatValue = previousCells[x, y];
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
        private float GetSum()
        {
            float[,] cells = (target as Array2DFloat).GetCells();

            var count = 0f;

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