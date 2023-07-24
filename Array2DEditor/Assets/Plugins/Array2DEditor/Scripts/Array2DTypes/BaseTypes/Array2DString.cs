using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DString : Array2D<string>
    {
        [SerializeField]
        CellRowString[] cells = new CellRowString[Consts.defaultGridSize];

        protected override CellRow<string> GetCellRow(int idx)
        {
            return cells[idx];
        }

        /// <inheritdoc cref="Array2D{T}.Clone"/>
        protected override Array2D<string> Clone(CellRow<string>[] cells)
        {
            return new Array2DString() { cells = cells.Select(r => new CellRowString(r)).ToArray() };
        }
    }

    [System.Serializable]
    public class CellRowString : CellRow<string>
    {
        /// <inheritdoc/>
        [UsedImplicitly]
        public CellRowString() { }

        /// <inheritdoc/>
        public CellRowString(CellRow<string> row)
            : base(row) { }
    }
}
