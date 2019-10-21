using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Array2D_ExampleEnum", menuName = "Array2D/ExampleEnum")]
    public class Array2DExampleEnum : Array2D<Enums.ExampleEnum>
    {
        [SerializeField]
        CellRowExampleEnum[] cells = new CellRowExampleEnum[Consts.defaultGridSize];

        protected override CellRow<Enums.ExampleEnum> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }

    [System.Serializable]
    public class CellRowExampleEnum : CellRow<Enums.ExampleEnum> { }
}