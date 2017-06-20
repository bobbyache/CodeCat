using System;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class CodeVersionListControl : UserControl
    {
        public event EventHandler SnapshotSelectionChanged;

        public string SyntaxFile { set { this.syntaxBox.Document.SyntaxFile = value; } }
        public bool Attached { get { return this.versionedCodeTopicSection != null; } }
        public float EditorFontSize { set { this.syntaxBox.FontSize = value; } }

        public IFileVersion SelectedSnapshot
        {
            get
            {
                if (versionListview.SelectedItems.Count == 1)
                    return (IFileVersion)versionListview.SelectedItems[0].Tag;
                else
                    return null;
            }
        }

        public CodeVersionListControl()
        {
            InitializeComponent();
            syntaxBox.ReadOnly = true;
            versionListview.SelectedIndexChanged += listviewSnapshots_SelectedIndexChanged;
        }

        public void DeleteSnapshot()
        {
            if (versionListview.SelectedItems.Count == 1)
            {
                DialogResult result = Gui.Dialogs.DeleteItemDialog(this, "snapshot");

                if (result == DialogResult.Yes)
                {
                    IFileVersion snapshot = (IFileVersion)versionListview.SelectedItems[0].Tag;
                    versionedCodeTopicSection.DeleteVersion(snapshot.Id);
                    syntaxBox.Document.Text = string.Empty;
                }
            }
        }

        private void listviewSnapshots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (versionListview.SelectedItems.Count == 1)
                syntaxBox.Document.Text = ((IFileVersion)versionListview.SelectedItems[0].Tag).Text();
            else
                syntaxBox.Document.Text = string.Empty;

            SnapshotSelectionChanged?.Invoke(this, new EventArgs());
        }

        private IVersionedCodeTopicSection versionedCodeTopicSection = null;

        public void Attach(IVersionedCodeTopicSection topicSection)
        {
            if (this.versionedCodeTopicSection != null)
            {
                this.versionedCodeTopicSection.SnapshotTaken -= (s, e) => { ListSnapshots(); };
                this.versionedCodeTopicSection.SnapshotDeleted -= (s, e) => { ListSnapshots(); };
            }
            this.versionedCodeTopicSection = topicSection;
            this.versionedCodeTopicSection.SnapshotTaken += (s, e) => { ListSnapshots(); };
            this.versionedCodeTopicSection.SnapshotDeleted += (s, e) => { ListSnapshots(); };
            ListSnapshots();
        }

        private void ListSnapshots()
        {
            versionListview.Items.Clear();
            foreach (IFileVersion snapshot in versionedCodeTopicSection.Versions)
            {
                ListViewItem fileVersion = new ListViewItem();
                fileVersion.Name = snapshot.Id;
                fileVersion.Tag = snapshot;
                fileVersion.Text = snapshot.TimeTaken.ToString();
                fileVersion.SubItems.Add(new ListViewItem.ListViewSubItem(fileVersion, snapshot.Description));
                versionListview.Items.Add(fileVersion);
            }
        }
    }
}
