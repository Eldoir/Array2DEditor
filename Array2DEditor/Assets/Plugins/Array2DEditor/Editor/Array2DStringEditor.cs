using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DString))]
    public class PieceDataStringEditor : Array2DEditor
    {
        private Vector2 cellSize = new Vector2(96, 16);


        protected override Vector2 GetCellSize()
        {
            return cellSize;
        }

        protected override void SetValue(SerializedProperty cell, int i, int j)
        {
            string[,] previousCells = (target as Array2DString).GetCells();

            cell.stringValue = default(string);

            if (i < gridSize.vector2IntValue.y && j < gridSize.vector2IntValue.x)
            {
                cell.stringValue = previousCells[i, j];
            }
        }
    }
}