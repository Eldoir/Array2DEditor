/*
 * Arthur Cousseau, 2017
 * https://www.linkedin.com/in/arthurcousseau/
 * Please share this if you enjoy it! :)
*/

using UnityEngine;

[CreateAssetMenu(fileName = "Piece_Data", menuName = "Eldoir/Piece")]
public class PieceData : ScriptableObject
{
    private const int defaultGridSize = 3;

    [SerializeField]
    [Range(1, 5)]
    private int gridSize = defaultGridSize;

    /// <summary>
    /// For now, the grid is a square, so the size is just an int.
    /// </summary>
    public int GridSize { get { return gridSize; } }

    [SerializeField]
    private CellRow[] cells = new CellRow[defaultGridSize];


    public bool[,] GetCells()
    {
        bool[,] ret = new bool[gridSize, gridSize];

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                ret[i, j] = cells[i].Row[j];
            }
        }

        return ret;
    }

    /// <summary>
    /// Just an example, you can remove this.
    /// </summary>
    public int GetCountActiveCells()
    {
        int count = 0;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (cells[i].Row[j]) count++;
            }
        }

        return count;
    }


    [System.Serializable]
    public class CellRow
    {
        [SerializeField]
        private bool[] row = new bool[defaultGridSize];

        public bool[] Row { get { return row; } }
    }
}
