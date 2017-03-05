using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;

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

        public BaseVersionFile(VersionPathGenerator versionPathGenerator, string description)
            : base(versionPathGenerator)
        {
            this.TimeTaken = versionPathGenerator.TimeStamp;
            this.Description = description;
        }
    }
}
