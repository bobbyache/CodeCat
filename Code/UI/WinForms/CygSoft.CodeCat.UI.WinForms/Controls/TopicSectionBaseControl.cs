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
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class TopicSectionBaseControl : UserControl
    {
        public event EventHandler Modified;

        protected ITopicSection topicSection;
        protected AppFacade application;
        protected ITopicDocument topicDocument;

        public string Id { get; private set; }

        public string Title { get { return this.txtTitle.Text; } }

        public Single FontSize { get { return Convert.ToSingle(cboFontSize.SelectedItem); } }

        public string Syntax
        {
            get
            {
                string currentSyntax = cboSyntax.SelectedItem.ToString();
                string syntax = string.IsNullOrEmpty(currentSyntax) ? ConfigSettings.DefaultSyntax.ToUpper() : currentSyntax.ToUpper();
                return syntax;
            }
            set
            {
                string syntax = string.IsNullOrEmpty(value) ? ConfigSettings.DefaultSyntax.ToUpper() : value.ToUpper();
                int index = cboSyntax.FindStringExact(syntax);
                if (index >= 0)
                    cboSyntax.SelectedIndex = index;
            }
        }

        

        public int ImageKey { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public Image IconImage { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; } }

        public TopicSectionBaseControl()
            : this(null, null, null)
        {
            
        }

        public TopicSectionBaseControl(AppFacade application, ITopicDocument topicDocument, ITopicSection topicSection)
        {
            InitializeComponent();

            if (topicSection == null)
                return;

            this.Id = topicSection.Id;

            this.application = application;
            this.topicSection = topicSection;
            this.topicDocument = topicDocument;

            SetDefaultFont();
            InitializeSyntaxList();

            Syntax = CodeTopicSection().Syntax;

            txtTitle.Text = topicSection.Title;
            this.IsModified = false;

            topicDocument.BeforeSave += topicDocument_BeforeContentSaved;
            topicDocument.AfterSave += topicDocument_AfterSave;

            RegisterEvents();

            SetChangeStatus();
        }

        private ICodeTopicSection CodeTopicSection()
        {
            return topicSection as ICodeTopicSection;
        }

        private void topicDocument_AfterSave(object sender, TopicEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        public event EventHandler ContentSaved;

        private void topicDocument_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            this.topicSection.Title = Title;
            ContentSaved?.Invoke(this, new EventArgs());
        }

        private void InitializeSyntaxList()
        {
            cboSyntax.Items.Clear();
            cboSyntax.Items.AddRange(application.GetSyntaxes());
        }

        private void SetDefaultFont()
        {
            cboFontSize.SelectedIndex = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
        }

        protected void RegisterEvents()
        {
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
        }

        protected void UnregisterEvents()
        {
            cboFontSize.SelectedIndexChanged -= cboFontSize_SelectedIndexChanged;
            cboSyntax.SelectedIndexChanged -= cboSyntax_SelectedIndexChanged;
            txtTitle.TextChanged -= SetModified;
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblEditStatus.Image = IconRepository.Get(Syntax).Image;
            Modify();
        }

        public bool IsModified { get; protected set; }

        protected void SetModified(object sender, EventArgs e)
        {
            Modify();
        }

        public void Modify()
        {
            if (!IsModified)
            {
                IsModified = true;
                SetChangeStatus();
                Modified?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler Reverted;
        public event EventHandler UnregisterFieldEvents;
        public event EventHandler RegisterFieldEvents;

        public void Revert()
        {
            UnregisterFieldEvents?.Invoke(this, new EventArgs());
            Reverted?.Invoke(this, new EventArgs());
            RegisterFieldEvents?.Invoke(this, new EventArgs());
        }

        public string SyntaxFile
        {
            get
            {
                string currentSyntax = cboSyntax.SelectedItem.ToString();
                string syn = string.IsNullOrEmpty(currentSyntax) ? ConfigSettings.DefaultSyntax.ToUpper() : currentSyntax.ToUpper();
                string syntaxFile = application.GetSyntaxFile(syn);
                return syntaxFile;
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modify();
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }
    }
}
