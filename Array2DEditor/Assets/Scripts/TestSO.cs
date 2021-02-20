using UnityEngine;

namespace Array2DEditor
{
    [CreateAssetMenu(menuName = "TestSO", fileName = "TestSO")]
    public class TestSO : ScriptableObject
    {
        [SerializeField]
        private TestArrayInt floorplan;

        [SerializeField]
        private TestArrayInt doorPlacement;

        [SerializeField]
        private TestArrayInt decorNodes;
    }
}
