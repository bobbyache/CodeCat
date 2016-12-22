using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
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

            this.FilePath = string.Empty;
            rtbDoc.SetInnerMargins(24, 24, 24, 24);
        }

        public string FilePath { get; private set; }
        public bool Modified { get { return this.rtbDoc.Modified; } }

        public void OpenFile(string filePath)
        {
            try
            {
                rtbDoc.LoadFile(filePath, RichTextBoxStreamType.RichText);
                this.FilePath = filePath;
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
                this.FilePath = filePath;
                rtbDoc.SaveFile(this.FilePath);
                rtbDoc.Modified = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        private void SelectAllToolStripMenuItem_Click(object sender, System.EventArgs e)
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

        private void CopyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CopySelection();
        }

        private void CutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CutSelection();
        }

        private void PasteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            PasteSelection();
        }

        private void SelectFontToolStripMenuItem_Click(object sender, System.EventArgs e)
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
                if (FontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionColor = ColorDialog1.Color;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FontColorToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormatToFontColor();
        }

        private void BoldToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormatToBold();
        }

        private void FormatToBold()
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;
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
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

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
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ItalicToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormatToItalic();
        }

        private void UnderlineToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormatToUnderline();
        }

        private void NormalToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;
                    newFontStyle = FontStyle.Regular;
                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PageColorToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.BackColor;
                if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.BackColor = ColorDialog1.Color;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mnuUndo_Click(object sender, System.EventArgs e)
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

        private void mnuRedo_Click(object sender, System.EventArgs e)
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

        private void LeftToolStripMenuItem_Click_1(object sender, System.EventArgs e)
        {
            AlignLeft();
        }

        private void CenterToolStripMenuItem_Click_1(object sender, System.EventArgs e)
        {
            AlignCenter();
        }

        private void RightToolStripMenuItem_Click_1(object sender, System.EventArgs e)
        {
            AlignRight();
        }

        private void AddBulletsToolStripMenuItem_Click(object sender, System.EventArgs e)
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

        private void RemoveBulletsToolStripMenuItem_Click(object sender, System.EventArgs e)
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

        private void mnuIndent0_Click(object sender, System.EventArgs e)
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

        private void mnuIndent5_Click(object sender, System.EventArgs e)
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

        private void mnuIndent10_Click(object sender, System.EventArgs e)
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

        private void mnuIndent15_Click(object sender, System.EventArgs e)
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

        private void mnuIndent20_Click(object sender, System.EventArgs e)
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

        private void FindToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
        }

        private void FindAndReplaceToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
        }

        private void mnuPageSetup_Click(object sender, System.EventArgs e)
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

        private void InsertImageToolStripMenuItem_Click(object sender, System.EventArgs e)
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
                    if (this.rtbDoc.CanPaste(df))
                    {
                        this.rtbDoc.Paste(df);
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

        private void tbrBold_Click(object sender, System.EventArgs e)
        {
            FormatToBold();
        }


        private void tbrItalic_Click(object sender, System.EventArgs e)
        {
            FormatToItalic();
        }


        private void tbrUnderline_Click(object sender, System.EventArgs e)
        {
            FormatToUnderline();
        }


        private void tbrFont_Click(object sender, System.EventArgs e)
        {
            FormatToFont();
        }


        private void tbrLeft_Click(object sender, System.EventArgs e)
        {
            AlignLeft();
        }


        private void tbrCenter_Click(object sender, System.EventArgs e)
        {
            AlignCenter();
        }


        private void tbrRight_Click(object sender, System.EventArgs e)
        {
            AlignRight();
        }


        private void tbrFind_Click(object sender, System.EventArgs e)
        {
        }


        private void tspColor_Click(object sender, EventArgs e)
        {
            FormatToFontColor();
        }

        private void rtbDoc_TextChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null)
                ContentChanged(this, new EventArgs());
        }

        private void rtbDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Key
        }

        private void rtbDoc_KeyUp(object sender, KeyEventArgs e)
        {

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
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenu.Show(Cursor.Position);
            }
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




//private void tbrOpen_Click(object sender, System.EventArgs e)
//{
//    OpenToolStripMenuItem_Click(this, e);
//}


//private void tbrNew_Click(object sender, System.EventArgs e)
//{
//    NewToolStripMenuItem_Click(this, e);
//}

//private void PreviewToolStripMenuItem_Click(object sender, System.EventArgs e)
//{
//    try
//    {
//        PrintPreviewDialog1.Document = PrintDocument1;
//        PrintPreviewDialog1.ShowDialog();
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message.ToString(), "Error");
//    }
//}




//private void PrintToolStripMenuItem_Click(object sender, System.EventArgs e)
//{
//    try
//    {
//        PrintDialog1.Document = PrintDocument1;
//        if (PrintDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//        {
//            PrintDocument1.Print();
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message.ToString(), "Error");
//    }
//}

//private void SaveToolStripMenuItem_Click(object sender, System.EventArgs e)
//{
//    try
//    {
//        if (currentFile == string.Empty)
//        {
//            SaveAsToolStripMenuItem_Click(this, e);
//            return;
//        }

//        try
//        {
//            string strExt;
//            strExt = System.IO.Path.GetExtension(currentFile);
//            strExt = strExt.ToUpper();
//            if (strExt == ".RTF")
//            {
//                rtbDoc.SaveFile(currentFile);
//            }
//            else
//            {
//                System.IO.StreamWriter txtWriter;
//                txtWriter = new System.IO.StreamWriter(currentFile);
//                txtWriter.Write(rtbDoc.Text);
//                txtWriter.Close();
//                txtWriter = null;
//                rtbDoc.SelectionStart = 0;
//                rtbDoc.SelectionLength = 0;
//            }

//            this.Text = "Editor: " + currentFile.ToString();
//            rtbDoc.Modified = false;
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show(ex.Message.ToString(), "File Save Error");
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message.ToString(), "Error");
//    }


//}

//private void NewToolStripMenuItem_Click(object sender, System.EventArgs e)
//{
//    try
//    {
//        if (rtbDoc.Modified == true)
//        {
//            System.Windows.Forms.DialogResult answer;
//            answer = MessageBox.Show("Save current document before creating new document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//            if (answer == System.Windows.Forms.DialogResult.No)
//            {
//                currentFile = "";
//                this.Text = "Editor: New Document";
//                rtbDoc.Modified = false;
//                rtbDoc.Clear();
//                return;
//            }
//            else
//            {
//                SaveToolStripMenuItem_Click(this, new EventArgs());
//                rtbDoc.Modified = false;
//                rtbDoc.Clear();
//                currentFile = "";
//                this.Text = "Editor: New Document";
//                return;
//            }
//        }
//        else
//        {
//            currentFile = "";
//            this.Text = "Editor: New Document";
//            rtbDoc.Modified = false;
//            rtbDoc.Clear();
//            return;
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message.ToString(), "Error");
//    }

//private void ExitToolStripMenuItem_Click(object sender, System.EventArgs e)
//{
//    try
//    {
//        if (rtbDoc.Modified == true)
//        {
//            System.Windows.Forms.DialogResult answer;
//            answer = MessageBox.Show("Save this document before closing?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//            if (answer == System.Windows.Forms.DialogResult.Yes)
//            {
//                return;
//            }
//            else
//            {
//                rtbDoc.Modified = false;
//                Application.Exit();
//            }
//        }
//        else
//        {
//            rtbDoc.Modified = false;
//            Application.Exit();
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message.ToString(), "Error");
//    }
//}


//public bool Find(string searchText, StringComparison searchType)
//{
//    int startPosition = rtbDoc.Text.IndexOf(searchText, searchType); ;
//    return FindText(searchText, startPosition, searchType);
//}

//public bool FindNext(string searchText, StringComparison searchType)
//{
//    int startPosition = rtbDoc.Text.IndexOf(searchText, rtbDoc.SelectionStart + 2, searchType);
//    return FindText(searchText, startPosition, searchType);
//}

//private bool FindText(string searchText, int startPosition, StringComparison searchType)
//{
//    if (startPosition == 0 || startPosition < 0)
//    {
//        return false;
//    }
//    else
//    {
//        rtbDoc.Select(startPosition, searchText.Length);
//        rtbDoc.ScrollToCaret();
//        this.Focus();

//        return true;
//    }
//}

//public bool Replace(string searchText, string replaceText, StringComparison searchType, bool replaceAll = false)
//{
//    if (rtbDoc.SelectedText.Length != 0)
//    {
//        rtbDoc.SelectedText = replaceText;
//    }

//    int startPosition = rtbDoc.Text.IndexOf(searchText, rtbDoc.SelectionStart + 2, searchType);
//    bool found = FindText(searchText, startPosition, searchType);

//    if (found)
//    {

//    }

//    return found;
//}

//public void DestroyMe()
//{
//    try
//    {
//        if (rtbDoc.Modified == true)
//        {
//            System.Windows.Forms.DialogResult answer;
//            answer = MessageBox.Show("Save current document before exiting?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//            if (answer == System.Windows.Forms.DialogResult.No)
//            {
//                rtbDoc.Modified = false;
//                rtbDoc.Clear();
//                return;
//            }
//            else
//            {
//                Save(currentFile);
//            }
//        }
//        else
//        {
//            rtbDoc.Clear();
//        }
//        currentFile = "";
//        this.Text = "Editor: New Document";
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message.ToString(), "Error");
//    }
//}