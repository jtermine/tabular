using System;
using System.Collections.Generic;
using System.Data;
using Tabular.Types;
using Termine.Promises.Generics;

namespace Tabular.Workloads
{
    public class DataTableWorkload: GenericWorkload
    {
        public DataTableWorkload()
        {
            RequestId = Guid.NewGuid().ToString("N");
        }

        public DataTable DataTable { get; set; }
        public List<IColumnDefinitionType> List { get; set; }
    }
}
