/*
 * Arthur Cousseau, 2019
 * https://www.linkedin.com/in/arthurcousseau/
 * Please share this if you enjoy it! :)
*/

using UnityEngine;

namespace Array2DEditor
{
    public abstract class Array2D<T> : ScriptableObject
    {

        [SerializeField]
        protected Vector2Int gridSize = Vector2Int.one * Consts.defaultGridSize;


        public Vector2Int GridSize { get { return gridSize; } }


        protected abstract CellRow<T> GetCellRow(int idx);


        public T[,] GetCells()
        {
            T[,] ret = new T[gridSize.y, gridSize.x];

            for (int i = 0; i < gridSize.y; i++)
            {
                for (int j = 0; j < gridSize.x; j++)
                {
                    ret[i, j] = GetCellRow(i).GetAt(j);
                }
            }

            return ret;
        }
    }

    [System.Serializable]
    public class CellRowBool : CellRow<bool> { }
    [System.Serializable]
    public class CellRowInt : CellRow<int> { }
    [System.Serializable]
    public class CellRowFloat : CellRow<float> { }
    [System.Serializable]
    public class CellRowString : CellRow<string> { }

    [System.Serializable]
    public class CellRow<T>
    {
        [SerializeField]
        private T[] row = new T[Consts.defaultGridSize];


        public T GetAt(int idx)
        {
            return row[idx];
        }
    }
}