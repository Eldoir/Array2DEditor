using UnityEngine;

namespace Array2DEditor
{
    public class ExampleScript : MonoBehaviour
    {
    
        [SerializeField]
        private Array2DBool shape = null;
    
        [SerializeField]
        private GameObject prefabToInstantiate = null;
    
    
    	void Start()
        {
            if (shape == null || prefabToInstantiate == null)
            {
                Debug.LogError("Fill in all the fields in order to start this example.");
                return;
            }
    
            var cells = shape.GetCells();
    
            var piece = new GameObject("Piece");
    
            for (var y = 0; y < shape.GridSize.y; y++)
            {
                for (var x = 0; x < shape.GridSize.x; x++)
                {
                    if (cells[y, x])
                    {
                        var prefabGO = Instantiate(prefabToInstantiate, new Vector3(y, 0, x), Quaternion.identity, piece.transform);
                        prefabGO.name = $"({x}, {y})";
                    }
                }
            }
    	}
    }
}
