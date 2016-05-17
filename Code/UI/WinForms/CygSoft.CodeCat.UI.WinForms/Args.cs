using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms
{
    
    public class OpenSnippetEventArgs : EventArgs
    {
        public IKeywordIndexItem Item { get; private set; }

        public OpenSnippetEventArgs(IKeywordIndexItem item)
        {
            this.Item = item;
        }
    }

    public class SelectSnippetEventArgs : EventArgs
    {
        public IKeywordIndexItem Item { get; private set; }

        public SelectSnippetEventArgs(IKeywordIndexItem item)
        {
            this.Item = item;
        }
    }

    public class SelectCodeFileEventArgs : EventArgs
    {
        public CodeFile Item { get; private set; }

        public SelectCodeFileEventArgs(CodeFile item)
        {
            this.Item = item;
        }
    }

    public class SearchDelimitedKeywordEventArgs : EventArgs
    {
        public string Keywords { get; private set; }

        public SearchDelimitedKeywordEventArgs(string keywords)
        {
            this.Keywords = keywords;
        }
    }

    public class SearchKeywordsModifiedEventArgs : SearchDelimitedKeywordEventArgs
    {
        public IKeywordIndexItem[] Items { get; private set; }

        public SearchKeywordsModifiedEventArgs(string keywords, IKeywordIndexItem[] items) : base(keywords)
        {
            this.Items = items;
        }
    }

    public class SaveCodeFileEventArgs : EventArgs
    {
        public SnippetForm Document { get; private set; }
        public CodeFile Item { get; private set; }

        public SaveCodeFileEventArgs(CodeFile item, SnippetForm document)
        {
            this.Document = document;
            this.Item = item;
        }
    }

    public class DeleteCodeFileEventArgs : EventArgs
    {
        public SnippetForm Document { get; private set; }
        public CodeFile Item { get; private set; }

        public DeleteCodeFileEventArgs(CodeFile item, SnippetForm document)
        {
            this.Document = document;
            this.Item = item;
        }
    }
}
