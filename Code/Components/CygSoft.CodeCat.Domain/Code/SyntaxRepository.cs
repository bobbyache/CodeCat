using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Code
{
    internal class SyntaxRepository
    {
        private string filePath;
        private Dictionary<string, SyntaxFile> syntaxFiles;

        public SyntaxRepository(string filePath)
        {
            this.filePath = filePath;
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
                    if (File.Exists(syntaxFiles[key].FilePath))
                        return syntaxFiles[key];
                }
                return null;
            }
        }

        public void Refresh()
        {
            XElement xElement = XElement.Load(this.filePath);
            syntaxFiles = (from m in xElement.Elements("SyntaxMapping")
                           select new SyntaxFile
                           (
                             (string)m.Attribute("Language"),
                             (string)m.Attribute("FilePath"),
                             (string)m.Attribute("FileExtension")
                           )).ToDictionary(r => r.Syntax.Trim().ToUpper(), r => r);
        }
    }
}
