using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public abstract class Array2D<T> : IEnumerable<T>
    {
        public Vector2Int GridSize => gridSize;
        
        [SerializeField]
        private Vector2Int gridSize = new Vector2Int(Consts.DefaultGridWidth, Consts.DefaultGridHeight);
        
        #pragma warning disable 414
        /// <summary>
        /// NOTE: Only used to display the cells in the Editor. This won't affect the build.
        /// </summary>
        [SerializeField]
        private Vector2Int cellSize;
        #pragma warning restore 414

        protected abstract void ResetGrid(int newWidth, int newHeight);
        protected abstract Row<T> GetRow(int idx);

        #region Grid Size

        public void SetGridSize(Vector2Int newGridSize) => SetGridSize(newGridSize.x, newGridSize.y);

        public void SetGridSize(int newWidth, int newHeight)
        {
            T[,] previousCells = GetCells();
            Vector2Int previousGridSize = gridSize;

            ResetGrid(newWidth, newHeight);

            // Restore cells from previous grid, if any
            for (int y = 0; y < newHeight; y++)
            {
                GetRow(y).SetSize(newWidth);

                for (int x = 0; x < newWidth; x++)
                {
                    if (x < previousGridSize.x && y < previousGridSize.y)
                    {
                        SetCell(x, y, previousCells[y, x]);
                    }
                }
            }

            gridSize.x = newWidth;
            gridSize.y = newHeight;
        }

        #endregion

        #region Cells

        public T this[int x, int y]
        {
            get => GetCell(x, y);
            set => SetCell(x, y, value);
        }
        
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
            return GetRow(y)[x];
        }
        
        public void SetCell(int x, int y, T value)
        {
            GetRow(y)[x] = value;
        }

        #endregion

        #region IEnumerable

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    yield return this[x, y];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        #endregion
    }
}
