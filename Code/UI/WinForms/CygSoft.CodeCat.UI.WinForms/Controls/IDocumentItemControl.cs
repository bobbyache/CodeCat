using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public interface IDocumentItemControl
    {
        //event EventHandler SyntaxChanged;

        string Id { get; }
        string Title { get; }
        string Text { get; }
        //string Syntax { get; set; }
        int ImageKey { get; }
        bool IsModified { get; }
        bool FileExists { get; }
        TabPage ParentTab { get; set; }
        void Revert();
    }
}
