using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.UiHelpers
{
    public static partial class Gui
    {
        public static class ToolBar
        {
            public static ToolStripButton CreateButton(ToolStrip toolstrip, string buttonText, string imageKey, bool showTextAndImage = false)
            {
                ToolStripButton btn = CreateButton(buttonText, imageKey, null, showTextAndImage);
                toolstrip.Items.Add(btn);
                return btn;
            }

            public static ToolStripButton CreateButton(ToolStrip toolstrip, string buttonText, string imageKey, System.EventHandler handler, bool showTextAndImage = false)
            {
                ToolStripButton btn = CreateButton(buttonText, imageKey, handler, showTextAndImage);
                toolstrip.Items.Add(btn);
                return btn;
            }

            public static ToolStripButton CreateButton(string buttonText, string imageKey, System.EventHandler handler, bool showTextAndImage = false)
            {
                ToolStripButton btn = new ToolStripButton();

                btn.DisplayStyle = showTextAndImage ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image;
                btn.Image = Gui.Resources.GetImage(imageKey);
                btn.ImageTransparentColor = Color.Magenta;
                btn.Name = CreateControlName(buttonText, "btn");
                btn.Size = new Size(24, 24);
                btn.Text = buttonText;
                btn.Click += handler;

                return btn;
            }

            private static string CreateControlName(string caption, string prefix = "", string postfix = "")
            {
                string legalName = caption.Replace(" ", "");
                legalName = legalName.Trim();
                return prefix + legalName + postfix;
            }
        }
    }
}
