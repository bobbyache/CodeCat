using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class RtfFileEditorControl : RtfEditorControl
    {
        public RtfFileEditorControl()
        {
            InitializeComponent();

            FilePath = string.Empty;
        }

        public string FilePath { get; private set; }

        public void OpenFile(string filePath)
        {
            try
            {
                rtbDoc.LoadFile(filePath, RichTextBoxStreamType.RichText);
                FilePath = filePath;
                rtbDoc.SelectionStart = 0;
                rtbDoc.SelectionLength = 0;
                rtbDoc.Modified = false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Save(string filePath)
        {
            try
            {
                FilePath = filePath;
                rtbDoc.SaveFile(FilePath);
                rtbDoc.Modified = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
