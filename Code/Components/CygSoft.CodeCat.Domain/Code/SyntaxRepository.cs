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
        private Dictionary<string, SyntaxMapping> syntaxMappings;

        public SyntaxRepository(string filePath)
        {
            this.filePath = filePath;
            Refresh();
        }

        public string[] Languages
        {
            get
            {
                if (this.syntaxMappings == null)
                    return new string[0];

                return syntaxMappings.Select(r => r.Value.Language.Trim().ToUpper()).ToArray();
            }
        }

        public string this[string language]
        {
            get
            {
                string key = language.ToUpper();
                if (syntaxMappings.ContainsKey(key))
                {
                    if (File.Exists(syntaxMappings[key].FilePath))
                        return syntaxMappings[key].FilePath;
                }
                return string.Empty;
            }
        }

        public void Refresh()
        {
            XElement xElement = XElement.Load(this.filePath);
            syntaxMappings = (from m in xElement.Elements("SyntaxMapping")
                                                  select new SyntaxMapping
                                                  (
                                                    (string)m.Attribute("Language"),
                                                    (string)m.Attribute("FilePath")
                                                  )).ToDictionary(r => r.Language.Trim().ToUpper(), r => r);
        }
    }
}
