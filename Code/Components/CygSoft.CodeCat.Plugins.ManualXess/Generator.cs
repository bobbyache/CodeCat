using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Plugins.ManualXess
{
    public class Generator
    {
        public string GenerateText(DataTable dataTable, string blueprintText, Variable[] variables)
        {
            StringBuilder completeTxtBuilder = new StringBuilder();

            foreach (DataRow rw in dataTable.Rows)
            {
                string txt = blueprintText;
                foreach (DataColumn column in dataTable.Columns)
                {
                    Variable variable = variables.Where(v => v.Name == column.ColumnName).SingleOrDefault();
                    if (variable != null)
                        txt = txt.Replace(variable.Placeholder, Convert.ToString(rw[column]));
                }
                completeTxtBuilder.AppendLine(txt);
            }

            return completeTxtBuilder.ToString();
        }
    }
}
