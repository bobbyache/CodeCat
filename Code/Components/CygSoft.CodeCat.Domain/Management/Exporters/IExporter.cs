using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Management.Exporters
{
    internal interface IExporter
    {
        string Generate();
    }
}
