using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class SearchableSnippetTopicSectionControl : BaseTopicSectionControl
    {
        public SearchableSnippetTopicSectionControl()
            : this(null, null, null)
        {

        }

        public SearchableSnippetTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ISearchableSnippetTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();
        }
    }
}
