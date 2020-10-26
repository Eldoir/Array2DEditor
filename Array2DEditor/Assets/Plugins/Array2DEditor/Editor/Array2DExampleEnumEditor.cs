using UnityEditor;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DExampleEnum))]
    public class Array2DExampleEnumEditor : Array2DEnumEditor<Enums.ExampleEnum>
    {
        // If your enum has long names, you can replace 70 by 150, for example.
        protected override int CellWidth => 70;
        // For enums, the cell height will just change the vertical spacing. 
        protected override int CellHeight => 16;
    }
}