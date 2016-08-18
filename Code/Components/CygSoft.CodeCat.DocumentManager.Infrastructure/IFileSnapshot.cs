using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFileSnapshot : IFile
    {
        DateTime TimeTaken { get; }
        string Description { get; }
        string Title { get; }
    }
}
