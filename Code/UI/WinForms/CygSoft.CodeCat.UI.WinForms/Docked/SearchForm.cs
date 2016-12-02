using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.UI.WinForms.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SearchForm : DockContent
    {
        private AppFacade application;

        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsAdded;
        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsRemoved;

        public event EventHandler<SearchDelimitedKeywordEventArgs> SearchExecuted;
        public event EventHandler<OpenSnippetEventArgs> OpenSnippet;
        public event EventHandler<SelectSnippetEventArgs> SelectSnippet;

        private bool searchEnabled;
        public bool SearchEnabled
        {
            get { return this.searchEnabled; }
            set
            {
                this.searchEnabled = value;
                this.btnFind.Enabled = value;
            }
        }

        public string KeywordSearchText
        {
            get { return this.keywordsTextBox.Text; }
            set { this.keywordsTextBox.Text = value; }
        }

        public bool SingleSnippetSelected { get { return this.codeSearchResultsControl1.SingleSnippetSelected; } }
        public bool MultipleSnippetsSelected { get { return this.codeSearchResultsControl1.MultipleSnippetsSelected; } }

        public IKeywordIndexItem SelectedSnippet
        {
            get { return this.codeSearchResultsControl1.SelectedSnippet; }
        }

        public IKeywordIndexItem[] SelectedSnippets
        {
            get { return this.codeSearchResultsControl1.SelectedSnippets; }
        }

        public SearchForm(AppFacade application)
        {
            InitializeComponent();

            this.codeSearchResultsControl1.Application = application;
            btnFind.Image = Resources.GetImage(Constants.ImageKeys.FindSnippets);

            this.application = application;
            this.HideOnClose = true;
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight;
            btnFind.Click += (s, e) => ExecuteSearch();
            keywordsTextBox.TextChanged += (s, e) => ExecuteSearch();

            this.codeSearchResultsControl1.OpenSnippet += codeSearchResultsControl1_OpenSnippet;
            this.codeSearchResultsControl1.SelectSnippet += codeSearchResultsControl1_SelectSnippet;
            this.codeSearchResultsControl1.KeywordsAdded += codeSearchResultsControl1_KeywordsAdded;
            this.codeSearchResultsControl1.KeywordsRemoved += codeSearchResultsControl1_KeywordsRemoved;
            this.codeSearchResultsControl1.SearchExecuted += codeSearchResultsControl1_SearchExecuted;
        }

        private void codeSearchResultsControl1_SearchExecuted(object sender, SearchDelimitedKeywordEventArgs e)
        {
            if (this.SearchExecuted != null)
                SearchExecuted(sender, e);
        }

        private void codeSearchResultsControl1_KeywordsRemoved(object sender, SearchKeywordsModifiedEventArgs e)
        {
            if (this.KeywordsRemoved != null)
                KeywordsRemoved(sender, e);
        }

        private void codeSearchResultsControl1_KeywordsAdded(object sender, SearchKeywordsModifiedEventArgs e)
        {
            if (this.KeywordsAdded != null)
                KeywordsAdded(sender, e);
        }

        private void codeSearchResultsControl1_SelectSnippet(object sender, SelectSnippetEventArgs e)
        {
            if (this.SelectSnippet != null)
                SelectSnippet(sender, e);
        }

        private void codeSearchResultsControl1_OpenSnippet(object sender, OpenSnippetEventArgs e)
        {
            if (this.OpenSnippet != null)
                OpenSnippet(sender, e);
        }

        public void ExecuteSearch()
        {
            if (this.searchEnabled)
            {
                this.codeSearchResultsControl1.ExecuteSearch(keywordsTextBox.Text);
            }
        }

        public void ExecuteSearch(string selectedId)
        {
            this.codeSearchResultsControl1.ExecuteSearch(keywordsTextBox.Text);
        }
    }
}
