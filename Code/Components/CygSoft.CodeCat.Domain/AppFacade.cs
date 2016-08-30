using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Code.Base;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
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
        private SyntaxRepository syntaxRepository;
        private CodeLibrary codeLibrary;
        private QikLibrary qikLibrary;
        private CodeGroupLibrary codeGroupLibrary;

        private Project project = new Project();

        public AppFacade(string syntaxFilePath)
        {
            this.syntaxRepository = new SyntaxRepository(syntaxFilePath);
            this.codeLibrary = new CodeLibrary();
            this.qikLibrary = new QikLibrary();
            this.codeGroupLibrary = new CodeGroupLibrary();
        }

        public string CodeSyntaxFolderPath 
        {
            get { return this.syntaxRepository.FilePath; }
        }

        public string ProjectFileExtension
        {
            get { return this.project.ProjectFileExtension; }
        }

        public string ProjectFileTitle
        {
            get { return this.project.FileTitle; }
        }

        public bool Loaded
        {
            get { return this.codeLibrary.Loaded && this.qikLibrary.Loaded && this.codeGroupLibrary.Loaded; }
        }

        public int GetIndexCount()
        {
            return this.codeLibrary.IndexCount + this.qikLibrary.IndexCount + this.codeGroupLibrary.IndexCount;
        }

        public void OpenContextFolder()
        {
            this.codeLibrary.OpenProjectFolder();
        }

        public void Open(string filePath, int currentVersion)
        {
            project.Open(filePath, currentVersion);
            this.codeLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
            this.qikLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
            this.codeGroupLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
        }

        public void Create(string filePath, int currentVersion)
        {
            project.Create(filePath, currentVersion);
            this.codeLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
            this.qikLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
            this.codeGroupLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
        }

        public IKeywordIndexItem[] GetLastOpenedIds()
        {
            List<IKeywordIndexItem> lastOpenedItems = new List<IKeywordIndexItem>();
            lastOpenedItems.AddRange(this.codeLibrary.GetLastOpenedIds());
            lastOpenedItems.AddRange(this.qikLibrary.GetLastOpenedIds());
            lastOpenedItems.AddRange(this.codeGroupLibrary.GetLastOpenedIds());
            return lastOpenedItems.ToArray();
        }

        public void SetLastOpenedIds(IKeywordIndexItem[] keywordIndexItems)
        {
            // filter by type rather than just send in a bunch of ids.
            this.codeLibrary.SetLastOpenedIds(keywordIndexItems.OfType<CodeKeywordIndexItem>().ToArray());
            this.qikLibrary.SetLastOpenedIds(keywordIndexItems.OfType<QikKeywordIndexItem>().ToArray());
            this.codeGroupLibrary.SetLastOpenedIds(keywordIndexItems.OfType<CodeGroupKeywordIndexItem>().ToArray());
        }

        public string[] GetSyntaxes()
        {
            return this.syntaxRepository.Languages;
        }

        public SyntaxFile[] GetSyntaxFileInfo()
        {
            return this.syntaxRepository.SyntaxFiles;
        }

        public string GetSyntaxFile(string syntax)
        {
            return this.syntaxRepository[syntax].FilePath;
        }


        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords)
        {
            List<IKeywordIndexItem> keywordIndexItems = new List<IKeywordIndexItem>();

            keywordIndexItems.AddRange(this.codeLibrary.FindIndeces(commaDelimitedKeywords));
            keywordIndexItems.AddRange(this.qikLibrary.FindIndeces(commaDelimitedKeywords));
            keywordIndexItems.AddRange(this.codeGroupLibrary.FindIndeces(commaDelimitedKeywords));

            return keywordIndexItems.ToArray();
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
            CodeKeywordIndexItem[] codeIndeces = indeces.OfType<CodeKeywordIndexItem>().ToArray();
            QikKeywordIndexItem[] qikIndeces = indeces.OfType<QikKeywordIndexItem>().ToArray();
            CodeGroupKeywordIndexItem[] codeGroupIndeces = indeces.OfType<CodeGroupKeywordIndexItem>().ToArray();

            this.codeLibrary.AddKeywords(codeIndeces, delimitedKeywordList);
            this.qikLibrary.AddKeywords(qikIndeces, delimitedKeywordList);
            this.codeGroupLibrary.AddKeywords(codeGroupIndeces, delimitedKeywordList);
        }

        public bool RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces)
        {
            CodeKeywordIndexItem[] codeIndeces = indeces.OfType<CodeKeywordIndexItem>().ToArray();
            QikKeywordIndexItem[] qikIndeces = indeces.OfType<QikKeywordIndexItem>().ToArray();
            CodeGroupKeywordIndexItem[] codeGroupIndeces = indeces.OfType<CodeGroupKeywordIndexItem>().ToArray();

            IKeywordIndexItem[] invalidCodeIndeces;
            IKeywordIndexItem[] invalidQikIndeces;
            IKeywordIndexItem[] invalidCodeGroupIndeces;

            bool canRemoveCodeKeywords = codeLibrary.CanRemoveKeywords(codeIndeces, keywords, out invalidCodeIndeces);
            bool canRemoveQikKeywords = this.qikLibrary.CanRemoveKeywords(qikIndeces, keywords, out invalidQikIndeces);
            bool canRemoveCodeGroupKeywords = this.codeGroupLibrary.CanRemoveKeywords(codeGroupIndeces, keywords, out invalidCodeGroupIndeces);

            List<IKeywordIndexItem> allInvalidIndeces = new List<IKeywordIndexItem>();
            allInvalidIndeces.AddRange(invalidCodeIndeces);
            allInvalidIndeces.AddRange(invalidQikIndeces);
            allInvalidIndeces.AddRange(invalidCodeGroupIndeces);

            invalidIndeces = allInvalidIndeces.ToArray();

            if (canRemoveCodeKeywords && canRemoveQikKeywords && canRemoveCodeGroupKeywords)
            {
                this.codeLibrary.RemoveKeywords(codeIndeces, keywords);
                this.qikLibrary.RemoveKeywords(qikIndeces, keywords);
                this.codeGroupLibrary.RemoveKeywords(codeGroupIndeces, keywords);
                return true;
            }
            else
            {
                return false;
            }
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

        public CodeFile OpenCodeFileTarget(IKeywordIndexItem keywordIndexItem)
        {
            return this.codeLibrary.OpenTarget(keywordIndexItem) as CodeFile;
        }

        public CodeFile CreateCodeSnippet(string syntax)
        {
            CodeFile codeFile = this.codeLibrary.CreateTarget(new CodeKeywordIndexItem("New Snippet", syntax, 
                string.Empty)) as CodeFile;
            return codeFile;
        }

        public IQikDocumentGroup CreateQikDocumentGroup(string syntax)
        {
            IQikDocumentGroup qikFile = this.qikLibrary.CreateTarget(new QikKeywordIndexItem("New Qik Template", 
                syntax, string.Empty)) as QikDocumentGroup;
            return qikFile;
        }

        public IQikDocumentGroup OpenQikDocumentGroup(IKeywordIndexItem keywordIndexItem)
        {
            return this.qikLibrary.OpenTarget(keywordIndexItem) as IQikDocumentGroup;
        }

        public ICodeGroupDocumentGroup CreateCodeGroupDocumentGroup(string syntax)
        {
            ICodeGroupDocumentGroup codeGroup = this.codeGroupLibrary.CreateTarget(new QikKeywordIndexItem("New Group Snippet", 
                syntax, string.Empty)) as ICodeGroupDocumentGroup;
            return codeGroup;
        }

        public ICodeGroupDocumentGroup OpenCodeGroupDocumentGroup(IKeywordIndexItem keywordIndexItem)
        {
            return this.codeGroupLibrary.OpenTarget(keywordIndexItem) as ICodeGroupDocumentGroup;
        }
    }
}
