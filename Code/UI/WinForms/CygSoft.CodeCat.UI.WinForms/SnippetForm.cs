using CygSoft.CodeCat.Domain.Code;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SnippetForm : DockContent
    {
        public SnippetForm(CodeFile codeFile, string syntaxFile)
        {
            InitializeComponent();

            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            tabControl.Alignment = TabAlignment.Left;

            this.codeFile = codeFile;
            this.syntaxBox.GotFocus += (s, e) => { Console.Write(this.IsActivated.ToString()); };

            this.syntaxBox.Document.Text = codeFile.Text;
            this.syntaxBox.Document.SyntaxFile = syntaxFile;

            this.Text = codeFile.Title;
            this.Tag = codeFile.Id;
            this.txtKeywords.Text = codeFile.CommaDelimitedKeywords;
            this.txtTitle.Text = codeFile.Title;

            this.CloseButtonVisible = true;
            this.CloseButton = true;
        }

        private CodeFile codeFile;

        //public event EventHandler MovePrevious;
        //public event EventHandler MoveNext;

        public string SnippetId
        {
            get
            {
                if (this.codeFile != null)
                    return this.codeFile.Id;
                return null;
            }
        }

        public string Keywords
        {
            get
            {
                if (this.codeFile != null)
                    return this.codeFile.CommaDelimitedKeywords;
                return null;
            }
        }

        public bool IsModified { get; private set; }
        public bool IsNew { get; private set; }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void DiscardChanges()
        {
            throw new NotImplementedException();
        }

        public void AddKeywords(string keywords, bool flagModified = true)
        {
            // in the case that the keywords have already been saved to the repository, ie.
            // we've added keywords to multiple items from the main menu's "Add Keywords"
            // then there is no reason to flag this snippet as modified.
            this.codeFile.CommaDelimitedKeywords += ("," + keywords);
        }

        private void SnippetForm_Activated(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Activated: {1}, Document: {0}", this.Text, this.IsActivated.ToString()));
            // There's a bug here. It seems that if you activate a docked Document
            // from a floating Document the activated event doesn't fire ???
            // However this only happens when you click in the SyntaxBox control and 
            //doesn't happen if you click the docked window tab.
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // if one of these forms cancel, it also seems to stop the application
            // from closing!
            bool cancel = false;
            e.Cancel = cancel;

            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Isn't currently working, but you could apparently use this to "tab"
            // between different windows...
            // http://stackoverflow.com/questions/22873825/how-to-detect-shifttab-when-overriding-processcmdkey
            // when the time comes, research something like handling multiple keys pressed at the same time etc.

            //// combine any number of keys here
            //if (keyData == (Keys.ControlKey | Keys.Back))
            //{
            //    if (MovePrevious != null)
            //        MovePrevious(this, new EventArgs());
            //}
            //else if (keyData == (Keys.ControlKey | Keys.Next))
            //{
            //    if (MoveNext != null)
            //        MoveNext(this, new EventArgs());
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
