using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SnapshotListCtrl : UserControl
    {
        public event EventHandler SnapshotSelectionChanged;

        public string SyntaxFile
        {
            set { this.syntaxBox.Document.SyntaxFile = value; }
        }

        public float EditorFontSize { set { this.syntaxBox.FontSize = value; } }

        public CodeSnapshot SelectedSnapshot
        {
            get
            {
                if (listviewSnapshots.SelectedItems.Count == 1)
                {
                    return (CodeSnapshot)listviewSnapshots.SelectedItems[0].Tag;
                }
                else
                    return null;
            }
        }

        public SnapshotListCtrl()
        {
            InitializeComponent();
            syntaxBox.ReadOnly = true;
            listviewSnapshots.SelectedIndexChanged += listviewSnapshots_SelectedIndexChanged;
        }

        public void DeleteSnapshot()
        {
            if (listviewSnapshots.SelectedItems.Count == 1)
            {
                DialogResult result = Dialogs.DeleteSnapshotDialogPrompt(this);

                if (result == DialogResult.Yes)
                {
                    CodeSnapshot snapshot = (CodeSnapshot)listviewSnapshots.SelectedItems[0].Tag;
                    codeFile.DeleteSnapshot(snapshot.Id);
                    syntaxBox.Document.Text = string.Empty;
                }
            }
        }

        private void listviewSnapshots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listviewSnapshots.SelectedItems.Count == 1)
            {
                syntaxBox.Document.Text = ((CodeSnapshot)listviewSnapshots.SelectedItems[0].Tag).Text;
            }
            else
                syntaxBox.Document.Text = string.Empty;

            if (SnapshotSelectionChanged != null)
                SnapshotSelectionChanged(this, new EventArgs());
        }

        private CodeFile codeFile = null;

        public void Attach(CodeFile codeFile)
        {
            if (this.codeFile != null)
            {
                this.codeFile.SnapshotTaken -= (s, e) => { ListSnapshots(); };
                this.codeFile.SnapshotDeleted -= (s, e) => { ListSnapshots(); };
            }
            this.codeFile = codeFile;
            this.codeFile.SnapshotTaken += (s, e) => { ListSnapshots(); };
            this.codeFile.SnapshotDeleted += (s, e) => { ListSnapshots(); };
            ListSnapshots();
        }

        private void ListSnapshots()
        {
            listviewSnapshots.Items.Clear();
            foreach (CodeSnapshot snapshot in codeFile.Snapshots)
            {
                ListViewItem snapShotItem = new ListViewItem();
                snapShotItem.Name = snapshot.Id;
                snapShotItem.Tag = snapshot;
                snapShotItem.Text = snapshot.DateCreated.ToString();
                snapShotItem.SubItems.Add(new ListViewItem.ListViewSubItem(snapShotItem, snapshot.Description));
                listviewSnapshots.Items.Add(snapShotItem);
            }
        }
    }
}
