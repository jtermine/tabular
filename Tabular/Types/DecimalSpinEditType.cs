namespace Tabular.Types
{
    public class DecimalSpinEditType: AbstractColumnDefinitionType
    {
        public decimal DefaultValue { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
    }
}