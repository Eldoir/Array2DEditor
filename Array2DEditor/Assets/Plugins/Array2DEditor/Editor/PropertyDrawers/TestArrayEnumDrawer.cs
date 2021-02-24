using UnityEditor;

namespace Array2DEditor
{
    public class TestArrayEnumDrawer<T> : TestArrayDrawer
    {
        protected override object GetDefaultCellValue() => 0;

        protected override object GetCellValue(SerializedProperty cell) => cell.enumValueIndex;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.enumValueIndex = (int) obj;
        }
    }
}
