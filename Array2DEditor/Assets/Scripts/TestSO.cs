using UnityEngine;

namespace Array2DEditor
{
    [CreateAssetMenu(menuName = "TestSO", fileName = "TestSO")]
    public class TestSO : ScriptableObject
    {
        [SerializeField]
        private TestArrayInt floorplan;

        [SerializeField]
        private TestArrayEnum doorPlacement;

        [SerializeField]
        private TestArrayBool decorNodes;
    }
}
