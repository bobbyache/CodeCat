using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFile
    {
        string Id { get; }
        string FilePath { get; }
        string Text { get; set; }
        string FileName { get; }
        string Folder { get; }
    }
}
