using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DBool : Array2D<bool>
    {
        [SerializeField]
        RowBool[] cells = new RowBool[Consts.DefaultGridWidth];

        protected override void ResetGrid(int newWidth, int newHeight)
        {
            cells = new RowBool[newHeight];

            for (int i = 0; i < newHeight; i++)
            {
                cells[i] = new RowBool();
            }
        }

        protected override Row<bool> GetRow(int idx)
        {
            return cells[idx];
        }
    }
}
