using System.IO;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseFilePathGenerator
    {
        public abstract string Id { get; }
        public abstract string FileName { get; }
        public abstract string FilePath { get; }
        public abstract string FileExtension { get; }

        public virtual string FolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.FilePath))
                    return string.Empty;

                return Path.GetDirectoryName(this.FilePath);
            }
        }

        public virtual bool FileExists { get { return File.Exists(this.FilePath); } }

        protected string CleanExtension(string ext)
        {
            if (ext.Length > 0)
            {
                if (ext.StartsWith("."))
                    return ext.Substring(1);
            }

            return ext;
        }
    }
}
