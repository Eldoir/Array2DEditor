using UnityEditor;
using System.Linq;

namespace Array2DEditor
{
    public class Array2DEnumEditor<T> : Array2DEditor
    {
        protected override int CellWidth => 70;
        protected override int CellHeight => 16;

        protected override void SetValue(SerializedProperty cell, int i, int j)
        {
            T[,] previousCells = (target as Array2D<T>).GetCells();
            int width = previousCells.GetLength(1);

            cell.enumValueIndex = 0;

            if (i < gridSize.vector2IntValue.y && j < gridSize.vector2IntValue.x)
            {
                cell.enumValueIndex = previousCells.Cast<int>().ToArray()[i * width + j];
            }
        }
    }
}