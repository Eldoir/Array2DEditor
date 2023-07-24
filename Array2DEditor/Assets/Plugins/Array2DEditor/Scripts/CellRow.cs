using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    [Serializable]
    public class CellRow<T> : IEnumerable<T>
    {
        [SerializeField]
        private T[] row = new T[Consts.defaultGridSize];

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="row"></param>
        public CellRow(CellRow<T> row)
            : this(row.ToArray()) { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        public CellRow(T[] row)
        {
            if (typeof(T).IsValueType)
                this.row = (T[])row.Clone();
            else if (typeof(ICloneable).IsAssignableFrom(typeof(T)))
                this.row = row.Select(c => (T)(c as ICloneable).Clone()).ToArray();
            else
                throw new NotSupportedException($"{typeof(T)} must implement {typeof(ICloneable)}.");
        }

        /// <summary>
        /// Used implicitly by <see cref="Array2D{T}"/> drawers.
        /// </summary>
        [UsedImplicitly]
        protected CellRow() { }

        public T this[int i]
        {
            get => row[i];
            set => row[i] = value;
        }

        #region IEnumerable

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < row.Length; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        #endregion
    }
}
