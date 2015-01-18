using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tabular.DataColumnExtensions;
using Tabular.Types;

namespace Tabular.TabModels
{
    public static class TabModelExtensions
    {
        public static IEnumerable<DataColumn> GetColumns(this IEnumerable<IColumnDefinitionType> tabModel)
        {
            var columnDefinitionTypes = tabModel as IColumnDefinitionType[] ?? tabModel.ToArray();

            var columns = columnDefinitionTypes.Select(f => f.AsDataColumn()).ToArray();

            return columns;
        }

        public static void SyncColumns(this DataTable dataTable, IEnumerable<IColumnDefinitionType> tabModel)
        {
            var columnDefinitionTypes = tabModel as IColumnDefinitionType[] ?? tabModel.ToArray();

            lock (Program.Lock)
            {
                foreach (var columnDefinitionType in columnDefinitionTypes.Where(columnDefinitionType => !dataTable.Columns.Contains(columnDefinitionType.Name)))
                {
                    dataTable.Columns.Add(columnDefinitionType.AsDataColumn());
                }
            }
        }
    }
}