using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class RtfEditorControl : UserControl
    {
        public event EventHandler ContentChanged;

        public RtfEditorControl()
        {
            InitializeComponent();

            ctxmnuBold.Image = tbrBold.Image;
            ctxmnuItalic.Image = tbrItalic.Image;
            ctxmnuUnderline.Image = tbrUnderline.Image;
            ctxmnuAlignLeft.Image = tbrLeft.Image;
            ctxmnuAlignRight.Image = tbrRight.Image;
            ctxmnuAlignCenter.Image = tbrCenter.Image;
            ctxmnuFont.Image = tbrFont.Image;
            ctxmnuFontColor.Image = tspColor.Image;

            SelectFontToolStripMenuItem.Image = tbrFont.Image;
            FontColorToolStripMenuItem.Image = tspColor.Image;
            BoldToolStripMenuItem.Image = tbrBold.Image;
            ItalicToolStripMenuItem.Image = tbrItalic.Image;
            UnderlineToolStripMenuItem.Image = tbrUnderline.Image;
            LeftToolStripMenuItem.Image = tbrLeft.Image;
            CenterToolStripMenuItem.Image = tbrCenter.Image;
            RightToolStripMenuItem.Image = tbrRight.Image;

            
            rtbDoc.SetInnerMargins(24, 24, 24, 24);
        }

        public void OpenFile(string filePath)
        {
            try
            {
                rtbDoc.LoadFile(filePath, RichTextBoxStreamType.RichText);
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
                rtbDoc.SaveFile(filePath);
                rtbDoc.Modified = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string Text
        {
            get { return rtbDoc.Text; }
            set { rtbDoc.Text = value; }
        }

        public string TextRtf
        {
            get { return rtbDoc.Rtf; }
            set { rtbDoc.Rtf = value; }
        }

        public bool Modified { get { return rtbDoc.Modified; } }

        private void SelectAll()
        {
            try
            {
                rtbDoc.SelectAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void CopySelection()
        {
            try
            {
                rtbDoc.Copy();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CutSelection()
        {
            try
            {
                rtbDoc.Cut();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PasteSelection()
        {
            try
            {
                rtbDoc.Paste();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelection();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutSelection();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteSelection();
        }

        private void SelectFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatToFont();
        }

        private void FormatToFont()
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    FontDialog1.Font = rtbDoc.SelectionFont;
                }
                else
                {
                    FontDialog1.Font = null;
                }
                FontDialog1.ShowApply = true;
                if (FontDialog1.ShowDialog() == DialogResult.OK)
                {
                    rtbDoc.SelectionFont = FontDialog1.Font;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FormatToFontColor()
        {
            try
            {
                ColorDialog1.Color = rtbDoc.ForeColor;
                if (ColorDialog1.ShowDialog() == DialogResult.OK)
                    rtbDoc.SelectionColor = ColorDialog1.Color;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatToFontColor();
        }

        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatToBold();
        }

        private void FormatToBold()
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    Font currentFont = rtbDoc.SelectionFont;
                    FontStyle newFontStyle;
                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Bold;
                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FormatToItalic()
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    Font currentFont = rtbDoc.SelectionFont;
                    FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Italic;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FormatToUnderline()
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    Font currentFont = rtbDoc.SelectionFont;
                    FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ItalicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatToItalic();
        }

        private void UnderlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatToUnderline();
        }

        private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    Font currentFont = rtbDoc.SelectionFont;
                    FontStyle newFontStyle;
                    newFontStyle = FontStyle.Regular;
                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PageColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.BackColor;
                if (ColorDialog1.ShowDialog() == DialogResult.OK)
                {
                    rtbDoc.BackColor = ColorDialog1.Color;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.CanUndo)
                {
                    rtbDoc.Undo();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mnuRedo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.CanRedo)
                {
                    rtbDoc.Redo();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AlignLeft()
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AlignRight()
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void AlignCenter()
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LeftToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AlignLeft();
        }

        private void CenterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AlignCenter();
        }

        private void RightToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AlignRight();
        }

        private void AddBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.BulletIndent = 10;
                rtbDoc.SelectionBullet = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RemoveBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionBullet = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mnuIndent0_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mnuIndent5_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 5;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mnuIndent10_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 10;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mnuIndent15_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 15;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mnuIndent20_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 20;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void FindAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void mnuPageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                PageSetupDialog1.Document = PrintDocument1;
                PageSetupDialog1.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InsertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog1.Title = "RTE - Insert Image File";
            OpenFileDialog1.DefaultExt = "rtf";
            OpenFileDialog1.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif";
            OpenFileDialog1.FilterIndex = 1;
            DialogResult result = OpenFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = OpenFileDialog1.FileName.Trim();
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;

                try
                {
                    string strImagePath = OpenFileDialog1.FileName;
                    Image img;
                    img = Image.FromFile(strImagePath);
                    Clipboard.SetDataObject(img);
                    DataFormats.Format df;
                    df = DataFormats.GetFormat(DataFormats.Bitmap);
                    if (rtbDoc.CanPaste(df))
                    {
                        rtbDoc.Paste(df);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void rtbDoc_SelectionChanged(object sender, EventArgs e)
        {
            if (rtbDoc.SelectionFont != null)
            {
                tbrBold.Checked = rtbDoc.SelectionFont.Bold;
                tbrItalic.Checked = rtbDoc.SelectionFont.Italic;
                tbrUnderline.Checked = rtbDoc.SelectionFont.Underline;
            }
        }

        private void tbrBold_Click(object sender, EventArgs e)
        {
            FormatToBold();
        }


        private void tbrItalic_Click(object sender, EventArgs e)
        {
            FormatToItalic();
        }


        private void tbrUnderline_Click(object sender, EventArgs e)
        {
            FormatToUnderline();
        }


        private void tbrFont_Click(object sender, EventArgs e)
        {
            FormatToFont();
        }


        private void tbrLeft_Click(object sender, EventArgs e)
        {
            AlignLeft();
        }


        private void tbrCenter_Click(object sender, EventArgs e)
        {
            AlignCenter();
        }


        private void tbrRight_Click(object sender, EventArgs e)
        {
            AlignRight();
        }

        private void tspColor_Click(object sender, EventArgs e)
        {
            FormatToFontColor();
        }

        private void rtbDoc_TextChanged(object sender, EventArgs e)
        {
            ContentChanged?.Invoke(this, new EventArgs());
        }

        private void rtbDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.I)
            {
                FormatToItalic();
                e.SuppressKeyPress = true;
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.B)
            {
                FormatToBold();
                e.SuppressKeyPress = true;
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.U)
            {
                FormatToUnderline();
                e.SuppressKeyPress = true;
            }
        }

        private void rtbDoc_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenu.Show(Cursor.Position);
        }

        private void ctxmnuAlignLeft_Click(object sender, EventArgs e)
        {
            AlignLeft();
        }

        private void ctxmnuAlignCenter_Click(object sender, EventArgs e)
        {
            AlignCenter();
        }

        private void ctxmnuAlignRight_Click(object sender, EventArgs e)
        {
            AlignRight();
        }

        private void ctxmnuCut_Click(object sender, EventArgs e)
        {
            CutSelection();
        }

        private void ctxmnuCopy_Click(object sender, EventArgs e)
        {
            CopySelection();
        }

        private void ctxmnuPaste_Click(object sender, EventArgs e)
        {
            PasteSelection();
        }

        private void ctxmnuSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void ctxmnuFontColor_Click(object sender, EventArgs e)
        {
            FormatToFontColor();
        }

        private void ctxmnuFont_Click(object sender, EventArgs e)
        {
            FormatToFont();
        }

        private void ctxmnuBold_Click(object sender, EventArgs e)
        {
            FormatToBold();
        }

        private void ctxmnuItalic_Click(object sender, EventArgs e)
        {
            FormatToItalic();
        }

        private void ctxmnuUnderline_Click(object sender, EventArgs e)
        {
            FormatToUnderline();
        }
     }
}