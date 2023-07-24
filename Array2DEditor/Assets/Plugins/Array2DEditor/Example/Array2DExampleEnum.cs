using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DExampleEnum : Array2D<ExampleEnum>
    {
        [SerializeField]
        CellRowExampleEnum[] cells = new CellRowExampleEnum[Consts.defaultGridSize];

        protected override CellRow<ExampleEnum> GetCellRow(int idx)
        {
            return cells[idx];
        }

        /// <inheritdoc cref="Array2D{T}.Clone"/>
        protected override Array2D<ExampleEnum> Clone(CellRow<ExampleEnum>[] cells)
        {
            return new Array2DExampleEnum() { cells = cells.Select(r => new CellRowExampleEnum(r)).ToArray() };
        }
    }

    [System.Serializable]
    public class CellRowExampleEnum : CellRow<ExampleEnum>
    {
        /// <inheritdoc/>
        [UsedImplicitly]
        public CellRowExampleEnum() { }

        /// <inheritdoc/>
        public CellRowExampleEnum(CellRow<ExampleEnum> row)
            : base(row) { }
    }
}
