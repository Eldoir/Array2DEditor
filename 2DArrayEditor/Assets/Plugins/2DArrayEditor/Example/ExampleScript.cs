using UnityEngine;

public class ExampleScript : MonoBehaviour
{

    [SerializeField]
    private PieceData pieceData;

    [SerializeField]
    private GameObject prefabToInstantiate;


	void Start()
    {
        if (pieceData == null || prefabToInstantiate == null)
        {
            Debug.LogError("Fill in all the fields in order to start this example.");
            return;
        }

        bool[,] cells = pieceData.GetCells();

        GameObject piece = new GameObject("Piece");

        for(int i = 0; i < pieceData.GridSize; i++)
        {
            for(int j = 0; j < pieceData.GridSize; j++)
            {
                if (cells[i, j])
                {
                    GameObject prefabGO = Instantiate(prefabToInstantiate, new Vector3(i, 0, j), Quaternion.identity, piece.transform);
                    prefabGO.name = "(" + i + ", " + j + ")";
                }
            }
        }
	}
}