using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public partial class BaseTopicSectionControl : UserControl, ITopicSectionBaseControl
    {
        public event EventHandler Modified;
        public event EventHandler ContentSaved;
        public event EventHandler Reverted;
        public event EventHandler UnregisterFieldEvents;
        public event EventHandler RegisterFieldEvents;

        protected ITopicSection topicSection;
        protected IAppFacade application;
        protected ITopicDocument topicDocument;
        protected IImageResources imageResources;

        public string Id { get; private set; }
        public string Title { get { return this.txtTitle.Text; } }

        public virtual int ImageKey { get { return -1; } }
        public virtual Icon ImageIcon { get { return null; } }
        public virtual Image IconImage { get { return null; } }

        public bool IsModified { get; protected set; }
        public bool FileExists { get { return topicSection.Exists; } }

        public BaseTopicSectionControl()
            : this(null, null, null, null)
        {

        }

        public BaseTopicSectionControl(IAppFacade application, IImageResources imageResources, ITopicDocument topicDocument, ITopicSection topicSection)
        {
            InitializeComponent();

            if (imageResources == null)
                throw new ArgumentNullException("Image Repository is a required constructor parameter and cannot be null");

            this.imageResources = imageResources;

            if (topicSection == null)
                return;

            this.Id = topicSection.Id;

            this.application = application;
            this.topicSection = topicSection;
            this.topicDocument = topicDocument;
            
            txtTitle.Text = topicSection.Title;
            this.IsModified = false;

            topicDocument.BeforeSave += topicDocument_BeforeContentSaved;
            topicDocument.AfterSave += topicDocument_AfterSave;
            txtTitle.TextChanged += (s, e) => Modify(forceInvoke: true);

            SetChangeStatus();
        }

        public void SetStateImage(Image image)
        {
            this.lblEditStatus.Image = image;
        }

        public void Modify(bool forceInvoke = false)
        {
            if (!IsModified || forceInvoke)
            {
                IsModified = true;
                SetChangeStatus();
                Modified?.Invoke(this, new EventArgs());
            }
        }

        public void Revert()
        {
            UnregisterFieldEvents?.Invoke(this, new EventArgs());
            Reverted?.Invoke(this, new EventArgs());
            RegisterFieldEvents?.Invoke(this, new EventArgs());
        }

        protected void SetModified(object sender, EventArgs e)
        {
            Modify();
        }

        private ICodeTopicSection CodeTopicSection()
        {
            return topicSection as ICodeTopicSection;
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }

        private void topicDocument_AfterSave(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void topicDocument_BeforeContentSaved(object sender, FileEventArgs e)
        {
            this.topicSection.Title = Title;
            ContentSaved?.Invoke(this, new EventArgs());
        }
    }
}
