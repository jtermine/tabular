using DevExpress.XtraPrinting.Native;

namespace Tabular.Types
{
    public class TextEditType: AbstractColumnDefinitionType
    {
        public TextEditType()
        {
            ValueType = ColumnValueType.TextEdit;
        }

        public string DefaultValue { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
    }
}
