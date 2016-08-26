using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Helpers.DocumentManager
{
    public class DocIndex
    {
        public string Id { get; private set; }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string Folder { get; private set; }

        public DocIndex(string id, string fileName, string filePath, string folderPath)
        {
            this.Id = id;
            this.FileName = fileName;
            this.FilePath = filePath;
            this.Folder = folderPath;
        }
    }

    public class DocumentFile
    {
        public string Id { get; private set; }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }

        public DocumentFile(string id, string fileName, string filePath)
        {
            this.Id = id;
            this.FileName = fileName;
            this.FilePath = filePath;
        }
    }

    public class DocFileSimulator
    {
        public DocIndex DocumentIndex { get; private set; }
        public DocumentFile DocumentFile1 { get; private set; }
        public DocumentFile DocumentFile2 { get; private set; }
        public DocumentFile DocumentFile3 { get; private set; }
        public DocumentFile DocumentFile4 { get; private set; }

        public DocFileSimulator()
        {
            this.DocumentIndex = new DocIndex("d33b59bd-54af-4f0b-967f-64084847b678", "document_index.xml", @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\document_index.xml", @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678");
            this.DocumentFile1 = new DocumentFile("f562810b-a1f7-4cf8-b370-dbaf87ff8759", "f562810b-a1f7-4cf8-b370-dbaf87ff8759.txt", @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\f562810b-a1f7-4cf8-b370-dbaf87ff8759.txt");
            this.DocumentFile2 = new DocumentFile("11334214-ca43-406b-9cae-f986c3c63332", "11334214-ca43-406b-9cae-f986c3c63332.txt", @"H:\ParentFolder\11334214-ca43-406b-9cae-f986c3c63332\11334214-ca43-406b-9cae-f986c3c63332.txt");
            this.DocumentFile3 = new DocumentFile("37c1dba5-9da3-4222-af34-43f98c674d82", "37c1dba5-9da3-4222-af34-43f98c674d82.txt", @"H:\ParentFolder\37c1dba5-9da3-4222-af34-43f98c674d82\37c1dba5-9da3-4222-af34-43f98c674d82.txt");
            this.DocumentFile4 = new DocumentFile("0f964889-a3ab-43d6-94a2-76e60cb9aae8", "0f964889-a3ab-43d6-94a2-76e60cb9aae8.txt", @"H:\ParentFolder\0f964889-a3ab-43d6-94a2-76e60cb9aae8\0f964889-a3ab-43d6-94a2-76e60cb9aae8.txt");
        }
    }
}
