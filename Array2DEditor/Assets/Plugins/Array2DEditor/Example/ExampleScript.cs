using UnityEngine;
using Array2DEditor;

public class ExampleScript : MonoBehaviour
{

    [SerializeField]
    private Array2DBool array2DBool;

    [SerializeField]
    private GameObject prefabToInstantiate;


	void Start()
    {
        if (array2DBool == null || prefabToInstantiate == null)
        {
            Debug.LogError("Fill in all the fields in order to start this example.");
            return;
        }

        bool[,] cells = array2DBool.GetCells();

        GameObject piece = new GameObject("Piece");

        for(int i = 0; i < array2DBool.GridSize.x; i++)
        {
            for(int j = 0; j < array2DBool.GridSize.y; j++)
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