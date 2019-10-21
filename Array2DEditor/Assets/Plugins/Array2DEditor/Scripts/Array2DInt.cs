using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Array2D_Int", menuName = "Array2D/Int")]
    public class Array2DInt : Array2D<int>
    {
        [SerializeField]
        CellRowInt[] cells = new CellRowInt[Consts.defaultGridSize];

        protected override CellRow<int> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }

    [System.Serializable]
    public class CellRowInt : CellRow<int> { }
}