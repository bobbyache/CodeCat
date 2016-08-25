using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Code.Base;
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

        private Project project = new Project();

        public AppFacade(string syntaxFilePath)
        {
            this.syntaxRepository = new SyntaxRepository(syntaxFilePath);
            this.codeLibrary = new CodeLibrary();
            this.qikLibrary = new QikLibrary();
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
            get { return this.codeLibrary.Loaded && this.qikLibrary.Loaded; }
        }

        public int GetIndexCount()
        {
            return this.codeLibrary.IndexCount + this.qikLibrary.IndexCount;
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
        }

        public void Create(string filePath, int currentVersion)
        {
            project.Create(filePath, currentVersion);
            this.codeLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
            this.qikLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
        }



        public IKeywordIndexItem[] GetLastOpenedIds()
        {
            List<IKeywordIndexItem> lastOpenedItems = new List<IKeywordIndexItem>();
            lastOpenedItems.AddRange(this.codeLibrary.GetLastOpenedIds());
            lastOpenedItems.AddRange(this.qikLibrary.GetLastOpenedIds());
            return lastOpenedItems.ToArray();
        }

        public void SetLastOpenedIds(IKeywordIndexItem[] keywordIndexItems)
        {
            // filter by type rather than just send in a bunch of ids.
            this.codeLibrary.SetLastOpenedIds(keywordIndexItems.OfType<CodeKeywordIndexItem>().ToArray());
            this.qikLibrary.SetLastOpenedIds(keywordIndexItems.OfType<QikKeywordIndexItem>().ToArray());
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

            this.codeLibrary.AddKeywords(codeIndeces, delimitedKeywordList);
            this.qikLibrary.AddKeywords(qikIndeces, delimitedKeywordList);
        }

        public bool RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces)
        {
            CodeKeywordIndexItem[] codeIndeces = indeces.OfType<CodeKeywordIndexItem>().ToArray();
            QikKeywordIndexItem[] qikIndeces = indeces.OfType<QikKeywordIndexItem>().ToArray();
            IKeywordIndexItem[] invalidCodeIndeces;
            IKeywordIndexItem[] invalidQikIndeces;

            bool canRemoveCodeKeywords = codeLibrary.CanRemoveKeywords(codeIndeces, keywords, out invalidCodeIndeces);
            bool canRemoveQikKeywords = this.qikLibrary.CanRemoveKeywords(qikIndeces, keywords, out invalidQikIndeces);
            List<IKeywordIndexItem> allInvalidIndeces = new List<IKeywordIndexItem>();
            allInvalidIndeces.AddRange(invalidCodeIndeces);
            allInvalidIndeces.AddRange(invalidQikIndeces);
            invalidIndeces = allInvalidIndeces.ToArray();

            if (canRemoveCodeKeywords && canRemoveQikKeywords)
            {
                this.codeLibrary.RemoveKeywords(codeIndeces, keywords);
                this.qikLibrary.RemoveKeywords(qikIndeces, keywords);
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

        public CodeFile CreateCodeSnippet(string syntax)
        {
            CodeFile codeFile = this.codeLibrary.CreateTarget(new CodeKeywordIndexItem("New Snippet", syntax, string.Empty)) as CodeFile;
            return codeFile;
        }

        public QikFile CreateQikTemplate(string syntax)
        {
            QikFile qikFile = this.qikLibrary.CreateTarget(new QikKeywordIndexItem("New Qik Template", syntax, string.Empty)) as QikFile;
            return qikFile;
        }

        public CodeFile OpenCodeFileTarget(IKeywordIndexItem keywordIndexItem)
        {
            return this.codeLibrary.OpenTarget(keywordIndexItem) as CodeFile;
        }

        public QikFile OpenQikSnippet(IKeywordIndexItem keywordIndexItem)
        {
            return this.qikLibrary.OpenTarget(keywordIndexItem) as QikFile;
        }
    }
}
