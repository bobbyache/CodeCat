using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Plugins.ManualXess
{
    public class DataMatrix
    {

        private DataTable dataTable = new DataTable();
        private Variables variables = new Variables();

        public DataTable Table { get { return this.dataTable; } }

        private string blueprintText;
        public string BlueprintText
        {
            get { return this.blueprintText; }
            set
            {
                this.blueprintText = value;
                UpdateVariables();
            }
        }

        public string[] Placeholders
        {
            get { return variables.Placeholders; }
        }

        public string[] Columns
        {
            get { return variables.Columns; }
        }

        public Variable[] Variables
        {
            get { return variables.VariableList; }
        }

        private void RefreshColumns(string[] columns)
        {
            foreach (string column in columns)
            {
                if (!dataTable.Columns.Contains(column))
                    dataTable.Columns.Add(new DataColumn(column, typeof(System.String)));
            }
        }

        public void RemoveOrphanedColumns()
        {
            List<DataColumn> orphanedColumns = new List<DataColumn>();

            foreach (DataColumn column in dataTable.Columns)
            {

                if (!variables.Exists(column.ColumnName))
                    orphanedColumns.Add(column);
            }

            foreach (DataColumn column in orphanedColumns)
                dataTable.Columns.Remove(column);
        }

        private void UpdateVariables()
        {
            this.variables.Update(this.BlueprintText);
            RefreshColumns(this.Columns);
        }

        public void Empty()
        {
            this.dataTable.Clear();
            this.dataTable.Columns.Clear();
        }
    }
}
