namespace Tabular.Types
{
    public class IntSpinEditType: AbstractColumnDefinitionType
    {
        public int DefaultValue { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}
