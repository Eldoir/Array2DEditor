using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public abstract class Array2D<T>
    {
        public Vector2Int GridSize => gridSize;
        
        [SerializeField]
        private Vector2Int gridSize = Vector2Int.one * Consts.defaultGridSize;
        
        #pragma warning disable 414
        /// <summary>
        /// NOTE: Only used to display the cells in the Editor. This won't affect the build.
        /// </summary>
        [SerializeField]
        private Vector2Int cellSize;
        #pragma warning restore 414

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
        
        public void SetCell(int x, int y, T value)
        {
            GetCellRow(y)[x] = value;
        }
    }
}
