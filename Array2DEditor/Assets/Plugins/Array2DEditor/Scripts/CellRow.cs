using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class CellRow<T>
    {
        [SerializeField]
        private T[] row = new T[Consts.defaultGridSize];

        public T this[int i]
        {
            get => row[i];
            set => row[i] = value;
        }
    }
}
