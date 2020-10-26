/*
 * Arthur Cousseau, 2020
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
            T[,] ret = new T[gridSize.x, gridSize.y];

            for (var x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    ret[x, y] = GetCellRow(x)[y];
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