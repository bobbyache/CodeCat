using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.Qik.FileManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikFile : IPersistableTarget
    {
        public event EventHandler ContentSaved;
        public event EventHandler ContentClosed;
        public event EventHandler ContentDeleted;
        public event EventHandler ContentReverted;
        public event EventHandler BeforeContentSaved;

        private IKeywordIndexItem indexItem;
        private QikFileManager fileManager = null;

        public IKeywordIndexItem IndexItem
        {
            get { return this.indexItem; }
        }

        public string Text { get; set; }

        public string[] Templates { get { return this.fileManager.Templates; } }

        public QikFile(QikKeywordIndexItem indexItem, string folderPath)
        {
            this.indexItem = indexItem;
            fileManager = new QikFileManager(folderPath, indexItem.Id);
        }

        public string Id { get { return this.IndexItem.Id; } }
        public string FilePath { get { return fileManager.IndexFilePath; } }
        public string FileTitle { get { return this.fileManager.IndexFileTitle; } }
        public string FolderPath { get { return this.fileManager.ParentFolder; } }

        public bool FileExists { get { return this.fileManager.IndexFileExists; } }

        public string Title
        {
            get { return this.IndexItem.Title; }
            set { this.IndexItem.Title = value; }
        }

        public string Syntax
        {
            get 
            {
                if (this.indexItem != null)
                    return (this.indexItem as QikKeywordIndexItem).Syntax;
                return null;
            }
            set
            {
                QikKeywordIndexItem indexItem = this.indexItem as QikKeywordIndexItem;
                if (indexItem != null)
                    indexItem.Syntax = value;
            }
        }

        public string CommaDelimitedKeywords
        {
            get { return this.IndexItem.CommaDelimitedKeywords; }
            set
            {
                this.IndexItem.SetKeywords(value);
            }
        }

        public int HitCount
        {
            get;
            private set;
        }

        public bool TemplateExists(string fileName)
        {
            return Templates.Any(t => t == fileName);
        }

        public void Revert()
        {
            if (Open())
            {
                if (ContentReverted != null)
                    ContentReverted(this, new EventArgs());
            }
        }

        public bool Open()
        {
            if (this.FileExists)
            {
                fileManager.Load(this.FolderPath, this.Id);
                return true;
            }
            return false;
        }

        public void Close()
        {
            if (ContentClosed != null)
                ContentClosed(this, new EventArgs());
        }

        public void Save()
        {
            if (this.FileExists)
            {
                if (BeforeContentSaved != null)
                    BeforeContentSaved(this, new EventArgs());

                fileManager.Save();
            }
            else
                fileManager.Create(this.FolderPath, this.Id);
            
            if (this.ContentSaved != null)
                ContentSaved(this, new EventArgs());
        }

        public void Delete()
        {
            fileManager.Delete();
            if (ContentDeleted != null)
                ContentDeleted(this, new EventArgs());
        }

        public string AddTemplate()
        {
            return fileManager.AddTemplate("New Template", "");
        }

        public void RemoveTemplate(string fileName)
        {
            fileManager.RemoveTemplate(fileName);
        }


        public void SetTemplateSyntax(string fileName, string syntax)
        {
            this.fileManager.SetTemplateSyntax(fileName, syntax);
        }

        public string GetTemplateSyntax(string fileName)
        {
            return this.fileManager.GetTemplateSyntax(fileName);
        }

        public void SetTemplateText(string fileName, string text)
        {
            this.fileManager.SetTemplateText(fileName, text);
        }

        public string GetTemplateText(string fileName)
        {
            return this.fileManager.GetTemplateText(fileName);
        }

        public void SetTemplateTitle(string fileName, string title)
        {
            this.fileManager.SetTemplateTitle(fileName, title);
        }

        public string GetTemplateTitle(string fileName)
        {
            return fileManager.GetTemplateTitle(fileName);
        }

        private void IncrementHitCount()
        {
            this.HitCount++;
        }
    }
}
