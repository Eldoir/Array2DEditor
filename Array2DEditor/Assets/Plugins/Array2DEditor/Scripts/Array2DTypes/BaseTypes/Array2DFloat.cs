using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DFloat : Array2D<float>
    {
        [SerializeField]
        CellRowFloat[] cells = new CellRowFloat[Consts.defaultGridSize];

        protected override CellRow<float> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
}
