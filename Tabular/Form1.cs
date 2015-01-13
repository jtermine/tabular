using System;
using System.Data;
using System.Linq;
using DevExpress.XtraGrid.Columns;
using Tabular.Promises;
using Tabular.Workloads;
using Termine.Promises.Interfaces;

namespace Tabular
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();

            var dataSet = new DataSet("columns");
            dataSet.Tables.Add("tableColumns");

            bindingSource1.DataSource = dataSet.Tables["tableColumns"];
            gridControl1.DataSource = bindingSource1.DataSource;
        }

        private void barAddColumn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var addColumnPromise = new AddColumnPromise();

            var dataSet = bindingSource1.DataSource as DataSet;

            if (dataSet != null && dataSet.Tables.Contains("tableColumns"))
                addColumnPromise.Prep(dataSet.Tables[0]);

            addColumnPromise
                .Prep(bindingSource1.DataSource)
                .WithSuccessHandler("success",
                    promise => Invoke(new Action<IAmAPromise<DataTableWorkload>>(SuccessAddColumn), promise))
                .RunAsync();
        }

        private void SuccessAddColumn(IAmAPromise<DataTableWorkload> promise)
        {
            foreach (DataColumn column in promise.Workload.DataTable.Columns)
            {
                if (gridView1.Columns.Select(f => f.FieldName).Contains(column.ColumnName)) continue;

                var gridColumn = new GridColumn
                {
                    Name = string.Format("GridColumn_{0}", column.ColumnName),
                    FieldName = column.ColumnName,
                    Caption = column.Caption,
                    Visible = true
                };

                gridView1.Columns.Add(gridColumn);
            }
        }
    }
}