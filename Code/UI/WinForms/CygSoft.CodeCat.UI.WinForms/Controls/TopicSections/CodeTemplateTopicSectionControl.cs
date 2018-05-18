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
using CygSoft.CodeCat.UI.Resources.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class CodeTemplateTopicSectionControl : BaseTopicSectionControl
    {
        public CodeTemplateTopicSectionControl()
        {
            InitializeComponent();
        }

        public CodeTemplateTopicSectionControl(AppFacade application, IImageResources imageResources, ITopicDocument topicDocument, ICodeTemplateTopicSection topicSection) 
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();
        }
    }
}
