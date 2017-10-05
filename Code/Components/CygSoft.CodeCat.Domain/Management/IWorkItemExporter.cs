using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Management
{
    public interface IWorkItemExporter
    {
        IndexExportImportData[] GetExportData();
    }
}
