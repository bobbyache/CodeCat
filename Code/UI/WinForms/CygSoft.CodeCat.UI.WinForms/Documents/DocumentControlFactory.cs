using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.UI.WinForms.Controls;
using System;

namespace CygSoft.CodeCat.UI.WinForms.Documents
{
    public static class DocumentControlFactory
    {
        public static IDocumentItemControl Create(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            if (topicSection is ICodeTopicSection)
            {
                if (topicSection is IQikScriptTopicSection)
                    return NewQikScriptControl(topicSection, groupOwner, application, modifiedEventHandler);
                else if (topicSection.FileExtension == "tpl")
                    return NewQikTemplateControl(topicSection, groupOwner, application, modifiedEventHandler);
                else
                    return NewCodeControl(topicSection, groupOwner, application, modifiedEventHandler);
            }

            else if (topicSection is IWebReferencesTopicSection)
                return NewUrlGroupControl(topicSection, groupOwner, application, modifiedEventHandler);
            else if (topicSection is IFileAttachmentsTopicSection)
                return NewFileGroupControl(topicSection, groupOwner, application, modifiedEventHandler);
            else if (topicSection is IPdfDocument)
                return NewPdfDocument(topicSection, groupOwner, application, modifiedEventHandler);
            else if (topicSection is IImageDocument)
                return NewImageDocument(topicSection, groupOwner, application, modifiedEventHandler);
            else if (topicSection is IImageSetDocument)
                return NewImageSetControl(topicSection, groupOwner, application, modifiedEventHandler);
            else if (topicSection is IRichTextDocument)
                return NewRichTextDocument(topicSection, groupOwner, application, modifiedEventHandler);
            else
                return null;
        }

        private static IDocumentItemControl NewFileGroupControl(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            FileGroupControl documentControl = new FileGroupControl(application, groupOwner as ICodeGroupDocumentSet, topicSection as IFileAttachmentsTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewRichTextDocument(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            RtfDocumentControl documentControl = new RtfDocumentControl(application, groupOwner as ICodeGroupDocumentSet, topicSection as IRichTextDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewImageSetControl(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            ImageSetControl documentControl = new ImageSetControl(application, groupOwner as ICodeGroupDocumentSet, topicSection as IImageSetDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewImageDocument(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            ImageControl documentControl = new ImageControl(application, groupOwner as ICodeGroupDocumentSet, topicSection as IImageDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewPdfDocument(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            PdfDocumentControl documentControl = new PdfDocumentControl(application, groupOwner as ICodeGroupDocumentSet, topicSection as IPdfDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewQikScriptControl(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            QikScriptCtrl documentControl = new QikScriptCtrl(application, groupOwner as IQikTemplateDocumentSet);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewQikTemplateControl(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            QikTemplateCodeCtrl documentControl = new QikTemplateCodeCtrl(application, groupOwner as IQikTemplateDocumentSet, topicSection as ICodeTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewCodeControl(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            CodeItemCtrl documentControl = new CodeItemCtrl(application, groupOwner as ICodeGroupDocumentSet, topicSection as ICodeTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewUrlGroupControl(ITopicSection topicSection, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            UrlGroupControl documentControl = new UrlGroupControl(application, groupOwner as ICodeGroupDocumentSet, topicSection as IWebReferencesTopicSection);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }
    }
}
