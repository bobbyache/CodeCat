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
        public event EventHandler FontSizeChanged;
        public event EventHandler SyntaxChanged;
        public event EventHandler AfterSyntaxChanged;

        protected ITopicSection topicSection;
        protected AppFacade application;
        protected ITopicDocument topicDocument;

        public string Id { get; private set; }

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
        }

        protected void UnregisterEvents()
        {
            cboFontSize.SelectedIndexChanged -= cboFontSize_SelectedIndexChanged;
            cboSyntax.SelectedIndexChanged -= cboSyntax_SelectedIndexChanged;
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            SyntaxChanged?.Invoke(this, new EventArgs());

            this.lblEditStatus.Image = IconRepository.Get(Syntax).Image;

            AfterSyntaxChanged?.Invoke(this, new EventArgs());
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
            FontSizeChanged?.Invoke(this, new EventArgs());
        }
    }
}
