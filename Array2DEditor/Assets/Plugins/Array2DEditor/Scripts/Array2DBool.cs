using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Array2D_Bool", menuName = "Array2D/Bool")]
    public class Array2DBool : Array2D<bool>
    {
        [SerializeField]
        CellRowBool[] cells = new CellRowBool[Consts.defaultGridSize];

        protected override CellRow<bool> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }

    [System.Serializable]
    public class CellRowBool : CellRow<bool> { }
}