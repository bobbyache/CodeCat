using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Qik
{
    public interface IQikTemplateKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
