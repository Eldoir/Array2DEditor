using UnityEngine;
using Array2DEditor;

public class ExampleScript : MonoBehaviour
{

    [SerializeField]
    private Array2DBool array2DBool = null;

    [SerializeField]
    private GameObject prefabToInstantiate = null;


	void Start()
    {
        if (array2DBool == null || prefabToInstantiate == null)
        {
            Debug.LogError("Fill in all the fields in order to start this example.");
            return;
        }

        bool[,] cells = array2DBool.GetCells();

        var piece = new GameObject("Piece");

        for(var x = 0; x < array2DBool.GridSize.x; x++)
        {
            for(var y = 0; y < array2DBool.GridSize.y; y++)
            {
                if (cells[x, y])
                {
                    var prefabGO = Instantiate(prefabToInstantiate, new Vector3(x, 0, y), Quaternion.identity, piece.transform);
                    prefabGO.name = "(" + x + ", " + y + ")";
                }
            }
        }
	}
}