using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DExampleEnum))]
    public class TestArrayExampleEnumDrawer : TestArrayEnumDrawer<ExampleEnum> {}
}
