using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseVersionFile : BaseFile, IFileVersion
    {
        public DateTime TimeTaken { get; private set; }
        public string Description { get; private set; }

        public string Title
        {
            get
            {
                string desc = string.Format("Version Snapshot: {0} {1}", this.TimeTaken.ToLongDateString(),
                    this.TimeTaken.ToLongTimeString());
                return desc;
            }
        }

        //public override string Id { get { return versionFileNamer.Id; } }
        //public override string FileName { get { return versionFileNamer.FileName; } }
        //public override string FilePath { get { return versionFileNamer.FilePath; } }

        public BaseVersionFile(VersionPathGenerator versionPathGenerator, string description)
            : base(versionPathGenerator)
        {
            this.TimeTaken = versionPathGenerator.TimeStamp;
            this.Description = description;
        }
    }
}
