using System.Collections.Generic;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using Tabular.Workloads;
using Termine.Promises;
using Termine.Promises.Generics;

namespace Tabular.Promises
{
    public class ChangeValuesPromise: Promise<SelectedCellsWorkload>
    {
        public override void Init()
        {
            WithValidator("hasSelectedCells", HasSelectedCells);
            WithValidator("hasDataTable", HasDataTable);
            WithExecutor("changeValue", ChangeValue);
        }

        private void HasDataTable(SelectedCellsWorkload selectedCellsWorkload)
        {
            var rowCount = selectedCellsWorkload.DataTable.Rows.Count;

            if (rowCount < 1) Abort(new DataSourceHasNoRows());
        }

        public ChangeValuesPromise Prep(IEnumerable<GridCell> gridCells, DataTable dataTable, string newValue )
        {
            Workload.GridCells = gridCells;
            Workload.NewValue = newValue;
            Workload.DataTable = dataTable;

            return this;
        }

        private void ChangeValue(SelectedCellsWorkload selectedCellsWorkload)
        {
            foreach (var gridCell in selectedCellsWorkload.GridCells)
            {
                Trace(new GenericEventMessage(0, gridCell.Column.FieldName));

                if (gridCell.RowHandle < 0) continue;

                var getRow = selectedCellsWorkload.DataTable.Rows[gridCell.RowHandle];

                if (gridCell.Column.ColumnType == typeof(string))
                    getRow[gridCell.Column.FieldName] = selectedCellsWorkload.NewValue;
            }
        }

        private void HasSelectedCells(SelectedCellsWorkload selectedCellsWorkload)
        {
            if (selectedCellsWorkload.GridCells.Equals(default(IEnumerable<GridCell>))) Abort(new SelectedCellsDoNotExist());
        }

        public class SelectedCellsDoNotExist : GenericEventMessage
        {
            public SelectedCellsDoNotExist()
            {
                EventId = 1;
                EventPublicMessage = "No selected cells were passed to the promise.";
                IsPublicMessage = true;
            }
        }

        public class DataSourceHasNoRows : GenericEventMessage
        {
            public DataSourceHasNoRows()
            {
                EventId = 2;
                EventPublicMessage = "The datasource provided to the promise does not have any rows.";
            }
        }
    }
}