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


        public Vector2Int GridSize => gridSize;


        protected abstract CellRow<T> GetCellRow(int idx);


        public T[,] GetCells()
        {
            T[,] ret = new T[gridSize.y, gridSize.x];

            for (var i = 0; i < gridSize.y; i++)
            {
                for (var j = 0; j < gridSize.x; j++)
                {
                    ret[i, j] = GetCellRow(i)[j];
                }
            }

            return ret;
        }
    }

    [System.Serializable]
    public class CellRow<T>
    {
        [SerializeField]
        private T[] row = new T[Consts.defaultGridSize];


        public T this[int i] => row[i];
    }
}