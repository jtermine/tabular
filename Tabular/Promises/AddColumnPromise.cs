using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tabular.Types;
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

        public AddColumnPromise Prep(List<IColumnDefinitionType> list, object table = null)
        {
            if (table as DataTable == null) return this;

            Workload.DataTable = table as DataTable;
            Workload.List = list;
            
            return this;
        }

        private void DataTableNotNull(DataTableWorkload dataTableWorkload)
        {
            if (Workload.DataTable == null) Abort(new GenericEventMessage(1, "The datatable is null blocking the promise from executing."));
        }

        private void AddColumn(DataTableWorkload dataTableWorkload)
        {
            var count = Workload.DataTable.Columns.Count + 1;

            foreach (
                var column in
                    Workload.List.Select(f => f as TextEditType)
                        .Where(f => f != null)
                        .Where(g => !Workload.DataTable.Columns.Contains(g.Name))
                        .Select(
                            textEditType =>
                                new DataColumn(textEditType.Name)
                                {
                                    Caption = textEditType.Caption,
                                    MaxLength = textEditType.MaxLength,
                                    DataType = typeof (string)
                                }))
            {
                Workload.DataTable.Columns.Add(column);
            }

            var row = Workload.DataTable.NewRow();

            foreach (DataColumn dataColumn in Workload.DataTable.Columns)
            {
                row[dataColumn.ColumnName] = string.Format("column_{0}_{1}", count, dataColumn.Caption);
            }

            Workload.DataTable.Rows.Add(row);
        }
    }
}
