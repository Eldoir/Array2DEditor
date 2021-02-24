using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DExampleEnum : Array2D<ExampleEnum>
    {
        [SerializeField]
        CellRowExampleEnum[] cells = new CellRowExampleEnum[Consts.defaultGridSize];

        protected override CellRow<ExampleEnum> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
}
