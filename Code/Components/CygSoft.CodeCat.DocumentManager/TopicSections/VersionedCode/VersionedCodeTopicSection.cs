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
    public class VersionedCodeTopicSection : CodeTopicSection, IVersionedCodeTopicSection
    {
        internal VersionedCodeTopicSection(string folder, string title, string extension, string syntax) : base(folder, title, extension, syntax)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.VersionedCode);
        }

        internal VersionedCodeTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description, syntax)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.VersionedCode);
        }

    }
}
