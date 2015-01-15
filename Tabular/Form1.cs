using System.Data;
using Tabular.GridColumnExtensions;
using Tabular.Promises;
using Tabular.TabModels;

namespace Tabular
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        readonly StudentTabModel _studentTabModel = new StudentTabModel();

        public Form1()
        {
            InitializeComponent();

            var dataSet = new DataSet("columns");
            dataSet.Tables.Add("tableColumns");

            bindingSource1.DataSource = dataSet.Tables["tableColumns"];
            gridControl1.DataSource = bindingSource1.DataSource;

            InitializeColumns();
        }

        private void InitializeColumns()
        {
            foreach (var column in _studentTabModel)
            {
                gridView1.Columns.Add(column.AsGridColumn());
            }
        }

        private void barAddColumn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var addColumnPromise = new AddColumnPromise();

            var dataSet = bindingSource1.DataSource as DataSet;

            if (dataSet != null && dataSet.Tables.Contains("tableColumns"))
                addColumnPromise.Prep(_studentTabModel, dataSet.Tables[0]);

            addColumnPromise
                .Prep(_studentTabModel, bindingSource1.DataSource)
                .RunAsync();
        }

        
    }
}