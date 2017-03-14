using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class QikScriptDocument : CodeTopicSection, IQikScriptDocument
    {
        internal QikScriptDocument(string folder, string title, string extension, string syntax) : base(folder, title, extension, syntax)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.QikScript);
        }

        internal QikScriptDocument(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description, syntax)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.QikScript);
        }
    }
}
