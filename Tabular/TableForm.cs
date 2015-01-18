using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Tabular.GridColumnExtensions;
using Tabular.Promises;
using Tabular.TabModels;

namespace Tabular
{
    public partial class TableForm : DevExpress.XtraEditors.XtraForm
    {
        readonly StudentTabModel _studentTabModel = new StudentTabModel();

        public TableForm()
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

        private void barOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var fileDialog = openFileDialog1.ShowDialog(this);

            switch (fileDialog)
            {
                    case DialogResult.OK:
                    new OpenTabularFilePromise()
                        .WithFileName(openFileDialog1.FileName)
                        .RunAsync();
                    break;
            }
        }

        private void gridView1_ShownEditor(object sender, System.EventArgs e)
        {
            var view = sender as GridView;

            if (view == null || !(view.ActiveEditor is LookUpEdit)) return;

            ((LookUpEdit)view.ActiveEditor).ShowPopup();

            //throw new DevExpress.Utils.HideException();
        }
    }
}