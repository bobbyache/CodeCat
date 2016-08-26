using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseFilePathGenerator
    {
        public abstract string Id { get; }
        public abstract string FileName { get; }
        public abstract string FilePath { get; }
        public abstract string FileExtension { get; }
    }
}
