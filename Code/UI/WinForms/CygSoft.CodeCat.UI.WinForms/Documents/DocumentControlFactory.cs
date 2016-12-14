﻿using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.UI.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Documents
{
    public static class DocumentControlFactory
    {
        public static IDocumentItemControl Create(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            if (document is ICodeDocument)
            {
                if (document is IQikScriptDocument)
                    return NewQikScriptControl(document, groupOwner, application, modifiedEventHandler);
                else if (document.FileExtension == "tpl")
                    return NewQikTemplateControl(document, groupOwner, application, modifiedEventHandler);
                else
                    return NewCodeControl(document, groupOwner, application, modifiedEventHandler);
            }

            else if (document is IUrlGroupDocument)
                return NewUrlGroupControl(document, groupOwner, application, modifiedEventHandler);
            else if (document is IFileGroupDocument)
                return NewFileGroupControl(document, groupOwner, application, modifiedEventHandler);
            else if (document is IPdfDocument)
                return NewPdfDocument(document, groupOwner, application, modifiedEventHandler);
            else if (document is IImageDocument)
                return NewImageDocument(document, groupOwner, application, modifiedEventHandler);
            else if (document is IImageSetDocument)
                return NewImageSetControl(document, groupOwner, application, modifiedEventHandler);
            else if (document is IRichTextDocument)
                return NewRichTextDocument(document, groupOwner, application, modifiedEventHandler);
            else
                return null;
        }

        private static IDocumentItemControl NewFileGroupControl(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            FileGroupControl documentControl = new FileGroupControl(application, groupOwner as ICodeGroupDocumentSet, document as IFileGroupDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewRichTextDocument(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            RtfDocumentControl documentControl = new RtfDocumentControl(application, groupOwner as ICodeGroupDocumentSet, document as IRichTextDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewImageSetControl(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            ImageSetControl documentControl = new ImageSetControl(application, groupOwner as ICodeGroupDocumentSet, document as IImageSetDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewImageDocument(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            ImageControl documentControl = new ImageControl(application, groupOwner as ICodeGroupDocumentSet, document as IImageDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewPdfDocument(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            PdfDocumentControl documentControl = new PdfDocumentControl(application, groupOwner as ICodeGroupDocumentSet, document as IPdfDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewQikScriptControl(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            QikScriptCtrl documentControl = new QikScriptCtrl(application, groupOwner as IQikTemplateDocumentSet);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewQikTemplateControl(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            QikTemplateCodeCtrl documentControl = new QikTemplateCodeCtrl(application, groupOwner as IQikTemplateDocumentSet, document as ICodeDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewCodeControl(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            CodeItemCtrl documentControl = new CodeItemCtrl(application, groupOwner as ICodeGroupDocumentSet, document as ICodeDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }

        private static IDocumentItemControl NewUrlGroupControl(IDocument document, IPersistableTarget groupOwner, AppFacade application, EventHandler modifiedEventHandler)
        {
            UrlGroupControl documentControl = new UrlGroupControl(application, groupOwner as ICodeGroupDocumentSet, document as IUrlGroupDocument);
            documentControl.Modified += modifiedEventHandler;
            return documentControl;
        }
    }
}
