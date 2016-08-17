using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocManager
    {
        // document group folder
        string Id { get; }
        string Folder { get; }
        string ParentFolder { get; }

        // index file properties
        bool IndexFileExists { get; }
        string IndexFilePath { get; }
        string IndexFileTitle { get; }

        bool Loaded { get; }

        // document manager methods (acts on the document group as a whole)
        void Create(string parentFolder, string id);
        void Open(string parentFolder, string id);
        void Delete();

        void SaveIndex();
        void SaveDocumentFiles();

        // files within the document group
        IDocumentFile[] DocumentFiles { get; }
        void CreateDocumentFile(IDocumentFile documentFile);
        IDocumentFile GetDocumentFile(string fileName);
        void DeleteDocumentFile(string fileName);
       
       // THESE ARE MORE SPECIALIZED METHODS !!!
 
        //CygSoft.CodeCat.Infrastructure.Qik.IQikScriptFile ScriptFile { get; }
        //string ScriptFileName { get; }
        //string ScriptFilePath { get; }
        //bool TemplateExists(string fileName);
        
    }
}
