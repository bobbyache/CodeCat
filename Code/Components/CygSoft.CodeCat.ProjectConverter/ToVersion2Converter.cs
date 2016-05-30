using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.ProjectConverter
{
    public class ToVersion2Converter
    {
        public string OldProjectFileExtension { get { return "xml"; } }
        public string OldProjectFilePath { get; private set; }
        public string OldProjectFolder { get; private set; }
        public string OldCodeFolder { get; private set; }
        public string OldCodeIndexFolder { get; private set; }
        public string OldCodeIndexFilePath { get; private set; }

        public string NewProjectFileExtension { get { return "codecat"; } }
        public string NewProjectFilePath { get; private set; }
        public string NewProjectFolder { get; private set; }
        public string NewCodeFolder { get; private set; }
        public string NewCodeIndexFolder { get; private set; }
        public string NewCodeIndexFilePath { get; private set; }

        public string TempProjectFolder { get; private set; }
        public string TempCodeFolder { get; private set; }
        public string TempProjectFilePath { get; private set; }
        public string TempCodeIndexFilePath { get; private set; }
        
        public string BackupProjectFolder { get; private set; }
        public string BackupCodeFolder { get; private set; }
        public string BackupProjectFilePath { get; private set; }
        public string BackupCodeIndexFilePath { get; private set; }

        public string FromVersion { get; private set; }
        public string ToVersion { get; private set; }

        public ToVersion2Converter(string filePath)
        {
            this.FromVersion = "1";
            this.ToVersion = "2";
            this.OldProjectFilePath = filePath;

            this.OldProjectFolder = Path.GetDirectoryName(filePath);
            this.OldCodeFolder = this.OldProjectFolder;
            this.OldCodeIndexFolder = this.OldProjectFolder;
            this.OldCodeIndexFilePath = filePath;

            this.NewProjectFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + "." + this.NewProjectFileExtension);
            this.NewProjectFolder = Path.GetDirectoryName(filePath);
            this.NewCodeFolder = Path.Combine(this.OldProjectFolder, "code");
            this.NewCodeIndexFolder = Path.Combine(this.OldProjectFolder, "code");
            this.NewCodeIndexFilePath = Path.Combine(this.NewCodeIndexFolder, "_code.xml");

            this.TempProjectFolder = Path.Combine(OldProjectFolder, "_temp");
            this.TempCodeFolder = Path.Combine(this.TempProjectFolder, "code");
            this.TempCodeIndexFilePath = Path.Combine(this.TempCodeFolder, "_code.xml");
            this.TempProjectFilePath = Path.Combine(this.TempProjectFolder, Path.GetFileNameWithoutExtension(filePath) + "." + this.NewProjectFileExtension);

            this.BackupProjectFolder = Path.Combine(OldProjectFolder, "_backup");
            this.BackupCodeFolder = Path.Combine(this.BackupProjectFolder, "code");
            this.BackupCodeIndexFilePath = Path.Combine(this.BackupProjectFolder, Path.GetFileName(filePath));
            this.BackupProjectFilePath = Path.Combine(this.BackupProjectFolder, Path.GetFileName(filePath));
        }

        public void Convert()
        {
            if (this.FromVersion == "1" && this.ToVersion == "2")
                ConvertVersion1ToVersion2(this.OldProjectFilePath);
            else
                throw new NotSupportedException("The project conversion is not supported.");
        }

        private void ConvertVersion1ToVersion2(string filePath)
        {
            BackupCurrent(filePath);
            ConvertToTempDirectory(filePath);
            DeleteCurrentFiles(filePath);
            RebuildFromTempDirectory(filePath);
        }

        private void RebuildFromTempDirectory(string filePath)
        {
            File.Move(this.TempProjectFilePath, this.NewProjectFilePath);
            Directory.Move(this.TempCodeFolder, this.NewCodeFolder);
            Directory.Delete(this.TempProjectFolder, true);
        }

        private void DeleteCurrentFiles(string filePath)
        {
            foreach (string file in Directory.EnumerateFiles(this.OldProjectFolder, "*.xml"))
                File.Delete(file);
        }

        private void BackupCurrent(string filePath)
        {
            DirectoryInfo backupDirectoryInfo = Directory.CreateDirectory(this.BackupProjectFolder);

            foreach (string file in Directory.EnumerateFiles(this.OldProjectFolder, "*.xml"))
                File.Copy(file, Path.Combine(this.BackupProjectFolder, Path.GetFileName(file)));
        }

        private void ConvertToTempDirectory(string filePath)
        {
            XDocument oldDocument = XDocument.Load(filePath);
            ToVersion2 converter = new ToVersion2();

            XDocument projectDocument = converter.CreateProjectDocument();
            XDocument codeIndexDocument = converter.CreateCodeIndexDocument(filePath);

            Directory.CreateDirectory(this.TempProjectFolder);
            Directory.CreateDirectory(this.TempCodeFolder);

            projectDocument.Save(this.TempProjectFilePath);
            codeIndexDocument.Save(this.TempCodeIndexFilePath);

            foreach (string file in Directory.EnumerateFiles(this.OldProjectFolder, "*.xml"))
            {
                string fileName = Path.GetFileName(file);
                string projFileName = Path.GetFileName(this.OldProjectFilePath);

                if (fileName != projFileName)
                {
                    XDocument newCodeDocument = converter.ConvertCodeFile(file, oldDocument);
                    newCodeDocument.Save(Path.Combine(this.TempCodeFolder, fileName));
                }

            }
        }
    }
}
