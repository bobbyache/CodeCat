using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public interface IDocumentItemControl
    {
        //event EventHandler SyntaxChanged;
        event EventHandler Modified;
        //event EventHandler Reverted;

        string Id { get; }
        string Title { get; }
        string Text { get; }
        //string Syntax { get; set; }
        int ImageKey { get; }
        Icon ImageIcon { get; }
        Image IconImage { get; }
        bool IsModified { get; }
        bool FileExists { get; }
        //TabPage ParentTab { get; set; }
        void Revert();
    }
}
