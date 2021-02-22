using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class TestArrayEnum : TestArray<TestEnum>
    {
        [SerializeField]
        CellRowTestEnum[] cells = new CellRowTestEnum[Consts.defaultGridSize];

        protected override CellRow<TestEnum> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
}
