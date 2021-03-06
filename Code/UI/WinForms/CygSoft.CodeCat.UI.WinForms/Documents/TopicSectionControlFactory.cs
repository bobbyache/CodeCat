﻿using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain.TopicSections.SearchableEventDiary;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Controls.TopicSections;
using System;

namespace CygSoft.CodeCat.UI.WinForms.Documents
{
    public static class TopicSectionControlFactory
    {
        public static ITopicSectionBaseControl Create(ITopicSection topicSection, IWorkItem workItem, AppFacade application, EventHandler modifiedEventHandler)
        {
            ITopicSectionBaseControl topicSectionControl = null;

            if (topicSection is ICodeTopicSection)
            {
                if (topicSection is IQikScriptTopicSection)
                    topicSectionControl = new QikScriptCtrl(application, workItem as IQikTemplateDocumentSet);

                else if (topicSection.FileExtension == "tpl")
                    topicSectionControl = new QikTemplateCodeCtrl(application, workItem as IQikTemplateDocumentSet, topicSection as ICodeTopicSection);

                else if (topicSection is IVersionedCodeTopicSection)
                    topicSectionControl = new VersionedCodeTopicSectionControl(application, workItem as ITopicDocument, topicSection as IVersionedCodeTopicSection);

                else if (topicSection is ISearchableSnippetTopicSection)
                    topicSectionControl = new SearchableSnippetTopicSectionControl(application, workItem as ITopicDocument, topicSection as ISearchableSnippetTopicSection);

                else
                    topicSectionControl = new SimpleCodeTopicSectionControl(application, workItem as ITopicDocument, topicSection as ICodeTopicSection);
            }

            else if (topicSection is ICodeTemplateTopicSection)
                topicSectionControl = new CodeTemplateTopicSectionControl(application, workItem as ITopicDocument, topicSection as ICodeTemplateTopicSection);

            else if (topicSection is ISearchableEventTopicSection)
                topicSectionControl = new SearchableEventTopicSectionControl(application, workItem as ITopicDocument, topicSection as ISearchableEventTopicSection);

            else if (topicSection is IWebReferencesTopicSection)
                topicSectionControl = new WebReferencesTopicSectionControl(application, workItem as ITopicDocument, topicSection as IWebReferencesTopicSection);

            else if (topicSection is IFileAttachmentsTopicSection)
                topicSectionControl = new FileAttachmentsTopicSectionControl(application, workItem as ITopicDocument, topicSection as IFileAttachmentsTopicSection);

            else if (topicSection is IPdfViewerTopicSection)
                topicSectionControl = new PdfDocumentControl(application, workItem as ITopicDocument, topicSection as IPdfViewerTopicSection);

            else if (topicSection is ISingleImageTopicSection)
                topicSectionControl = new ImageControl(application, workItem as ITopicDocument, topicSection as ISingleImageTopicSection);
            //TODO: Must replace the current "ImageControl" (you've currently commented out the code for BaseImageTopicSectionControl and SingleImageTopicSectionControl).
            //topicSectionControl = new SingleImageTopicSectionControl(application, topicDocument as ITopicDocument, topicSection as ISingleImageTopicSection);

            else if (topicSection is IImagePagerTopicSection)
                topicSectionControl = new ImageSetControl(application, workItem as ITopicDocument, topicSection as IImagePagerTopicSection);

            else if (topicSection is IRichTextEditorTopicSection)
                topicSectionControl = new RichTextBoxTopicSectionControl(application, workItem as ITopicDocument, topicSection as IRichTextEditorTopicSection);

            else if (topicSection is ITasksTopicSection)
                topicSectionControl = new TaskListTopicSectionControl(application, workItem as ITopicDocument, topicSection as ITasksTopicSection);

            else
                return null;

            if (topicSectionControl != null)
                topicSectionControl.Modified += modifiedEventHandler;

            return topicSectionControl;
        }
    }
}
