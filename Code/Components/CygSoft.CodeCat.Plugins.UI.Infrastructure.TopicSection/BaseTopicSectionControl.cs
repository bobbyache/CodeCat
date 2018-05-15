using System;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.Plugins.UI.Infrastructure.TopicSection
{
    public partial class BaseTopicSectionControl : UserControl, ITopicSectionBaseControl
    {
        public event EventHandler Modified;
        public event EventHandler ContentSaved;
        public event EventHandler Reverted;
        public event EventHandler UnregisterFieldEvents;
        public event EventHandler RegisterFieldEvents;

        protected ITopicSection topicSection;
        protected /* AppFacade */ object application;
        protected ITopicDocument topicDocument;

        public string Id { get; private set; }
        public string Title { get { return this.txtTitle.Text; } }

        public virtual int ImageKey {  get { return -1; /* IconRepository.Get("TEXT").Index; */ } }
        public virtual Icon ImageIcon {  get { return null; /* IconRepository.Get("TEXT").Icon; */ } }
        public virtual Image IconImage { get { return null; /* IconRepository.Get("TEXT").Image; */ } }

        public bool IsModified { get; protected set; }
        public bool FileExists { get { return topicSection.Exists; } }

        public BaseTopicSectionControl()
            : this(null, null, null)
        {

        }

        public BaseTopicSectionControl(object application, ITopicDocument topicDocument, ITopicSection topicSection)
        {
            InitializeComponent();

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
