using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DAudioClip : Array2D<AudioClip>
    {
        [SerializeField]
        RowAudioClip[] cells = new RowAudioClip[Consts.DefaultGridWidth];

        protected override void ResetGrid(int newWidth, int newHeight)
        {
            cells = new RowAudioClip[newHeight];
            
            for(int i = 0; i < newHeight; i++)
            {
                cells[i] = new RowAudioClip();
            }
        }

        protected override Row<AudioClip> GetRow(int idx)
        {
            return cells[idx];
        }
    }
}