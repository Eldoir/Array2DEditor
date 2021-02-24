using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DString))]
    public class TestArrayStringDrawer : TestArrayDrawer
    {
        protected override object GetDefaultCellValue() => string.Empty;

        protected override object GetCellValue(SerializedProperty cell) => cell.stringValue;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.stringValue = (string) obj;
        }
    }
}
