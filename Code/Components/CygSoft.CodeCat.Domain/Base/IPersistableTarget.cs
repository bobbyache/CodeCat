using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Base
{
    public interface IPersistableTarget : IFile, IKeywordTarget
    {
        int HitCount { get; }
        string Title { get; set; }
    }
}
