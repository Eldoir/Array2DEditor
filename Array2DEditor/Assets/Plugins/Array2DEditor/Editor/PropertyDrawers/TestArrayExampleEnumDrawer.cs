using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DExampleEnum))]
    public class TestArrayExampleEnumDrawer : TestArrayDrawer
    {
        protected override object GetDefaultCellValue() => 0;

        protected override object GetCellValue(SerializedProperty cell) => cell.enumValueIndex;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.enumValueIndex = (int) obj;
        }
    }
}
