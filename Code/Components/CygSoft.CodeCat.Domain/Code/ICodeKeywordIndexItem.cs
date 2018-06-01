using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Domain.Code
{
    // This interface is necessary in order to expose a specialized index item to the GUI layer.
    // One cannot directly depend on XmlKeywordIndexItem in the GUI layer because the type is not
    // referenced by GUI layer directly.

    // It's ok to do this because types will naturally become more specialized when moving towards
    // the top layer.
    public interface ICodeKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
