using System;
using System.Data;
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
    }
}
