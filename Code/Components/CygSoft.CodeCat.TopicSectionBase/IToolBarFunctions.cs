using System;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public interface IToolBarFunctions
    {
        ToolStripButton CreateButton(ToolStrip toolstrip, string buttonText, Image image, bool showTextAndImage = false);
        ToolStripButton CreateButton(string buttonText, Image image, EventHandler handler, bool showTextAndImage = false);
        ToolStripButton CreateButton(ToolStrip toolstrip, string buttonText, Image image, EventHandler handler, bool showTextAndImage = false);
    }
}