using CygSoft.CodeCat.Infrastructure.Qik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.Qik.FileManager
{
    internal class ScriptFile : BaseFile, IQikScriptFile
    {
        public ScriptFile(string filePath)
            : base(filePath)
        {
        }
    }
}
