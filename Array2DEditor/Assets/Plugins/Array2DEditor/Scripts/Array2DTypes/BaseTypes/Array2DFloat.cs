using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DFloat : Array2D<float>
    {
        [SerializeField]
        RowFloat[] cells = new RowFloat[Consts.DefaultGridWidth];

        protected override void ResetGrid(int newWidth, int newHeight)
        {
            cells = new RowFloat[newHeight];

            for (int i = 0; i < newHeight; i++)
            {
                cells[i] = new RowFloat();
            }
        }

        protected override Row<float> GetRow(int idx)
        {
            return cells[idx];
        }
    }
}
