using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DBool : Array2D<bool>
    {
        [SerializeField]
        CellRowBool[] cells = new CellRowBool[Consts.defaultGridSize];

        protected override CellRow<bool> GetCellRow(int idx)
        {
            return cells[idx];
        }

        /// <inheritdoc cref="Array2D{T}.Clone"/>
        protected override Array2D<bool> Clone(CellRow<bool>[] cells)
        {
            return new Array2DBool() { cells = cells.Select(r => new CellRowBool(r)).ToArray() };
        }
    }

    [System.Serializable]
    public class CellRowBool : CellRow<bool>
    {
        /// <inheritdoc/>
        [UsedImplicitly]
        public CellRowBool() { }

        /// <inheritdoc/>
        public CellRowBool(CellRow<bool> row)
            : base(row) { }
    }
}
