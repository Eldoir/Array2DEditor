using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DAudioClip : Array2D<AudioClip>
    {
        [SerializeField]
        CellRowAudioClip[] cells = new CellRowAudioClip[Consts.defaultGridSize];

        protected override CellRow<AudioClip> GetCellRow(int idx)
        {
            return cells[idx];
        }

        /// <inheritdoc cref="Array2D{T}.Clone"/>
        protected override Array2D<AudioClip> Clone(CellRow<AudioClip>[] cells)
        {
            return new Array2DAudioClip() { cells = cells.Select(r => new CellRowAudioClip(r)).ToArray() };
        }
    }

    [System.Serializable]
    public class CellRowAudioClip : CellRow<AudioClip>
    {
        /// <inheritdoc/>
        [UsedImplicitly]
        public CellRowAudioClip() { }

        /// <inheritdoc/>
        public CellRowAudioClip(CellRow<AudioClip> row)
            : base(row) { }
    }
}