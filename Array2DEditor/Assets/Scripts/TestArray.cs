using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public abstract class TestArray<T>
    {
        [SerializeField]
        private Vector2Int gridSize = Vector2Int.one;

        #if UNITY_EDITOR
        [SerializeField]
        [Tooltip("Only used to display the cells in the Editor.")]
        private Vector2Int cellSize = new Vector2Int(32, 16);
        #endif
    
        protected abstract CellRow<T> GetCellRow(int idx);


        public T[,] GetCells()
        {
            var ret = new T[gridSize.x, gridSize.y];

            for (var x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    ret[x, y] = GetCell(x, y);
                }
            }

            return ret;
        }

        public T GetCell(int x, int y)
        {
            return GetCellRow(x)[y];
        }
    }
}
