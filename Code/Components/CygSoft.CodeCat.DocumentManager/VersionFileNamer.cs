using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    internal class VersionFileNamer
    {
        public string SourceFilePath { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public string Id
        {
            get { return CreateId(this.SourceFilePath, this.TimeStamp); }
        }

        public string FileName
        {
            get { return CreateFileName(this.SourceFilePath, this.TimeStamp); }
        }

        public string FilePath
        {
            get { return CreateFilePath(); }
        }

        public VersionFileNamer(string sourceFilePath, DateTime timeStamp)
        {
            this.SourceFilePath = sourceFilePath;
            this.TimeStamp = timeStamp;
        }

        private string CreateFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(this.SourceFilePath), CreateFileName(this.SourceFilePath, this.TimeStamp));
        }

        private string CreateId(string sourceFileName, DateTime timeStamp)
        {
            string fileTitle = Path.GetFileNameWithoutExtension(sourceFileName);
            return fileTitle + "_" + TodayPart(timeStamp) + "_" + TimePart(timeStamp);
        }

        private string CreateFileName(string sourceFileName, DateTime timeStamp)
        {
            string extension = Path.GetExtension(sourceFileName);
            string fileTitle = Path.GetFileNameWithoutExtension(sourceFileName);
            return fileTitle + "_" + TodayPart(timeStamp) + "_" + TimePart(timeStamp) + "." + extension;
        }

        private string TodayPart(DateTime theDate)
        {
            return Path.Combine(theDate.Year + "-" + EnsureTwoCharacters(theDate.Month) + "-" + EnsureTwoCharacters(theDate.Day));
        }

        private string TimePart(DateTime theDate)
        {
            string directory =
                EnsureTwoCharacters(theDate.Hour) + "-" +
                EnsureTwoCharacters(theDate.Minute) + "-" +
                EnsureTwoCharacters(theDate.Second) + "-" +
                theDate.Millisecond.ToString();

            return directory;
        }

        private string EnsureTwoCharacters(int dayOrMonth)
        {
            if (dayOrMonth < 10)
                return "0" + dayOrMonth.ToString();
            else
                return dayOrMonth.ToString();
        }
    }
}
