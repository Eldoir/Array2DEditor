using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DBool))]
    public class Array2DBoolEditor : Array2DEditor
    {
        protected override int CellWidth => 16;
        protected override int CellHeight => 16;

        protected override void SetValue(SerializedProperty cell, int x, int y)
        {
            bool[,] previousCells = (target as Array2DBool).GetCells();

            cell.boolValue = default(bool);

            if (x < gridSize.vector2IntValue.x && y < gridSize.vector2IntValue.y)
            {
                cell.boolValue = previousCells[x, y];
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

            var count = 0;

            for (var x = 0; x < gridSize.vector2IntValue.x; x++)
            {
                for (var y = 0; y < gridSize.vector2IntValue.y; y++)
                {
                    count += (cells[x, y] ? 1 : 0);
                }
            }

            return count;
        }
    }
}