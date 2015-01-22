using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Tabular.Types;

namespace Tabular.GridColumnExtensions
{
    public class IdentityGridColumn: GridColumn
    {
        public IdentityGridColumn()
        {
            Init();
        }

        public IdentityGridColumn(IdentityColumnType textEditType)
        {
            Init();
            Populate(textEditType);
        }

        private void Init()
        {
            ColumnEdit = new RepositoryItemTextEdit();
        }

        private void Populate(IdentityColumnType textEditType)
        {
            Name = string.Format("GridColumn_{0}", textEditType.Name);
            FieldName = textEditType.Name;
            Caption = textEditType.Caption;
            Visible = false;
            
            var repEditor = ColumnEdit as RepositoryItemTextEdit;
            if (repEditor == null) return;

            repEditor.ReadOnly = true;
        }
    }
}
