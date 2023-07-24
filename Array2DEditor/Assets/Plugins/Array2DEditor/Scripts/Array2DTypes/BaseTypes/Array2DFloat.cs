using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DFloat : Array2D<float>
    {
        [SerializeField]
        CellRowFloat[] cells = new CellRowFloat[Consts.defaultGridSize];

        protected override CellRow<float> GetCellRow(int idx)
        {
            return cells[idx];
        }

        /// <inheritdoc cref="Array2D{T}.Clone"/>
        protected override Array2D<float> Clone(CellRow<float>[] cells)
        {
            return new Array2DFloat() { cells = cells.Select(r => new CellRowFloat(r)).ToArray() };
        }
    }

    [System.Serializable]
    public class CellRowFloat : CellRow<float>
    {
        /// <inheritdoc/>
        [UsedImplicitly]
        public CellRowFloat() { }

        /// <inheritdoc/>
        public CellRowFloat(CellRow<float> row)
            : base(row) { }
    }
}
