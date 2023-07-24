using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DInt : Array2D<int>
    {
        [SerializeField]
        CellRowInt[] cells = new CellRowInt[Consts.defaultGridSize];

        protected override CellRow<int> GetCellRow(int idx)
        {
            return cells[idx];
        }

        /// <inheritdoc cref="Array2D{T}.Clone"/>
        protected override Array2D<int> Clone(CellRow<int>[] cells)
        {
            return new Array2DInt() { cells = cells.Select(r => new CellRowInt(r)).ToArray() };
        }
    }

    [System.Serializable]
    public class CellRowInt : CellRow<int>
    {
        /// <inheritdoc/>
        [UsedImplicitly]
        public CellRowInt() { }

        /// <inheritdoc/>
        public CellRowInt(CellRow<int> row)
            : base(row) { }
    }
}
