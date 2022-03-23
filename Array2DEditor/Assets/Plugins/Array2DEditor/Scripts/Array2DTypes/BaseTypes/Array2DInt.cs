using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DInt : Array2D<int>
    {
        [SerializeField]
        RowInt[] cells = new RowInt[Consts.DefaultGridWidth];

        protected override void ResetGrid(int newWidth, int newHeight)
        {
            cells = new RowInt[newHeight];

            for (int i = 0; i < newHeight; i++)
            {
                cells[i] = new RowInt();
            }
        }

        protected override Row<int> GetRow(int idx)
        {
            return cells[idx];
        }
    }
}
