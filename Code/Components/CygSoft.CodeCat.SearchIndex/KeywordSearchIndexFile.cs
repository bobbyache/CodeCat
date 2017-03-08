using System.IO;

namespace CygSoft.CodeCat.Search.KeywordIndex
{

    internal class KeywordSearchIndexFile : IKeywordSearchIndexFile
    {
        public KeywordSearchIndexFile(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; private set; }

        public bool CorrectFormat {  get { return true; } }

        public bool CorrectVersion { get { return true; } }

        public bool FileExists { get { return File.Exists(FilePath); } }

        public string Open()
        {
            if (FileExists)
            {
                using (var file = new FileStream(FilePath, FileMode.Open))
                using (var reader = new StreamReader(file))
                {
                    return reader.ReadToEnd();
                }
            }
            return null;
        }

        public void Save(string fileText)
        {
            Save(fileText, FilePath);
        }

        public void SaveAs(string fileText, string filePath)
        {
            Save(fileText, FilePath);
            FilePath = filePath;
        }

        public void Save(string fileText, string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Truncate, FileAccess.Write))
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(fileText);
                streamWriter.Flush();
            }
        }
    }
}
