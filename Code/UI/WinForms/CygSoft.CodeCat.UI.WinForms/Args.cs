using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms
{
    public class TopicIndexEventArgs : EventArgs
    {
        public IKeywordIndexItem Item { get; private set; }

        public TopicIndexEventArgs(IKeywordIndexItem item)
        {
            this.Item = item;
        }
    }

    public class SearchDelimitedKeywordEventArgs : EventArgs
    {
        public int MatchedItemCount { get; private set; }
        public string Keywords { get; private set; }

        public SearchDelimitedKeywordEventArgs(string keywords, int matchedItemCount)
        {
            this.Keywords = keywords;
            this.MatchedItemCount = matchedItemCount;
        }
    }

    public class SearchKeywordsModifiedEventArgs : EventArgs
    {
        public IKeywordIndexItem[] Items { get; private set; }
        public string Keywords { get; private set; }

        public SearchKeywordsModifiedEventArgs(string keywords, IKeywordIndexItem[] items)
        {
            this.Items = items;
            this.Keywords = keywords;
        }
    }

    public class WorkItemSavedFileEventArgs : EventArgs
    {
        public IWorkItemForm ContentDocument { get; private set; }
        public IWorkItem WorkItem { get; private set; }

        public WorkItemSavedFileEventArgs(IWorkItem WorkItem, IWorkItemForm contentDocument)
        {
            this.ContentDocument = contentDocument;
            this.WorkItem = WorkItem;
        }
    }
}
