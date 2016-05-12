﻿using System;
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
        public string EditorSyntaxFile
        {
            set { this.syntaxBox.Document.SyntaxFile = value; }
        }

        public float EditorFontSize { set { this.syntaxBox.FontSize = value; } }

        public SnapshotListCtrl()
        {
            InitializeComponent();
            syntaxBox.ReadOnly = true;
            btnDelete.Click += btnDelete_Click;
            listviewSnapshots.SelectedIndexChanged += listviewSnapshots_SelectedIndexChanged;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listviewSnapshots.SelectedItems.Count == 1)
            {
                DialogResult dialogResult = MessageBox.Show(this, "Sure you want to delete this snapshot?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dialogResult == DialogResult.Yes)
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
                syntaxBox.Document.Text = ((CodeSnapshot)listviewSnapshots.SelectedItems[0].Tag).Text;
            else
                syntaxBox.Document.Text = string.Empty;
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
