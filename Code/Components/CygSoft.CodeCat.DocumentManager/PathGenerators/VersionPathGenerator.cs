using CygSoft.CodeCat.DocumentManager.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.PathGenerators
{
    public class VersionPathGenerator : BaseFilePathGenerator
    {
        private string sourceFilePath;
        public DateTime TimeStamp { get; private set; }

        public VersionPathGenerator(string sourceFilePath, DateTime timeStamp)
        {
            this.sourceFilePath = sourceFilePath;
            this.TimeStamp = timeStamp;
        }

        public override string FileExtension
        {
            get { return CleanExtension(Path.GetExtension(this.sourceFilePath)); }
        }

        public override string FileName
        {
            get { return CreateFileName(sourceFilePath, this.TimeStamp); }
        }

        public override string FilePath
        {
            get { return CreateFilePath(); }
        }

        public override string Id
        {
            get { return CreateId(sourceFilePath, this.TimeStamp); }
        }

        private string CreateFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(sourceFilePath), CreateFileName(sourceFilePath, this.TimeStamp));
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

        private string CleanExtension(string extension)
        {
            if (extension.Length > 0)
            {
                if (extension.StartsWith("."))
                    return extension.Substring(1);
            }

            return extension;
        }
    }
}
