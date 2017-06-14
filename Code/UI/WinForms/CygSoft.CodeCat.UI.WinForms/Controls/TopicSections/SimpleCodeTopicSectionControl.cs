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

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class SimpleCodeTopicSectionControl : BaseCodeTopicSectionControl
    {
        public SimpleCodeTopicSectionControl()
            : this(null, null, null)
        {

        }

        public SimpleCodeTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();
        }
    }
}
