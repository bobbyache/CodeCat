using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure.Qik
{
    public interface ITemplateFile
    {
        string Syntax { get; set; }
        string Title { get; set; }
        string FilePath { get; }
        string Text { get; set; }
        string FileName { get; }

        bool Exists { get; }
    }
}
