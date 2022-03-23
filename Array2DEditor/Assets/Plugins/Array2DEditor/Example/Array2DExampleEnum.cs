using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DExampleEnum : Array2D<ExampleEnum>
    {
        [SerializeField]
        RowExampleEnum[] cells = new RowExampleEnum[Consts.DefaultGridWidth];

        protected override void ResetGrid(int newWidth, int newHeight)
        {
            cells = new RowExampleEnum[newHeight];

            for (int i = 0; i < newHeight; i++)
            {
                cells[i] = new RowExampleEnum();
            }
        }

        protected override Row<ExampleEnum> GetRow(int idx)
        {
            return cells[idx];
        }
    }
    
    [System.Serializable]
    public class RowExampleEnum : Row<ExampleEnum> { }
}
