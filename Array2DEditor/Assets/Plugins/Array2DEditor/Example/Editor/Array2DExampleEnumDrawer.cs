using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DExampleEnum))]
    public class Array2DExampleEnumDrawer : Array2DEnumDrawer<ExampleEnum> {}
}
