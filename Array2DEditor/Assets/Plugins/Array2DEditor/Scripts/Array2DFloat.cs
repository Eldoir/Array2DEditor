using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Array2D_Float", menuName = "Array2D/Float")]
    public class Array2DFloat : Array2D<float>
    {
        [SerializeField]
        CellRowFloat[] cells = new CellRowFloat[Consts.defaultGridSize];

        protected override CellRow<float> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }

    [System.Serializable]
    public class CellRowFloat : CellRow<float> { }
}