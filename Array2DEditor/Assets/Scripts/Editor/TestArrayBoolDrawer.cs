using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(TestArrayBool))]
    public class TestArrayBoolDrawer : TestArrayDrawer
    {
        protected override void SetValue(SerializedProperty cell, int x, int y)
        {
            /*var previousCells = (target as TestArrayInt).GetCells();

            cell.intValue = default;

            if (x < gridSizeProperty.vector2IntValue.x && y < gridSizeProperty.vector2IntValue.y)
            {
                cell.intValue = previousCells[x, y];
            }*/
        }
    }
}
