using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class CellRowBool : CellRow<bool> { }
    
    [System.Serializable]
    public class CellRowFloat : CellRow<float> { }
    
    [System.Serializable]
    public class CellRowInt : CellRow<int> { }
    
    [System.Serializable]
    public class CellRowString : CellRow<string> { }
    
    [System.Serializable]
    public class CellRowSprite : CellRow<Sprite> { }
    
    [System.Serializable]
    public class CellRowAudioClip : CellRow<AudioClip> { }
}
