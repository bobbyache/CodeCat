using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.ProjectConverter
{
    public class Converter
    {
        public void Convert(string filePath, string oldVersion, string newVersion)
        {
            if (oldVersion == "1" && newVersion == "2")
                ConvertVersion1ToVersion2(filePath);
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
            string projectDirectory = Path.GetDirectoryName(filePath);
            DirectoryInfo tempDirectoryInfo = Directory.CreateDirectory(TempDirectory(projectDirectory));

            File.Move(Path.Combine(tempDirectoryInfo.FullName, Path.GetFileName(filePath)), Path.Combine(projectDirectory, Path.GetFileName(filePath)));
            File.Move(Path.Combine(tempDirectoryInfo.FullName, "_code.xml"), Path.Combine(projectDirectory, "_code.xml"));
            Directory.Move(Path.Combine(tempDirectoryInfo.FullName, "_code"), Path.Combine(projectDirectory, "_code"));
            Directory.Delete(tempDirectoryInfo.FullName, true);
        }

        private string BackupDirectory(string projectDirectory)
        {
            string directory = Path.Combine(projectDirectory, "_backup");
            return directory;
        }

        private string TempDirectory(string projectDirectory)
        {
            string directory = Path.Combine(projectDirectory, "_temp");
            return directory;
        }

        private void DeleteCurrentFiles(string filePath)
        {
            string projectDirectory = Path.GetDirectoryName(filePath);

            foreach (string file in Directory.EnumerateFiles(projectDirectory, "*.xml"))
                File.Delete(file);
        }

        private void BackupCurrent(string filePath)
        {
            string projectDirectory = Path.GetDirectoryName(filePath);
            DirectoryInfo backupDirectoryInfo = Directory.CreateDirectory(BackupDirectory(projectDirectory));

            foreach (string file in Directory.EnumerateFiles(projectDirectory, "*.xml"))
                File.Copy(file, Path.Combine(backupDirectoryInfo.FullName, Path.GetFileName(file)));
        }

        private void ConvertToTempDirectory(string filePath)
        {
            XDocument oldDocument = XDocument.Load(filePath);
            ToVersion2 converter = new ToVersion2();

            XDocument projectDocument = converter.CreateProjectDocument();
            XDocument codeIndexDocument = converter.CreateCodeIndexDocument(filePath);

            string projectDirectory = Path.GetDirectoryName(filePath);
            DirectoryInfo tempDirectoryInfo = Directory.CreateDirectory(TempDirectory(projectDirectory));

            projectDocument.Save(Path.Combine(tempDirectoryInfo.FullName, Path.GetFileName(filePath)));
            codeIndexDocument.Save(Path.Combine(tempDirectoryInfo.FullName, "_code.xml"));

            DirectoryInfo tempCodeDirectory = Directory.CreateDirectory(Path.Combine(tempDirectoryInfo.FullName, "_code"));

            foreach (string file in Directory.EnumerateFiles(projectDirectory, "*.xml"))
            {
                string fileName = Path.GetFileName(file);
                if (fileName != Path.GetFileName(filePath))
                {
                    XDocument newCodeDocument = converter.ConvertCodeFile(file, oldDocument);
                    newCodeDocument.Save(Path.Combine(tempCodeDirectory.FullName, fileName));
                }

            }
        }
    }
}
