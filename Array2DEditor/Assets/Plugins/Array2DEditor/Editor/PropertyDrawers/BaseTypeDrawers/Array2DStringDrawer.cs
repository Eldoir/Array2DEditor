using UnityEditor;
using UnityEngine;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DString))]
    public class Array2DStringDrawer : Array2DDrawer
    {
        protected override Vector2Int GetDefaultCellSizeValue() => new Vector2Int(64, 16);

        protected override object GetDefaultCellValue() => string.Empty;

        protected override object GetCellValue(SerializedProperty cell) => cell.stringValue;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.stringValue = (string) obj;
        }
    }
}
