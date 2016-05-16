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

namespace Weif_1_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //var unsavedDocs = this.dockPanel1.Contents.OfType<ISnippetDocument>()
            //    .Where(doc => doc.IsModified == true);

            //var savedDocs = this.dockPanel1.Contents.OfType<ISnippetDocument>()
            //    .Where(doc => doc.IsModified == false);
        }

        private void dockPanel1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            var unsavedDocs = this.dockPanel1.Contents.OfType<ISnippetDocument>()
                .Where(doc => doc.IsModified == true);
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
                MessageBox.Show("You have unsaved snippets. Please save these before continuing");
            }
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
                MessageBox.Show("You have unsaved snippets. Please save these before continuing");
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (!UnsavedSnippetDocuments())
            {

                ClearSnippetDocuments();
                Application.Exit();
            }
            else
            {
                MessageBox.Show("You have unsaved snippets. Please save these before continuing");
            }
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
    }
}
