using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public class QikScriptTopicSection : CodeTopicSection, IQikScriptTopicSection
    {
        internal QikScriptTopicSection(string folder, string title, string extension, string syntax) : base(folder, title, extension, syntax)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.QikScript);
        }

        internal QikScriptTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description, syntax)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.QikScript);
        }
    }
}
