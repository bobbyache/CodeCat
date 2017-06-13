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
        protected ITopicSection topicSection;
        protected AppFacade application;
        protected ITopicDocument topicDocument;

        public string Id { get; private set; }

        public TopicSectionBaseControl(AppFacade application, ITopicDocument topicDocument, ITopicSection topicSection)
        {
            InitializeComponent();

            this.Id = topicSection.Id;

            this.application = application;
            this.topicSection = topicSection;
            this.topicDocument = topicDocument;
        }
    }
}
