using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Infrastructure
{
    public class SyntaxRepository
    {
        private Dictionary<string, SyntaxFile> syntaxFiles;

        public string FilePath { get; private set; }

        public SyntaxRepository(string filePath)
        {
            this.FilePath = filePath;
            Refresh();
        }

        public SyntaxFile[] SyntaxFiles
        {
            get
            {
                if (this.syntaxFiles == null)
                    return new SyntaxFile[0];

                return syntaxFiles.Select(r => r.Value).ToArray();
            }
        }

        public string[] Languages
        {
            get
            {
                if (this.syntaxFiles == null)
                    return new string[0];

                return syntaxFiles.Select(r => r.Value.Syntax.Trim().ToUpper()).ToArray();
            }
        }

        public string[] FileExtensions
        {
            get
            {
                if (this.syntaxFiles == null)
                    return new string[0];

                return syntaxFiles.Select(r => r.Value.Extension.Trim().ToUpper()).ToArray();
            }
        }

        public SyntaxFile this[string language]
        {
            get
            {
                string key = language.ToUpper();
                if (syntaxFiles.ContainsKey(key))
                {
                    return syntaxFiles[key];
                }
                return null;
            }
        }

        public void Refresh()
        {

            XElement xElement = XElement.Load(this.FilePath);
            syntaxFiles = (from m in xElement.Elements("SyntaxMapping")
                           select new SyntaxFile
                           (
                             (string)m.Attribute("Language"),
                             RootedFilePath((string)m.Attribute("FilePath")),
                             (string)m.Attribute("FileExtension")
                           )).ToDictionary(r => r.Syntax.Trim().ToUpper(), r => r);
        }

        private string RootedFilePath(string relativeOrRootedPath)
        {
            string rootedFilePath = null;

            if (!Path.IsPathRooted(relativeOrRootedPath))
            {
                string rootDirectory = Path.GetDirectoryName(this.FilePath);
                rootedFilePath = Path.Combine(rootDirectory, relativeOrRootedPath);
                return rootedFilePath;
            }
            else
            {
                rootedFilePath = relativeOrRootedPath;
            }
            return rootedFilePath;
        }
    }
}
