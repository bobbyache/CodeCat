using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class RtfEditorControl : UserControl
    {
        public event EventHandler ContentChanged;

        public RtfEditorControl()
        {
            InitializeComponent();

            richTextBox.Font = new Font("Calibri", 12);

            ctxmnuBold.Image = btnBold.Image;
            ctxmnuItalic.Image = btnItalic.Image;
            ctxmnuUnderline.Image = btnUnderline.Image;
            ctxmnuAlignLeft.Image = tbrLeft.Image;
            ctxmnuAlignRight.Image = tbrRight.Image;
            ctxmnuAlignCenter.Image = tbrCenter.Image;
            ctxmnuFont.Image = btnFormat.Image;
            ctxmnuFontColor.Image = tspColor.Image;

            mnuFont.Image = btnFormat.Image;
            mnuFontColor.Image = tspColor.Image;
            mnuBold.Image = btnBold.Image;
            mnuItalic.Image = btnItalic.Image;
            mnuUnderline.Image = btnUnderline.Image;
            mnuAlignLeft.Image = tbrLeft.Image;
            mnuAlignCenter.Image = tbrCenter.Image;
            mnuAlignRight.Image = tbrRight.Image;

            richTextBox.SetInnerMargins(24, 24, 24, 24);

            mnuCopy.Click += (s, e) => CopySelection();
            mnuCut.Click += (s, e) => CutSelection();
            mnuPaste.Click += (s, e) => PasteSelection();
            mnuFont.Click += (s, e) => FormatFont();
            mnuItalic.Click += (s, e) => Italic();

            mnuUnderline.Click += (s, e) => Underline();
            mnuNormal.Click += (s, e) => FormatFontToNormal();
            mnuPageColor.Click += (s, e) => SetPageColour();

            mnuSelectAll.Click += (s, e) => SelectAll();

            mnuIndent0.Click += (s, e) => richTextBox.SelectionIndent = 0;
            mnuIndent5.Click += (s, e) => richTextBox.SelectionIndent = 5;
            mnuIndent10.Click += (s, e) => richTextBox.SelectionIndent = 10;
            mnuIndent15.Click += (s, e) => richTextBox.SelectionIndent = 15;
            mnuIndent20.Click += (s, e) => richTextBox.SelectionIndent = 20;

            mnuAlignLeft.Click += (s, e) => AlignLeft();
            mnuAlignCenter.Click += (s, e) => AlignCenter();
            mnuAlignRight.Click += (s, e) => AlignRight();

            mnuAddBullets.Click += (s, e) => AddBullets();
            mnuRemoveBullets.Click += (s, e) => richTextBox.SelectionBullet = false;

            //FindToolStripMenuItem.Click 
            //FindAndReplaceToolStripMenuItem.Click += (s, e) =>

            mnuFontColor.Click += (s, e) => FormatToFontColor();
            mnuBold.Click += (s, e) => Bold();
            mnuUndo.Click += (s, e) => Undo();
            mnuRedo.Click += (s, e) => Redo();

            btnBold.Click += (s, e) => Bold();
            btnItalic.Click += (s, e) => Italic();
            btnUnderline.Click += (s, e) => Underline();
            btnFormat.Click += (s, e) => FormatFont();


            tbrLeft.Click += (s, e) => AlignLeft();
            tbrCenter.Click += (s, e) => AlignCenter();
            tbrRight.Click += (s, e) => AlignRight();

            ctxmnuAlignLeft.Click += (s, e) => AlignLeft();
            ctxmnuAlignCenter.Click += (s, e) => AlignCenter();
            ctxmnuAlignRight.Click += (s, e) => AlignRight();

            tspColor.Click += (s, e) => FormatToFontColor();

            richTextBox.TextChanged += (s, e) => ContentChanged?.Invoke(this, new EventArgs());

            ctxmnuCut.Click += (s, e) => CutSelection();
            ctxmnuCopy.Click += (s, e) => CopySelection();
            ctxmnuPaste.Click += (s, e) => PasteSelection();
            ctxmnuSelectAll.Click += (s, e) => SelectAll();
            ctxmnuFontColor.Click += (s, e) => FormatToFontColor();

            ctxmnuBold.Click += (s, e) => Bold();
            ctxmnuFont.Click += (s, e) => FormatFont();
            ctxmnuItalic.Click += (s, e) => Italic();
            ctxmnuUnderline.Click += (s, e) => Underline();

            mnuInsertImage.Click += (s, e) => InsertImage();
        }

        private void InsertImage()
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
                    if (richTextBox.CanPaste(df))
                    {
                        richTextBox.Paste(df);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void Undo()
        {
            if (richTextBox.CanUndo)
            {
                richTextBox.Undo();
            }
        }

        private void Redo()
        {
            if (richTextBox.CanRedo)
            {
                richTextBox.Redo();
            }
        }

        private void AddBullets()
        {
            richTextBox.BulletIndent = 10;
            richTextBox.SelectionBullet = true;
        }

        public void OpenFile(string filePath)
        {
            try
            {
                richTextBox.LoadFile(filePath, RichTextBoxStreamType.RichText);
                richTextBox.SelectionStart = 0;
                richTextBox.SelectionLength = 0;
                richTextBox.Modified = false;
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
                richTextBox.SaveFile(filePath);
                richTextBox.Modified = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string Text
        {
            get { return richTextBox.Text; }
            set { richTextBox.Text = value; }
        }

        public string TextRtf
        {
            get { return richTextBox.Rtf; }
            set { richTextBox.Rtf = value; }
        }

        public bool Modified { get { return richTextBox.Modified; } }

        private void SelectAll()
        {
            try
            {
                richTextBox.SelectAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CopySelection()
        {
            try
            {
                richTextBox.Copy();
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
                richTextBox.Cut();
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
                richTextBox.Paste();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FormatFont()
        {
            try
            {
                if (!(richTextBox.SelectionFont == null))
                {
                    FontDialog1.Font = richTextBox.SelectionFont;
                }
                else
                {
                    FontDialog1.Font = null;
                }
                FontDialog1.ShowApply = true;
                if (FontDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox.SelectionFont = FontDialog1.Font;
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
                ColorDialog1.Color = richTextBox.ForeColor;
                if (ColorDialog1.ShowDialog() == DialogResult.OK)
                    richTextBox.SelectionColor = ColorDialog1.Color;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Bold()
        {
            try
            {
                if (!(richTextBox.SelectionFont == null))
                {
                    Font currentFont = richTextBox.SelectionFont;
                    FontStyle newFontStyle;
                    newFontStyle = richTextBox.SelectionFont.Style ^ FontStyle.Bold;
                    richTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Italic()
        {
            try
            {
                if (!(richTextBox.SelectionFont == null))
                {
                    Font currentFont = richTextBox.SelectionFont;
                    FontStyle newFontStyle;

                    newFontStyle = richTextBox.SelectionFont.Style ^ FontStyle.Italic;

                    richTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Underline()
        {
            try
            {
                if (!(richTextBox.SelectionFont == null))
                {
                    Font currentFont = richTextBox.SelectionFont;
                    FontStyle newFontStyle;

                    newFontStyle = richTextBox.SelectionFont.Style ^ FontStyle.Underline;

                    richTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
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
                richTextBox.SelectionAlignment = HorizontalAlignment.Left;
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
                richTextBox.SelectionAlignment = HorizontalAlignment.Right;
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
                richTextBox.SelectionAlignment = HorizontalAlignment.Center;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetPageColour()
        {
            ColorDialog1.Color = richTextBox.BackColor;
            if (ColorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox.BackColor = ColorDialog1.Color;
            }
        }

        private void FormatFontToNormal()
        {
            if (!(richTextBox.SelectionFont == null))
            {
                Font currentFont = richTextBox.SelectionFont;
                FontStyle newFontStyle;
                newFontStyle = FontStyle.Regular;
                richTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.I)
            {
                Italic();
                e.SuppressKeyPress = true;
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.B)
            {
                Bold();
                e.SuppressKeyPress = true;
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.U)
            {
                Underline();
                e.SuppressKeyPress = true;
            }
        }

        private void richTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(Cursor.Position);
            }
        }
    }
}