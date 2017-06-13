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
        public event EventHandler FontChanged;
        public event EventHandler Modified;
        public event EventHandler ContentSaved;
        public event EventHandler Reverted;
        public event EventHandler UnregisterFieldEvents;
        public event EventHandler RegisterFieldEvents;

        protected ITopicSection topicSection;
        protected AppFacade application;
        protected ITopicDocument topicDocument;

        public string Id { get; private set; }

        public string Title { get { return this.txtTitle.Text; } }

        public Single FontSize { get { return Convert.ToSingle(cboFontSize.SelectedItem); } }

        public virtual int ImageKey {  get { return IconRepository.Get("TEXT").Index; } }
        public virtual Icon ImageIcon {  get { return IconRepository.Get("TEXT").Icon; } }
        public virtual Image IconImage { get { return IconRepository.Get("TEXT").Image; } }

        public bool IsModified { get; protected set; }

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

            txtTitle.Text = topicSection.Title;
            this.IsModified = false;

            topicDocument.BeforeSave += topicDocument_BeforeContentSaved;
            topicDocument.AfterSave += topicDocument_AfterSave;

            RegisterEvents();

            SetChangeStatus();
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

        public void Modify(Image statusImage)
        {
            Modify();
            this.lblEditStatus.Image = statusImage;
        }

        public void Revert()
        {
            UnregisterFieldEvents?.Invoke(this, new EventArgs());
            Reverted?.Invoke(this, new EventArgs());
            RegisterFieldEvents?.Invoke(this, new EventArgs());
        }

        protected void RegisterEvents()
        {
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
        }

        protected void SetModified(object sender, EventArgs e)
        {
            Modify();
        }

        private ICodeTopicSection CodeTopicSection()
        {
            return topicSection as ICodeTopicSection;
        }

        private void SetDefaultFont()
        {
            cboFontSize.SelectedIndex = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontChanged?.Invoke(this, new EventArgs());
        }

        private void topicDocument_AfterSave(object sender, TopicEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void topicDocument_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            this.topicSection.Title = Title;
            ContentSaved?.Invoke(this, new EventArgs());
        }
    }
}
