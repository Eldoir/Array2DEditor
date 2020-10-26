using UnityEditor;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DString))]
    public class Array2DStringEditor : Array2DEditor
    {
        protected override int CellWidth => 96;
        protected override int CellHeight => 16;

        protected override void SetValue(SerializedProperty cell, int x, int y)
        {
            string[,] previousCells = (target as Array2DString).GetCells();

            cell.stringValue = default(string);

            if (x < gridSize.vector2IntValue.x && y < gridSize.vector2IntValue.y)
            {
                cell.stringValue = previousCells[x, y];
            }
        }
    }
}