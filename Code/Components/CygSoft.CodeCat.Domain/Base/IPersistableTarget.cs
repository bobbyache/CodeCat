using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace CygSoft.CodeCat.Domain.Base
{
    public interface IPersistableTarget : IFile, IKeywordTarget
    {
        int HitCount { get; }
        string Title { get; set; }
    }
}
