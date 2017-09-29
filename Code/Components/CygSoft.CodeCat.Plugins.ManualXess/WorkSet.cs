using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Plugins.ManualXess
{
    public class WorkSet
    {
        private DataMatrix dataMatrix = new DataMatrix();
        private Generator generator = new Generator();

        public string BlueprintText
        {
            get { return this.dataMatrix.BlueprintText; }
            set { this.dataMatrix.BlueprintText = value; }
        }
        public DataTable Table { get { return this.dataMatrix.Table; } }

        public string[] Placeholders
        {
            get { return this.dataMatrix.Placeholders; }
        }

        public string[] Columns
        {
            get { return this.dataMatrix.Columns; }
        }

        public string GenerateText()
        {
            return generator.GenerateText(this.dataMatrix.Table, this.dataMatrix.BlueprintText, this.dataMatrix.Variables);
        }

        public void RemoveOrphanedColumns()
        {
            dataMatrix.RemoveOrphanedColumns();
        }
    }
}
