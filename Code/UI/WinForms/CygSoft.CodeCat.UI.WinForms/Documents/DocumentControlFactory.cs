using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Controls.TopicSections;
using System;

namespace CygSoft.CodeCat.UI.WinForms.Documents
{
    public static class DocumentControlFactory
    {
        public static ITopicSectionBaseControl Create(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            if (topicSection is ICodeTopicSection)
            {
                if (topicSection is IQikScriptTopicSection)
                    return NewQikScriptControl(topicSection, topicDocument, application, modifiedEventHandler);
                else if (topicSection.FileExtension == "tpl")
                    return NewQikTemplateControl(topicSection, topicDocument, application, modifiedEventHandler);
                else if (topicSection is IVersionedCodeTopicSection)
                    return NewVersionedCodeControl(topicSection, topicDocument, application, modifiedEventHandler);
                else
                    return NewCodeControl(topicSection, topicDocument, application, modifiedEventHandler);
            }

            else if (topicSection is IWebReferencesTopicSection)
                return NewUrlGroupControl(topicSection, topicDocument, application, modifiedEventHandler);
            else if (topicSection is IFileAttachmentsTopicSection)
                return NewFileGroupControl(topicSection, topicDocument, application, modifiedEventHandler);
            else if (topicSection is IPdfViewerTopicSection)
                return NewPdfDocument(topicSection, topicDocument, application, modifiedEventHandler);
            else if (topicSection is ISingleImageTopicSection)
                return NewImageDocument(topicSection, topicDocument, application, modifiedEventHandler);
            else if (topicSection is IImagePagerTopicSection)
                return NewImageSetControl(topicSection, topicDocument, application, modifiedEventHandler);
            else if (topicSection is IRichTextEditorTopicSection)
                return NewRichTextDocument(topicSection, topicDocument, application, modifiedEventHandler);
            else
                return null;
        }

        private static ITopicSectionBaseControl NewFileGroupControl(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            FileGroupControl documentControl = new FileGroupControl(application, topicDocument as ITopicDocument, topicSection as IFileAttachmentsTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewRichTextDocument(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            RtfDocumentControl documentControl = new RtfDocumentControl(application, topicDocument as ITopicDocument, topicSection as IRichTextEditorTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewImageSetControl(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            ImageSetControl documentControl = new ImageSetControl(application, topicDocument as ITopicDocument, topicSection as IImagePagerTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewImageDocument(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            ImageControl documentControl = new ImageControl(application, topicDocument as ITopicDocument, topicSection as ISingleImageTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewPdfDocument(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            PdfDocumentControl documentControl = new PdfDocumentControl(application, topicDocument as ITopicDocument, topicSection as IPdfViewerTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewQikScriptControl(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            QikScriptCtrl documentControl = new QikScriptCtrl(application, topicDocument as IQikTemplateDocumentSet);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewQikTemplateControl(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            QikTemplateCodeCtrl documentControl = new QikTemplateCodeCtrl(application, topicDocument as IQikTemplateDocumentSet, topicSection as ICodeTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewCodeControl(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            SimpleCodeTopicSectionControl documentControl = new SimpleCodeTopicSectionControl(application, topicDocument as ITopicDocument, topicSection as ICodeTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewVersionedCodeControl(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            VersionedCodeTopicSectionControl documentControl = new VersionedCodeTopicSectionControl(application, topicDocument as ITopicDocument, topicSection as IVersionedCodeTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static ITopicSectionBaseControl NewUrlGroupControl(ITopicSection topicSection, IPersistableTarget topicDocument, AppFacade application, EventHandler modifiedEventHandler)
        {
            UrlGroupControl documentControl = new UrlGroupControl(application, topicDocument as ITopicDocument, topicSection as IWebReferencesTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }
    }
}
