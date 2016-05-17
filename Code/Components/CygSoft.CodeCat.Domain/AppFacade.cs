using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Code.Base;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain
{
    public class AppFacade
    {
        private CodeLibrary codeLibrary = new CodeLibrary();
        private Project project = new Project();

        public AppFacade()
        {
        }

        public string CodeSyntaxFolderPath 
        {
            get { return this.codeLibrary.SyntaxFolderPath; }
            set { this.codeLibrary.SyntaxFolderPath = value; }
        }

        public bool Loaded
        {
            get { return this.codeLibrary.Loaded; }
        }

        public string GetContextFilePath()
        {
            return this.codeLibrary.FilePath;
        }

        public string GetContextFileTitle()
        {
            return this.codeLibrary.FileTitle;
        }

        public string GetContextFolderPath()
        {
            return this.codeLibrary.FolderPath;
        }

        public int GetIndexCount()
        {
            return this.codeLibrary.IndexCount;
        }

        public void OpenContextFolder()
        {
            this.codeLibrary.OpenProjectFolder();
        }

        public void Open(string filePath, int currentVersion)
        {
            project.Open(filePath, currentVersion);
            this.codeLibrary.Open(project.GetIndexPath(), 
                project.GetLibraryFolder(), currentVersion);
        }

        public void Create(string filePath, int currentVersion)
        {
            project.Create(filePath, currentVersion);
            this.codeLibrary.Create(project.GetIndexPath(), 
                project.GetLibraryFolder(), currentVersion);
        }

        public string[] GetSyntaxes()
        {
            return this.codeLibrary.GetLanguages();
        }

        public string GetSyntaxFile(string language)
        {
            return this.codeLibrary.GetSyntaxFile(language);
        }

        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords)
        {
            return this.codeLibrary.FindIndeces(commaDelimitedKeywords);
        }

        public string[] AllKeywords(IKeywordIndexItem[] indeces)
        {
            return this.codeLibrary.AllKeywords(indeces);
        }

        public string CopyAllKeywords(IKeywordIndexItem[] indeces)
        {
            return this.codeLibrary.CopyAllKeywords(indeces);
        }

        public void AddKeywords(IKeywordIndexItem[] indeces, string delimitedKeywordList)
        {
            this.codeLibrary.AddKeywords(indeces, delimitedKeywordList);
        }

        public bool RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces)
        {
            return this.codeLibrary.RemoveKeywords(indeces, keywords, out invalidIndeces);
        }

        public string KeywordArrayToDelimitedText(string[] keywords)
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases(keywords);

            return keyPhrases.DelimitKeyPhraseList();
        }

        public string RemoveKeywordsFromDelimitedText(string keywords, string removeKeywords)
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases(keywords);
            keyPhrases.RemovePhrases(removeKeywords);

            return keyPhrases.DelimitKeyPhraseList();
        }

        public string AddKeywordsToDelimitedText(string keywords, string addKeywords)
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases(keywords);
            keyPhrases.AddKeyPhrases(addKeywords);

            return keyPhrases.DelimitKeyPhraseList();
        }

        public CodeFile CreateCodeSnippet(string syntax)
        {
            CodeFile codeFile = this.codeLibrary.CreateFile() as CodeFile;
            codeFile.Title = "New Snippet";
            codeFile.Syntax = syntax;
            return codeFile;
        }

        public CodeFile OpenCodeSnippet(IKeywordIndexItem index)
        {
            return this.codeLibrary.OpenFile(index) as CodeFile;
        }

        public void CloseCodeSnippet(string snippetId, bool save = false)
        {
            this.codeLibrary.CloseFile(snippetId, save);
        }

        public void RemoveCodeSnippet(string snippetId)
        {
            this.codeLibrary.RemoveFile(snippetId);
        }

        public string[] FindOrphanedFiles()
        {
            return this.codeLibrary.FindOrphanedFiles();
        }

        public IKeywordIndexItem[] FindMissingFiles()
        {
            return this.codeLibrary.FindMissingFiles();
        }

        public CodeFile CreateCodeSnippetFromOrphan(string id)
        {
            return this.codeLibrary.CreateFileFromOrphan(id) as CodeFile;
        }
    }
}
