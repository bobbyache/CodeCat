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

// -------------------------------------------------------------------------------
// ---> will never return anything because the forms are owned by the dockPanel.
// -------------------------------------------------------------------------------
//var unsavedDocs = this.OwnedForms.OfType<ISnippetDocument>()
//    .Where(doc => doc.IsModified == true);

// -------------------------------------------------------------------------------
// ---> returns the number of "docked" document forms, anything undocked will not
// be returned.
// -------------------------------------------------------------------------------
//var unsavedDocs = dockPanel1.Documents.OfType<ISnippetDocument>()
//    .Where(doc => doc.IsModified == true);


// -------------------------------------------------------------------------------
// ---> return the number of free floating windows that are not docked...
// however, if one of these is docked with another floating window, they are both
// removed from the "FloatingWindows" collection.
// -------------------------------------------------------------------------------
//var unsavedDocs = dockPanel1.FloatWindows.OfType<ISnippetDocument>()
//    .Where(doc => doc.IsModified == true);

// -------------------------------------------------------------------------------
// ---> returns all docked and undocked forms!
// -------------------------------------------------------------------------------
//var unsavedDocs = this.dockPanel1.Contents.OfType<ISnippetDocument>()
//    .Where(doc => doc.IsModified == true);
// -------------------------------------------------------------------------------
//var unsavedDocs = this.dockPanel1.Contents.OfType<ISnippetDocument>()
//    .Where(doc => doc.IsModified == true);

//var savedDocs = this.dockPanel1.Contents.OfType<ISnippetDocument>()
//    .Where(doc => doc.IsModified == false);


namespace Weif_1_Test
{
    public partial class Form1 : Form
    {
        private SearchForm searchForm;

        public Form1()
        {
            InitializeComponent();

            dockPanel1.ContentRemoved += dockPanel1_ContentRemoved;

            searchForm = new SearchForm();
            searchForm.Show(dockPanel1, DockState.DockLeft);

            CreateSnippetIfNone();
        }

        private void dockPanel1_ContentRemoved(object sender, DockContentEventArgs e)
        {
            CreateSnippetIfNone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UnsavedSnippetDocuments())
            {
                DialogResult result = PromptSaveChanges();

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveAllDocuments();
                    ClearSnippetDocuments();
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                    e.Cancel = true;
                else
                    ClearSnippetDocuments();
            }
        }

        private void CreateSnippetIfNone()
        {
            if (!this.dockPanel1.Contents.OfType<ISnippetDocument>().Any())
                CreateSnippetDocument();
        }

        private void CreateSnippetDocument()
        {
            SnippetDocument form2 = new SnippetDocument();
            form2.Show(dockPanel1, DockState.Document);
        }

        private void OpenSnippetDocument()
        {
            SnippetDocument form2 = new SnippetDocument(Guid.NewGuid().ToString());
            form2.Show(dockPanel1, DockState.Document);
        }

        private void mnuNewProject_Click(object sender, EventArgs e)
        {
            if (!UnsavedSnippetDocuments())
            {
                ClearSnippetDocuments();
                // create new project...
                CreateSnippetDocument();
            }
            else
            {
                DialogResult result = PromptSaveChanges();

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveAllDocuments();
                    ClearSnippetDocuments();
                    // create new project...
                    CreateSnippetDocument();
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    ClearSnippetDocuments();
                    // create new project...
                    CreateSnippetDocument();
                }
            }
        }

        private SnippetDocument[] UnsavedDocuments()
        {
            return this.dockPanel1.Contents.OfType<SnippetDocument>().Where(doc => doc.IsModified == true).ToArray();
        }

        private bool UnsavedSnippetDocuments()
        {
            return this.dockPanel1.Contents.OfType<ISnippetDocument>().Where(doc => doc.IsModified == true).Any();
            //bool unsavedDocs = this.dockPanel1.Contents.OfType<ISnippetDocument>()
            //    .Where(doc => doc.IsModified == true).Count() 
        }

        private void ClearSnippetDocuments()
        {
            var snippetDocs = this.dockPanel1.Contents.OfType<SnippetDocument>().ToList();

            while (snippetDocs.Count() > 0)
            {
                SnippetDocument snippetDoc = snippetDocs.First();
                snippetDocs.Remove(snippetDoc);
                snippetDoc.Close();
            }
        }

        private void mnuOpenProject_Click(object sender, EventArgs e)
        {
            if (!UnsavedSnippetDocuments())
            {
                
                ClearSnippetDocuments();
                // load the last project...

                // load the last opened snippets.
                OpenSnippetDocument();
                OpenSnippetDocument();
            }
            else
            {
                DialogResult result = PromptSaveChanges();

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveAllDocuments();
                    ClearSnippetDocuments();
                    // load the last project...

                    // load the last opened snippets.
                    OpenSnippetDocument();
                    OpenSnippetDocument();
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    ClearSnippetDocuments();
                    // load the last project...

                    // load the last opened snippets.
                    OpenSnippetDocument();
                    OpenSnippetDocument();
                }
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit(); // this causes an invalid operation exception !!!
            // The Form.Closed and Form.Closing events are not raised when the Application.Exit method is called to exit your application.
            // https://social.msdn.microsoft.com/Forums/windows/en-US/f23247b8-e0ca-46eb-afb0-dbf22d4c84ac/thisclose-vs-applicationexit?forum=winforms
        }

        private void SaveAllDocuments()
        {
            IEnumerable<SnippetDocument> documents = this.dockPanel1.Contents.OfType<SnippetDocument>().Where(doc => doc.IsModified == true);
            foreach (SnippetDocument document in documents)
                document.SaveChanges();
        }

        private DialogResult PromptSaveChanges()
        {
            SaveSnippetDialog dialog = new SaveSnippetDialog(UnsavedDocuments());
            DialogResult result = dialog.ShowDialog(this);

            return result;
        }

        private void mnuAddSnippet_Click(object sender, EventArgs e)
        {
            CreateSnippetDocument();
        }

        private void mnuSaveSnippet_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveDocument != null && dockPanel1.ActiveDocument is ISnippetDocument)
                (dockPanel1.ActiveDocument as ISnippetDocument).SaveChanges();
        }

        private void mnuCloseSnippet_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveDocument != null && dockPanel1.ActiveDocument is ISnippetDocument)
                (dockPanel1.ActiveDocument as SnippetDocument).Close();
        }

        private void mnuSearchPane_Click(object sender, EventArgs e)
        {
            searchForm.Activate();
        }
    }
}
