using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DAudioClip))]
    public class Array2DAudioClipDrawer : Array2DObjectDrawer<AudioClip>
    {
        protected override Vector2Int GetDefaultCellSizeValue() => new Vector2Int(96, 16);
    }
}
