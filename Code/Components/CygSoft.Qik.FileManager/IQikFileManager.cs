using System;
namespace CygSoft.Qik.FileManager
{
    interface IQikFileManager
    {
        CygSoft.CodeCat.Infrastructure.Qik.ITemplateFile AddTemplate(string title, string syntax);
        void Create(string parentFolder, string id);
        void Delete();
        string Folder { get; }
        CygSoft.CodeCat.Infrastructure.Qik.ITemplateFile GetTemplate(string fileName);
        string Id { get; }
        bool IndexFileExists { get; }
        string IndexFilePath { get; }
        string IndexFileTitle { get; }
        void Load(string parentFolder, string id);
        string ParentFolder { get; }
        void RemoveTemplate(string fileName);
        void Save();
        CygSoft.CodeCat.Infrastructure.Qik.IQikScriptFile ScriptFile { get; }
        string ScriptFileName { get; }
        string ScriptFilePath { get; }
        bool TemplateExists(string fileName);
        CygSoft.CodeCat.Infrastructure.Qik.ITemplateFile[] Templates { get; }
    }
}
