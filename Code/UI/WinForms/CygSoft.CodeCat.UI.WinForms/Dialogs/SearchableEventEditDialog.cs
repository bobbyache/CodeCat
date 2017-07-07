using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.TopicSections.SearchableEventDiary;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Dialogs
{
    public partial class SearchableEventEditDialog : Form
    {
        private AppFacade application;
        public ISearchableEventKeywordIndexItem DiaryEvent { get; private set; }

        public SearchableEventEditDialog(AppFacade application, ISearchableEventKeywordIndexItem diaryEvent)
        {
            InitializeComponent();

            if (application == null)
                return;

            this.Icon = null; // IconRepository.Get(codeSnippet.Syntax).Icon;
            this.application = application;
            DiaryEvent = diaryEvent;
            txtTitle.Text = diaryEvent?.Title;
            txtKeywords.Text = diaryEvent?.CommaDelimitedKeywords;
            rtfEditorControl.TextRtf = diaryEvent?.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                DiaryEvent.SetKeywords(txtKeywords.Text);
                DiaryEvent.Title = txtTitle.Text.Trim();
                DiaryEvent.Text = rtfEditorControl.TextRtf;

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateFields()
        {
            if (txtTitle.Text.Trim() == "")
            {
                Gui.Dialogs.NoInputValueForMandatoryField(this, "Title");
                return false;
            }

            if (txtKeywords.Text.Trim() == "")
            {
                Gui.Dialogs.NoInputValueForMandatoryField(this, "Keywords");
                return false;
            }

            if (rtfEditorControl.Text.Trim() == "")
            {
                Gui.Dialogs.NoInputValueForMandatoryField(this, "Text");
                return false;
            }

            return true;
        }
    }
}
