using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(TestArrayInt))]
    public class TestArrayIntDrawer : TestArrayDrawer
    {
        protected override object GetDefaultCellValue() => 0;

        protected override object GetCellValue(SerializedProperty cell) => cell.intValue;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.intValue = (int) obj;
        }
    }
}
