using Array2DEditor;
using UnityEngine;

[System.Serializable]
public class TestArray
{
    [SerializeField]
    private Vector2Int gridSize = Vector2Int.one;

    [SerializeField]
    private Vector2Int cellSize = new Vector2Int(32, 16);
    
    [SerializeField]
    private CellRowInt[] cells = new CellRowInt[Consts.defaultGridSize];
}
