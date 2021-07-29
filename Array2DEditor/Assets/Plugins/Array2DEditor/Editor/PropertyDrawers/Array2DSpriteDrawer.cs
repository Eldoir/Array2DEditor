using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DSprite))]
    public class Array2DSpriteDrawer : Array2DDrawer
    {
        protected override Vector2Int GetDefaultCellSizeValue() => new Vector2Int(64, 64);

        protected override object GetDefaultCellValue() => null;

        protected override object GetCellValue(SerializedProperty cell) => cell.objectReferenceValue;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.objectReferenceValue = (Sprite)obj;
        }
    }
}
