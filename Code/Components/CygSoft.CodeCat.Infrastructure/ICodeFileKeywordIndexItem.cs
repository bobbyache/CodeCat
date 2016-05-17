using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface ICodeFileKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
