using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public interface IContentDocument
    {
        event EventHandler DocumentDeleted;
        event EventHandler<DocumentSavedFileEventArgs> DocumentSaved;

        string SnippetId { get; }
        IKeywordIndexItem KeywordIndex { get; }

        string Text { get; set; }
        Image IconImage { get; }
        bool ShowIndexEditControls { get; set; }

        bool IsNew { get; }
        bool IsModified { get; }

        void Show(DockPanel docPanel, DockState dockState);
        void Activate();

        bool SaveChanges();

        void FlagSilentClose();
        void Close();

        void AddKeywords(string keywords, bool flagModified = true);
        void RemoveKeywords(string keywords, bool flagModified = true);
    }
}
