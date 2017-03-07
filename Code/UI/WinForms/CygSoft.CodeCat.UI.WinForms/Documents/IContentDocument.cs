using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public interface IContentDocument
    {
        event EventHandler DocumentDeleted;
        event EventHandler<DocumentSavedFileEventArgs> DocumentSaved;

        string Id { get; }

        string Text { get; set; }
        Image IconImage { get; }
        bool CloseWithoutPrompts { get; set; }
        bool HeaderFieldsVisible { get; set; }

        bool IsNew { get; }
        bool IsModified { get; }

        void Show(DockPanel docPanel, DockState dockState);
        void Activate();
        IKeywordIndexItem GetKeywordIndex();

        bool SaveChanges();
        void Close();

        void AddKeywords(string keywords, bool flagModified = true);
        void RemoveKeywords(string keywords, bool flagModified = true);
    }
}
