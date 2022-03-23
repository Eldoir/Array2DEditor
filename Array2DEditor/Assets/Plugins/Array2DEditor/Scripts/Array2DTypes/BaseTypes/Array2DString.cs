using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DString : Array2D<string>
    {
        [SerializeField]
        RowString[] cells = new RowString[Consts.DefaultGridWidth];

        protected override void ResetGrid(int newWidth, int newHeight)
        {
            cells = new RowString[newHeight];

            for (int i = 0; i < newHeight; i++)
            {
                cells[i] = new RowString();
            }
        }

        protected override Row<string> GetRow(int idx)
        {
            return cells[idx];
        }
    }
}
