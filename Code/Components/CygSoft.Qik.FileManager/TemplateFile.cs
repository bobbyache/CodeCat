using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.Qik.FileManager
{
    internal class TemplateFile : QikFile
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
