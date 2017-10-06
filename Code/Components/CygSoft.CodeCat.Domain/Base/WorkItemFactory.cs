using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Base
{
    internal static class WorkItemFactory
    {
        public static IWorkItem Create(IKeywordIndexItem indexItem, string folderPath)
        {
            IWorkItem workItem = null;

            if (indexItem is CodeKeywordIndexItem)
                workItem = new CodeFile(indexItem as CodeKeywordIndexItem, folderPath);
            else if (indexItem is QikTemplateKeywordIndexItem)
                workItem = new QikTemplateDocumentSet(indexItem as QikTemplateKeywordIndexItem, folderPath);
            else if (indexItem is TopicKeywordIndexItem)
                workItem = new TopicDocument(indexItem as TopicKeywordIndexItem, folderPath);

            return workItem;
        }
    }
}
