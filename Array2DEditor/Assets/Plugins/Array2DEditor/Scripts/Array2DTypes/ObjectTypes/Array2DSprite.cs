using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DSprite : Array2D<Sprite>
    {
        [SerializeField]
        CellRowSprite[] cells = new CellRowSprite[Consts.defaultGridSize];

        protected override CellRow<Sprite> GetCellRow(int idx)
        {
            return cells[idx];
        }

        /// <inheritdoc cref="Array2D{T}.Clone"/>
        protected override Array2D<Sprite> Clone(CellRow<Sprite>[] cells)
        {
            return new Array2DSprite() { cells = cells.Select(r => new CellRowSprite(r)).ToArray() };
        }
    }

    [System.Serializable]
    public class CellRowSprite : CellRow<Sprite>
    {
        /// <inheritdoc/>
        [UsedImplicitly]
        public CellRowSprite() { }

        /// <inheritdoc/>
        public CellRowSprite(CellRow<Sprite> row)
            : base(row) { }
    }
}
