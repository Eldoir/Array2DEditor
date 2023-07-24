using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class CellRow<T> : IEnumerable<T>
    {
        [SerializeField]
        private T[] row = new T[Consts.defaultGridSize];

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
