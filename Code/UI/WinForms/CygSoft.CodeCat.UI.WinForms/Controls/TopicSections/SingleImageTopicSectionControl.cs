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
using CygSoft.CodeCat.UI.WinForms.UiHelpers;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class SingleImageTopicSectionControl : BaseImageTopicSectionControl
    {
        public SingleImageTopicSectionControl()
            : this(null, null, null)
        {

        }

        public SingleImageTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ISingleImageTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();
        }
    }
}
