using System;
using System.Data;
using System.Linq;
using DevExpress.XtraGrid.Columns;
using Tabular.Promises;
using Tabular.TabModels;
using Tabular.Types;
using Tabular.Workloads;
using Termine.Promises.Interfaces;

namespace Tabular
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        StudentTabModel _studentTabModel;

        public Form1()
        {
            InitializeComponent();

            var dataSet = new DataSet("columns");
            dataSet.Tables.Add("tableColumns");

            bindingSource1.DataSource = dataSet.Tables["tableColumns"];
            gridControl1.DataSource = bindingSource1.DataSource;

            
            _studentTabModel = new StudentTabModel
            {
                new TextEditType
                {
                    Caption = "First Name",
                    DefaultValue = "",
                    MaxLength = 1000,
                    MinLength = 0,
                    Name = "FirstName"
                },
                new TextEditType
                {
                    Caption = "Last Name",
                    DefaultValue = "",
                    MaxLength = 1000,
                    MinLength = 0,
                    Name = "LastName"
                },
                new TextEditType
                {
                    Caption = "Address Line 1",
                    DefaultValue = "",
                    MaxLength = 1000,
                    MinLength = 0,
                    Name = "AddressLine1"
                },
                new TextEditType
                {
                    Caption = "Address Line 2",
                    DefaultValue = "",
                    MaxLength = 1000,
                    MinLength = 0,
                    Name = "AddressLine2"
                }
            };
        }

        private void barAddColumn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var addColumnPromise = new AddColumnPromise();

            var dataSet = bindingSource1.DataSource as DataSet;

            if (dataSet != null && dataSet.Tables.Contains("tableColumns"))
                addColumnPromise.Prep(_studentTabModel, dataSet.Tables[0]);

            addColumnPromise
                .Prep(_studentTabModel, bindingSource1.DataSource)
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