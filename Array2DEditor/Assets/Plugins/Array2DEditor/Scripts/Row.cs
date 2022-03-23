using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Row<T>
    {
        [SerializeField]
        private T[] row = null;

        public T this[int i]
        {
            get
            {
                if (row == null)
                    SetSize(Consts.DefaultGridWidth);

                return row[i];
            }
            set
            {
                if (row == null)
                    SetSize(Consts.DefaultGridWidth);

                row[i] = value;
            }
        }

        public void SetSize(int newSize)
        {
            row = new T[newSize];
        }
    }
}
