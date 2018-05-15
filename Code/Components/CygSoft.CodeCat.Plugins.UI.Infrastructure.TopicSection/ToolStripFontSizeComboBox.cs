using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.Plugins.UI.Infrastructure.TopicSection
{
    public class ToolStripFontSizeComboBox : ToolStripComboBox
    {
        public Single FontSize { get { return Convert.ToSingle(this.SelectedItem); } }

        public ToolStripFontSizeComboBox() : base()
        {
            this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Size = new System.Drawing.Size(75, 25);
            LoadFonts();
        }

        public void LoadFonts()
        {
            this.Items.Clear();
            this.Items.AddRange(new object[] {
                "8",
                "9",
                "10",
                "11",
                "12",
                "13",
                "14",
                "15",
                "16"
            });
        }

        public void SetFont(int fontSize)
        {
            this.SelectedIndex = this.FindStringExact(fontSize.ToString());
        }
    }
}
