using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using Tabular.Types;
using Termine.Promises.Generics;

namespace Tabular.Workloads
{
    public class DataTableWorkload: GenericWorkload
    {
        public DataTable DataTable { get; set; }
        public ConcurrentQueue<Action> FormActions { get; set; }
        public List<IColumnDefinitionType> List { get; set; }
    }
}
