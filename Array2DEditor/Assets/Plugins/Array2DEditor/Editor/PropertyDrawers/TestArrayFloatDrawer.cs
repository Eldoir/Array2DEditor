using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DFloat))]
    public class TestArrayFloatDrawer : TestArrayDrawer
    {
        protected override object GetDefaultCellValue() => 0f;

        protected override object GetCellValue(SerializedProperty cell) => cell.floatValue;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.floatValue = (float) obj;
        }
    }
}
