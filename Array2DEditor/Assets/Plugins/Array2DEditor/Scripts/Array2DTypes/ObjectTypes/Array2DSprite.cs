using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DSprite : Array2D<Sprite>
    {
        [SerializeField]
        RowSprite[] cells = new RowSprite[Consts.DefaultGridWidth];

        protected override void ResetGrid(int newWidth, int newHeight)
        {
            cells = new RowSprite[newHeight];

            for (int i = 0; i < newHeight; i++)
            {
                cells[i] = new RowSprite();
            }
        }

        protected override Row<Sprite> GetRow(int idx)
        {
            return cells[idx];
        }
    }
}
