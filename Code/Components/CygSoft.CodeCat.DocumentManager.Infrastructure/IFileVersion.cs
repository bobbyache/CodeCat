using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFileVersion : IFile
    {
        DateTime TimeTaken { get; }
        string Description { get; }
        string Title { get; }
    }
}
