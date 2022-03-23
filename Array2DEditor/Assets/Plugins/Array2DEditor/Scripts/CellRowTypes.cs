using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class RowBool : Row<bool> { }
    
    [System.Serializable]
    public class RowFloat : Row<float> { }
    
    [System.Serializable]
    public class RowInt : Row<int> { }
    
    [System.Serializable]
    public class RowString : Row<string> { }
    
    [System.Serializable]
    public class RowSprite : Row<Sprite> { }
    
    [System.Serializable]
    public class RowAudioClip : Row<AudioClip> { }
}
