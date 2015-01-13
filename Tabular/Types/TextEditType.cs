namespace Tabular.Types
{
    public class TextEditType: AbstractColumnDefinitionType
    {
        public string DefaultValue { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
    }
}
