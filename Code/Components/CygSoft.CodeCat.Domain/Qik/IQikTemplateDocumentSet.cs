using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using System;

namespace CygSoft.CodeCat.Domain.Qik
{
    public interface IQikTemplateDocumentSet : IKeywordTarget, IFile, ITitledEntity
    {
        event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedLeft;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedRight;

        ICompiler Compiler { get; }

        string Text { get; set; }
        string Syntax { get; set; }
        bool TemplateExists(string id);

        // don't like this... should not be returning "all" documents.
        // should be treating a collection of Template and a collection of Script.
        ICodeTopicSection[] TopicSections { get; }

        ICodeTopicSection[] TemplateSections { get; }

        ICodeTopicSection ScriptSection { get; }
        ICodeTopicSection GetTemplateSection(string id);

        ICodeTopicSection AddTemplateSection(string syntax);
        void RemoveTemplateSection(string id);

        void MoveTemplateSectionLeft(string id);
        void MoveTemplateSectionRight(string id);
    }
}
