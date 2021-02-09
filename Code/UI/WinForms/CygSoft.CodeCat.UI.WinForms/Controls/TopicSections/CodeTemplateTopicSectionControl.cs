using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class CodeTemplateTopicSectionControl : BaseTopicSectionControl
    {
        public CodeTemplateTopicSectionControl() => InitializeComponent();
        public CodeTemplateTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ICodeTemplateTopicSection topicSection)
            : base(application, topicDocument, topicSection) => InitializeComponent();
    }
}
