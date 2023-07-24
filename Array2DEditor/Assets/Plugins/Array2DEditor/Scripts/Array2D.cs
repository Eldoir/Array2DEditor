using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Array2DEditor
{
    [Serializable]
    public abstract class Array2D<T> : IEnumerable<T>, ICloneable
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
            return GetCellRow(y)[x];
        }

        public void SetCell(int x, int y, T value)
        {
            GetCellRow(y)[x] = value;
        }

        #region IEnumerable

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    yield return this[y, x];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        #endregion

        #region ICloneable

        /// <summary>
        /// Returns a new instance of <see cref="Array2D{T}"/> with cloned values.
        /// If <typeparamref name="T"/> is a reference type, it must implement <see cref="ICloneable"/>.
        /// </summary>
        public object Clone()
        {
            CellRow<T>[] newCells = new CellRow<T>[gridSize.y];

            for (int y = 0; y < gridSize.y; y++)
            {
                newCells[y] = GetCellRow(y);
            }

            return Clone(newCells);
        }

        protected abstract Array2D<T> Clone(CellRow<T>[] cells);

        #endregion
    }
}
