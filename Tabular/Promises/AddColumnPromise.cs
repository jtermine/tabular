using System;
using System.Data;
using Tabular.Workloads;
using Termine.Promises;
using Termine.Promises.Generics;
using Termine.Promises.NLogInstrumentation;

namespace Tabular.Promises
{
    public class AddColumnPromise: Promise<DataTableWorkload>
    {
        public override void Init()
        {
            this.WithNLogInstrumentation();
            this.WithValidator("dataTableNotNull", DataTableNotNull);
            this.WithExecutor("addColumn", AddColumn);
        }

        public AddColumnPromise Prep(object table = null)
        {
            if (table as DataTable == null) return this;

            Workload.DataTable = table as DataTable;
            return this;
        }

        private void DataTableNotNull(DataTableWorkload dataTableWorkload)
        {
            if (Workload.DataTable == null) Abort(new GenericEventMessage(1, "The datatable is null blocking the promise from executing."));
        }

        private void AddColumn(DataTableWorkload dataTableWorkload)
        {
            var count = Workload.DataTable.Columns.Count + 1;
            var columnFieldName = string.Format("Col_{0}", count);
            var columnCaption = string.Format("Column {0}", count);
            var column = new DataColumn(columnFieldName) {Caption = columnCaption};
            var dateColumn = new DataColumn(string.Format("Col_{0}_date", count), typeof (DateTime));
            
            Workload.DataTable.Columns.Add(column);
            Workload.DataTable.Columns.Add(dateColumn);
            
            var row = Workload.DataTable.NewRow();

            foreach (DataColumn dataColumn in Workload.DataTable.Columns)
            {
                if (dataColumn.ColumnName.Contains("date"))
                {
                    row[dataColumn.ColumnName] = DateTime.Now;
                    continue;
                }
                
                row[dataColumn.ColumnName] = string.Format("column_{0}_{1}", count, dataColumn.Caption);
            }

            Workload.DataTable.Rows.Add(row);
        }
    }
}
