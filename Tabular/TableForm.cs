using System;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Tabular.Promises;
using Tabular.TabModels;
using Timer = System.Timers.Timer;

namespace Tabular
{
    public partial class TableForm : XtraForm
    {
        private readonly ConcurrentQueue<DataRow> _rows = new ConcurrentQueue<DataRow>();
        private readonly StudentTabModel _studentTabModel = new StudentTabModel();
        private readonly DataSet _dataSet = new DataSet("columns");
        private readonly FormTimer _timer = new FormTimer();

        public TableForm()
        {
            InitializeComponent();

            _dataSet.Tables.Add("tableColumns");

            var dataTable = _dataSet.Tables["tableColumns"];

            dataTable.SyncColumns(new StudentTabModel());

            bindingSource1.DataSource = dataTable;
            gridControl1.DataSource = bindingSource1.DataSource;

            _timer.Tick += timer1_Tick;

            //InitializeColumns();
        }

        

        private void barAddColumn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var addColumnPromise = new AddColumnPromise();

            addColumnPromise
                .Prep(_studentTabModel, _dataSet.Tables["tableColumns"], _rows)
                .RunAsync();
        }

        private void barOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var fileDialog = openFileDialog1.ShowDialog(this);

            //switch (fileDialog)
            //{
            //    case DialogResult.OK:
            //        new OpenTabularFilePromise()
            //            .WithFileName(openFileDialog1.FileName)
            //            .RunAsync();
            //        break;
            //}
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            var view = sender as GridView;

            if (view == null || !(view.ActiveEditor is LookUpEdit)) return;

            ((LookUpEdit) view.ActiveEditor).ShowPopup();

            //throw new DevExpress.Utils.HideException();
        }

        private void barChange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0) return;

            var selectedCells = gridView1.GetSelectedCells();

            new ChangeValuesPromise().Prep(selectedCells, (bindingSource1.DataSource as DataTable), "newValue").Run();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataRow result;

            while (_rows.TryDequeue(out result))
            {
                if (result == null) continue;
                _dataSet.Tables["tableColumns"].Rows.Add(result);
            }
        }
    }
}