using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.WinForms.TopicSectionBase;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class CodeTemplateTopicSectionControl : BaseTopicSectionControl
    {
        public CodeTemplateTopicSectionControl(IAppFacade application, IImageResources imageResources, ITopicDocument topicDocument, ICodeTemplateTopicSection topicSection) 
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();
        }
    }
}
