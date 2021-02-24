using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DBool))]
    public class TestArrayBoolDrawer : TestArrayDrawer
    {
        protected override object GetDefaultCellValue() => false;

        protected override object GetCellValue(SerializedProperty cell) => cell.boolValue;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.boolValue = (bool) obj;
        }
    }
}
