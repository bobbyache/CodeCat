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

            //ToolStripLabel searchLabel = new ToolStripLabel();
            //searchLabel.Text = "Search";
            //searchLabel.Alignment = ToolStripItemAlignment.Right;
            //ToolStripTextBox searchTextBox = new ToolStripTextBox();
            //searchTextBox.Width = 300;
            //searchTextBox.Alignment = ToolStripItemAlignment.Right;

            HeaderToolstrip.Items.Add(CreateButton());
            HeaderToolstrip.Items.Add(CreateButton());
            HeaderToolstrip.Items.Add(CreateButton());
            //HeaderToolstrip.Items.Add(searchTextBox);
            //HeaderToolstrip.Items.Add(searchLabel);
            
        }

        private ToolStripButton CreateButton()
        {
            ToolStripButton btn = new ToolStripButton();
            btn.Alignment = ToolStripItemAlignment.Right;
            btn.Text = "Test";
            return btn;
        }
    }
}
