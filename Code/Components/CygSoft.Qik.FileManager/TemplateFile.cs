using CygSoft.CodeCat.Infrastructure.Qik;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.Qik.FileManager
{
    internal class TemplateFile : BaseFile, ITemplateFile
    {
        public string Syntax { get; set; }
        public string Title { get; set; }


        public TemplateFile(string filePath, string title, string syntax)
            : base(filePath)
        {
            this.Syntax = syntax;
            this.Title = title;
        }
    }
}
