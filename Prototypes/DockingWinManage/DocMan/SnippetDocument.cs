using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Weif_1_Test
{
    public partial class SnippetDocument : DockContent, ISnippetDocument
    {
        private string guidString = Guid.NewGuid().ToString();

        public string Id { get; private set; }
        public bool IsModified { get; private set; }
        public bool IsNew { get; private set; }

        public SnippetDocument()
        {
            InitializeComponent();
            // only allow this form to be a "Document", don't allow it to float or dock to sides.
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Id = Guid.NewGuid().ToString();
            this.Text = this.Id;
            this.IsNew = true;
            this.IsModified = false;
            SetStatus();
        }

        public SnippetDocument(string id)
        {
            InitializeComponent();
            // only allow this form to be a "Document", don't allow it to float or dock to sides.
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Id = id;
            this.Text = id;
            this.IsNew = false;
            this.IsModified = false;
            SetStatus();
        }

        public void SaveChanges()
        {
            this.IsNew = false;
            this.IsModified = false;
            SetStatus();
        }

        public void DiscardChanges()
        {
            this.IsModified = false;
            SetStatus();
        }

        private void SetStatus()
        {
            lblStatus.Text = string.Format("New: {0}, Has Changes: {1}", this.IsNew, this.IsModified);
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            //Debug.WriteLine("Activated: {0}, IsActive: {1}", guidString, this.IsActivated);
        }

        private void Form2_Click(object sender, EventArgs e)
        {
            //Debug.WriteLine("Clicked: ", guidString);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
            SetStatus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DiscardChanges();
            SetStatus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            this.IsModified = true;
            SetStatus();
        }
    }
}
