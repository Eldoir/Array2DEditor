using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public abstract class TestArray<T>
    {
        [SerializeField]
        private Vector2Int gridSize = Vector2Int.one * Consts.defaultGridSize;
        
        #pragma warning disable 219
        /// <summary>
        /// NOTE: Only used to display the cells in the Editor. This won't affect the build.
        /// </summary>
        [SerializeField]
        private Vector2Int cellSize = new Vector2Int(32, 16);
        #pragma warning restore 219

        protected abstract CellRow<T> GetCellRow(int idx);


        public T[,] GetCells()
        {
            var ret = new T[gridSize.y, gridSize.x];

            for (var y = 0; y < gridSize.y; y++)
            {
                for (var x = 0; x < gridSize.x; x++)
                {
                    ret[y, x] = GetCell(x, y);
                }
            }

            return ret;
        }

        public T GetCell(int x, int y)
        {
            return GetCellRow(y)[x];
        }
    }
}
