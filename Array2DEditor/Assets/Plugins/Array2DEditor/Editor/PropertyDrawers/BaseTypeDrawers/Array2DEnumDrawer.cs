using UnityEditor;
using UnityEngine;

namespace Array2DEditor
{
    public class Array2DEnumDrawer<T> : Array2DDrawer
    {
        protected override Vector2Int GetDefaultCellSizeValue() => new Vector2Int(64, 16);
        
        protected override object GetDefaultCellValue() => 0;

        protected override object GetCellValue(SerializedProperty cell) => cell.enumValueIndex;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.enumValueIndex = (int) obj;
        }
    }
}
