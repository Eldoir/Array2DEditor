using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Array2D_String", menuName = "Array2D/String")]
    public class Array2DString : Array2D<string>
    {
        [SerializeField]
        CellRowString[] cells = new CellRowString[Consts.defaultGridSize];

        protected override CellRow<string> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }

    [System.Serializable]
    public class CellRowString : CellRow<string> { }
}