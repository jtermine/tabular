using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using Tabular.TabModels;
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

        public AddColumnPromise Prep(List<IColumnDefinitionType> list, DataTable dataTable, ConcurrentQueue<DataRow> rows)
        {
            Workload.Rows = rows;
            Workload.DataTable = dataTable;
            Workload.List = list;
            
            return this;
        }

        private void DataTableNotNull(DataTableWorkload dataTableWorkload)
        {
            if (Workload.DataTable == null) Abort(new GenericEventMessage(1, "The datatable is null blocking the promise from executing."));
        }

        private void AddColumn(DataTableWorkload dataTableWorkload)
        {
            Workload.Rows.Enqueue(Workload.DataTable.GetDataRow(Workload.List));
        }
    }
}