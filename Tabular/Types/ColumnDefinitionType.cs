using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabular.Types
{
    public class ColumnDefinitionType
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public ColumnValueType ValueType { get; set; }
    }
}
