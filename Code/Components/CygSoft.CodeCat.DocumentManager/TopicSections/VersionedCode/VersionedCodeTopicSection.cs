using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.TopicSections.VersionedCode
{
    public class VersionedCodeTopicSection : TopicSection, IVersionedCodeTopicSection
    {
        internal VersionedCodeTopicSection(string folder, string title)
            : base(new DocumentPathGenerator(folder, "vercod"), title, null)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.FileAttachments);
        }

        internal VersionedCodeTopicSection(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "vercod", id), title, description, ordinal)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.FileAttachments);
        }
    }
}
